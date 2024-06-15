using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace SnakeGame.Models;

public enum DifficultyLevel { Easy, Medium, Hard }
public class Game
{

    public DifficultyLevel Difficulty { get; set; } = DifficultyLevel.Medium;

    private Canvas _canvas;
    private Snake _snake;
    private Apple _apple;
    private Random _rnd = new Random();
    private int _appleCounter = 0;

    public Game(Canvas canvas)
    {
        _canvas = canvas;
        _snake = new Snake();
        _apple = new Apple();
    }

    public void InitializeGame()
    {
        _appleCounter = 0;

        // Clear existing game objects from the canvas
        _canvas.Children.Clear();

        // Re-initialize the snake and place a new apple
        _snake.Initialize(_canvas);
        PlaceApple();
    }

    public void UpdateGame()
    {
        _snake.Move();

        if (_snake.IsDead(_canvas))
        {
            EndGame();
        }
        else
        {
            CheckCollisions();
        }
    }

    private void CheckCollisions()
    {
        if (_snake.HeadX == Canvas.GetLeft(_apple.Shape) && _snake.HeadY == Canvas.GetTop(_apple.Shape))
        {
            _snake.Grow();
            _appleCounter++;
            PlaceApple();
        }
    }

    public void OnKeyDown(Key key)
    {
        _snake.ChangeDirection(key);
    }

    public void OnCanvasSizeChanged()
    {
        // Adjust the game elements based on new canvas size if necessary
    }

    private void PlaceApple()
    {
        double x = _rnd.Next(0, (int)((_canvas.Width - 1) / 10)) * 10;
        double y = _rnd.Next(0, (int)((_canvas.Height - 1) / 10)) * 10;

        Canvas.SetLeft(_apple.Shape, x);
        Canvas.SetTop(_apple.Shape, y);

        if (!_canvas.Children.Contains(_apple.Shape))
        {
            _canvas.Children.Add(_apple.Shape);
        }
    }

    public void ResetGame()
    {
        InitializeGame();
    }

    private void EndGame()
    {
        MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
        if (mainWindow != null)
        {
            mainWindow.StopGame();
        }

        MessageBoxResult result = MessageBox.Show("Game Over! Your score: " + _appleCounter, "Game Over", MessageBoxButton.YesNo, MessageBoxImage.Question);

        if (result == MessageBoxResult.Yes)
        {
            mainWindow.RestartGame();
        }
        else
        {
            Application.Current.Shutdown();
        }
    }
}