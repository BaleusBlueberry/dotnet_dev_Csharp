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
using Tic_Tac_Toe.ElementControl;

namespace Tic_Tac_Toe;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        InitializeBoard();
    }

    public void InitializeBoard()
    {
        TicTakToeElement[] ticTakToeBoard = new TicTakToeElement[9];
        
        int indexNum = 0;
        
        for (double x = 0; x < 3; x++)
        {
            for (double y = 0; y < 3; y++)
            {
                ticTakToeBoard[indexNum] = new TicTakToeElement(x, y, "X");
                indexNum++;
            }
                
        }

    }


    public BitmapImage Icon => new BitmapImage(new Uri($"{AppDomain.CurrentDomain.BaseDirectory}/Resources/X.png", UriKind.Absolute));
}
