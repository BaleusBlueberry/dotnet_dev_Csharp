﻿using System.Data.Common;
using System.Security.Cryptography.X509Certificates;
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
using Tic_Tac_Toe.Controls;
using Tic_Tac_Toe.Enums;


namespace Tic_Tac_Toe;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        SetTheme();
    }

    private void NewGame_Click(object sender, RoutedEventArgs e)
    {
        GameType gameType;
        if (sender == Btn_PvP)
        {
            gameType = GameType.PvP;
        }
        else if (sender == Btn_PvC)
        {
            gameType = GameType.PvC;
        }
        else if (sender == Btn_CvC)
        {
            gameType = GameType.CvC;
        }
        else
        {
            return;
        }

        MyBoard.StartNewGame(gameType);
    }
    private void SetTheme()
    {
        var windowsAppThemes = new ClassLibrary.WindowsAppThemes();
        var appTheme = windowsAppThemes.GetWindowsAppTheme();

        if (appTheme == WindowsAppTheme.Light) Syncfusion.SfSkinManager.SfSkinManager.SetVisualStyle(this, Syncfusion.SfSkinManager.VisualStyles.Windows11Light);
        else if (appTheme == WindowsAppTheme.Dark) Syncfusion.SfSkinManager.SfSkinManager.SetVisualStyle(this, Syncfusion.SfSkinManager.VisualStyles.Windows11Dark);
    }
}