using ClashOfClansHelper.Controls;
using ClassLibrary;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace ClashOfClansHelper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ThemeHelper.SetTheme(this);
            /*PrintListBuilding();*/

            ListBuilding();
        }

        public void ListBuilding()
        {
            string dataFolderPath = @".\Resources\Data\";

            // Get all files in the "Data" folder
            string[] dataFiles = Directory.GetFiles(dataFolderPath);

            foreach (string filePath in dataFiles)
            {
                string ClearedName = Path.GetFileNameWithoutExtension(filePath);

                DropListSelectLevel.Items.Add($"{ClearedName}");
            }
        }

        public void PrintListBuilding(string path)
        {
            // Get the levels array
            JsonElement Cannon = ReadableElement(path);
            JsonElement levels = Cannon.GetProperty("levels");
            // Populate ComboBox with level numbers
            foreach (JsonProperty level in levels.EnumerateObject())
            {
                // Extract level data dynamically
                JsonElement levelData = level.Value;

                Dictionary<string, string> properties = new Dictionary<string, string>();

                foreach (var property in levelData.EnumerateObject())
                {
                    properties[property.Name] = GetPropertyValue(property.Value);
                }

                // Create BuildingInfoBox and set its properties dynamically
                BuildingInfoBox buildingInfoBox = new BuildingInfoBox(properties)
                {
                    Margin = new Thickness(10),
                    Width = 100,
                    Height = 100
                };

                // Add BuildingInfoBox to the ListOfBuildings stack panel
                ListOfBuildings.Children.Add(buildingInfoBox);
            }
        }

        public JsonElement ReadableElement(string path)
        {
            string text = File.ReadAllText($@".\Resources\Data\{path}.json");

            // Parse JSON
            JsonDocument doc = JsonDocument.Parse(text);
            JsonElement root = doc.RootElement;

            // Get the levels array

            return root;
        }
        private string GetPropertyValue(JsonElement element)
        {
            switch (element.ValueKind)
            {
                case JsonValueKind.String:
                    return element.GetString();
                case JsonValueKind.Number:
                    // Convert number to string
                    return element.ToString();
                case JsonValueKind.True:
                    return "true";
                case JsonValueKind.False:
                    return "false";
                case JsonValueKind.Null:
                default:
                    return null;
            }
        }

        private void DropListSelectLevel_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ListOfBuildings.Children.Clear();

            string selectedItem = DropListSelectLevel.SelectedItem.ToString();
            string ClearedName = Path.GetFileNameWithoutExtension(selectedItem);
            MessageBox.Show(ClearedName);
            PrintListBuilding(ClearedName);
        }
    }
}

