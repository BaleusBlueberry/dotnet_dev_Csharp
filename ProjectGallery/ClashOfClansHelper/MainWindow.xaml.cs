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
using System.Net.NetworkInformation;
using System.Linq;
using System.Text.RegularExpressions;
using System.Diagnostics.Eventing.Reader;
using System.Windows.Input;
using System.Windows.Media;

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

        ImageLoader cocGreenArrow = new ImageLoader("greenarrow.png");

        DataContext = cocImage;

    }
    public TownHallLevel selectedTownHall = new TownHallLevel();

    public List<TownHallLevel> townHallLevels = new List<TownHallLevel>();

    public IDictionary<int, Dictionary<string, string>> dictionaryOfBildings = new Dictionary<int, Dictionary<string, string>>();

    public bool goldPass = false;

    // made this into a method to make it easy to read
    private void toggleGoldPass()
    {
        goldPass = !goldPass;
    }

    public void ConvertTownHallJsonToList()
    //takes the data of the townhalls from a folder and convert it into a list
    {
        string TownHallPath = @".\Resources\TownHall.json";
        string dataInString = File.ReadAllText(TownHallPath);

        townHallLevels = JsonConvert.DeserializeObject<List<TownHallLevel>>(dataInString);

    }

    public void AsembleTownHallDropList()
    // asembles the townhall droplist and the droplist of the buldings types
    {
        string[] ListOfBuildingTypes = new string[] {
        "Defensive Buildings", "Army Buildings", "Traps", "Resource Buildings",
        };

        ConvertTownHallJsonToList();

        // asembeling the drop list of the ListOfBuildingTypes
        foreach (string buildingType in ListOfBuildingTypes)
        {
            DropListSelectBuildingType.Items.Add($"{buildingType}");
        }

        // asembeling the drop list of the town hall levels
        for (int i = 1; i <= 16; i++)
        {
            DropListSelectTownhall.Items.Add($"level {i}");
        }
    }

    public void AsembleBuldingDropList(string _buldingType)
    // asembles the droplist of DropListSelectBuilding with the buldings names
    {
        DropListSelectBuilding.Items.Clear();

        string dataFolderPath = @".\Resources\Data\";
        // Get all files in the "Data" folder

        // gets the folder of the current buidling type
        string dataFolderPathOfDefensiveBuildings = dataFolderPath + _buldingType;

        // asembeling the drop list of the Defensive Buldings
        string[] dataFiles = Directory.GetFiles(dataFolderPathOfDefensiveBuildings);

        //if user didn't select a town hall at DropListSelectTownhall
        if (DropListSelectTownhall.SelectedItem == null)
        {
            // displays all buildings
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
            // display only buildings that exist in the currently selected DropListSelectTownhall
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
                        // convers the property of the selected town hall to int dynamically acording to the current buliding name
                        int propertyValue = (int)property.GetValue(selectedTownHall);
                        if (propertyValue != 0)
                        {
                            DropListSelectBuilding.Items.Add($"{ClearedName}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

    }

    public void PrintListBuilding(string path, string buildingType)
    // displays all the buildings in the current path inside buildingType
    {
        // finds the location of the item
        string dataInString = File.ReadAllText($@".\Resources\Data\{buildingType}\{path}.json");

        dictionaryOfBildings = ConvertJsonToDictionary(dataInString);

        // loops over each bulding inside dictionaryOfBildings 
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

                        // tests if the current bulding level is lower then the level of that bulding inside the current selected level of TownHallLevel
                        // to skip over any building that has a higher level then the allowed level of the currently selected TownHallLevel
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
                // creates a new SingleBuilding with its level and picture data
                SingleBuilding bulding = new SingleBuilding(building["Level"], building["picture"], building);

                // Create BuildingInfoBox and set its properties dynamically from the bulding
                BuildingInfoBox buildingInfoBox = new BuildingInfoBox(bulding)
                {
                    Margin = new Thickness(10),
                    Width = 100,
                    Height = 100
                };

                // Add BuildingInfoBox to the ListOfBuildings stack panel
                ListOfBuildings.Children.Add(buildingInfoBox);

                // Adds the event listener in order to make BuildingInfoBox_BuildingButtonClicked work for each element
                buildingInfoBox.BuildingButtonClicked += BuildingInfoBox_BuildingButtonClicked;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }


    public static IDictionary<int, Dictionary<string, string>> ConvertJsonToDictionary(string jsonData)
    //takes in a json and convers it into a Dictionary<string, string> dynamically
    // in order to find each entery of the dictionary with a level
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
    // when clicking an item of the droplist of buildingtype
    // test and assembles the list of buildings in the dropbox DropListSelectBuilding according to the current townhall and what types of buildings
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
                // Assuming the selected item is a string sets it ass a string
                selection = selectedItem.ToString();

            }
            else
            {
                MessageBox.Show("error: selected bulding type is invalid");
                return;
            }

            // puts the name of the building in the AsembleBuldingDropList
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
    // when user selects a town hall from the selectedTownHall
    {
        ListOfBuildings.Children.Clear();

        await RednderselectedTownHallAsync(); // Await the asynchronous method here to load the current townhall data

        TownHallImage.Source = new BitmapImage(new Uri(selectedTownHall.picture));

        if (DropListSelectBuildingType.SelectedItem != null)
        {
            AsembleBuldingDropList(DropListSelectBuildingType.SelectedItem.ToString());
            PrintBuildings();
        }

        PrintBuildings();
        return;
    }

    private async Task RednderselectedTownHallAsync()
    // loads the current town hall data 
    {
        selectedTownHall = new TownHallLevel();

        // if the user didn't select a town hall, set it to max (16 level)
        if (DropListSelectTownhall.SelectedItem == null)
        {
            selectedTownHall = townHallLevels[15];
        }
        else
        {
            // translate all data of the currently selected DropListSelectTownhall to string and clean it
            string currentStringOfTownHall = DropListSelectTownhall.SelectedItem.ToString();
            currentStringOfTownHall = currentStringOfTownHall.Replace("level", "").Trim();
            int levelNumber;

            // convert it to int
            if (int.TryParse(currentStringOfTownHall, out levelNumber))
            {
                // set the current selectedTownHall to what the user selected
                selectedTownHall = townHallLevels[levelNumber - 1];
            }
            else
            {
                MessageBox.Show("error: could not convert string to int of the current town hall");
            }
        }
    }

    private void PrintBuildings()
    //tests before asembling the the DropListSelectBuilding
    {

        string selectedBuildingTypeName;
        string selectedBuildingName;

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

            // print the DropListSelectBuilding
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

    private void BuildingInfoBox_BuildingButtonClicked(object sender, EventArgs e)
    {
        // Get the building dictionary from the BuildingInfoBox instance
        if (sender is BuildingInfoBox buildingInfoBox)
        {
            Dictionary<string, string> building = buildingInfoBox.GetBuilding();
            AddBuildingToGridInfo(building);
        }
    }

    private void AddBuildingToGridInfo(Dictionary<string, string> building)
    // this function takes in a single dictionarry or a building
    // and displays all the information in the UsersListBox
    {
        bool isFirstLevel = true;

        Dictionary<string, string> PreviusBuilding = null;

        ResetSingleBuilding();

        UnloadPreviusBuldingImage();

        List<string> goldPassDiscountsListNames = new List<string>()
        {
            "gold", "elixir", "darkelixer", "build",
        };

        List<string> skippbleValues = new List<string>()
        {
            "gained", "required", "unlock"
        };

        // gose over each dictionarry entery of a building in order to find the level and save it
        foreach (var i in building)
        {
            // if the entery is a level
            if (i.Key == "Level")
            {
                int currentLevel = int.Parse(i.Value);

                //if the level is grear then 1
                if (currentLevel > 1)
                {
                    //loads an image of the building thats one level below the current bulding level
                    LoadPreviusBuildingImage(currentLevel);
                    isFirstLevel = false;
                    // loads in the PreviusBuilding data
                    PreviusBuilding = dictionaryOfBildings[currentLevel - 1];
                }
            }
        }

        // gose over each dictionarry entery of a building in order to display the data
        foreach (var i in building)
        {
            // logic to find the picture entry and display it in the BuildingInfoImage
            if (i.Key == "picture")
            {
                BuildingInfoImage.Source = new BitmapImage(new Uri(i.Value));
                continue;
            }
            string buildingKey = i.Key.Replace("_", " ");
            string buildingValue = i.Value;

            // builds the current line that would be displayed it UsersListBox
            SingleBuildingSingleLine buildingInfo = new SingleBuildingSingleLine()
            {
                Key = buildingKey.Insert(buildingKey.Length, ":    "),
            };

            //test if the current building entery contains a gold pass discountble item
            if (SearchForMatchingString(goldPassDiscountsListNames, buildingKey))
            {
                buildingInfo.Value = buildingValue;

                // test if the user clicked on goladpass
                if (goldPass)
                {
                    //IsStrikethrough SecondValue ShowArrow
                    buildingInfo.ShowArrow = true;
                    buildingInfo.IsStrikethrough = true;
                    buildingInfo.SecondValue = DisplayCorrectValueAfterGoldPass(buildingKey, buildingValue);
                }

                UsersListBox.Items.Add(buildingInfo);
                continue;
            }

            // check for spasific names to skip over
            if (SearchForMatchingString(skippbleValues, buildingKey))
            {
                buildingInfo.Value = buildingValue;
                UsersListBox.Items.Add(buildingInfo);
                continue;
            }

            if (isFirstLevel)
            {
                buildingInfo.Value = buildingValue;
            }

            // loads the previus buildings info
            if (PreviusBuilding != null)
            {

                buildingInfo.Value = PreviusBuilding.FirstOrDefault(x => x.Key == i.Key).Value;
                buildingInfo.SecondValue = buildingValue;
                buildingInfo.ShowArrow = true;
            }

            UsersListBox.Items.Add(buildingInfo);
        }
        UsersListBox.Items.Refresh();
    }

    private void LoadPreviusBuildingImage(int currentBuildingLevel)
    // loads the previus bulding at PreviusBuildingInfoImage
    {
        BuildingUpgradeArrow.Visibility = Visibility.Visible;

        // loads the previus building 
        Dictionary<string, string> PreviusBuilding = dictionaryOfBildings[currentBuildingLevel - 1];

        // find the key picture inside the dictionarry in order to save it into the source of PreviusBuildingInfoImage
        foreach (var i in PreviusBuilding)
        {
            if (i.Key == "picture")
            {
                PreviusBuildingInfoImage.Source = new BitmapImage(new Uri(i.Value));
            }
        }
    }
    private void UnloadPreviusBuldingImage()
    //unload the PreviusBuildingInfoImage and hide BuildingUpgradeArrow in the list of building info
    {
        PreviusBuildingInfoImage.Source = null;
        BuildingUpgradeArrow.Visibility = Visibility.Hidden;
    }
    private bool SearchForMatchingString(List<string> values, string key)
    // searches for a maching string in key inside the list
    {

        string cleanKey = key.ToLower().Replace(" ", "");

        bool keyContainsName = false;

        //find if any goldpass value is inside the key string
        foreach (string i in values)
        {
            if (cleanKey.Contains(i)) keyContainsName = true;
        }
        return keyContainsName;
    }

    private string DisplayCorrectValueAfterGoldPass(string key, string value)
    // logic to calculate the value acording to the type of the key
    {
        List<string> goldPassDiscountsListResurces = new List<string>()
        {
            "gold", "elixir", "darkelixer",
        };
        string goldPassDiscoutBuilding = "build";

        string cleanKey = key.ToLower().Replace(" ", "");

        // finds if the key contains any of the gold pass resurce words
        if (goldPassDiscountsListResurces.Any(resource => cleanKey.IndexOf(resource) != -1))
        {
            // takes out anythings thats not a number
            string cleanValue = Regex.Replace(value, "[^0-9]", "");

            // returns the discounted resurce value of the inputed value
            if (double.TryParse(cleanValue, out double valueAsInt))
            {
                double result = valueAsInt * 0.8;
                string output = result.ToString();
                return output;
            }
            // finds if the key has a build in it to assemble the discounted time
        }
        else if (cleanKey.Contains(goldPassDiscoutBuilding))
        {

            int hours = 0;

            string[] valueData = value.Split(' ');

            // test each value of the list and convert hours and days into hours
            foreach (string part in valueData)
            {

                string trimmedPart = part.Trim().ToLower();
                int currentPartInt = StringToNumber(trimmedPart);

                //finds the days
                if (trimmedPart.EndsWith("h"))
                {

                    hours += currentPartInt;
                }

                if (trimmedPart.EndsWith("d"))
                {

                    hours += currentPartInt * 24;
                }

            }

            double discountedHours = Math.Floor(hours * 0.8);

            if (discountedHours < 24)
            {
                return $"{discountedHours}h";

            }
            else if (discountedHours >= 24)
            {

                double days = Math.Floor(discountedHours / 24);

                double finalHours = discountedHours - days * 24;

                string finalString = $"{days}d " + (finalHours == 0 ? "" : $"{finalHours}h");

                return finalString;


            }
        }

        return value;
    }

    private int StringToNumber(string stringValue)
    {

        string cleanValue = Regex.Replace(stringValue, "[^0-9]", "");
        if (int.TryParse(cleanValue, out int valueAsInt))
        {
            return valueAsInt;
        }
        else
        {
            MessageBox.Show("there was an error converting a string to a number: returns 0");
            return 0;
        }
    }

    private BitmapImage ClashOfClansImage(string fileName)
    //loads an image of the filename 
    {

        string? assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
        Uri uri = new Uri($"pack://application:,,,/{assemblyName};component/Resources/{fileName}.jpg");
        return new BitmapImage(uri);

    }

    private void ResetSingleBuilding()
    //resets the UsersListBox and erases BuildingInfoImage
    {
        UsersListBox.Items.Clear();
        BuildingInfoImage.Source = null;
    }

    private void GoldPassToggle_Click(object sender, RoutedEventArgs e)
    {
        // Toggle the goldPass variable or perform any other logic here
        toggleGoldPass();

        // Update the background color of the GoldPassToggle button based on goldPass variable
        GoldPassToggle.Background = goldPass ? Brushes.Yellow : Brushes.White;

        var hoverStyle = new Style(typeof(Button));
        hoverStyle.Setters.Add(new Setter(Button.BackgroundProperty, goldPass ? Brushes.Gold : Brushes.SkyBlue));
        hoverStyle.Setters.Add(new Setter(Button.BorderBrushProperty, goldPass ? Brushes.DarkGoldenrod : Brushes.DodgerBlue));
        GoldPassToggle.Style = hoverStyle;
    }
}
