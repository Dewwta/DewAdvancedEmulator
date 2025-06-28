using DewAdvancedEmulator.Core;

public static class Program
{
    public static void Main(string[] args)
    {
        Console.Title = "DewAdvancedEmulator";

        if (args.Length == 0)
        {
            Console.WriteLine("Usage: gbaemu <path-to-rom.gba>");
            return;
        }

        string romPath = args[0];
        string biosPath = args[1];

        byte[] romData = File.ReadAllBytes(romPath);
        byte[] biosData = File.ReadAllBytes(biosPath);

        var memory = new Memory();
        memory.LoadBios(biosData);
        memory.LoadRom(romData);

        var cpu = new CPU();
        cpu.ConnectMemory(memory);
        cpu.Reset();
        while (true)
        {
            cpu.Step();
            Thread.Sleep(16);
        }
    }
    
}