using System.Net.Http;
using System.Net.Http.Json;
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
using static System.Net.WebRequestMethods;

namespace UsersCRUDApi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HttpClient client = new HttpClient();
        private List<User> users = new List<User>();
        public MainWindow()
        {
            InitializeComponent();

            client.BaseAddress = new Uri("https://663110d1c92f351c03dc122d.mockapi.io/");

            UsersListBox.ItemsSource = users;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            users = await client.GetFromJsonAsync<List<User>>("Users");
            UsersListBox.ItemsSource = users;
        }

        private async void Button_Click_AddUser(object sender, RoutedEventArgs e)
        {
            User userToAdd = new User
            {
                Name = TBUserName.Text,
                Email = TBUserEmail.Text,

            };
            await client.PostAsJsonAsync("Users", userToAdd);

            Button_Click(sender, e);
        }

        private async void Button_ClickUpdate(object sender, RoutedEventArgs e)
        {
            if (UsersListBox.SelectedItem is User userToUpdate)
            {
                userToUpdate.Name = TBUserName.Text;
                userToUpdate.Email = TBUserEmail.Text;
                userToUpdate.Image = "https://www.timeforkids.com/wp-content/uploads/2018/08/Hero-Volcano.jpg?w=200";

                await client.PutAsJsonAsync("Users/" + userToUpdate.ID, userToUpdate);

                Button_Click(sender, e);
            }
        }

        private async void Button_ClickDelete(object sender, RoutedEventArgs e)
        {
            if (UsersListBox.SelectedItem is User userToUpdate)
            {
                if (MessageBox.Show("are you sure", "delete", MessageBoxButton.YesNo) == MessageBoxResult.No) {
                    return;
                }

                await client.DeleteAsync("Users/" + userToUpdate.ID);

                Button_Click(sender, e);
            }
        }
    }
}