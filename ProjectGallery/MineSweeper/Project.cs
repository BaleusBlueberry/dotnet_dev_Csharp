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
