using ClassLibrary;
using System.Reflection;
using System.Windows.Media.Imaging;

namespace Tic_Tac_Toe
{
    public class Project : IProjectMeta
    {
        public string Name { get; set; } = "Tic-Tac-Toe";

        public BitmapImage Icon
        {
            get
            {
                string? assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
                Uri uri = new Uri($"pack://application:,,,/{assemblyName};component/resources/tic.png");
                return new BitmapImage(uri);
            }
        }
        public void Run()
        {
            MainWindow window = new MainWindow();
            window.ShowDialog();
        }
    }
}
