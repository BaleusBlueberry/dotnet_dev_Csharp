using SnakeGame.Models;
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
using System.Windows.Threading;

namespace SnakeGame;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private Game _game;
    private DispatcherTimer _gameTimer;

    public MainWindow()
    {
        InitializeComponent();
        GameCanvas.Loaded += GameCanvas_Loaded;
        GameCanvas.SizeChanged += GameCanvas_SizeChanged;
        this.KeyDown += Window_KeyDown;
    }

    private void GameCanvas_Loaded(object sender, RoutedEventArgs e)
    {
        _game = new Game(GameCanvas);
        StartGame();
    }

    private void StartGame()
    {
        _game.InitializeGame();

        _gameTimer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(100)
        };
        _gameTimer.Tick += GameTimer_Tick;
        _gameTimer.Start();
    }

    private void GameTimer_Tick(object sender, EventArgs e)
    {
        _game.UpdateGame();
    }

    private void Window_KeyDown(object sender, KeyEventArgs e)
    {
        _game.OnKeyDown(e.Key);
    }

    private void GameCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        if (_game != null)
        {
            _game.OnCanvasSizeChanged();
        }
    }

    public void CloseGame()
    {
        this.Close();
    }

    public void StopGame()
    {
        if (_gameTimer != null)
        {
            _gameTimer.Stop();
        }
    }

    public void RestartGame()
    {
        StopGame();
        GameCanvas.Children.Clear();
        _game.ResetGame();
        _gameTimer.Start();
    }
}
