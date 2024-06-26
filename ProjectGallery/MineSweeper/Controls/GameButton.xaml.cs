﻿using MineSweeper.Enums;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace MineSweeper
{
    public partial class GameButton : Button
    {
        private bool _isMouseDown;
        public UserInput userInput = UserInput.empty;
        public LocationType locationType = LocationType.empty;
        public int Row { set; get; }
        public int Col{ set; get; }

        public GameButton()
        {
            InitializeComponent();
            BtnImage = new Image();
            Content = BtnImage;

            BtnImage.Source = new BitmapImage(new Uri("pack://application:,,,/resources/ButtonUnclicked.png"));
        }
    }
}