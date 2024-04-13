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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClashOfClansHelper.Controls
{

    /// <summary>
    /// Interaction logic for BuildingInfoBox.xaml
    /// </summary>
    public partial class BuildingInfoBox : UserControl
    {
        public event EventHandler BuildingButtonClicked;

        public BuildingInfoBox(Dictionary<string, string> project)
        {
            InitializeComponent();
            DataContext = project;
        }
    }
}
