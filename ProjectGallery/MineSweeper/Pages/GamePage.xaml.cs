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
                        Focusable = true
                    };

                    Grid.SetRow(btn, i);
                    Grid.SetColumn(btn, j);

                    // btn.KeyDown += BoardKeyDown;//?

                    btn.PreviewMouseDown += GameButton_PreviewMouseDown;
                    btn.PreviewMouseUp += GameButton_PreviewMouseUp;
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
            _buttons[0, 0].Focus();
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
                /* if (sender is GameButton button && button.Tag is Dictionary<string, object> tagDict && tagDict.TryGetValue("userInput", out var userInput))
                 {
                     switch (userInput)
                     {
                         case UserInput.empty:
                             BtnImage.Source = _bitmapImages["ButtonClicked"];
                             break;
                         case UserInput.qestionMark:
                             BtnImage.Source = _bitmapImages["ButtonQuestionMarkClicked"];
                             break;
                         case UserInput.clicked:
                             break;
                         default:
                             BtnImage.Source = _bitmapImages["ButtonClicked"];
                             break;
                     }
                 }*/
                // _isMouseDown = true;

                //switch (this.userInput)
                //{
                //    case UserInput.empty:
                //        BtnImage.Source = _bitmapImages["ButtonClicked"];
                //        break;
                //    case UserInput.qestionMark:
                //        BtnImage.Source = _bitmapImages["ButtonQuestionMarkClicked"];
                //        break;
                //    case UserInput.clicked:
                //        break;
                //    default:
                //        BtnImage.Source = _bitmapImages["ButtonClicked"];
                //        break;
                //}

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

        private void GameButton_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            /*
           // _isMouseDown = false;
          //  if (sender is GameButton button && button.Tag is Dictionary<string, object> tagDict && tagDict.TryGetValue("userInput", out var userInput))
          //  {
                switch (userInput)
                {
                    //case UserInput.empty:
                    //    BtnImage.Source = _bitmapImages["ButtonUnclicked"];
                    //    break;
                    //case UserInput.qestionMark:
                    //    BtnImage.Source = _bitmapImages["ButtonQuestionMark"];
                    //    break;
                    //case UserInput.flag:
                    //    BtnImage.Source = _bitmapImages["ButtonFlag"];
                    //    break;
                    case UserInput.clicked:

                        break;
                    default:
                        BtnImage.Source = _bitmapImages[this.locationType.ToString()];
                        //BtnImage.Source = _bitmapImages["ButtonUnclicked"];
                        break;
                }
          //  }
            */
        }

        private void GameButton_RightButtonDown(object sender, MouseButtonEventArgs e)
        {
            GameButton btn = sender as GameButton;

            //   if (sender is GameButton button && button.Tag is Dictionary<string, object> tagDict && tagDict.TryGetValue("userInput", out var userInput))
            //   {
            //switch (userInput)
            //{
            //    case UserInput.empty:
            //        tagDict["userInput"] = UserInput.flag;
            //        button.BtnImage.Source = _bitmapImages["ButtonFlag"];
            //        break;
            //    case UserInput.flag:
            //        tagDict["userInput"] = UserInput.qestionMark;
            //        button.BtnImage.Source = _bitmapImages["ButtonQuestionMark"];
            //        break;
            //    case UserInput.qestionMark:
            //        tagDict["userInput"] = UserInput.empty;
            //        button.BtnImage.Source = _bitmapImages["ButtonUnclicked"];
            //        break;
            //    default:
            //        button.BtnImage.Source = _bitmapImages["ButtonUnclicked"];
            //        break;
            //}
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
