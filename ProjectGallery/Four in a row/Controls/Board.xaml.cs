using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
using System.Windows.Threading;
using Four_in_a_row.Enums;
using Tic_Tac_Toe.Controls;

namespace Four_in_a_row.Controls;

/// <summary>
/// Interaction logic for Board.xaml
/// </summary>
public partial class Board : UserControl, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    public EventHandler<GameEndEventArgs> GameEnded;

    private string PlayerOneContent = "X";
    private string PlayerTwoContent = "O";

    private readonly Button[,] _buttons = new Button[6, 7];

    private readonly Random _rnd = new Random();

    private bool _isPlayerOneTurn = true;
    private bool _gameIsActive = false;
    public GameType? _gameType;


    public Board()
    {
        //
        InitializeComponent();
        InitializeGameGrid();

        DataContext = this;

    }

    public GameType? CurrentGameType
    {
        get
        {
            return _gameType;
        }
        set
        {
            _gameType = value;
            OnPropertyChanged(nameof(CurrentGameType));
        }
    }

    public void OnPropertyChanged(string name)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    public bool IsPlayerOneTurn
    {
        get => _isPlayerOneTurn;
        set
        {
            _isPlayerOneTurn = value;

            OnPropertyChanged(nameof(_isPlayerOneTurn));
            OnPropertyChanged(nameof(CurrentPlayerTurn));
        }
    }
    private void OnGameEnd(GameResult result)
    {
        GameEnded?.Invoke(this, new GameEndEventArgs(result));
    }


    public string CurrentPlayerTurn => IsPlayerOneTurn ? "Player 1" : "Player 2";

    private void InitializeGameGrid()
    {
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                Button btn = new Button()
                {
                    FontSize = 45,
                    FontWeight = FontWeights.Bold,
                    Margin = new Thickness(5),
                    Background = new SolidColorBrush(Colors.DarkSlateGray),
                    Foreground = new SolidColorBrush(Colors.Cyan),
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
        if (!_gameIsActive)
        {
            MessageBox.Show("Please Start The Game First");
            return;
        }
        if (CurrentGameType == GameType.PvC && !IsPlayerOneTurn || CurrentGameType == GameType.CvC) return;

        Button btn = sender as Button;

        if (btn == null) return;

        if (btn.Content == null)
        {
            btn.Content = IsPlayerOneTurn ? PlayerOneContent : PlayerTwoContent;
        }
        else
        {
            return;
        }

        if (ProcessEndGame())
        {
            return;
        }
        IsPlayerOneTurn = !IsPlayerOneTurn;

        // After human's turn

        if (CurrentGameType == GameType.PvC && !IsPlayerOneTurn)
        {
            ComputerMove();
        }
    }

    private void ComputerMove()
    {
        DispatcherTimer timer = new DispatcherTimer()
        {
            Interval = TimeSpan.FromSeconds(_rnd.Next(10) / 10.0)
        };

        timer.Tick += (sender, e) =>
        {
            timer.Stop();

            Button btn;
            do
            {
                int row = _rnd.Next(3);
                int col = _rnd.Next(3);
                btn = _buttons[row, col];
            } while (btn.Content != null);

            btn.Content = IsPlayerOneTurn ? PlayerOneContent : PlayerTwoContent;
            if (ProcessEndGame()) return;

            IsPlayerOneTurn = !IsPlayerOneTurn;

            if (CurrentGameType == GameType.CvC && !IsBoardFull())
            {
                ComputerMove();
            }
        };
        timer.Start();
    }

    private bool ProcessEndGame()
    {

        bool win = CheckForWinner();
        if (win)
        {
            GameResult result = IsPlayerOneTurn ? GameResult.PlayerOneWins : GameResult.PlayerTwoWins;

            OnGameEnd(result);

            _gameIsActive = false;
            return true;
        }
        if (IsBoardFull())
        {
            GameResult result = GameResult.Draw;

            OnGameEnd(result);

            _gameIsActive = false;
            return true;
        }



        return false;
    }

    public void StartNewGame(GameType gameType)
    {
        if (_gameIsActive)
        {
            return;
        }

        CurrentGameType = gameType;
        IsPlayerOneTurn = true;
        _gameIsActive = true;

        foreach (Button btn in _buttons)
        {
            btn.Content = null;
        }

        if (gameType == GameType.CvC) ComputerMove();

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

    public void ResetGame()
    {
        CurrentGameType = null;
        _gameType = null;
        IsPlayerOneTurn = true;
        _gameIsActive = false;

        foreach (Button btn in _buttons)
        {
            btn.Content = null;
        }
    }
}


