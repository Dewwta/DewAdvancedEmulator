namespace DewAdvancedEmulator.Structs
{
    public struct DecodedInstruction
    {
        public string Mnemonic;
        public uint Rd, Rn, Operand2;
        public bool IsImmediate;
        public bool SetFlags;
    }
}
