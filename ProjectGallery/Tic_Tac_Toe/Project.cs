using ClassLibrary;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Media.Imaging;

namespace Tic_Tac_Toe;

    public class Project : IProjectMeta
    {
        public string Name { get; set; } = "Tic-Tac-Toe";

        public string ProjectName { get; set; } = "Tic_Tac_Toe.exe";

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

