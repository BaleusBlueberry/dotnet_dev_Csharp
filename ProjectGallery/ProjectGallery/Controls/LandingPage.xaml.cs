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
    }
}
