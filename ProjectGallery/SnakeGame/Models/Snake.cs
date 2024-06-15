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
    private int _direction; // 0=Up, 1=Right, 2=Down, 3=Left
    private Canvas _canvas;

    public Snake()
    {
        _body = new List<Rectangle>();
        _direction = 1; // Initial direction is right
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

        switch (_direction)
        {
            case 0:
                y -= 10; // Move up
                break;
            case 1:
                x += 10; // Move right
                break;
            case 2:
                y += 10; // Move down
                break;
            case 3:
                x -= 10; // Move left
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

    public void ChangeDirection(Key key)
    {
        switch (key)
        {
            case Key.Up:
                if (_direction != 2) _direction = 0;
                break;
            case Key.Right:
                if (_direction != 3) _direction = 1;
                break;
            case Key.Down:
                if (_direction != 0) _direction = 2;
                break;
            case Key.Left:
                if (_direction != 1) _direction = 3;
                break;
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
        _direction = 1; // Reset direction to default (right)
    }
}
