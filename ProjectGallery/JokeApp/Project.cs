using ClassLibrary;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Media.Imaging;

namespace JokeApp;

public class Project : IProjectMeta
{
    public string Name { get; set; } = "Joker-App";

    public string ProjectName { get; set; } = "JokeApp.exe";

    public string ProjectDescription { get; set; } = "Joke Button App: This application uses the JokeAPI to fetch and display various jokes.\n" +
                                                     "Users can customize joke preferences, including categories and blacklist flags.\n" +

                                                     "Features:\n" +
                                                     "1. Fetch Jokes: Click the 'Tell me a 𝓙𝓸𝓴𝓮!' button to retrieve a joke from the API.\n" +
                                                     "2. Category Selection: Choose from categories like Programming, Miscellaneous Christmas.\n" +
                                                     "3. Blacklist Flags: Exclude jokes with flags like nsfw, religious, political, racist, sexist, and explicit.\n" +
                                                     "4. Search: Find jokes containing specific keywords.\n" +
                                                     "5. Dynamic UI: Stylish buttons, checkboxes, and text elements for a smooth user experience.\n\n" +
                                                     "Technical Details:\n" +
                                                     "- API Integration: Asynchronous HTTP requests for responsive interactions.\n" +
                                                     "- Customization: Process user inputs to form API requests.\n" +
                                                     "- Error Handling: Manages API request failures and errors.\n\n" +
                                                     "This project showcases API integration, dynamic UI updates, and error handling in a WPF application.";



    public BitmapImage Icon
    {
        get
        {
            string? assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            Uri uri = new Uri($"pack://application:,,,/{assemblyName};component/Resources/cloun.png");
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

