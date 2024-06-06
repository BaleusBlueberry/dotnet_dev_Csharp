using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;

namespace ClassLibrary
{
    public static class WindowHelper
    {
        // Import the necessary Win32 API functions
        [DllImport("user32.dll")]
        public static extern int ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hwnd, int msg, int wparam, int lparam);

        // Constants for Win32 API functions
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        // Method to enable dragging for a window
        public static void EnableWindowDragging(Window window)
        {
            window.MouseLeftButtonDown += (sender, e) =>
            {
                // Start dragging the window
                ReleaseCapture();
                SendMessage(new System.Windows.Interop.WindowInteropHelper(window).Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            };
        }
    }
}