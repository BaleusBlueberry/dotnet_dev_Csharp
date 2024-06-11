using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using MineSweeper.Enums;

namespace MineSweeper.Pages
{
    /// <summary>
    /// Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        private string difficulty;

        public GameButton[,] _buttons = new GameButton[16, 16];

        public bool[,] takenMineSpots = new bool[16, 16]; 

        public GamePage(string difficulty)
        {
            InitializeComponent();
            this.difficulty = difficulty;
            InitializeGame();
        }

        public Dictionary<string, BitmapImage> btnImages = new Dictionary<string, BitmapImage>
        {
            { "Unclicked", new BitmapImage(new Uri("pack://application:,,,/resources/ButtonUnclicked.png")) },
            { "Click", new BitmapImage(new Uri("pack://application:,,,/resources/ButtonClicked.png")) },
        };

        private void InitializeGame()
        {
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    GameButton btn = new GameButton()
                    {
                        BtnImage = { Source = btnImages["Unclicked"] },
                    };

                    Grid.SetRow(btn, i);
                    Grid.SetColumn(btn, j);

                    // btn.KeyDown += BoardKeyDown;//?

                    btn.PreviewMouseDown += GameButton_PreviewMouseDown;
                    btn.PreviewMouseRightButtonDown += GameButton_RightButtonDown;

                    _buttons[i, j] = btn;

                    MineBoard.Children.Add(btn);
                }
            }

            // Initialize the game based on the difficulty
            // Example:
            // if (difficulty == "Easy") { /* Set up easy game */ }
            // else if (difficulty == "Medium") { /* Set up medium game */ }
            // else if (difficulty == "Hard") { /* Set up hard game */ }

        }

        private void BoardKeyDown(object sender, KeyEventArgs e)
        {
            GameButton btn = sender as GameButton;

            if (btn != null)
            {
                switch (btn.Tag)
                {
                    case "empty":
                        btn.BtnImage.Source = btnImages["Click"];
                        break;
                    case "owo":
                        break;
                }
            }
        }
        private void GameButton_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            GameButton btn = sender as GameButton;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                // expose bomb or or number
                switch (btn.userInput)
                {

                    case UserInput.clicked:

                        break;
                    default:
                        btn.BtnImage.Source = Images._bitmapImages[btn.locationType.ToString()];
                        //BtnImage.Source = _bitmapImages["ButtonUnclicked"];
                        break;
                }
            }
        }

        private void GameButton_RightButtonDown(object sender, MouseButtonEventArgs e)
        {
            GameButton btn = sender as GameButton;

            switch (btn.userInput)
            {
                case UserInput.empty:
                    btn.userInput = UserInput.flag;
                    btn.BtnImage.Source = Images._bitmapImages["ButtonFlag"];
                    break;
                case UserInput.flag:
                    btn.userInput = UserInput.qestionMark;
                    btn.BtnImage.Source = Images._bitmapImages["ButtonQuestionMark"];
                    break;
                case UserInput.qestionMark:
                    btn.userInput = UserInput.empty;
                    btn.BtnImage.Source = Images._bitmapImages["ButtonUnclicked"];
                    break;
                default:
                    btn.BtnImage.Source = Images._bitmapImages["ButtonUnclicked"];
                    break;
            }
            //  }
        }
    }
}
