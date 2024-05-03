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
using System.Text.Json.Serialization;
using System.Reflection.Emit;

namespace ClashOfClansHelper;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        ThemeHelper.SetTheme(this);

        AsembleTownHallDropList();

        ImageLoader cocImage = new ImageLoader("clashofclanstextv1.png");

        DataContext = cocImage;

    }

    public List<TownHallLevel> townHallLevels = new List<TownHallLevel>();

    public void ConvertTownHallJsonToList()
    {
        string TownHallPath = @".\Resources\TownHall.json";
        string dataInString = File.ReadAllText(TownHallPath);

        townHallLevels = JsonConvert.DeserializeObject<List<TownHallLevel>>(dataInString);

    }

    public void AsembleTownHallDropList()
    {
        string[] ListOfBuildingTypes = new string[] {
        "Defensive Buildings", "Buildings", "Traps"
        };

        ConvertTownHallJsonToList();

        // asembeling the drop list of the ListOfBuildingTypes
        foreach (string buildingType in ListOfBuildingTypes)
        {
            DropListSelectBuildingType.Items.Add($"{buildingType}");
        }

        // asembeling the drop list of the town hall levels
        for (int i = 1; i <= 15; i++)
        {
            DropListSelectTownhall.Items.Add($"level {i}");
        }
    }

    public void AsembleBuldingDropList(string _buldingType)
    {
        DropListSelectBuilding.Items.Clear();

        string dataFolderPath = @".\Resources\Data\";
        // Get all files in the "Data" folder

        string dataFolderPathOfDefensiveBuildings = dataFolderPath + _buldingType;

        // asembeling the drop list of the Defensive Buldings
        string[] dataFiles = Directory.GetFiles(dataFolderPathOfDefensiveBuildings);

        if (DropListSelectTownhall.SelectedItem == null)
        {
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
        else
        {
            foreach (string filePath in dataFiles)
            {
                try
                {
                    TownHallLevel mytown = selectedTownHall;
                    string ClearedName = Path.GetFileNameWithoutExtension(filePath).Replace(" ", "");
                    Type townHallType = typeof(TownHallLevel);

                    // Get the property info for the property corresponding to ClearedName
                    PropertyInfo property = townHallType.GetProperty(ClearedName);

                    // Check if the property exists
                    if (property != null)
                    {
                        int propertyValue = (int)property.GetValue(selectedTownHall);
                        if (propertyValue != 0)
                        {
                            DropListSelectBuilding.Items.Add($"{ClearedName}");
                        }
                    }
                    /*if (selectedTownHall.GetType().GetProperties().Select(p =>) == 0)*/
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
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

                if (int.TryParse(building["Level"], out int buildingLvl))
                {
                    string ClearedName = path.Replace(" ", "");
                    Type townHallType = typeof(TownHallLevel);

                    // Get the property info for the property corresponding to ClearedName
                    PropertyInfo property = townHallType.GetProperty(ClearedName);

                    // Check if the property exists
                    if (property != null)
                    {
                        int propertyValue = (int)property.GetValue(selectedTownHall);
                        if (buildingLvl > propertyValue)
                        {
                            continue;
                        }
                    }
                }
                else
                {
                    // Handle the case where the value of "Level" cannot be parsed as an integer
                    MessageBox.Show("Error: Unable to parse building level as an integer.");
                    continue;
                }
                
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

    private async void DropListBuildingType_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        ListOfBuildings.Children.Clear();

        await RednderselectedTownHallAsync();

        try
        {
            string selection;
            ComboBox comboBox = sender as ComboBox;

            if (comboBox == null) return;

            // Get the selected item from the ComboBox
            object selectedItem = comboBox.SelectedItem;

            // Check if the selected item is not null
            if (selectedItem != null)
            {
                // Assuming the selected item is a string, you can assign it to the selection variable
                selection = selectedItem.ToString();

                // Now you can use the selection variable as needed
            }
            else
            {
                MessageBox.Show("error: selected bulding type is invalid");
                return;
            }

            AsembleBuldingDropList(selection);
            PrintBuildings();
        }
        catch (Exception ex) { MessageBox.Show(ex.Message); }

        return;
    }

    private void DropListSelectedBuilding_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {

        ListOfBuildings.Children.Clear();

        PrintBuildings();
        return;

    }

    private async void DropListSelectedTownHall_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        ListOfBuildings.Children.Clear();

        await RednderselectedTownHallAsync(); // Await the asynchronous method here

        TownHallImage.Source = new BitmapImage(new Uri(selectedTownHall.picture));

        if (DropListSelectBuildingType.SelectedItem != null)
        {
            AsembleBuldingDropList(DropListSelectBuildingType.SelectedItem.ToString());
            PrintBuildings();
        }

        PrintBuildings();
        return;
    }

    public TownHallLevel selectedTownHall = new TownHallLevel();

    private async Task RednderselectedTownHallAsync()
    {
        selectedTownHall = new TownHallLevel();

        if (DropListSelectTownhall.SelectedItem == null)
        {
            selectedTownHall = townHallLevels[15];
        }
        else
        {
            string currentStringOfTownHall = DropListSelectTownhall.SelectedItem.ToString();
            currentStringOfTownHall = currentStringOfTownHall.Replace("level", "").Trim();
            int levelNumber;
            if (int.TryParse(currentStringOfTownHall, out levelNumber))
            {
                selectedTownHall = townHallLevels[levelNumber - 1];
            }
            else
            {
                MessageBox.Show("error: could not convert string to int of the current town hall");
            }
        }
    }

    private void PrintBuildings()
    {

        string selectedBuildingTypeName;
        string selectedBuildingName;

        // test if the dropdown of townhall is empty

        // test if the dropdown of Buildingtype is empty 
        if (DropListSelectBuildingType.SelectedItem == null || DropListSelectBuilding.SelectedItem == null)
        {
            return;
        }

        RednderselectedTownHallAsync();

        try
        {
            selectedBuildingTypeName = DropListSelectBuildingType.SelectedItem.ToString();
            selectedBuildingName = DropListSelectBuilding.SelectedItem.ToString();

            PrintListBuilding(selectedBuildingName, selectedBuildingTypeName);

        }
        catch (Exception ex)
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
        ResetSingleBuilding();

        foreach (var i in building)
        {
            if (i.Key == "picture")
            {
                BuildingInfoImage.Source = new BitmapImage(new Uri(i.Value));
                continue;
            }

            var buildingSingleInfo = new DataItem();

            SingleBuildingSingleLine buldingInfo = new SingleBuildingSingleLine()
            {
                Key = i.Key.Replace("_", " ").Insert(i.Key.Length, ":    "),
                Value = i.Value,
            };

            UsersListBox.Items.Add(buldingInfo);
        }
        UsersListBox.Items.Refresh();
    }

    public BitmapImage ClashOfClansImage(string fileName)
    {

        string? assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
        Uri uri = new Uri($"pack://application:,,,/{assemblyName};component/Resources/{fileName}.jpg");
        return new BitmapImage(uri);

    }

    public void ResetSingleBuilding()
    {
        UsersListBox.Items.Clear();
        BuildingInfoImage.Source = null;
    }
}

public class SingleBuildingSingleLine
{

    public string Key { get; set; }

    public string Value { get; set; }
}
public class SingleBuildingImage
{
    public string Image { get; set; }
}

