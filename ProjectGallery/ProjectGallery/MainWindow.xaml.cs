using ClassLibrary;
using ProjectGallery.Controls;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


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
        WindowHelper.EnableWindowDragging(this);
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
                Width = 80,
                Height = 80
            };

            ProjectsTestsPanel.Children.Add(button);
        }
    }

    private void CloseProject()
    {
        // Get the current process
        Process currentProcess = Process.GetCurrentProcess();

        // Kill all associated processes (including subprocesses)
        foreach (Process process in Process.GetProcessesByName(currentProcess.ProcessName))
        {
            process.Kill();
        }
    }

    private void closeWindow(object sender, RoutedEventArgs e)
    {
        CloseProject();
    }
    private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        // This event handler will be called when the window is clicked, and it will trigger the window dragging functionality
    }
}