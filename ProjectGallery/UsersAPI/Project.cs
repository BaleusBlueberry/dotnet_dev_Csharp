using ClassLibrary;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Media.Imaging;

namespace UsersAPI
{
    public class Project : IProjectMeta
    {
        public string Name { get; set; } = "UsersAPI";

        public string ProjectName { get; set; } = "UsersAPI.exe";
        public BitmapImage Icon
        {
            get
            {
                string? assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
                Uri uri = new Uri($"pack://application:,,,/{assemblyName};component/Resources/uapi.png");
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
