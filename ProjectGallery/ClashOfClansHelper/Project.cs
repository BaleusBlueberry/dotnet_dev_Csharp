using ClassLibrary;
using System.Reflection;
using System.Windows.Media.Imaging;

namespace ClashOfClansHelper
{
    public class Project : IProjectMeta
    {
        public string Name { get; set; } = "Clash Of Clans Helper";

        public BitmapImage Icon
        {
            get
            {
                string? assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
                Uri uri = new Uri($"pack://application:,,,/{assemblyName};component/Resources/coc.jpg");
                return new BitmapImage(uri);
            }
        }

        public void Run()
        {
            MainWindow winodw = new MainWindow();
            winodw.ShowDialog();
        }
    }
}
