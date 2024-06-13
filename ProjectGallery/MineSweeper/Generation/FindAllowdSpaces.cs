using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper.Generation
{
    internal static class FindAllowdSpaces
    {
        public static List<(int, int)> Find(int row, int col, int rows, int cols)
        {
            bool isLeft = col == 0;
            bool isRight = col == cols - 1;
            bool isTop = row == 0;
            bool isBottom = row == rows - 1;

            List<(int, int)> checkSpots = new List<(int, int)>();

            if (isLeft && isTop)
            {
                // Top-left corner
                checkSpots.Add((row + 1, col));
                checkSpots.Add((row + 1, col + 1));
                checkSpots.Add((row, col + 1));
            }
            else if (isLeft && isBottom)
            {
                // Bottom-left corner
                checkSpots.Add((row - 1, col));
                checkSpots.Add((row - 1, col + 1));
                checkSpots.Add((row, col + 1));
            }
            else if (isRight && isTop)
            {
                // Top-right corner
                checkSpots.Add((row + 1, col));
                checkSpots.Add((row + 1, col - 1));
                checkSpots.Add((row, col - 1));
            }
            else if (isRight && isBottom)
            {
                // Bottom-right corner
                checkSpots.Add((row - 1, col));
                checkSpots.Add((row - 1, col - 1));
                checkSpots.Add((row, col - 1));
            }
            else if (isTop)
            {
                // Top edge
                checkSpots.Add((row + 1, col));
                checkSpots.Add((row + 1, col - 1));
                checkSpots.Add((row + 1, col + 1));
                checkSpots.Add((row, col - 1));
                checkSpots.Add((row, col + 1));
            }
            else if (isBottom)
            {
                // Bottom edge
                checkSpots.Add((row - 1, col));
                checkSpots.Add((row - 1, col - 1));
                checkSpots.Add((row - 1, col + 1));
                checkSpots.Add((row, col - 1));
                checkSpots.Add((row, col + 1));
            }
            else if (isLeft)
            {
                // Left edge
                checkSpots.Add((row - 1, col));
                checkSpots.Add((row - 1, col + 1));
                checkSpots.Add((row, col + 1));
                checkSpots.Add((row + 1, col));
                checkSpots.Add((row + 1, col + 1));
            }
            else if (isRight)
            {
                // Right edge
                checkSpots.Add((row - 1, col));
                checkSpots.Add((row - 1, col - 1));
                checkSpots.Add((row, col - 1));
                checkSpots.Add((row + 1, col));
                checkSpots.Add((row + 1, col - 1));
            }
            else
            {
                // Middle
                checkSpots.Add((row - 1, col - 1));
                checkSpots.Add((row - 1, col));
                checkSpots.Add((row - 1, col + 1));
                checkSpots.Add((row, col - 1));
                checkSpots.Add((row, col + 1));
                checkSpots.Add((row + 1, col - 1));
                checkSpots.Add((row + 1, col));
                checkSpots.Add((row + 1, col + 1));
            }

            // Add the current spot as well
            checkSpots.Add((row, col));

            return checkSpots;
        }
    }
}
