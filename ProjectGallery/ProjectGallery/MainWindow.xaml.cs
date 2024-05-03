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
        new Tic_Tac_Toe.Project(),
        new ClashOfClansHelper.Project(),
        new JokeApp.Project(),
    };

    private IProjectMeta[] testProjects = new IProjectMeta[]
    {
        new PersonManager.Project(),
        new UsersAPI.Project(),
        new UsersCRUDApi.Project(),
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

            ProjectButton button = new ProjectButton(project)
            {
                Margin = new Thickness(10),
                Width = 160,
                Height = 160
            };
            ProjectsPanel.Children.Add(button);
        }

        foreach ( var project in testProjects)
        {

            ProjectButton button = new ProjectButton(project)
            {
                Margin = new Thickness(10),
                Width = 140,
                Height = 140
            };

            ProjectsTestsPanel.Children.Add(button);
        }
    }

}
/*syncfusionskin:SfSkinManager.Theme="{syncfusionskin:SkinManagerExtension ThemeName=Windows11Dark}"*/

