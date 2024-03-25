using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe;

namespace TicTakToe.elemets
{
    internal class GameLoop
    {
        public int NumbOfPlayers { get; set; }

        

        internal GameLoop()
        {
            InitGame();

        }

        private void InitGame()
        {
            NumbOfPlayers = InputHelpers.GetValidInteger("Enter the number of players (0, 1, or 2):", 0, 2);
            Console.WriteLine($"Number of players set to: {NumbOfPlayers}");

        }


    }
}
