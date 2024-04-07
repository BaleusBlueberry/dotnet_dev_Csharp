﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tic_Tac_Toe.Enums;

namespace Tic_Tac_Toe.Controls;

    /// <summary>
    /// Interaction logic for Board.xaml
    /// </summary>
    public partial class Board : UserControl
    {
        
        private string PlayerOneContent = "X";
        private string PlayerTwoContent = "O";

        private readonly Button[,] _buttons = new Button[3, 3];

        private bool _isPlayerOneTurn = true;
        private GameType _gameType;


        public Board()
        {
            InitializeComponent();
            InitializeGameGrid();
        }

        private void InitializeGameGrid()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Button btn = new Button()
                    {
                        FontSize = 45,
                        FontWeight = FontWeights.Bold,
                        Margin = new Thickness(5),

                    };

                    btn.Click += Button_Click;

                    Grid.SetRow(btn, i);
                    Grid.SetColumn(btn, j);

                    _buttons[i, j] = btn;

                    BoardControl.Children.Add(btn);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null) return;

            if (btn.Content == null)
            {
                btn.Content = _isPlayerOneTurn ? PlayerOneContent : PlayerTwoContent;
            }

            if (ProcessEndGame())
            {
                
            }
            _isPlayerOneTurn = !_isPlayerOneTurn;
        }

        private bool ProcessEndGame()
        {

            bool win = CheckForWinner();
            if (win)
            {
                
                return true;
            }

            

            return false;
        }

        public void StartNewGame(GameType gameType)
        {
            _gameType = gameType;
            _isPlayerOneTurn = true;

        }

    private bool IsBoardFull()
    {
        foreach (Button button in _buttons)
        {
            if (button.Content == null)
            {
                return false;
            }
        }

        return true;
    }

    private bool CheckForWinner()
    {
        for (int i = 0; i < 3; i++)
        {
            if (AreButtonsEqual(_buttons[i, 0], _buttons[i, 1], _buttons[i, 2]))
            {
                return true;
            }
            if (AreButtonsEqual(_buttons[0, i], _buttons[1, i], _buttons[2, i]))
            {
                return true;
            }
        }

        if (AreButtonsEqual(_buttons[0, 0], _buttons[1, 1], _buttons[2, 2]))
        {
            return true;
        }
        if (AreButtonsEqual(_buttons[0, 2], _buttons[1, 1], _buttons[2, 0]))
        {
            return true;
        }

        return false;
    }

    private bool AreButtonsEqual(Button b1, Button b2, Button b3)
        {
            return b1.Content != null && b1.Content == b2.Content && b2.Content == b3.Content;
        }
    }

