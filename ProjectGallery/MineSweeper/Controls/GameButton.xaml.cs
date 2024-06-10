using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace MineSweeper
{
    public partial class GameButton : Button
    {
        private bool _isMouseDown;

        public GameButton()
        {
            InitializeComponent();
            BtnImage = new Image();
            Content = BtnImage;

            BtnImage.Source = new BitmapImage(new Uri("pack://application:,,,/resources/ButtonUnclicked.png"));

            PreviewMouseDown += GameButton_PreviewMouseDown;
            PreviewMouseUp += GameButton_PreviewMouseUp;
            MouseLeave += GameButton_MouseLeave;
        }

        private void GameButton_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                _isMouseDown = true;
                BtnImage.Source = new BitmapImage(new Uri("pack://application:,,,/resources/ButtonClicked.png"));
            }
        }

        private void GameButton_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            _isMouseDown = false;
            BtnImage.Source = new BitmapImage(new Uri("pack://application:,,,/resources/ButtonUnclicked.png"));
        }

        private void GameButton_MouseLeave(object sender, MouseEventArgs e)
        {
            if (_isMouseDown)
            {
                BtnImage.Source = new BitmapImage(new Uri("pack://application:,,,/resources/ButtonUnclicked.png"));
            }
        }
    }
}