using ClassLibrary;
using ProjectGallery.Controls;
using System.Windows;


namespace ProjectGallery;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private IProjectMeta[] projects = new IProjectMeta[] {
        new PersonManager.Project(),
        new MemoryGame.Project(),
        new Tic_Tac_Toe.Project(),
        new ClashOfClansHelper.Project(),
        new JokeApp.Project(),
    };

    public MainWindow()
    {
        InitializeComponent();
        InitializeProjectButtons();

        ThemeHelper.SetTheme(this);
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

