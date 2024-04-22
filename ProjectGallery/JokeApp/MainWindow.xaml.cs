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

            string finalString = GetFinalSubmitString();

            MessageBox.Show($"https://v2.jokeapi.dev/joke/{finalString}");

            string response = await client.GetStringAsync($"https://v2.jokeapi.dev/joke/{finalString}");
            return response;
        }

        private string GetChoices()
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

            return FormatListToString(selectedChoices);
        }

        private string GetBlacklists()
        {
            List<string> blacklistsChoices = new List<string>();

            string finalString = "";
            // If "Any" checkbox is checked, return all categories
            try
            {
                if (IsAnyCheckboxChecked())
                {
                    foreach (CheckBox checkBox in BlacklistChoices.Children)
                    {
                        if (checkBox.IsChecked == true)
                        {
                            blacklistsChoices.Add(checkBox.Content.ToString());
                        }
                    }
                    string formatedBlackList = FormatListToString(blacklistsChoices);

                    finalString = string.Concat("?blacklistFlags=", formatedBlackList);
                    MessageBox.Show(finalString);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("failed to get categories: " + ex.Message);
            }

            return finalString;
        }

        private string GetFinalSubmitString()
        {
            string choices = GetChoices();

            string Blacklists = GetBlacklists();



            string choicesFilterd = string.Concat(choices, Blacklists);

            return choicesFilterd;
        }

        private string FormatListToString(List<string> list)
        {
            string outputString = "";

            if (list.Count == 1)
            {
                outputString = list[0];
            }
            else
            {
                foreach (string choice in list)
                {
                    outputString += choice + ",";
                }
                outputString = outputString[..^1];
            }
            return outputString;
        }
        private bool IsAnyCheckboxChecked()
        {
            foreach (CheckBox checkBox in BlacklistChoices.Children)
            {
                if (checkBox.IsChecked == true)
                {
                    return true;
                }
            }
            return false;
        }

        private string FormatSearchTerms()
        {

            return "";
        }
    }
}