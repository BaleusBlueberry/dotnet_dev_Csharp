using SnakeGame.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        private Game _game;
        private Stopwatch _stopwatch = new Stopwatch();
        private long UpdateInterval = 100;

        public GamePage(DifficultyLevel difficulty)
        {
            InitializeComponent();
            _game = new Game(GameCanvas, difficulty);
            GameCanvas.Loaded += GameCanvas_Loaded;
            this.PreviewKeyDown += Window_PreviewKeyDown;
            CompositionTarget.Rendering += CompositionTarget_Rendering;

            switch (difficulty)
            {
                case DifficultyLevel.Easy:
                    UpdateInterval = 150;
                    break;
                case DifficultyLevel.Medium:
                    UpdateInterval = 100;
                    break;
                case DifficultyLevel.Hard:
                    UpdateInterval = 50;
                    break;
                default:
                    throw new ArgumentException("Invalid difficulty level specified.");
            }
        }

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            if (_stopwatch.ElapsedMilliseconds > UpdateInterval)
            {
                _game.UpdateGame();
                _stopwatch.Restart();
                GameCanvas.Focus();
            }
        }

        private void GameCanvas_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _game.InitializeGame();
            StartGame();
            GameCanvas.Focus();
        }

        private void StartGame()
        {
            _game.RestartGame();
            _stopwatch.Start();
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e) 
        {
            switch (e.Key)
            {
                case Key.Up:
                    _game.OnKeyDown(Direction.Up);
                    break;
                case Key.Down:
                    _game.OnKeyDown(Direction.Down);
                    break;
                case Key.Left:
                    _game.OnKeyDown(Direction.Left);
                    break;
                case Key.Right:
                    _game.OnKeyDown(Direction.Right);
                    break;
                default:
                    break;
            }
        }

        public void StopGame()
        {
            _stopwatch.Stop();
        }

        public void RestartGame()
        {
            StopGame();
            StartGame();
        }
    }
}
