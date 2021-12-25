using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace BreakoutGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer gameTimer = new DispatcherTimer();
        int ballSpeed = 5;
        int ballDX = 1;
        int ballDY = 1;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            // center paddle
            var centerPos = (this.Width - Paddle.Width) / 2;
            MovePaddle(centerPos);
            Canvas.SetTop(Paddle, (this.Height - 100));
            // position the ball
            Canvas.SetLeft(Ball, ((this.Width - Ball.Width) / 2));
            Canvas.SetTop(Ball, 300);
            // set game timer
            gameTimer.Interval = new TimeSpan(0,0,0,0,1000/60); // 60fps
            gameTimer.Tick +=  new EventHandler(GameTimer_Tick);
            gameTimer.Start();
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            // moving ball
            var ballXpostion = Canvas.GetLeft(Ball);
            var ballYpostion = Canvas.GetTop(Ball);

            if (ballXpostion < 0 || ballXpostion > (this.Width - Ball.Width - 15))
                ballDX = -ballDX;

            if (ballYpostion < 0)
                ballDY = -ballDY;

            // collision with paddle
            Rect recBall = new Rect(Canvas.GetLeft(Ball), Canvas.GetTop(Ball), Ball.Width, Ball.Height);
            Rect recPaddle = new Rect(Canvas.GetLeft(Paddle), Canvas.GetTop(Paddle), Paddle.Width, Paddle.Height);
            
            if(recBall.IntersectsWith(recPaddle))
            {
                ballDY = -ballDY;
            }

            Canvas.SetLeft(Ball, ballXpostion + ballSpeed * ballDX);
            Canvas.SetTop(Ball, ballYpostion + ballSpeed * ballDY);
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
