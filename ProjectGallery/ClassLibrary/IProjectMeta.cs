using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ClassLibrary
{
    public interface IProjectMeta
    {
        public string Name { get; }

        public BitmapImage Icon
        {
            get
            {
                string? assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
                Uri uri = new Uri($"pack://application:,,,/{assemblyName};component/Resources/Plus.png");
                return new BitmapImage(uri);
            }
        }

        public void Run();
    }
}
