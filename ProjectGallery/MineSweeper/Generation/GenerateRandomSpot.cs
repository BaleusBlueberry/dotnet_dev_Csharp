using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper.Generation
{
    internal class GenerateRandomSpot
    {
        private Random _random = new Random();
        private HashSet<(int, int)> _takenSpots = new HashSet<(int, int)>();

        public (int, int) GenerateUniqueRandomSpot()
        {
            int row, col;
            do
            {
                row = _random.Next(0, 16);
                col = _random.Next(0, 16);
            }
            while (_takenSpots.Contains((row, col)));

            _takenSpots.Add((row, col));
            return (row, col);
        }


    }
    
}
