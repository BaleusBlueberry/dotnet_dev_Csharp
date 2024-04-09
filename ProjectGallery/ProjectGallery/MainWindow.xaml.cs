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
using ProjectGallery.Controls;
using Tic_Tac_Toe;
using MemoryGame;
using ClassLibrary;


namespace ProjectGallery;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private IProjectMeta[] projects = new IProjectMeta[] {
        new MemoryGame.Project(),
        new MemoryGame.Project(),
        new Tic_Tac_Toe.Project(),
    };

    public MainWindow()
    {
        InitializeComponent();
        InitializeProjectButtons();

        SetTheme();
    }

    private void SetTheme()
    {
        var windowsAppThemes = new ClassLibrary.WindowsAppThemes();
        var appTheme = windowsAppThemes.GetWindowsAppTheme();
        
        if (appTheme == WindowsAppTheme.Light) Syncfusion.SfSkinManager.SfSkinManager.SetVisualStyle(this, Syncfusion.SfSkinManager.VisualStyles.Windows11Light);
        else if (appTheme == WindowsAppTheme.Dark) Syncfusion.SfSkinManager.SfSkinManager.SetVisualStyle(this, Syncfusion.SfSkinManager.VisualStyles.Windows11Dark);
    }

    private void InitializeProjectButtons()
    {

        foreach (var project in projects)
        {
            //Button button = new Button() {
            //	Width = 100,
            //	Height = 100,
            //	Margin = new Thickness(10)
            //};

            //StackPanel pnl = new StackPanel();

            //pnl.Orientation = Orientation.Vertical;

            //Image img = new Image();
            //img.Width = 50;
            //img.Height = 50;

            //TextBlock tb = new TextBlock();
            //tb.Text = "Button";
            //tb.VerticalAlignment = VerticalAlignment.Center;

            //pnl.Children.Add(img);
            //pnl.Children.Add(tb);

            //button.Content = pnl;


            ProjectButton button = new ProjectButton(project)
            {
                Margin = new Thickness(10),
                Width = 160,
                Height = 160
            };
            ProjectsPanel.Children.Add(button);
        }
    }

}
/*syncfusionskin:SfSkinManager.Theme="{syncfusionskin:SkinManagerExtension ThemeName=Windows11Dark}"*/

