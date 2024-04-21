using ClassLibrary;
using System.Net.Http;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

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

            string themeColor = ThemeHelper.SetTextColor();

            foreach (CheckBox checkBox in CategoryChoices.Children)
            {
                checkBox.Checked += CategoryCheckBox_Checked;
            }

            CategoryChoiceAny.Checked += CategoryCheckBoxAny_Checked;
        }

        private void CategoryCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            // If any of the CategoryChoices checkboxes are checked, uncheck CategoryChoiceAny
            CategoryChoiceAny.IsChecked = false;
        }

        private void CategoryCheckBoxAny_Checked(object sender, RoutedEventArgs e)
        {
            foreach (CheckBox checkBox in CategoryChoices.Children)
            {
                checkBox.IsChecked = false;
            }

        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            TB_Joke.Text = "Loading joke...";
            try
            {
                string joke = await GetJokeAPI();

                JokeDTO? jokeObj = JsonSerializer.Deserialize<JokeDTO>(joke, new JsonSerializerOptions());

                if (jokeObj.Type == "twopart")
                {
                    TB_Joke.Text = jokeObj.JokeSetup + "\n-----\n" + jokeObj.JokeDelivery;
                }
                else if (jokeObj.Type == "single")
                {
                    TB_Joke.Text = jokeObj.Joke;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show($"failed to get joke: {ex.Message}");
            }

        }

        private async Task<string> GetJokeAPI()
        {
            List<string> choices = GetChoices();

            string choicesFilterd = "";

            if (choices.Count == 1)
            {
                choicesFilterd = choices[0];
            }
            else
            {
                foreach (string choice in choices)
                {
                    choicesFilterd += choice + ",";
                }

                choicesFilterd.Remove(choicesFilterd.Length - 1, 1);
            }

            MessageBox.Show($"https://v2.jokeapi.dev/joke/{choicesFilterd}");

            string response = await client.GetStringAsync($"https://v2.jokeapi.dev/joke/{choicesFilterd}");
            return response;
        }

        private List<string> GetChoices()
        {
            List<string> selectedChoices = new List<string>();


            // If "Any" checkbox is checked, return all categories
            try
            {
                if (CategoryChoiceAny.IsChecked == true)
                {

                    selectedChoices.Add(CategoryChoiceAny.Content.ToString());

                }
                else // If "Any" checkbox is not checked, only return selected checkboxes
                {
                    foreach (CheckBox checkBox in CategoryChoices.Children)
                    {
                        if (checkBox.IsChecked == true)
                        {
                            selectedChoices.Add(checkBox.Content.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("failed to get categories: " + ex.Message);
                selectedChoices.Add("Any");
            }

            return selectedChoices;
        }
    }
}