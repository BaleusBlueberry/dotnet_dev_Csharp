using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

namespace Tic_Tac_Toe.Elements
{
    /// <summary>
    /// Interaction logic for BoardButton.xaml
    /// </summary>
    public partial class BoardButton : UserControl
    {
        public BoardButton(TicTakToeElement ticTakToeElement)
        {
            InitializeComponent();
            DataContext = ticTakToeElement;

            TicTakToeButton.Click += (sender, e) => ticTakToeElement.Clicked();
        }
    }
}
