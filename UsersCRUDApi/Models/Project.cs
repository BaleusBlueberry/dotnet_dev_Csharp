using ClassLibrary;
using System.Reflection;
using System.Windows.Media.Imaging;

namespace UsersCRUDApi;

public class Project : IProjectMeta
{
    public string Name { get; set; } = "Users CRUD Api";

    public string ProjectName { get; set; } = "UsersCRUDApi.exe";
    public BitmapImage Icon
    {
        get
        {
            string? assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            Uri uri = new Uri($"pack://application:,,,/{assemblyName};component/Resources/usercrudapi.png");
            return new BitmapImage(uri);
        }
    }
    public void Run()
    {
        MainWindow window = new MainWindow();
        window.ShowDialog();
    }
}

