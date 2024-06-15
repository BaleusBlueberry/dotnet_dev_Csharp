using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SnakeGame.Models;

public class Apple
{
    public Rectangle Shape { get; }

    public Apple()
    {
        Shape = new Rectangle { Width = 10, Height = 10, Fill = Brushes.Red };
    }
}

