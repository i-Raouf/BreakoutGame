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

namespace BreakoutGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            var centerPos = (this.Width - Paddle.Width) / 2;
            MovePaddle(centerPos);
        }

        private void MovePaddle(double newXPos)
        {
            if (newXPos < 0)
                newXPos = 0;
            if (newXPos > this.Width - Paddle.Width - 15)
                newXPos =this.Width - Paddle.Width - 15;
            Canvas.SetLeft(Paddle, newXPos);
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            var position = e.GetPosition(GameCanva);
            MovePaddle(position.X);
        }
    }
}
