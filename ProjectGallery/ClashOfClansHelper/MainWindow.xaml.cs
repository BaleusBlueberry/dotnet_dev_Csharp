using ClashOfClansHelper.Controls;
using ClassLibrary;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
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
                try
                {
                    string ClearedName = Path.GetFileNameWithoutExtension(filePath);
                    DropListSelectBuilding.Items.Add($"{ClearedName}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void PrintListBuilding(string path)
        {
            // Get the levels array
            JsonElement BuildingsList = ReadableElement(path);
            JsonElement levels = BuildingsList.GetProperty("levels");

            // Populate ComboBox with level numbers
            foreach (JsonProperty level in levels.EnumerateObject())
            {
                // Extract level data dynamically
                JsonElement levelData = level.Value;

                Dictionary<string, string> buildingInfoList = new Dictionary<string, string>();
                
                foreach (var property in levelData.EnumerateObject())
                {
                    buildingInfoList[property.Name] = GetPropertyValue(property.Value);
                }

                try
                {
                    // Create BuildingInfoBox and set its properties dynamically
                    BuildingInfoBox buildingInfoBox = new BuildingInfoBox(buildingInfoList)
                    {
                        Margin = new Thickness(10),
                        Width = 100,
                        Height = 100
                    };
                    // Add BuildingInfoBox to the ListOfBuildings stack panel
                    ListOfBuildings.Children.Add(buildingInfoBox);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);}
            }
        }

        public Dictionary<string, string> CurrentBuildingDictionary { get; set; } = new Dictionary<string, string>();

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

        private void DropListSelectedBuilding_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ListOfBuildings.Children.Clear();

            string selectedItem = DropListSelectBuilding.SelectedItem.ToString();
            string ClearedName = Path.GetFileNameWithoutExtension(selectedItem);
            PrintListBuilding(ClearedName);
        }
    }
}

