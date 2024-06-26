﻿using Microsoft.Win32;

namespace ClassLibrary
{
    public class WindowsAppThemes
    {

        public WindowsAppTheme GetWindowsAppTheme()
        {
            using (var key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize"))
            {
                if (key != null)
                {
                    var value = key.GetValue("AppsUseLightTheme");
                    if (value != null)
                    {
                        return (int)value == 0 ? WindowsAppTheme.Dark : WindowsAppTheme.Light;
                    }
                }
            }

            return WindowsAppTheme.Unknown;
        }
    }
}
