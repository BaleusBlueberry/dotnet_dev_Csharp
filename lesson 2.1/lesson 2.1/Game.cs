using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lesson_2._1
{
    internal class Game
    {
        private Player player1;
        private Player player2;
        private GameBoard board;

        public Game()
        {
            board = new GameBoard();
            InitializeGame
        }

        private void InitializeGame()
        {
            Console.Clear();
            board.Reset();
        }
    }
}
