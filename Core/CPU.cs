namespace DewAdvancedEmulator.Core
{
    public class CPU
    {
        #region - Var -

        private Memory memory;
        private uint[] registers = new uint[16]; // R0-R15
        private uint cpsr;

        #endregion

        public void ConnectMemory(Memory mem)
        {
            memory = mem;
        }
        public void Reset()
        {
            registers[15] = 0x00000000; // R15 = PC
        }
        public void Step()
        {
            uint pc = registers[15];
            uint instruction = memory.ReadWord(pc);

            Console.WriteLine($"[CPU] PC=0x{pc:X8} Instruction=0x{instruction:X8}");
            DecodeAndExecute(instruction);
            registers[15] += 4; // ARM mode uses 4-byte instructions

        }
        private void DecodeAndExecute(uint instruction)
        {
            uint cond = (instruction >> 28) & 0xF;
            uint opcode = (instruction >> 21) & 0xF;

            Console.WriteLine($"[CPU] Condition: {cond:X}, Opcode: {opcode:X}");
        }
        public void PushState()
        {
            registers[13] -= 4; // R13 = SP
            memory.WriteWord(registers[13], registers[15]);

            registers[13] -= 4;
            memory.WriteWord(registers[13], cpsr);
        }
        public void SetPC(uint value)
        {
            registers[15] = value;
        }

    }
}
