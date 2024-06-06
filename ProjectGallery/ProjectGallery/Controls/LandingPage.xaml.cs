using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProjectGallery
{
    public partial class LandingPage : Window
    {
        public LandingPage(IProjectMeta project)
        {
            InitializeComponent();
            WindowHelper.EnableWindowDragging(this);
            DataContext = project;

            OpenProject.Click += (sender, e) =>
            {
                this.Hide();
                project.Run();
            };
        }
        private void ReturnIcon_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // This event handler will be called when the window is clicked, and it will trigger the window dragging functionality
        }
    }
}
