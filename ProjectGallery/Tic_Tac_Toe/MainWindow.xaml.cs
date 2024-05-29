using ClassLibrary;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Tic_Tac_Toe.Controls;
using Tic_Tac_Toe.Enums;


namespace Tic_Tac_Toe;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window, INotifyPropertyChanged
{
    public MainWindow()
    {
        InitializeComponent();

        ThemeHelper.SetTheme(this);

        MyBoard.GameEnded += HandleGameEnded;

        DataContext = this;
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    public string endGameState;
    private int _playerOneScore = 0;
    private int _playerTwoScore = 0;
    public int PlayerOneScore
    {
        get { return _playerOneScore; }
        set
        {
            _playerOneScore = value;
            OnPropertyChanged(nameof(PlayerOneScore));
        }
    }

    public int PlayerTwoScore
    {
        get { return _playerTwoScore; }
        set
        {
            _playerTwoScore = value;
            OnPropertyChanged(nameof(PlayerTwoScore));
        }
    }

    public void OnPropertyChanged(string name)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    public void HandleGameEnded(object? sender, GameEndEventArgs e)
    {
        switch (e.GameResult)
        {
            case GameResult.PlayerOneWins:
                PlayerOneScore++;
                EndGameState = "Player 1 won!";
                break;
            case GameResult.PlayerTwoWins:
                PlayerTwoScore++;
                EndGameState = "Player 2 won!";
                break;
            case GameResult.Draw:
                EndGameState = "Draw!";
                break;
        }
    }

    public string EndGameState
    {
        get => endGameState;
        set
        {
            endGameState = value;
            OnPropertyChanged(nameof(EndGameState));
            MessageBox.Show(EndGameState);
        }
    }

    public void NewGame_Click(object sender, RoutedEventArgs e)
    {
        GameType gameType;
        if (sender == Btn_PvP)
        {
            gameType = GameType.PvP;
        }
        else if (sender == Btn_PvC)
        {
            gameType = GameType.PvC;
        }
        else if (sender == Btn_CvC)
        {
            gameType = GameType.CvC;
        }
        else
        {
            return;
        }

        MyBoard.StartNewGame(gameType);
    }

    public void fileExitMenuItem_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }



}
