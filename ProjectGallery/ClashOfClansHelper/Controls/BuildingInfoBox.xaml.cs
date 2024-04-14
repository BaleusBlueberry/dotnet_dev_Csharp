using System.Windows.Controls;

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
