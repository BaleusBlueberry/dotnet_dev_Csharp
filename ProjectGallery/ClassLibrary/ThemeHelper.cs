using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClassLibrary;

public static class ThemeHelper
{
    public static void SetTheme(Window window)
    {
        var windowsAppThemes = new ClassLibrary.WindowsAppThemes();
        var appTheme = windowsAppThemes.GetWindowsAppTheme();

        if (appTheme == WindowsAppTheme.Light)
        {
            Syncfusion.SfSkinManager.SfSkinManager.SetVisualStyle(window, Syncfusion.SfSkinManager.VisualStyles.Windows11Light);
        }
        else if (appTheme == WindowsAppTheme.Dark)
        {
            Syncfusion.SfSkinManager.SfSkinManager.SetVisualStyle(window, Syncfusion.SfSkinManager.VisualStyles.Windows11Dark);
        }
    }
}
