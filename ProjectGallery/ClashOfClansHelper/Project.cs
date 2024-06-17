using ClassLibrary;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Media.Imaging;

namespace ClashOfClansHelper
{
    public class Project : IProjectMeta
    {
        public string Name { get; set; } = "Clash Of Clans Helper";

        public string ProjectName { get; set; } = "ClashOfClansHelper.exe";

        public string ProjectDescription { get; set; } = "Clash of Clans Helper App: This application aids players in managing and optimizing their Clash of Clans village.\n" +
                                                         "Users can view, compare, and plan upgrades for various buildings in their village.\n" +

                                                         "Features:\n" +
                                                         "1. Town Hall Level Selection: Choose from different Town Hall levels to view relevant buildings and upgrades.\n" +
                                                         "2. Building Type and Name Selection: Select building types and specific buildings to view detailed information.\n" +
                                                         "3. Gold Pass Toggle: Apply Gold Pass discounts to see reduced costs and times for upgrades.\n" +
                                                         "4. Dynamic Building Information: Display current and previous levels of buildings with visual indicators for upgrades.\n" +
                                                         "5. Responsive UI: Stylish, user-friendly interface with dynamic elements and images for an enhanced user experience.\n\n" +
                                                         "Technical Details:\n" +
                                                         "- Data Integration: Parses JSON files to retrieve building data and displays it dynamically.\n" +
                                                         "- Customization: Processes user selections to provide tailored information.\n" +
                                                         "- Error Handling: Manages file read errors and invalid selections gracefully.\n\n" +
                                                         "Challenges: \n" +
                                                         "-I had to dynamically read data and use it without having a class for every value.\n" +
                                                         "-Figure out i should use await for a function that takes time to process.\n" +
                                                         "-Displaying everything in a dynamic and interactive way was the biggest challenge.\n" +
                                                         "This project showcases data integration, dynamic UI updates, and error handling in a WPF application.";


        public BitmapImage Icon
        {
            get
            {
                string? assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
                Uri uri = new Uri($"pack://application:,,,/{assemblyName};component/Resources/coc.jpg");
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
