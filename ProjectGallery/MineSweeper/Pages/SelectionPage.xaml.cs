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
    /// Interaction logic for SelectionPage.xaml
    /// </summary>
    public partial class SelectionPage : Page
    {
        public SelectionPage()
        {
            InitializeComponent();
        }
        private void Easy_Click(object sender, RoutedEventArgs e)
        {
            NavigateToGame("Easy");
        }

        private void Medium_Click(object sender, RoutedEventArgs e)
        {
            NavigateToGame("Medium");
        }

        private void Hard_Click(object sender, RoutedEventArgs e)
        {
            NavigateToGame("Hard");
        }

        private void NavigateToGame(string difficulty)
        {
            GamePage gamePage = new GamePage(difficulty);
            NavigationService.Navigate(gamePage);
        }
    }
}
