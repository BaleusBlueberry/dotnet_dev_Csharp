﻿using ClassLibrary;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace PersonManager;

public partial class MainWindow : Window
{
    private const string filePath = "people.json";
    private readonly ICollectionView peopleView;


    public MainWindow()
    {
        InitializeComponent();
        ThemeHelper.SetTheme(this);

        peopleView = CollectionViewSource.GetDefaultView(people);

        PeopleGrid.ItemsSource = peopleView;
        LoadFile();
    }

    private ObservableCollection<Person> people
    {
        get;
        set;
    } = new ObservableCollection<Person>();


    private void HandleFilterKeyUp(object sender, KeyEventArgs e)
    {
        string filterString = TB_Filter.Text.ToLower();

        peopleView.Filter = o => {
            if (o is Person personToFilter)
            {
                return personToFilter.Name.ToLower().Contains(filterString);
            }
            return false;
        };
    }

    public void HandleSelectionChange(object sender, SelectionChangedEventArgs e)
    {
        if (PeopleGrid.SelectedItem is Person selectedPerson)
        {
            TB_ID.Text = selectedPerson.ID.ToString();
            TB_Name.Text = selectedPerson.Name.ToString();
            TB_Age.Text = selectedPerson.Age.ToString();
        }
    }

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
    }

    private void Btn_Update_OnClick(object sender, RoutedEventArgs e)
    {
        if (PeopleGrid.SelectedItem is Person selectedPerson &&
            int.TryParse(TB_ID.Text, out int id) &&
            int.TryParse(TB_Age.Text, out int age) &&
            TB_Name.Text.Length > 0)
        {
            selectedPerson.ID = id;
            selectedPerson.Age = age;
            selectedPerson.Name = TB_Name.Text;

            PeopleGrid.Items.Refresh();

            SaveData();
            CleanData();
        }
    }

    private void Btn_Add_OnClick(object sender, RoutedEventArgs e)
    {
        if (
            int.TryParse(TB_ID.Text, out int id) &&
            int.TryParse(TB_Age.Text, out int age) &&
            TB_Name.Text.Length > 0)
        {
            Person newPerson = new Person()
            {
                ID = GenerateID(),
                Name = TB_Name.Text,
                Age = age
            };

            people.Add(newPerson);

            SaveData();
            CleanData();

        }
    }

    private void HandleDeleteClick(object sender, RoutedEventArgs e)
    {
        MessageBoxResult result = MessageBox.Show("Are you sure?", "delete", MessageBoxButton.YesNo);

        if (result == MessageBoxResult.No)
        {
            return;
        }

        Button btn = sender as Button;
        if (btn == null)
        {
            return;
        }

        if (btn.DataContext is Person personToDelete)
        {
            people.Remove(personToDelete);
            SaveData();
            CleanData();
        }
    }

    private int GenerateID()
    {
        return people.Count == 0 ? 1 : people.Max(p => p.ID) + 1;
    }

    private void SaveData()
    {
        try
        {
            string rawData = JsonSerializer.Serialize(people);
            File.WriteAllText(filePath, rawData);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Failed to save data: {ex.Message}");
        }
    }
    private void CleanData()
    {
        TB_Age.Clear();
        TB_ID.Clear();
        TB_Name.Clear();

        PeopleGrid.SelectedItem = null;

    }

    private void HandleFilterFocus(object sender, RoutedEventArgs e)
    {
        if (TB_Filter.Text != "Filter...") return;
        TB_Filter.Text = "";
    }

    private void HandleFilterLostFocus(object sender, RoutedEventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(TB_Filter.Text))
        {
            return;
        }

        TB_Filter.Text = "Filter...";
    }
}