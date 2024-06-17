using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using ClassLibrary;

namespace MineSweeper
{
    public class Project : IProjectMeta
    {
        public string Name { get; set; } = "Mine Sweeper";

        public string ProjectName { get; set; } = "MineSweeper.exe";

        public string ProjectDescription { get; set; } =
            "Minesweeper Game App: A classic Minesweeper game where players uncover tiles to avoid mines and reveal numbers indicating adjacent mines.\n" +
            "Players can choose from Easy, Medium, or Hard difficulty levels, each with different grid sizes and bomb counts.\n\n" +

            "Features:\n" +
            "1. Grid Layout: Navigate through a grid of tiles, avoiding hidden mines and strategically uncovering safe tiles.\n" +
            "2. Flagging System: Mark potential mine locations with flags to aid in navigation.\n" +
            "3. Timer: Track elapsed time to compete for the fastest completion times.\n" +
            "4. End Game Handling: Display game over messages and options to restart or return to difficulty selection.\n" +
            "5. Interactive UI: Intuitive interface with clickable buttons and visual indicators for gameplay actions.\n\n" +

            "Technical Details:\n" +
            "- Game Initialization: Generates random bomb placements based on selected difficulty and ensures initial click does not hit a mine.\n" +
            "- Tile Numbering: Calculates and displays numbers on tiles to indicate nearby mines.\n" +
            "- Recursive Uncovering: Automatically uncovers adjacent empty tiles when an empty tile is clicked.\n" +
            "- Event Handling: Manages mouse events for clicking tiles and right-clicking for flagging mines.\n" +
            "- UI Updates: Updates tile visuals dynamically based on game events and user interactions.\n\n" +

            "Challenges: \n" +
            "- Dynamically saving to data for easy access, ended up storing in an enum as a variable inside each button.\n" +
            "- Figure how to make each button calculate if it should be empty, have a number when next to a mine.\n" +
            "- Making sure the game works as the original game, implementing all the rules and mechanics.\n" +

            "This project showcases fundamental game mechanics, user interface design, and event-driven programming using WPF.\n" +
            "By far the hardest project i have dose so far";


        public BitmapImage Icon
        {
            get
            {
                string? assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
                Uri uri = new Uri($"pack://application:,,,/{assemblyName};component/resources/MineSweeper.png");
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

}
