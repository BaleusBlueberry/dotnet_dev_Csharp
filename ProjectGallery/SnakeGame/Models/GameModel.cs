using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using SnakeGame.Pages;

namespace SnakeGame.Models;
public enum DifficultyLevel { Easy, Medium, Hard }
public enum Direction
{
    Up,
    Right,
    Down,
    Left
}
public class Game
{
    public DifficultyLevel Difficulty { get; set; } = DifficultyLevel.Medium;
    private Canvas _canvas;
    private Snake _snake;
    private Apple _apple;
    private Random _rnd = new Random();
    private int _appleCounter = 0;

    public Game(Canvas canvas, DifficultyLevel difficulty = DifficultyLevel.Medium)
    {
        _canvas = canvas;
        _snake = new Snake();
        _apple = new Apple();
        Difficulty = difficulty;
    }

    public void InitializeGame()
    {
        _appleCounter = 0;
        _canvas.Children.Clear();
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

    public void OnKeyDown(Direction direction)
    {
        _snake.ChangeDirection(direction);
    }

    private void PlaceApple()
    {
        int maxX = (int)(_canvas.Width - 10);
        int maxY = (int)(_canvas.Height - 10);

        double x = _rnd.Next(1, maxX / 10) * 10;  
        double y = _rnd.Next(1, maxY / 10) * 10;  

        Canvas.SetLeft(_apple.Shape, x);
        Canvas.SetTop(_apple.Shape, y);

        if (!_canvas.Children.Contains(_apple.Shape))
        {
            _canvas.Children.Add(_apple.Shape);
        }
    }

    public void RestartGame()
    {
        InitializeGame();
        _snake.ChangeDirection(Direction.Right);
    }

    private void EndGame()
    {
        MessageBoxResult result = MessageBox.Show("Game Over! Your score: " + _appleCounter, "Game Over", MessageBoxButton.YesNo, MessageBoxImage.Question);

        if (result == MessageBoxResult.Yes)
        {
            RestartGame();
        }
        else
        {
            Application.Current.Shutdown();
        }
    }
}

