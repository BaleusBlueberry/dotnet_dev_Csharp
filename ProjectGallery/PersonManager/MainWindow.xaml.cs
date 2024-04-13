using ClassLibrary;
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

namespace PersonManager;

public partial class MainWindow : Window
{
    private const string filePath = "people.json";
    public MainWindow()
    {
        InitializeComponent();

        ThemeHelper.SetTheme(this);

        peopleGrid.ItemsSource = people;

        LoadFile();
    }

    private List<Person> people
    {
        get;
        set;
    } = new List<Person>();

    private void LoadFile()
    {
        if (!File.Exists(filePath))
        {
            return;
        }

        try
        {

            string rawData = File.ReadAllText(filePath);
            List<Person> result = JsonSerializer.Deserialize<List<Person>>(rawData);

            if (result == null)
            {
                return;
            }

            foreach (Person person in result)
            {
                people.Add(person);
            }

        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }

        /*foreach (Person person in people)
        {
            peopleGrid.Items.Add(person);
        }*/
    }
}