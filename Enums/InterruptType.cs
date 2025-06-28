using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DewAdvancedEmulator.Enums
{
    public enum InterruptType : ushort
    {
        VBlank = 1 << 0,
        HBlank = 1 << 1,
        VCounterMatch = 1 << 2,
        Timer0 = 1 << 3,
        Timer1 = 1 << 4,
        Timer2 = 1 << 5,
        Timer3 = 1 << 6,
        Serial = 1 << 7,
        DMA0 = 1 << 8,
        DMA1 = 1 << 9,
        DMA2 = 1 << 10,
        DMA3 = 1 << 11,
        Keypad = 1 << 12,
        GamePak = 1 << 13
    };
}
