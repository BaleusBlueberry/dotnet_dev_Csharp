﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Tic_Tac_Toe.ElementControl;

public interface TicTacToeInterface
{
    double Horizontal { get; }

    double Vertical { get; }

    BitmapImage Icon { get; }

    void Clicked();

}

