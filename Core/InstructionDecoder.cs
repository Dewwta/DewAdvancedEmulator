namespace DewAdvancedEmulator.Core
{
    public static class InstructionDecoder
    {
        public static string Decode(uint instruction)
        {
            // Extract fields
            uint cond = (instruction >> 28) & 0xF;
            uint opcode = (instruction >> 21) & 0xF;
            bool isImmediate = ((instruction >> 25) & 0x1) == 1;

            // Just a string representation for now
            return $"COND={cond:X} OPCODE={opcode:X} IMM={(isImmediate ? 1 : 0)}";
        }
    }
}
