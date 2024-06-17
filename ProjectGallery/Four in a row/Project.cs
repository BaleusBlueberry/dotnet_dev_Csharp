using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Four_in_a_row;

public class Project : IProjectMeta
{
    public string Name { get; set; } = "Four in a row";

    public string ProjectName { get; set; } = "Four in a row.exe";
    public string ProjectDescription { get; set; } = "Four in a Row Game App: This application allows players to enjoy the classic Four in a Row game with various gameplay modes.\n" +
                                                     "Players can challenge each other or play against the computer in different difficulty levels.\n" +

                                                     "Features:\n" +
                                                     "1. Game Modes: Choose from PvP (Player vs Player), PvC (Player vs Computer), or CvC (Computer vs Computer) modes.\n" +
                                                     "2. Interactive Grid: Click on arrows above the board to drop tokens and strategically connect four tokens in a row to win.\n" +
                                                     "3. Turn-Based Gameplay: Alternate turns between players or watch as computer players battle it out.\n" +
                                                     "4. Dynamic Scoring: Keep track of wins for both players and recognize draws.\n" +
                                                     "5. Responsive UI: Modern interface with customizable game settings and intuitive controls for seamless gameplay.\n" +
                                                     "5. Re-using Elements and logic from other projects such as tic-tac-toe to save time.\n\n" +
                                                     "Technical Details:\n" +
                                                     "- Event Handling: Implements event-driven programming to manage player moves and game outcomes.\n" +
                                                     "- AI Implementation: Utilizes randomized moves and strategic algorithms for computer opponents.\n" +
                                                     "- Error Handling: Ensures smooth gameplay experience by handling user input and game state transitions.\n\n" +
                                                     "Challenges: \n" +
                                                     "-Testing if anyone has won the game was very challenging that required long sessions of debugging and imprisonments.\n" +
                                                     "-Testing if the move or check  i want to dynamically make is in bound and will not crash the game.\n" +
                                                     "This project demonstrates interactive game development, AI integration, and user interface design using WPF controls.";
    public BitmapImage Icon
    {
        get
        {
            string? assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            Uri uri = new Uri($"pack://application:,,,/{assemblyName};component/resources/4InaRow.png");
            return new BitmapImage(uri);
        }
    }
    private void Run()
    {
        Process appProcess = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = ProjectName,
                UseShellExecute = true
            }
        };
        appProcess.Start();
        appProcess.WaitForExit();
    }
}

