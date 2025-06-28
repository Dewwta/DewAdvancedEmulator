using System;
using System.Reflection;
using System.Runtime.CompilerServices;

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

        byte[] romData = File.ReadAllBytes(romPath);
    }
    
}