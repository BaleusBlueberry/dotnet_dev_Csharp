using System;
using System.Collections.Generic;
using System.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using MineSweeper.Enums;
using MineSweeper.Generation;

namespace MineSweeper.Pages;

public partial class GamePage : Page
{
    private string difficulty;

    public GameButton[,] _buttons = new GameButton[16, 16];

    public bool gameActive = false;

    public bool firstMove = true;

    private DispatcherTimer _timer;

    private int _timeElapsed;

    private int _bombCount;

    public GamePage(string difficulty)
    {
        InitializeComponent();
        this.difficulty = difficulty;
        InitializeGame();
    }

    private void InitializeGame()
    {
        GlobalSettings.ResetMineSpots();
        gameActive = true;
        firstMove = true;

        _timer?.Stop();
        _timeElapsed = 0;
        Timer.Text = "0";

        _bombCount = difficulty == "Easy" ? 20 : difficulty == "Medium" ? 37 : difficulty == "Hard" ? 54 : 0;
        BombCount.Text = _bombCount.ToString();

        // generates the board itself 
        for (int i = 0; i < 16; i++)
        {
            for (int j = 0; j < 16; j++)
            {
                GameButton btn = new GameButton()
                {
                    BtnImage = { Source = Images._bitmapImages["empty"] },
                };

                Grid.SetRow(btn, i);
                Grid.SetColumn(btn, j);

                btn.Row = i;
                btn.Col = j;

                btn.PreviewMouseLeftButtonDown += BoardKeyDown;
                btn.PreviewMouseRightButtonDown += GameButton_RightButtonDown;

                _buttons[i, j] = btn;

                MineBoard.Children.Add(btn);
            }
        }
        int amountOfBombs = difficulty == "Easy" ? 20 : difficulty == "Medium" ? 37 : difficulty == "Hard" ? 54 : 0;
        _bombCount = amountOfBombs;


        List<(int, int)> generateBombsList = GenerateRandomSpot.GenerateRandomSpots(amountOfBombs);
        foreach ((int row, int col) in generateBombsList)
        {
            _buttons[row, col].locationType = LocationType.mine;
        }

        _timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(1)
        };

