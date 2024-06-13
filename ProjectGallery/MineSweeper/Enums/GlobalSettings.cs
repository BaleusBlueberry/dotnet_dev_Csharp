using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper.Enums;

public static class GlobalSettings
{
    public static bool[,] TakenMineSpots = new bool[16, 16];

    public static void ResetMineSpots()
    {
        Array.Clear(TakenMineSpots, 0, TakenMineSpots.Length);
    }
}

