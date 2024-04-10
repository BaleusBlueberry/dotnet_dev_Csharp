using System.ComponentModel;
using System.Data.Common;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
        SetTheme();
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    private int playerOneScore = 0;
    private int playerTwoScore = 0;
    private string endGameState;

    private void OnPropertyChanged(string name)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    private void HandleGameEnded(object? sender, GameEndEventArgs e)
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
        }
    }

    public int PlayerOneScore
    {
        get => playerOneScore;
        set
        {
            playerOneScore = value;
            OnPropertyChanged(nameof(PlayerOneScore));
        }
    }

    public int PlayerTwoScore
    {
        get => playerTwoScore;
        set
        {
            playerTwoScore = value;
            OnPropertyChanged(nameof(PlayerTwoScore));
        }
    }



    private void NewGame_Click(object sender, RoutedEventArgs e)
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
    private void SetTheme()
    {
        var windowsAppThemes = new ClassLibrary.WindowsAppThemes();
        var appTheme = windowsAppThemes.GetWindowsAppTheme();

        if (appTheme == WindowsAppTheme.Light) Syncfusion.SfSkinManager.SfSkinManager.SetVisualStyle(this, Syncfusion.SfSkinManager.VisualStyles.Windows11Light);
        else if (appTheme == WindowsAppTheme.Dark) Syncfusion.SfSkinManager.SfSkinManager.SetVisualStyle(this, Syncfusion.SfSkinManager.VisualStyles.Windows11Dark);
    }
}
