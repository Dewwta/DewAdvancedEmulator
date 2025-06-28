using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DewAdvancedEmulator.Core
{
    public class MMIO
    {
        private InterruptController interrupts;
        public void ConnectInterrupts(InterruptController ic)
        {
            interrupts = ic;
        }
        public byte ReadByte(uint address)
        {
            switch (address)
            {
                case 0x04000200:
                    return (byte)(interrupts.ReadIE() & 0xFF);
                case 0x04000201:
                    return (byte)((interrupts.ReadIE() >> 8) & 0xFF);
                case 0x04000202:
                    return (byte)(interrupts.ReadIF() & 0xFF);
                case 0x04000203:
                    return (byte)((interrupts.ReadIF() >> 8) & 0xFF);
                case 0x04000208:
                    return interrupts.ReadIME();
                default:
                    Console.WriteLine($"[MMIO] ReadByte @ 0x{address:X8}");
                    return 0;
            }
        }
        public void WriteByte(uint address, byte value)
        {
            switch (address)
            {
                case 0x04000200:
                    interrupts.WriteIE((ushort)((interrupts.ReadIE() & 0xFF00) | value));
                    break;
                case 0x04000201:
                    interrupts.WriteIE((ushort)((interrupts.ReadIE() & 0x00FF) | (value << 8)));
                    break;
                case 0x04000202:
                    interrupts.WriteIF((ushort)((interrupts.ReadIF() & 0xFF00) | value));
                    break;
                case 0x04000203:
                    interrupts.WriteIF((ushort)((interrupts.ReadIF() & 0x00FF) | (value << 8)));
                    break;
                case 0x04000208:
                    interrupts.WriteIME(value);
                    break;
                default:
                    Console.WriteLine($"[MMIO] WriteByte 0x{value:X2} @ 0x{address:X8}");
                    break;
            }
        }
    }
}
