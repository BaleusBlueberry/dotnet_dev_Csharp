using ClassLibrary;
using System.Windows.Controls;

namespace ProjectGallery.Controls;

public partial class ProjectButton : UserControl
{
    public ProjectButton(IProjectMeta project)
    {
        InitializeComponent();
        DataContext = project;

        LandingPage landingPage = new LandingPage(project);
        landingPage.DataContext = project;

        MainButton.Click += (sender, e) => landingPage.ShowDialog();

        /*MainButton.Click += (sender, e) => project.Run();*/
    }
}

