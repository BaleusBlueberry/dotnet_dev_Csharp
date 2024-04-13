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
            PrintListBuilding();
        }

        public void PrintListBuilding()
        {

            foreach (BuildingInfoBox buildingInfoBox in ListOfBuildings.Children)
            {
                // Subscribe to the BuildingButtonClicked event
                buildingInfoBox.BuildingButtonClicked += BuildingInfoBox_BuildingButtonClicked;
            }

            // Get the levels array
            JsonElement Cannon = ReadableElement("Cannon");
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

                // Add level number to ComboBox
                DropListSelectLevel.Items.Add($"Level: {properties["level"]}");

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

        private void CreateNewLine(Dictionary<string, string> properties)
        {
            foreach (var child in properties)
            {
                 // Access property.Key and property.Value here
                    // For example:
                    string key = child.Key;
                    string value = child.Value;

                    TextBox textBox = new TextBox();

                    // Set properties for the TextBox if needed
                    textBox.Width = 150;
                    textBox.Height = 30;
                    textBox.Text = $"{key}: {value}";

                    // Add the TextBox to the WrapPanel
                    ListOfInfo.Children.Add(textBox);
                
            }

                

           
        }
        private void BuildingInfoBox_BuildingButtonClicked(object sender, EventArgs e)
        {
            // Handle the event by calling CreateNewLine
            if (sender is BuildingInfoBox buildingInfoBox)
            {
                CreateNewLine(buildingInfoBox.DataContext as Dictionary<string, string>);
            }
        }
    }
}

