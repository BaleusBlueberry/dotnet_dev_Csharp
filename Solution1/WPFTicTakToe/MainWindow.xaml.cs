using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFTicTakToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void GameStartButton_Loaded(object sender, RoutedEventArgs e)
        {
            // Calculate the width for the button
            double windowWidth = mainGrid.ActualWidth;
            double buttonWidth = windowWidth * 0.75; // 75% of the window's width

            // Set the width for the button
            gameStartButton.Width = buttonWidth;
        }

        private void GameStartButton_OnClick(object sender, RoutedEventArgs e)
        {
            // Access the CheckBoxes and TextBoxes and get their values
            bool player1IsRobot = checkBoxPlayer1.IsChecked ?? false;
            bool player2IsRobot = checkBoxPlayer2.IsChecked ?? false;

            string player1Name = textBoxPlayer1.Text=="" ? textBoxPlayer1.Text : "Player1";
            string player2Name = textBoxPlayer2.Text=="" ? textBoxPlayer1.Text : "Player2";

            GameWindow gameWindow = new GameWindow(player1Name, player2Name);
            gameWindow.Show();

        }
    }
}