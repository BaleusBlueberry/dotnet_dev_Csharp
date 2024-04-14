using ClassLibrary;
using System.Windows.Controls;

namespace ProjectGallery.Controls;

/// <summary>
/// Interaction logic for ProjectButton.xaml
/// </summary>
public partial class ProjectButton : UserControl
{
    public ProjectButton(IProjectMeta project)
    {
        InitializeComponent();
        DataContext = project;

        MainButton.Click += (sender, e) => project.Run();
    }
}

