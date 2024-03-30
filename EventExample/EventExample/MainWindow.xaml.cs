using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EventExample;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window {
    public MainWindow() {
        InitializeComponent();

        List<Dog> dogs = Dog.GetListFromFile();

        GridOfDogs.ItemsSource = dogs;
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        Dog seletedDog = GridOfDogs.SelectedItem as Dog;

        if (seletedDog == null)
        {
            MessageBox.Show("No dog selected");
            return;
        }

        DogDisplay dogDisplay = new DogDisplay(seletedDog);
        dogDisplay.Show();
    }
}