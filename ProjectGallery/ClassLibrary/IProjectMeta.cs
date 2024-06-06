using System.Diagnostics;
using System.Reflection;
using System.Windows.Media.Imaging;

namespace ClassLibrary;

public interface IProjectMeta
{
    public string Name { get; }

    public string ProjectName { get; } //added

    public BitmapImage Icon
    {
        get
        {
            string? assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            Uri uri = new Uri($"pack://application:,,,/{assemblyName};component/Resources/Plus.png");
            return new BitmapImage(uri);
        }
    }

    /*public void Run();*/

    public void Run()
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
