using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace MineSweeper.Pages
{
    /// <summary>
    /// Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        private string difficulty;

        private readonly GameButton[,] _buttons = new GameButton[16, 16];

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
                        Tag = "empty",
                        BtnImage = { Source = btnImages["Unclicked"] },
                        Focusable = true
                    };

                    Grid.SetRow(btn, i);
                    Grid.SetColumn(btn, j);

                    btn.KeyDown += BoardKeyDown;

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
    }
}
