using System.Web;
using System.Windows;
using Newtonsoft.Json.Linq;
using System.Windows.Controls;
using static ClashOfClansHelper.MainWindow;
using System.Windows.Media.Imaging;
using System.Windows.Data;

namespace ClashOfClansHelper.Controls
{

    /// <summary>
    /// Interaction logic for BuildingInfoBox.xaml
    /// </summary>
    public partial class BuildingInfoBox : UserControl
    {
        private Dictionary<string, string> _building;

        public event EventHandler BuildingButtonClicked;

        public BuildingInfoBox(SingleBuilding project)
        {
            DataContext = project;
            _building = project.bulding;
            InitializeComponent();
        }


        private void BuildingButton_OnClick(object sender, RoutedEventArgs e)
        {
            BuildingButtonClicked?.Invoke(this, EventArgs.Empty);
        }

        public Dictionary<string, string> GetBuilding()
        {
            return _building;
        }
    }
}
