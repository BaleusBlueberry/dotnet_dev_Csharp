using ClassLibrary;
using System.Reflection;
using System.Windows.Media.Imaging;

namespace LinqDemoData;

public class Project : IProjectMeta
{
    public string Name { get; set; } = "Joker-App";

    public BitmapImage Icon
    {
        get
        {
            string? assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            Uri uri = new Uri($"pack://application:,,,/{assemblyName};component/Resources/test.png");
            return new BitmapImage(uri);
        }
    }


    public void Run()
    {
        MainWindow window = new MainWindow();
        window.ShowDialog();
    }
}

