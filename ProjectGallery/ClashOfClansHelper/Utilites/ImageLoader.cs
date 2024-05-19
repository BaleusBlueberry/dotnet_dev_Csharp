using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ClashOfClansHelper
{
    public class ImageLoader
    {
        string imageNameInside;

        public ImageLoader(string imageName) {
            imageNameInside = imageName;
        }

        public BitmapImage Icon
        {

            get
            { 
                string? assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
                Uri uri = new Uri($"pack://application:,,,/{assemblyName};component/Resources/{imageNameInside}");


                return new BitmapImage(uri);
            }
        }

    }
    
}

