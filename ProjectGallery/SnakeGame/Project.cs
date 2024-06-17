using ClassLibrary;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Media.Imaging;

namespace SnakeGame;

public class Project : IProjectMeta
{
    public string Name { get; set; } = "Snake Game";

    public string ProjectName { get; set; } = "SnakeGame.exe";

    public string ProjectDescription { get; set; } = "Snake Game: This application recreates the classic Snake game where players control a growing snake.\n" +
                                                     "Players maneuver the snake to eat apples while avoiding collisions with walls and its own body.\n" +

                                                     "Features:\n" +
                                                     "1. Snake Movement: Control the snake's direction using arrow keys or on-screen controls.\n" +
                                                     "2. Apple Placement: Randomly place apples within the game area for the snake to eat.\n" +
                                                     "3. Score Tracking: Count the number of apples eaten as the score.\n" +
                                                     "4. Difficulty Levels: Choose from Easy, Medium, and Hard, adjusting speed and challenge.\n" +
                                                     "5. Game Over Handling: Display score upon game over and offer options to restart or exit.\n\n" +
                                                     "Technical Details:\n" +
                                                     "- Canvas Rendering: Utilize WPF Canvas to render game elements dynamically.\n" +
                                                     "- Game Logic: Implement snake movement, collision detection, and apple placement algorithms.\n" +
                                                     "- User Input Handling: Manage key events to control snake movement and game interactions.\n\n" +
                                                     "This project demonstrates fundamental game development concepts in a WPF environment, including " +
                                                     "game loop synchronization, user input handling, and basic graphical interface implementation.";

    public BitmapImage Icon
    {
        get
        {
            string? assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            Uri uri = new Uri($"pack://application:,,,/{assemblyName};component/Resources/snake.png");
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

