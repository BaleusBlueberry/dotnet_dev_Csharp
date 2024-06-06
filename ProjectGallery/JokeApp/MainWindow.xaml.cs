using ClassLibrary;
using System.ComponentModel;
using System.Net.Http;
using System.Printing;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using JokeApp.Functions;

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

        Formatter formatter = new Formatter();

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

                
                if (jokeObj.APIError)
                {
                    TB_Joke.Text = jokeObj.ErrorMessage + "\n-----\n" + jokeObj.ErrorMessageMore;
                }
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

            string response = await client.GetStringAsync($"https://v2.jokeapi.dev/joke/{finalString}");
            return response;
        }

        private string GetChoices()
        {
            bool anyChecked = false;

            List<string> selectedChoices = new();

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
                            anyChecked = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("failed to get categories: " + ex.Message);
                selectedChoices.Add("Any");
            }

            if (!anyChecked) selectedChoices.Add("Any");

            return formatter.FormatList(selectedChoices);
        }

        private string GetBlacklists()
        {
            List<string> blacklistsChoices = new();

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
                    string formatedBlackList = formatter.FormatList(blacklistsChoices);

                    finalString = string.Concat("?blacklistFlags=", formatedBlackList);

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

            string blacklists = GetBlacklists();

            string searchterms = FormatSearchTerms();

            string choicesFilterd = choices;

            if (blacklists == "" & searchterms != "")
            {
                choicesFilterd = String.Concat(choices, "?", searchterms);
            } else if (blacklists != "" & searchterms != "")
            {
                choicesFilterd = String.Concat(choices, blacklists, "&", searchterms);
            } else choicesFilterd = string.Concat(choices, blacklists);

            return choicesFilterd;
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
            string returnString = "";

            if (JokeSearch.Text == "") return "";

            return "contains=" + JokeSearch.Text;
        }
    }
}