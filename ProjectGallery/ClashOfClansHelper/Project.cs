using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using ClashOfClansHelper;

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
