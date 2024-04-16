using System.Net.Http;
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

namespace JokeApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HttpClient client = new HttpClient();
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            TB_Joke.Text = "Loading joke...";
            try
            {
                await GetJokeAPI();
                //TB_Joke.Text = joke;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"failed to get joke: {ex.Message}");
            }
            

        }

        private async Task GetJokeAPI()
        {
            
        }
    }
}