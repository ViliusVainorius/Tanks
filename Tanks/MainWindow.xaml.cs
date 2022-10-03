using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
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
using System.Windows.Threading;
namespace Tanks
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Socket socket;
        Socket keepAliveSocket;

        DispatcherTimer gameTimer = new DispatcherTimer();

        ImageBrush playerImage = new ImageBrush();
        ImageBrush player2Image = new ImageBrush();
        ImageBrush livesImage = new ImageBrush();
        Rect PlayerHitBox;
        DateTime previous;

        int speed = 5;
        int playerSpeed = 3;

        double lives;
        double i;

        bool moveLeft, moveRight, moveUp, moveDown, gameOver;
        bool noMoveLeft, noMoveRight, noMoveUp, noMoveDown;

        public MainWindow()
        {
            InitializeComponent();

            MyCanvas.Focus();

            gameTimer.Tick += GameLoop;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);

            StartGame();
        }

        private void GameLoop(object sender, EventArgs e)
        {
            byte[] data;
            PlayerHitBox = new Rect(Canvas.GetLeft(Player), Canvas.GetTop(Player), Player.Width, Player.Height);

            if (moveLeft == true && Canvas.GetLeft(Player) > 0)
            {
                Canvas.SetLeft(Player, Canvas.GetLeft(Player) - playerSpeed);
                data = Encoding.ASCII.GetBytes("Moved left");
                socket.Send(data);
            }
            if (moveRight == true && Canvas.GetLeft(Player) + 90 < Application.Current.MainWindow.Width)
            {
                Canvas.SetLeft(Player, Canvas.GetLeft(Player) + playerSpeed);
                data = Encoding.ASCII.GetBytes("Moved right");
                socket.Send(data);
            }
            if (moveUp == true && Canvas.GetTop(Player) > 0)
            {
                Canvas.SetTop(Player, Canvas.GetTop(Player) - playerSpeed);
            }
            if (moveDown == true && Canvas.GetTop(Player) + 90 < Application.Current.MainWindow.Height)
            {
                Canvas.SetTop(Player, Canvas.GetTop(Player) + playerSpeed);
            }

            foreach (var x in MyCanvas.Children.OfType<Rectangle>())
            {
                if((string)x.Tag == "wall")
                {
                    Rect wallHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    if (moveLeft == true && PlayerHitBox.IntersectsWith(wallHitBox))
                    {
                        Canvas.SetLeft(Player, Canvas.GetLeft(Player) + 10);
                        noMoveLeft = true;
                        moveLeft = false;
                    }
                    if (moveRight == true && PlayerHitBox.IntersectsWith(wallHitBox))
                    {
                        Canvas.SetLeft(Player, Canvas.GetLeft(Player) - 10);
                        noMoveRight = true;
                        moveRight = false;
                    }
                    if (moveDown == true && PlayerHitBox.IntersectsWith(wallHitBox))
                    {
                        Canvas.SetTop(Player, Canvas.GetTop(Player) - 10);
                        noMoveDown = true;
                        moveDown = false;
                    }
                    if (moveUp == true && PlayerHitBox.IntersectsWith(wallHitBox))
                    {
                        Canvas.SetTop(Player, Canvas.GetTop(Player) + 10);
                        noMoveUp = true;
                        moveUp = false;
                    }

                }
            }

            if((DateTime.Now - previous).Seconds > 30)
            {
                data = new byte[0];
                try
                {
                    socket.Receive(data);
                    socket.Send(data, SocketFlags.None);
                }
                catch { }
            }
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left && noMoveLeft == false)
            {
                moveLeft = true;
                noMoveLeft = noMoveRight = noMoveUp = noMoveDown = false;
                Player.RenderTransform = new RotateTransform(-90, Player.Width / 2, Player.Height / 2);
            }
            if (e.Key == Key.Right && noMoveRight == false)
            {
                moveRight = true;
                noMoveLeft = noMoveRight = noMoveUp = noMoveDown = false;
                Player.RenderTransform = new RotateTransform(90, Player.Width / 2, Player.Height / 2);

            }
            if (e.Key == Key.Up && noMoveUp == false)
            {
                moveUp = true;
                noMoveLeft = noMoveRight = noMoveUp = noMoveDown = false;
                Player.RenderTransform = new RotateTransform(0, Player.Width / 2, Player.Height / 2);

            }
            if (e.Key == Key.Down && noMoveDown == false)
            {
                moveDown = true;
                noMoveLeft = noMoveRight = noMoveUp = noMoveDown = false;
                Player.RenderTransform = new RotateTransform(-180, Player.Width / 2, Player.Height / 2);

            }
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                moveLeft = false;
            }
            if (e.Key == Key.Right)
            {
                moveRight = false;
            }
            if (e.Key == Key.Up)
            {
                moveUp = false;
            }
            if (e.Key == Key.Down)
            {
                moveDown = false;
            }

            if (e.Key == Key.Enter && gameOver == true)
            {
                StartGame();
            }
        }

        private void StartGame()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.Blocking = false;
            socket.Connect(new IPEndPoint(IPAddress.Loopback, 8888));

            byte[] data = new byte[0];
            socket.Send(data);

            previous = DateTime.Now;

            gameTimer.Start();

            moveRight = false;
            moveLeft = false;
            moveUp = false;
            moveDown = false;
            gameOver = false;

            lives = 3;

            LivesText.Content = "Gyvybės: 3";

            playerImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/tankBlue.png"));
            player2Image.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/tankRed.png"));
            livesImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/3Hearts.png"));

            Player.Fill = playerImage;
            Player2.Fill = player2Image;

            MyCanvas.Background = Brushes.DarkGray;
        }

        private void ChangeTankSkins(Rectangle tank)
        {

        }
    }
}
