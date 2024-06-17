using SnakeGame.Models;
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

namespace SnakeGame.Pages
{
    /// <summary>
    /// Interaction logic for MenuePage.xaml
    /// </summary>
    public partial class MenuePage : Page
    {
        public MenuePage()
        {
            InitializeComponent();
        }
        private void Easy_Click(object sender, RoutedEventArgs e)
        {
            NavigateToGame(DifficultyLevel.Easy);
        }

        private void Medium_Click(object sender, RoutedEventArgs e)
        {
            NavigateToGame(DifficultyLevel.Medium);
        }

        private void Hard_Click(object sender, RoutedEventArgs e)
        {
            NavigateToGame(DifficultyLevel.Hard);
        }

        private void NavigateToGame(DifficultyLevel difficulty)
        {
            GamePage gamePage = new GamePage(difficulty);
            NavigationService.Navigate(gamePage);
        }
    }
}
