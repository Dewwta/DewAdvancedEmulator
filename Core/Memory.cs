using System;

namespace DewAdvancedEmulator.Core
{
    public class Memory
    {
        #region - Memory Byte Allocations -

        private byte[] bios = new byte[0x4000];
        private byte[] ewram = new byte[0x40000];
        private byte[] iwram = new byte[0x8000];
        private byte[] vram = new byte[0x18000];
        private byte[] ioRegisters = new byte[0x400];
        private byte[] cartridgeRom;

        #endregion

        #region - Loading -

        public void LoadBios(byte[] biosData)
        {
            if (biosData.Length != 0x4000)
                throw new ArgumentException("BIOS must be exactly 16KB.");
            
            Array.Copy(biosData, bios, bios.Length);
        }
        public void LoadRom(byte[] romData)
        {
            cartridgeRom = romData ?? throw new ArgumentNullException(nameof(romData));
        }

        #endregion

        #region - Read/Write Bytes -

        public byte ReadByte(uint address)
        {
            if (address <= 0x3FFF)
                return bios[address];
            else if (address >= 0x02000000 && address < 0x02040000)
                return ewram[address - 0x02000000];
            else if (address >= 0x03000000 && address < 0x03008000)
                return iwram[address - 0x03000000];
            else if (address >= 0x06000000 && address < 0x06018000)
                return vram[address - 0x06000000];
            else if (address >= 0x04000000 && address < 0x04000400)
                return ioRegisters[address - 0x04000000];
            else if (address >= 0x08000000 && address < 0x0E000000 && cartridgeRom != null)
                return cartridgeRom[address - 0x08000000];
            else
                return 0; // open bus
        }
        public void WriteByte(uint address, byte value)
        {
            if (address >= 0x02000000 && address < 0x02040000)
                ewram[address - 0x02000000] = value;
            else if (address >= 0x03000000 && address < 0x03008000)
                iwram[address - 0x03000000] = value;
            else if (address >= 0x06000000 && address < 0x06018000)
                vram[address - 0x06000000] = value;
            else if (address >= 0x04000000 && address < 0x04000400)
                ioRegisters[address - 0x04000000] = value;
            else
            {
                // ignore writes to BIOS or ROM for now
            }
        }
        public ushort ReadHalfWord(uint address)
        {
            byte low = ReadByte(address);
            byte high = ReadByte(address + 1);
            return (ushort)(low | (high << 8));
        }
        public uint ReadWord(uint address)
        {
            byte b0 = ReadByte(address);
            byte b1 = ReadByte(address + 1);
            byte b2 = ReadByte(address + 2);
            byte b3 = ReadByte(address + 3);
            return (uint)(b0 | (b1 << 8) | (b2 << 16) | (b3 << 24));
        }
        public void WriteHalfWord(uint address, ushort value)
        {
            WriteByte(address, (byte)(value & 0xFF));
            WriteByte(address + 1, (byte)((value >> 8) & 0xFF));
        }
        
        #endregion


    }
}
