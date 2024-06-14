using ClassLibrary;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Media.Imaging;

namespace Tic_Tac_Toe;

    public class Project : IProjectMeta
    {
        public string Name { get; set; } = "Tic-Tac-Toe";

        public string ProjectName { get; set; } = "Tic_Tac_Toe.exe";

        public string ProjectDescription { get; set; } =
            "Tic-Tac-Toe Game App: Play the classic Tic-Tac-Toe game in various modes against another player or computer opponents.\n" +
            "Enjoy strategic gameplay where players take turns marking X or O on a 3x3 grid to achieve a winning pattern.\n\n" +

            "Features:\n" +
            "1. Game Modes: Choose from PvP (Player vs Player), PvC (Player vs Computer), or CvC (Computer vs Computer) modes.\n" +
            "2. Interactive Grid: Click on squares to place X or O and strategically align three marks in a row to win.\n" +
            "3. Turn-Based Gameplay: Alternate turns between players or watch as computer players make their moves.\n" +
            "4. Score Tracking: Keep track of wins for both players and recognize draws.\n" +
            "5. Responsive UI: Modern interface with customizable game settings and intuitive controls for seamless gameplay.\n\n" +

            "Technical Details:\n" +
            "- Event Handling: Implements event-driven programming to manage player moves and game outcomes.\n" +
            "- AI Implementation: Utilizes strategic algorithms for computer opponents in PvC and CvC modes.\n" +
            "- Game Logic: Validates winning conditions and prevents invalid moves during gameplay.\n" +
            "- UI Design: Responsive layout with animations and feedback for player interactions.\n\n" +

            "This project demonstrates interactive game development, AI integration, and user interface design using WPF controls.";


    public BitmapImage Icon
        {
            get
            {
                string? assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
                Uri uri = new Uri($"pack://application:,,,/{assemblyName};component/resources/tic.png");
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

