using System;
using System.Collections.Generic;
using System.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MineSweeper.Enums;
using MineSweeper.Generation;

namespace MineSweeper.Pages;

public partial class GamePage : Page
{
    private string difficulty;

    public GameButton[,] _buttons = new GameButton[16, 16];

    public bool gameActive = false;

    public bool firstMove = true;

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

                // btn.KeyDown += BoardKeyDown;//?

                btn.PreviewMouseLeftButtonDown += BoardKeyDown;
                btn.PreviewMouseRightButtonDown += GameButton_RightButtonDown;

                _buttons[i, j] = btn;

                MineBoard.Children.Add(btn);
            }
        }

        // Initialize the game based on the difficulty
        // Example:
        // if (difficulty == "Easy") { /* 20 bombs */ }
        // else if (difficulty == "Medium") { /* 40 bombs */ }
        // else if (difficulty == "Hard") { /* 60 bombs */ }
        int amountOfBombs = difficulty == "Easy" ? 20 : difficulty == "Medium" ? 40 : difficulty == "Hard" ? 60 : 0;


        List<(int, int)> generateBombsList = GenerateRandomSpot.GenerateRandomSpots(amountOfBombs);
        foreach ((int row, int col) in generateBombsList)
        {
            _buttons[row, col].locationType = LocationType.mine;
        }
    }

    private void RenderEndOfGame(bool win)
    {
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
                RenderEndOfGame(false);
                mine = true;
                break;

        }
        if (mine) return;
        btn.BtnImage.Source = btn.locationType != LocationType.empty ? Images._bitmapImages[btn.locationType.ToString()] : Images._bitmapImages["ButtonClicked"];
        btn.userInput = UserInput.clicked;
    }

    private void DoFirstMove(GameButton btn)
    {
        // test if the button is in corner

        int row = btn.Row;
        int col = btn.Col;

        int rows = _buttons.GetLength(0);
        int cols = _buttons.GetLength(1);

        List<(int, int)> checkSposts = FindAllowdSpaces.Find(row, col, rows, cols);

        foreach ((int r, int c) in checkSposts)
        {
            if (_buttons[r, c].locationType == LocationType.mine)
            {
                _buttons[r, c].locationType = LocationType.empty;
            }
        }

        GenerateBoardNumbers();

        firstMove = false;

        btn.BtnImage.Source = Images._bitmapImages["ButtonClicked"];
        btn.userInput = UserInput.clicked;
    }

    private void GameButton_RightButtonDown(object sender, MouseButtonEventArgs e)
    {
        GameButton btn = sender as GameButton;

        if (!gameActive) return;

        switch (btn.userInput)
        {
            case UserInput.empty:
                btn.userInput = UserInput.flag;
                btn.BtnImage.Source = Images._bitmapImages["ButtonFlag"];
                break;
            case UserInput.flag:
                btn.userInput = UserInput.qestionMark;
                btn.BtnImage.Source = Images._bitmapImages["ButtonQuestionMark"];
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
        e.Handled = true; // Prevent further handling of the event
    }

    private void ShowRestOfGameBoard()
    {
        gameActive = false;
        foreach (GameButton btn in _buttons)
        {
            if (btn.locationType == LocationType.mine) btn.BtnImage.Source = Images._bitmapImages["mine"];
            else if (btn.locationType == LocationType.empty & btn.userInput == UserInput.flag) btn.BtnImage.Source = Images._bitmapImages["ButtonMineWrongGuess"];
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
}

