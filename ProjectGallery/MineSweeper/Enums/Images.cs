using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MineSweeper.Enums
{
    public class Images
    {
        private static string nameSpace = "pack://application:,,,/resources/";

        public static Dictionary<string, BitmapImage> _bitmapImages = new Dictionary<string, BitmapImage>()
        {
            {"ButtonClicked", new BitmapImage(new Uri($"{nameSpace}ButtonClicked.png"))},
            {"ButtonFlag", new BitmapImage(new Uri($"{nameSpace}ButtonFlag.png"))},
            {"ButtonUnclicked", new BitmapImage(new Uri($"{nameSpace}ButtonUnclicked.png"))},
            {"ButtonQuestionMark", new BitmapImage(new Uri($"{nameSpace}ButtonQuestionMark.png"))},
            {"ButtonQuestionMarkClicked", new BitmapImage(new Uri($"{nameSpace}ButtonQuestionMarkClicked.png"))},
            {"one", new BitmapImage(new Uri($"{nameSpace}Button1.png"))},
        };
    }
}
