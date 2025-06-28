using DewAdvancedEmulator.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DewAdvancedEmulator.Core
{
    public class InterruptController
    {
        #region - Var -

        private ushort IE;
        private ushort IF;
        private byte IME;
        private CPU cpu;

        #endregion

        #region - Setup/Reset -

        public void ConnectCPU(CPU c)
        {
            cpu = c;
        }
        public void Reset()
        {
            IE = 0;
            IF = 0;
            IME = 0;
        }

        #endregion

        #region - Write/Read -

        public void WriteIE(ushort value) => IE = value;
        public ushort ReadIE() => IE;

        public void WriteIF(ushort value)
        {
            IF &= (ushort)~value;
        }
        public ushort ReadIF() => IF;

        public void WriteIME(byte value) => IME = (byte)(value & 1);
        public byte ReadIME() => IME;

        #endregion

        public void RequestInterrupt(InterruptType type)
        {
            IF |= (ushort)type;
        }

        public void CheckAndHandleInterrupts()
        {
            if (IME == 0)
                return;

            ushort active = (ushort)(IE & IF);
            if (active != 0)
            {
                HandleInterrupt(active);
            }
        }

        private void HandleInterrupt(ushort active)
        {
            for (int i = 0; i < 14; i++)
            {
                if ((active & (1 << i)) != 0)
                {
                    Console.WriteLine($"[INT] Servicing interrupt {(InterruptType)(1 << i)}");

                    // Clear interrupt flag
                    IF &= (ushort)~(1 << i);

                    // Push PC and CPSR to stack (simplified)
                    cpu.PushState();

                    // Set PC to interrupt vector (0x00000018 in GBA)
                    cpu.SetPC(0x00000018);

                    // Disable further interrupts
                    IME = 0;

                    break;
                }
            }
        }
    }
}
