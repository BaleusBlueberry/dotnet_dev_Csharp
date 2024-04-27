using ClashOfClansHelper.Controls;
using ClassLibrary;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Newtonsoft.Json.Linq;
using Path = System.IO.Path;
using System;
using System.Web;
using System.Windows.Data;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection;
using Newtonsoft.Json;
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
            /*PrintListBuilding();*/

            ConvertTownHallJsonToList();

            ListBuilding();

            ImageLoader cocImage = new ImageLoader("clashofclansfont");

            DataContext = cocImage;

        }

        public string[] ListOfBuildingTypes = new string[] {
        "Defensive Buildings", "Buildings", "Traps"
        };


        /*public List<TownHallList> townHallLevels = JsonConvert.DeserializeObject<TownHallList>(TownHallPath);*/

        public List<TownHallLevel> townHallLevels = new List<TownHallLevel>();

        public void ConvertTownHallJsonToList()
        {
            string TownHallPath = @".\Resources\TownHall.json";
            string dataInString = File.ReadAllText(TownHallPath);

            townHallLevels = JsonConvert.DeserializeObject<List<TownHallLevel>>(dataInString);

        }

        public void ListBuilding()
        {
            string dataFolderPath = @".\Resources\Data\";
            // Get all files in the "Data" folder

            string dataFolderPathOfDefensiveBuildings = dataFolderPath + "Defensive Buildings";

            // asembeling the drop list of the ListOfBuildingTypes
            foreach (string buildingType in ListOfBuildingTypes)
            {
                DropListSelectBuildingType.Items.Add($"{buildingType}");
            }

            // asembeling the drop list of the Defensive Buldings
            string[] dataFiles = Directory.GetFiles(dataFolderPathOfDefensiveBuildings);
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

            // asembeling the drop list of the town hall levels
            for (int i = 0; i <= 15; i++)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem
                {
                    Content = $"level {i}", // Set the display content
                    Tag = townHallLevels[i] // Set the tag to the instance of TownHallLevel
                };
                DropListSelectTownhall.Items.Add(comboBoxItem);
            }

        }

        public IDictionary<int, Dictionary<string, string>> dictionaryOfBildings = new Dictionary<int, Dictionary<string, string>>();

        public void PrintListBuilding(string path, string buildingType)
        {

            string dataInString = File.ReadAllText($@".\Resources\Data\{buildingType}\{path}.json");

            dictionaryOfBildings = ConvertJsonToDictionary(dataInString);

            foreach (Dictionary<string, string> building in dictionaryOfBildings.Values)
            {
                try
                {
                    SingleBuilding bulding = new SingleBuilding(building["Level"], building["picture"], building);
                    // Create BuildingInfoBox and set its properties dynamically
                    BuildingInfoBox buildingInfoBox = new BuildingInfoBox(bulding)
                    {
                        Margin = new Thickness(10),
                        Width = 100,
                        Height = 100
                    };
                    // Add BuildingInfoBox to the ListOfBuildings stack panel
                    ListOfBuildings.Children.Add(buildingInfoBox);

                    buildingInfoBox.BuildingButtonClicked += BuildingInfoBox_BuildingButtonClicked;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }


        public static IDictionary<int, Dictionary<string, string>> ConvertJsonToDictionary(string jsonData)
        {
            var jArray = JArray.Parse(jsonData);
            var dictionary = new Dictionary<int, Dictionary<string, string>>();

            foreach (var jToken in jArray)
            {
                var jObject = (JObject)jToken;
                var level = jObject["Level"].Value<int>();
                var properties = jObject.Properties()
                    .ToDictionary(p => p.Name, p => p.Value.ToString());

                dictionary[level] = properties;
            }

            return dictionary;
        }



        private void DropListSelectedBuilding_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ListOfBuildings.Children.Clear();

            PrintBuildings();
            return;

            string selectedItem = DropListSelectBuilding.SelectedItem.ToString();
            string ClearedName = Path.GetFileNameWithoutExtension(selectedItem);
            PrintListBuilding(ClearedName, "Defensive Buildings");
        }

        private void DropListSelectedTownHall_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ListOfBuildings.Children.Clear();

            PrintBuildings();
            return;

            string selectedItem = DropListSelectBuilding.SelectedItem.ToString();
            string ClearedName = Path.GetFileNameWithoutExtension(selectedItem);
            PrintListBuilding(ClearedName, "Defensive Buildings");
        }

        private void PrintBuildings()
        {
            TownHallLevel selectedTownHall;
            string selectedBuildingTypeName;
            string selectedBuildingName;

            // test if the dropdown of townhall is empty
            ComboBoxItem selectedTownHallItem = (ComboBoxItem)DropListSelectTownhall.SelectedItem;

            // test if the dropdown of Buildingtype is empty 
            if (DropListSelectBuildingType.SelectedItem == null || DropListSelectBuilding.SelectedItem == null)
            {
                return;
            }

            if (selectedTownHallItem == null)
            {
                selectedTownHall = townHallLevels[15];
            }
            else
            {
                TownHallLevel tag = selectedTownHallItem.Tag as TownHallLevel;
                selectedTownHall = tag;
            }

            try
            {
                selectedBuildingTypeName = DropListSelectBuildingType.SelectedItem.ToString();
                selectedBuildingName = DropListSelectBuilding.SelectedItem.ToString();

                PrintListBuilding(selectedBuildingName, selectedBuildingTypeName);
                
            } catch(Exception ex)
            {
                MessageBox.Show("error: " + ex.Message);
            }
           
        }

        private Uri stringToUri(string value)
        {
            return new Uri(value);
        }

        public class SingleBuilding
        {
            public string level { get; set; }
            public string picture { get; set; }
            public Dictionary<string, string> bulding { get; set; }

            public SingleBuilding(string level, string picture, Dictionary<string, string> bulding)
            {
                this.level = level;
                this.picture = picture;
                this.bulding = bulding;
            }
        }

        public class DataItem
        {
            public string Column1 { get; set; }
            public string Column2 { get; set; }

        }


        private void BuildingInfoBox_BuildingButtonClicked(object sender, EventArgs e)
        {
            // Get the building dictionary from the BuildingInfoBox instance
            if (sender is BuildingInfoBox buildingInfoBox)
            {
                Dictionary<string, string> building = buildingInfoBox.GetBuilding();
                AddBuildingToGridInfo(building);
            }
        }

        public void AddBuildingToGridInfo(Dictionary<string, string> building)
        {
            buildingGridList.Items.Clear();

            foreach (var i in building)
            {
                if (i.Key == "picture") continue;

                var buildingSingleInfo = new DataItem();
                buildingSingleInfo.Column1 = i.Key.Replace("_", " ");
                buildingSingleInfo.Column2 = i.Value;

                buildingGridList.Items.Add(buildingSingleInfo);
            }

            buildingGridList.Items.Refresh();
        }

        public BitmapImage ClashOfClansImage(string fileName)
        {

            string? assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            Uri uri = new Uri($"pack://application:,,,/{assemblyName};component/Resources/{fileName}.jpg");
            return new BitmapImage(uri);

        }
    }
}

