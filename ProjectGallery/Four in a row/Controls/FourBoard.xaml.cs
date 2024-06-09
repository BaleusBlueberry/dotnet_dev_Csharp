using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
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
using Four_in_a_row.Controls;
using Four_in_a_row.Enums;

namespace Four_in_a_row.Controls;

/// <summary>
/// Interaction logic for FourBoard.xaml
/// </summary>
public partial class FourBoard : UserControl, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    public EventHandler<GameEndEventArgs> GameEnded;

    private string PlayerOneContent = "Green";
    private string PlayerTwoContent = "Red";

    private readonly ButtonMen[,] _buttons = new ButtonMen[6, 7];

    private readonly Random _rnd = new Random();

    private bool _isPlayerOneTurn = true;
    private bool _gameIsActive = false;
    public GameType? _gameType;


    public FourBoard()
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

    private void ChangeButtonColor(ButtonMen btn)
    {
        btn.Background = btn.Tag.ToString() == "Green" ? new SolidColorBrush(color: Colors.Green) : new SolidColorBrush(color: Colors.Red);
        // if e color is green
    }

    public string CurrentPlayerTurn => IsPlayerOneTurn ? "Player 1" : "Player 2";

    private void InitializeGameGrid()
    {
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                ButtonMen btn = new ButtonMen()
                {
                    FontSize = 45,
                    FontWeight = FontWeights.Bold,
                    Margin = new Thickness(5),
                    Background = new SolidColorBrush(Colors.DarkSlateGray),
                    Foreground = new SolidColorBrush(Colors.Cyan),
                };

                Grid.SetRow(btn, i);
                Grid.SetColumn(btn, j);

                _buttons[i, j] = btn;

                BoardControl.Children.Add(btn);
            }
        }

        for (int i = 0; i < 7; i++)
        {
            ArrowDown arrow = new ArrowDown
            {
                Tag = i,
                Margin = new Thickness(10, 0, 10, 0),
            };

            arrow.Click += Arrow_Clicked;

            Grid.SetRow(arrow, 0);
            Grid.SetColumn(arrow, i);

            DropArrows.Children.Add(arrow);
        }
    }

    private void Arrow_Clicked(object sender, RoutedEventArgs e)
    {

        if (!_gameIsActive)
        {
            MessageBox.Show("Please Start The Game First");
            return;
        }

        if (CurrentGameType == GameType.PvC && !IsPlayerOneTurn || CurrentGameType == GameType.CvC) return;

        ArrowDown arrow = sender as ArrowDown;

        if (arrow == null)
        {
            return;
        }
        int column = (int)arrow.Tag;

        PlaceToken(column);

        // find the right button in the grid _buttons.
        // do this Button btn = [possition of the button]_buttons as Button;

        // make a move here, make it green if first player and red if second using ChangeButtonColor(btn)
        // and     private string PlayerOneContent = "Green";
        // and     private string PlayerTwoContent = "Red";

        // After human's turn

        if (CurrentGameType == GameType.PvC && !IsPlayerOneTurn)
        {
            ComputerMove();
        }
    }

    private void PlaceToken(int column)
    {

        for (int row = 5; row >= 0; row--)
        {
            if (_buttons[row, column].Tag.ToString() == "")
            {
                _buttons[row, column].Tag = _isPlayerOneTurn ? "Green" : "Red";
                ChangeButtonColor(_buttons[row, column]);


                if (CheckForWinner())
                {
                    ProcessEndGame(true);
                }
                else if (IsBoardFull())
                {
                    ProcessEndGame(false);
                }
                else
                {
                    IsPlayerOneTurn = !IsPlayerOneTurn;
                }
                return;
            }
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

            ButtonMen btn;

            int col;
            do
            {
                int row = _rnd.Next(6);
                col = _rnd.Next(7);
                btn = _buttons[row, col];

            } while (btn.Tag.ToString() != "");

            PlaceToken(col);

            if (CurrentGameType == GameType.CvC && !IsBoardFull() && _gameIsActive)
            {
                ComputerMove();
            }
        };
        timer.Start();
    }

    public void ProcessEndGame(bool isThereWinner)
    {
        if (isThereWinner)
        {
            GameResult result = IsPlayerOneTurn ? GameResult.PlayerOneWins : GameResult.PlayerTwoWins;

            OnGameEnd(result);

            _gameIsActive = false;
        }
        if (IsBoardFull())
        {
            GameResult result = GameResult.Draw;

            OnGameEnd(result);

            _gameIsActive = false;
        }
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

        foreach (ButtonMen btn in _buttons)
        {
            btn.Tag = "";
            btn.Background = new SolidColorBrush(Colors.LightBlue);
        }

        if (gameType == GameType.CvC) ComputerMove();

    }

    private bool IsBoardFull()
    {
        foreach (ButtonMen button in _buttons)
        {
            if (button.Tag.ToString() == "")
            {
                return false;
            }
        }
        return true;
    }

    private bool CheckForWinner()
    {
        int checkedCorrect = 0;

        for (int row = 0; row < 6; row++)
        {
            for (int col = 0; col < 7; col++)
            {
                if (_buttons[row, col].Tag.ToString() != "")
                {
                    if (col <= 3)
                    {
                        if (_buttons[row, col].Tag.ToString() == _buttons[row, col + 1].Tag.ToString() &&
                            _buttons[row, col].Tag.ToString() == _buttons[row, col + 2].Tag.ToString() &&
                            _buttons[row, col].Tag.ToString() == _buttons[row, col + 3].Tag.ToString()
                           )
                        {
                            return true;
                        }
                    }
                    if (row >= 3)
                    {
                        if (_buttons[row, col].Tag.ToString() == _buttons[row - 1, col].Tag.ToString() &&
                            _buttons[row, col].Tag.ToString() == _buttons[row - 2, col].Tag.ToString() &&
                            _buttons[row, col].Tag.ToString() == _buttons[row - 3, col].Tag.ToString()
                           )
                        {
                            return true;
                        }
                    }
                    // top right
                    if (col <= 3 && row >= 3)
                    {
                        if (_buttons[row, col].Tag.ToString() == _buttons[row - 1, col + 1].Tag.ToString() &&
                            _buttons[row, col].Tag.ToString() == _buttons[row - 2, col + 2].Tag.ToString() &&
                            _buttons[row, col].Tag.ToString() == _buttons[row - 3, col + 3].Tag.ToString()
                           )
                        {
                            return true;
                        }
                    }
                    // top left 
                    if (col >= 3 && row >= 3)
                    {
                        if (_buttons[row, col].Tag.ToString() == _buttons[row - 1, col - 1].Tag.ToString() &&
                            _buttons[row, col].Tag.ToString() == _buttons[row - 2, col - 2].Tag.ToString() &&
                            _buttons[row, col].Tag.ToString() == _buttons[row - 3, col - 3].Tag.ToString()
                           )
                        {
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }

    public void ResetGame()
    {
        CurrentGameType = null;
        _gameType = null;
        IsPlayerOneTurn = true;
        _gameIsActive = false;

        foreach (ButtonMen btn in _buttons)
        {
            btn.Tag = "";
            btn.Background = new SolidColorBrush(Colors.LightBlue);
        }
    }
}
