using SnakeGame.Models;
using System.Diagnostics;
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
    private Stopwatch _stopwatch = new Stopwatch();
    private const long UpdateInterval = 100;

    public MainWindow()
    {
        InitializeComponent();
        GameCanvas.Loaded += GameCanvas_Loaded;
        GameCanvas.SizeChanged += GameCanvas_SizeChanged;
        this.KeyDown += Window_KeyDown;
        CompositionTarget.Rendering += CompositionTarget_Rendering;
    }

    private void CompositionTarget_Rendering(object sender, EventArgs e)
    {
        if (_stopwatch.ElapsedMilliseconds > UpdateInterval)
        {
            _game.UpdateGame();
            _stopwatch.Restart();
        }
    }

    private void GameCanvas_Loaded(object sender, RoutedEventArgs e)
    {
        _game = new Game(GameCanvas);
        StartGame();
    }

    private void StartGame()
    {
        GameCanvas.Children.Clear();
        _game.ResetGame();
        _game.InitializeGame();
        _stopwatch.Start();
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
        _stopwatch.Stop();
    }

    public void RestartGame()
    {
        StopGame();
        StartGame(); // Use StartGame() instead of manually starting the timer
    }
}
