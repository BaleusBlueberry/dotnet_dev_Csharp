using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LinqDemoData;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public List<Product> rawListOfProducts;

    public MainWindow()
    {
        InitializeComponent();

        LoadProducts();

        AddButtons("Get All", GetAllMethod, GetAllSyntax);
        AddButtons("Get All Names", GetAllNamesMethod, GetAllNamesSyntax);
        AddButtons("Get All Objects", GetAllObjMethod, GetAllObjMethod);
        AddButtons("Get All New Objects", GetAllNewObjMethod, GetAllNewObjMethod);
        AddButtons("Orderr By", OrderByMethod, OrderBySyntax);
        AddButtons("Orderr By twice", OrderByMethodTwice, OrderByMethodTwice);
    }

    private void GetAllMethod(object sender, RoutedEventArgs e)
    {
        IEnumerable<Product> result =
            rawListOfProducts.Select(product => product);

        ResultsDataGrid.ItemsSource = result;
    }

    private void GetAllNamesMethod(object sender, RoutedEventArgs e)
    {
        IEnumerable<string> result =
            rawListOfProducts.Select(product => product.Name);

        List<string> listOfNames = result.ToList();

        ResultsDataGrid.ItemsSource = result;
    }

    private void GetAllObjMethod(object sender, RoutedEventArgs e)
    {
        Random rnd = new Random();

        IEnumerable<Product> result =
            rawListOfProducts.Select(product => new Product
            {
                Name = product.Name + " ProductObj",
                Id = rnd.Next()
            });


        ResultsDataGrid.ItemsSource = result;
    }
    private void GetAllNewObjMethod(object sender, RoutedEventArgs e)
    {
        Random rnd = new Random();

        var result =
            rawListOfProducts.Select(product => new
            {
                Name = product.Name + " new object",
                Id = rnd.Next()
            });


        ResultsDataGrid.ItemsSource = result;
    }


    private void OrderByMethod(object sender, RoutedEventArgs e)
    {
        IEnumerable<Product> result =
            rawListOfProducts.OrderBy(product => product.Price);
            /*rawListOfProducts.OrderByDescending(product => product.Id);*/


        ResultsDataGrid.ItemsSource = result;
    }

    private void OrderByMethodTwice(object sender, RoutedEventArgs e)
    {
        IEnumerable<Product> result =
            rawListOfProducts.OrderBy(product => product.CategoryId).ThenBy(product => product.Price);
        /*rawListOfProducts.OrderByDescending(product => product.Id);*/


        ResultsDataGrid.ItemsSource = result;
    }

    private void GetAllSyntax(object sender, RoutedEventArgs e)
    {
        IEnumerable<Product> result =
            from product in rawListOfProducts
            select product;

        ResultsDataGrid.ItemsSource = result;
    }

    private void GetAllNamesSyntax(object sender, RoutedEventArgs e)
    {
        IEnumerable<string> result =
            from product in rawListOfProducts
            select product.Name;

        List<string> listOfNames = result.ToList();

        ResultsDataGrid.ItemsSource = result;
    }

    private void OrderBySyntax(object sender, RoutedEventArgs e)
    {
        IEnumerable<Product> result =
            from product in rawListOfProducts
            orderby product.Id descending
            select product;

        ResultsDataGrid.ItemsSource = result;
    }

    private void AddButtons(string name,
        RoutedEventHandler clickMethod,
        RoutedEventHandler clickSyntax)
    {

        StackPanel stackPanel = new StackPanel()
        {
            Orientation = Orientation.Horizontal
        };
        ButtonStack.Children.Add(stackPanel);

        Button btnMethod = new Button
        {
            Margin = new Thickness(5),
            Padding = new Thickness(5),
            FontSize = 15,
            Content = name + " (M)"
        };
        btnMethod.Click += clickMethod;

        stackPanel.Children.Add(btnMethod);

        Button btnSyntax = new Button
        {
            Margin = new Thickness(5),
            Padding = new Thickness(5),
            FontSize = 15,
            Content = name + " (S)"
        };
        btnSyntax.Click += clickSyntax;

        stackPanel.Children.Add(btnSyntax);
    }

    private void LoadProducts()
    {
        string rawJson = File.ReadAllText("Products.json");
        rawListOfProducts = JsonSerializer.Deserialize<List<Product>>(rawJson);
        //ResultsDataGrid.ItemsSource = rawListOfProducts;
    }
}
