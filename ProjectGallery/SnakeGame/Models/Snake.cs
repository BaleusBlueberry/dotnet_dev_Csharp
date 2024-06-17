using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SnakeGame.Models;

public class Snake
{
    private List<Rectangle> _body;
    private Direction _direction;
    private Canvas _canvas;

    public Snake()
    {
        _body = new List<Rectangle>();
        _direction = Direction.Right; // Initial direction is right
    }

    public double HeadX => Canvas.GetLeft(_body[0]);
    public double HeadY => Canvas.GetTop(_body[0]);

    public void Initialize(Canvas canvas)
    {
        _canvas = canvas;
        _body.Clear();
        Rectangle head = new Rectangle { Width = 10, Height = 10, Fill = Brushes.Green };
        _canvas.Children.Add(head);
        Canvas.SetLeft(head, 50);
        Canvas.SetTop(head, 50);
        _body.Add(head);
    }

    public void Move()
    {
        double x = HeadX;
        double y = HeadY;

        // Calculate new head position based on current direction
        switch (_direction)
        {
            case Direction.Up:
                y -= 10;
                break;
            case Direction.Right:
                x += 10;
                break;
            case Direction.Down:
                y += 10;
                break;
            case Direction.Left:
                x -= 10;
                break;
        }

        // Move the head
        Rectangle newHead = new Rectangle { Width = 10, Height = 10, Fill = Brushes.Green };
        _body.Insert(0, newHead);
        Canvas.SetLeft(newHead, x);
        Canvas.SetTop(newHead, y);
        _canvas.Children.Add(newHead);

        // Remove the tail
        Rectangle tail = _body[_body.Count - 1];
        _canvas.Children.Remove(tail);
        _body.RemoveAt(_body.Count - 1);
    }

    public void ChangeDirection(Direction direction)
    {
        // Prevent the snake from reversing direction
        if ((_direction == Direction.Up && direction != Direction.Down) ||
            (_direction == Direction.Right && direction != Direction.Left) ||
            (_direction == Direction.Down && direction != Direction.Up) ||
            (_direction == Direction.Left && direction != Direction.Right))
        {
            _direction = direction;
        }
    }

    public void Grow()
    {
        Rectangle newPart = new Rectangle { Width = 10, Height = 10, Fill = Brushes.Green };
        _body.Add(newPart);
    }

    public bool IsDead(Canvas canvas)
    {
        // Check if the snake collides with canvas boundaries
        double headX = HeadX;
        double headY = HeadY;

        // Check boundaries
        if (headX < 0 || headX >= canvas.ActualWidth || headY < 0 || headY >= canvas.ActualHeight)
        {
            return true;
        }

        for (int i = 1; i < _body.Count; i++)
        {
            if (headX == Canvas.GetLeft(_body[i]) && headY == Canvas.GetTop(_body[i]))
            {
                return true;
            }
        }

        return false;
    }

    public void Reset()
    {
        _body.Clear();
        Rectangle head = new Rectangle { Width = 10, Height = 10, Fill = Brushes.Green };
        _canvas.Children.Add(head);
        Canvas.SetLeft(head, 50);
        Canvas.SetTop(head, 50);
        _body.Add(head);
        _direction = Direction.Right; // Reset direction to default (right)
    }
}
