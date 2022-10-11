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
using SharedObjects;
using Newtonsoft.Json;

namespace Tanks
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Socket socket;

        DispatcherTimer gameTimer = new DispatcherTimer();

        ImageBrush playerImage = new ImageBrush();
        ImageBrush player2Image = new ImageBrush();
        ImageBrush livesImage = new ImageBrush();
        Rect PlayerHitBox;

        Rect playerHitBoxObject;

        DateTime previous;

        //TANK OBJECT
        Rectangle player;
        Rectangle player2;
        Tank tank = new Tank(60, 75, 135, 267);
        Tank tank2 = new Tank(60, 75, 635, 167);
        //WALL OBJECT
        List<Wall> walls = new List<Wall>();
        Wall wall1 = new Wall(400, 30, 35, 35);
        Wall wall2 = new Wall(30, 800, 35, 35);
        Wall wall3 = new Wall(400, 30, 830, 35);
        Wall wall4 = new Wall(30, 800, 35, 405);
        Wall wall5 = new Wall(198, 30, 276, 207);
        Wall wall6 = new Wall(198, 30, 530, 65);



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

            //ADDING TANK TO MAIN WINDOW (1st MAIN PLAYER)
            Rectangle playerTank = tank.createTank();
            MyCanvas.Children.Add(playerTank);
            Canvas.SetTop(playerTank, tank.Y);
            Canvas.SetLeft(playerTank, tank.X);
            playerTank.Fill = playerImage;
            player = playerTank;

            //ADDING TANK TO MAIN WINDOW (2nd OTHER PLAYER)
            Rectangle playerTank2 = tank2.createTank();
            MyCanvas.Children.Add(playerTank2);
            Canvas.SetTop(playerTank2, tank2.Y);
            Canvas.SetLeft(playerTank2, tank2.X);
            playerTank2.Fill = player2Image;
            player2 = playerTank2;

            //TANK SKIN OVER RECTANGLE
            //1st PLAYER SKIN (blue)
            playerImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/tankBlue.png"));
            //2nd PLAYER SKIN (red)
            player2Image.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/tankRed.png"));

            //ADD ALL WALLS TO WALL LIST FOR MAPPING
            walls.Add(wall1);
            walls.Add(wall2);
            walls.Add(wall3);
            walls.Add(wall4);
            walls.Add(wall5);
            walls.Add(wall6);

            //PUTTING WALL OBJECTS TO SCREEN
            foreach (var item in walls)
            {
                var wall = item.createWall();
                MyCanvas.Children.Add(wall);
                Canvas.SetLeft(wall, item.X);
                Canvas.SetTop(wall, item.Y);
            }


            gameTimer.Tick += GameLoop;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);

            StartGame();
        }

        private void GameLoop(object sender, EventArgs e)
        {
            byte[] data;
            PlayerAction action = null;


            // tank = player
            PlayerHitBox = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), tank.Width, tank.Height);
            playerHitBoxObject = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), tank.Width, tank.Height);

            if (moveLeft == true && Canvas.GetLeft(player) > 0)
            {
                Canvas.SetLeft(player, Canvas.GetLeft(player) - playerSpeed);
                action = new PlayerAction(ActionType.move, (int)MoveSide.Left);
            }
            if (moveRight == true && Canvas.GetLeft(player) + 90 < Application.Current.MainWindow.Width)
            {
                Canvas.SetLeft(player, Canvas.GetLeft(player) + playerSpeed);
                action = new PlayerAction(ActionType.move, (int)MoveSide.Right);
            }
            if (moveUp == true && Canvas.GetTop(player) > 0)
            {
                Canvas.SetTop(player, Canvas.GetTop(player) - playerSpeed);
                action = new PlayerAction(ActionType.move, (int)MoveSide.Up);
            }
            if (moveDown == true && Canvas.GetTop(player) + 90 < Application.Current.MainWindow.Height)
            {
                Canvas.SetTop(player, Canvas.GetTop(player) + playerSpeed);
                action = new PlayerAction(ActionType.move, (int)MoveSide.Down);
            }

            foreach (var x in MyCanvas.Children.OfType<Rectangle>())
            {
                if((string)x.Tag == "wall")
                {
                    Rect wallHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    if (moveLeft == true && PlayerHitBox.IntersectsWith(wallHitBox))
                    {
                        Canvas.SetLeft(player, Canvas.GetLeft(player) + 10);
                        noMoveLeft = true;
                        moveLeft = false;
                    }
                    if (moveRight == true && PlayerHitBox.IntersectsWith(wallHitBox))
                    {
                        Canvas.SetLeft(player, Canvas.GetLeft(player) - 10);
                        noMoveRight = true;
                        moveRight = false;
                    }
                    if (moveDown == true && PlayerHitBox.IntersectsWith(wallHitBox))
                    {
                        Canvas.SetTop(player, Canvas.GetTop(player) - 10);
                        noMoveDown = true;
                        moveDown = false;
                    }
                    if (moveUp == true && PlayerHitBox.IntersectsWith(wallHitBox))
                    {
                        Canvas.SetTop(player, Canvas.GetTop(player) + 10);
                        noMoveUp = true;
                        moveUp = false;
                    }

                }
            }

            if(action != null)
            {
                string json = JsonConvert.SerializeObject(action);
                data = Encoding.ASCII.GetBytes(json);
                socket.Send(data);
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
                player.RenderTransform = new RotateTransform(-90, tank.Width / 2, tank.Height / 2);
            }
            if (e.Key == Key.Right && noMoveRight == false)
            {
                moveRight = true;
                noMoveLeft = noMoveRight = noMoveUp = noMoveDown = false;
                player.RenderTransform = new RotateTransform(90, tank.Width / 2, tank.Height / 2);

            }
            if (e.Key == Key.Up && noMoveUp == false)
            {
                moveUp = true;
                noMoveLeft = noMoveRight = noMoveUp = noMoveDown = false;
                player.RenderTransform = new RotateTransform(0, tank.Width / 2, tank.Height / 2);

            }
            if (e.Key == Key.Down && noMoveDown == false)
            {
                moveDown = true;
                noMoveLeft = noMoveRight = noMoveUp = noMoveDown = false;
                player.RenderTransform = new RotateTransform(-180, tank.Width / 2, tank.Height / 2);

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

            livesImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/3Hearts.png"));


            MyCanvas.Background = Brushes.DarkGray;
        }


    }        
}
