using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MineSweeper.Pages
{
    /// <summary>
    /// Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        private string difficulty;

        private readonly GameButton[,] _buttons = new GameButton[6, 7];

        public GamePage(string difficulty)
        {
            InitializeComponent();
            this.difficulty = difficulty;
            InitializeGame();
        }

        private void InitializeGame()
        {
            // Initialize the game based on the difficulty
            // Example:
            // if (difficulty == "Easy") { /* Set up easy game */ }
            // else if (difficulty == "Medium") { /* Set up medium game */ }
            // else if (difficulty == "Hard") { /* Set up hard game */ }
        }
    }
}
