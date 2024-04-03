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
using System.Windows.Shapes;

using System.Windows;

namespace WPFTicTakToe
{
    public partial class GameWindow : Window
    {
        public GameWindow(string player1Name, string player2Name)
        {
            InitializeComponent();

            // Set player names
            DataContext = new ViewModel(player1Name, player2Name);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Handle button click event (tic-tac-toe logic)
        }
    }

    public class ViewModel
    {
        public string Player1Name { get; }
        public string Player2Name { get; }

        public ViewModel(string player1Name, string player2Name)
        {
            Player1Name = player1Name;
            Player2Name = player2Name;
        }
    }
}