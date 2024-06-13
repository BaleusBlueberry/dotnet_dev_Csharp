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
            {"empty", new BitmapImage(new Uri($"{nameSpace}ButtonUnclicked.png"))},
            {"ButtonQuestionMark", new BitmapImage(new Uri($"{nameSpace}ButtonQuestionMark.png"))},
            {"ButtonQuestionMarkClicked", new BitmapImage(new Uri($"{nameSpace}ButtonQuestionMarkClicked.png"))},
            {"mine", new BitmapImage(new Uri($"{nameSpace}ButtonMine.png"))},
            {"ButtonMineWrongGuess", new BitmapImage(new Uri($"{nameSpace}ButtonMineWrongGuess.png"))},
            {"ButtonMineClicked", new BitmapImage(new Uri($"{nameSpace}ButtonMineClicked.png"))},
            {"one", new BitmapImage(new Uri($"{nameSpace}Button1.png"))},
            {"two", new BitmapImage(new Uri($"{nameSpace}Button2.png"))},
            {"three", new BitmapImage(new Uri($"{nameSpace}Button3.png"))},
            {"four", new BitmapImage(new Uri($"{nameSpace}Button4.png"))},
            {"five", new BitmapImage(new Uri($"{nameSpace}Button5.png"))},
            {"six", new BitmapImage(new Uri($"{nameSpace}Button6.png"))},
            {"seven", new BitmapImage(new Uri($"{nameSpace}Button7.png"))},
            {"eight", new BitmapImage(new Uri($"{nameSpace}Button8.png"))},

        };
    }
}
