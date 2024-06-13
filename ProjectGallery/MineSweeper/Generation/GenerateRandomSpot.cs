using MineSweeper.Enums;

namespace MineSweeper.Generation
{
    internal class GenerateRandomSpot
    {
        private static Random _random = new Random();

        public static (int, int) GenerateUniqueRandomSpot()
        {
            int row, col;
            do
            {
                row = _random.Next(0, 16);
                col = _random.Next(0, 16);
            }
            while (GlobalSettings.TakenMineSpots[row, col]);

            GlobalSettings.TakenMineSpots[row, col] = true;

            return (row, col);
        }

        public static List<(int, int)> GenerateRandomSpots(int amount)
        {
            List<(int, int)> _newBombs = new List<(int, int)>();

            for (int i = 0; i < amount; i++)
            {
                _newBombs.Add(GenerateUniqueRandomSpot());
            }

            return _newBombs;
        }


    }
}