        _timer.Tick += Timer_Tick;
    }

    private void RenderEndOfGame(bool win)
    {
        _timer.Stop();

        if (win)
        {
            var Result = MessageBox.Show($"You have won a {difficulty} difficulty game?", "Would you like to play another?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (Result == MessageBoxResult.Yes) InitializeGame();
            if (Result == MessageBoxResult.No)
            {
                SelectionPage selectionPage = new SelectionPage();
                NavigationService.Navigate(selectionPage);
            }
        }
        else
        {
            ShowRestOfGameBoard();
        }
    }

    private void BoardKeyDown(object sender, MouseButtonEventArgs e)
    {
        GameButton btn = sender as GameButton;

        if (!(btn != null & gameActive & btn.userInput != UserInput.clicked)) return;

        if (firstMove)
        {
            DoFirstMove(btn);
            return;
        }

        bool mine = false;
        // expose bomb or or number
        switch (btn.locationType)
        {
            case LocationType.mine:
                btn.BtnImage.Source = Images._bitmapImages["ButtonMineClicked"];
                btn.userInput = UserInput.clicked;
                RenderEndOfGame(false);
                mine = true;
                break;

        }
        if (mine) return;

        if (btn.userInput is UserInput.flag) _bombCount--; 
        btn.BtnImage.Source = btn.locationType != LocationType.empty ? Images._bitmapImages[btn.locationType.ToString()] : Images._bitmapImages["ButtonClicked"];
        btn.userInput = UserInput.clicked;

        CheckNearbyTiles(btn);
        CheckIFWin();
    }

    private void DoFirstMove(GameButton btn)
    {
        int row = btn.Row;
        int col = btn.Col;

        int rows = _buttons.GetLength(0);
        int cols = _buttons.GetLength(1);

        // test if the button is in corner or side 

        List<(int, int)> checkSposts = FindAllowdSpaces.Find(row, col, rows, cols);

        int takenMinesOut = 0;
        foreach ((int r, int c) in checkSposts)
        {
            if (_buttons[r, c].locationType == LocationType.mine)
            {
                _buttons[r, c].locationType = LocationType.empty;
                takenMinesOut++;
            }
        }

        _bombCount = _bombCount - takenMinesOut;

        GenerateBoardNumbers();

        // make the app know the next move will not be the first one
        firstMove = false;

        btn.BtnImage.Source = Images._bitmapImages["ButtonClicked"];
        btn.userInput = UserInput.clicked;

        CheckNearbyTiles(btn);

        _timer.Start();
    }

    private void GameButton_RightButtonDown(object sender, MouseButtonEventArgs e)
    {
        GameButton btn = sender as GameButton;

        if (!gameActive) return;

        switch (btn.userInput)
        {
            case UserInput.empty:
                btn.userInput = UserInput.flag;
                _bombCount--;
                btn.BtnImage.Source = Images._bitmapImages["ButtonFlag"];
                break;
            case UserInput.flag:
                btn.userInput = UserInput.qestionMark;
                btn.BtnImage.Source = Images._bitmapImages["ButtonQuestionMark"];
                _bombCount++;
                break;
            case UserInput.qestionMark:
                btn.userInput = UserInput.empty;
                btn.BtnImage.Source = Images._bitmapImages["empty"];
                break;
            case UserInput.clicked:
                break;
            default:
                btn.BtnImage.Source = Images._bitmapImages["empty"];
                break;
        }

        BombCount.Text = _bombCount.ToString();
        CheckIFWin();
    }

    private void ShowRestOfGameBoard()
    {
        gameActive = false;
        foreach (GameButton btn in _buttons)
        {
            if (btn.userInput == UserInput.clicked) continue;
            if (btn.locationType == LocationType.mine & btn.userInput != UserInput.flag) btn.BtnImage.Source = Images._bitmapImages["mine"];
            else if (btn is { locationType: LocationType.empty, userInput: UserInput.flag }) btn.BtnImage.Source = Images._bitmapImages["ButtonMineWrongGuess"];
        }
    }

    public void GenerateBoardNumbers()
    {
        foreach (GameButton btn in _buttons)
        {
            if (btn.locationType == LocationType.mine) continue;

            int numberOfMines = 0;

            int row = btn.Row;
            int col = btn.Col;

            int rows = _buttons.GetLength(0);
            int cols = _buttons.GetLength(1);

            List<(int, int)> checkSpots = FindAllowdSpaces.Find(row, col, rows, cols);

            foreach ((int r, int c) in checkSpots)
            {
                if (_buttons[r, c].locationType == LocationType.mine)
                {
                    numberOfMines++;
                }
            }

            if (numberOfMines > 0)
            {
                LocationType type;
                switch (numberOfMines)
                {
                    case 1:
                        type = LocationType.one;
                        break;
                    case 2:
                        type = LocationType.two;
                        break;
                    case 3:
                        type = LocationType.three;
                        break;
                    case 4:
                        type = LocationType.four;
                        break;
                    case 5:
                        type = LocationType.five;
                        break;
                    case 6:
                        type = LocationType.six;
                        break;
                    case 7:
                        type = LocationType.seven;
                        break;
                    case 8:
                        type = LocationType.eight;
                        break;
                    default:
                        type = LocationType.eight;
                        MessageBox.Show("error, found more then 8 bombs");
                        break;
                }
                btn.locationType = type;
            }
        }
    }

    public void CheckNearbyTiles(GameButton btn)
    {

        List<(int, int)> listOfConnectedTiles = FindConnectedEmptyButtons(btn);

        foreach (var (r, c) in listOfConnectedTiles)
        {
            int row = _buttons[r, c].Row;
            int col = _buttons[r, c].Col;

            int rows = _buttons.GetLength(0);
            int cols = _buttons.GetLength(1);

            List<(int, int)> checkSpots = FindAllowdSpaces.Find(row, col, rows, cols);

            foreach (var (_row, _col) in checkSpots)
            {
                if (_buttons[_row, _col].locationType == LocationType.empty & _buttons[_row, _col].userInput == UserInput.empty) continue;

                if (_buttons[_row, _col].userInput != UserInput.clicked)
                {
                    _buttons[_row, _col].BtnImage.Source = Images._bitmapImages[_buttons[_row, _col].locationType.ToString()];
                    _buttons[_row, _col].userInput = UserInput.clicked;
                }
            }
            _buttons[r, c].BtnImage.Source = Images._bitmapImages["ButtonClicked"];
            _buttons[r, c].userInput = UserInput.clicked;
        }
    }

    public List<(int, int)> FindConnectedEmptyButtons(GameButton btn)
    {
        int startRow = btn.Row;
        int startCol = btn.Col;

        var connectedButtons = new List<(int, int)>();
        var queue = new Queue<(int, int)>();
        var visited = new bool[_buttons.GetLength(0), _buttons.GetLength(1)];

        queue.Enqueue((startRow, startCol));

        while (queue.Count > 0)
        {
            var (row, col) = queue.Dequeue();

            if (row < 0 || col < 0 || row >= _buttons.GetLength(0) || col >= _buttons.GetLength(1))
                continue;

            if (visited[row, col])
                continue;

            if (_buttons[row, col].locationType != LocationType.empty)
                continue;

            visited[row, col] = true;
            connectedButtons.Add((row, col));

            // Check horizontal and vertical neighbors
            var directions = new (int, int)[]
            {
                (0, -1), // left
                (0, 1),  // right
                (-1, 0), // top
                (1, 0)   // bottom
            };

            foreach (var (dr, dc) in directions)
            {
                int newRow = row + dr;
                int newCol = col + dc;
                if (newRow >= 0 && newRow < _buttons.GetLength(0) && newCol >= 0 && newCol < _buttons.GetLength(1) && !visited[newRow, newCol] && _buttons[newRow, newCol].locationType == LocationType.empty)
                {
                    queue.Enqueue((newRow, newCol));
                }
            }
        }

        return connectedButtons;
    }

    private void Restart(object sender, RoutedEventArgs e)
    {
        InitializeGame();
    }
    private void Timer_Tick(object sender, EventArgs e)
    {
        _timeElapsed++;
        Timer.Text = _timeElapsed.ToString();
    }

    private void CheckIFWin()
    {
        foreach (GameButton button in _buttons)
        {
            switch (button.userInput)
            {
                case UserInput.flag:
                    if (button.locationType != LocationType.mine) return;
                    break;
            }

            if ((button.locationType != LocationType.mine) & button.userInput != UserInput.clicked)
            {
                return;
            }
        }

        RenderEndOfGame(true);
    }
}
