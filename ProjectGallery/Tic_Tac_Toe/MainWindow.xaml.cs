using ClassLibrary;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Tic_Tac_Toe.Controls;
using Tic_Tac_Toe.Enums;
using Button = System.Windows.Controls.Button;


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
        WindowHelper.EnableWindowDragging(this);

        MyBoard.GameEnded += HandleGameEnded;

        DataContext = this;

        CreateButtons();
    }
    private GameType? gameType;
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

    public Dictionary<string, bool> coloredButtons = new Dictionary<string, bool>
    {
        { "Btn_PvP", false },
        { "Btn_PvC", false },
        { "Btn_CvC", false }
    };

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

    public void SelectGameType(object sender, RoutedEventArgs e)
    {
        Button button = sender as Button;

        if (button.Name == "Btn_PvP")
        {
            gameType = GameType.PvP;

        }
        else if (button.Name == "Btn_PvC")
        {
            gameType = GameType.PvC;
        }
        else if (button.Name == "Btn_CvC")
        {
            gameType = GameType.CvC;
        }

        if (AnyOtherButtonIsPressed())
        {
            ResetAllColorButtons();
            ChangeButtonColor(button);
        } else
        {
            ChangeButtonColor(button);
        }
        

    }
    private void StartGameButton_Click(object sender, RoutedEventArgs e)
    {
        if (gameType == GameType.PvP || gameType == GameType.PvC || gameType == GameType.CvC)
            MyBoard.StartNewGame((GameType)gameType);
        else MessageBox.Show("Please Select a Game Type");
    }

    private void CreateButtons()
    {
        string[] buttonDisplayNames = new string[] {
            "PvP (Player Vs Player)",
            "PvC (Player Vs Computer)",
            "CvC (Computer Vs Computer)"
        };
        string[] buttonNames = new string[] {
            "Btn_PvP",
            "Btn_PvC",
            "Btn_CvC"
        };

        for (int i = 0; i < buttonDisplayNames.Length; i++)
        {
            Button button = new Button
            {
                Content = buttonDisplayNames[i],
                Name = buttonNames[i],
                Margin = new Thickness(10, 10, 10, 10),
                Background = new SolidColorBrush(Colors.Cyan),
                Foreground = new SolidColorBrush(Colors.Black),
        };
            button.Click += SelectGameType;

            buttonList.Children.Add(button);
        };
    }

    private void ResetAllColorButtons()
    {
        foreach (KeyValuePair<string, bool> keyValue in coloredButtons)
        {
            coloredButtons[keyValue.Key] = false;
        }

        foreach (Button button in buttonList.Children)
        {
            ResetColorButton(button);
        }
    }

    private void ChangeButtonColor(Button button)
    {
        string buttonName = button.Name;

        if (coloredButtons[buttonName])
        {
            button.Background = new SolidColorBrush(Colors.Cyan);
            button.Foreground = new SolidColorBrush(Colors.Black);
        } else
        {
            button.Background = new SolidColorBrush(Colors.Red);
            button.Foreground = new SolidColorBrush(Colors.Yellow);
        }
       
        coloredButtons[buttonName] = !coloredButtons[buttonName];

    }
    private void ResetColorButton(object sender)
    {
        Button currentButton = sender as Button;
        {

            currentButton.Background = new SolidColorBrush(Colors.Cyan);
            currentButton.Foreground = new SolidColorBrush(Colors.Black);
            return;
        }
    }

    private bool AnyOtherButtonIsPressed()
    {
        foreach (KeyValuePair<string, bool> keyValue in coloredButtons)
        {
            if (coloredButtons[keyValue.Key] = true)
            {
                return true;
            }
        }
        return false;
    }

    private void ResetGame_Click(object sender, RoutedEventArgs e)
    {
        MyBoard.ResetGame();
        ResetAllColorButtons();
        gameType = null;
    }

    private void closeWindow(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
    private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        // This event handler will be called when the window is clicked, and it will trigger the window dragging functionality
    }
}
