using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Tic_Tac_Toe.ElementControl;

public class TicTakToeElement : TicTacToeInterface
{
    public int Horizontal { get; set; }
    public int Vertical { get; set; }
    public BitmapImage Icon { get; set; }

    public TicTakToeElement(int horizontal, int vertical, string icon)
    {
        Horizontal = horizontal;
        Vertical = vertical;
        Icon = new BitmapImage(new Uri($"{AppDomain.CurrentDomain.BaseDirectory}/Resources/{(icon == "X" ? "X" : "O")}.png", UriKind.Absolute));
    }

    public void Clicked()
    {
        MainWindow window = new MainWindow();
        window.ShowDialog();
    }
}

