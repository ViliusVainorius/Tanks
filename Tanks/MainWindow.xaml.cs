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
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using Newtonsoft.Json;
using System.Threading;
using System.Windows.Markup;

namespace Tanks
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Socket socket;

        DispatcherTimer gameTimer = new DispatcherTimer();

        ImageBrush livesImage = new ImageBrush();
        Rect PlayerHitBox;

        Rect playerHitBoxObject;

        DateTime previous;

        bool moveLeft, moveRight, moveUp, moveDown, gameOver;
        bool noMoveLeft, noMoveRight, noMoveUp, noMoveDown;

        public MainWindow()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.Blocking = false;
            socket.Connect(new IPEndPoint(IPAddress.Loopback, 8888));

            Packet packet;

            packet = new Packet(new byte[0]);
            packet.SendData(socket);
            previous = DateTime.Now;

            InitializeComponent();

            MyCanvas.Focus();

            while(true)
            {
                packet = Packet.ReceiveData(socket);
                if (packet == null)
                {
                    Thread.Sleep(100);
                    SendKeepAlive();
                    continue;
                }

                XmlSerializer ser = new XmlSerializer(typeof(StartGamePacket));
                using (Stream stream = new MemoryStream(packet.Data))
                {
                    using (XmlReader reader = XmlReader.Create(stream))
                    {
                        StartGamePacket startGamePacket = (StartGamePacket)ser.Deserialize(reader);
                        GameSession.xmlFileName = startGamePacket.fileName;
                        GameSession.Instance.self = startGamePacket.self;
                    }
                }

                break;
            }

            TeamFactory factory = new ConcreteTeamFactory();
            Tank heavyTank = factory.GetTank("Heavy"); // "Heavy" or "Light"


            Tank[] tanks = GameSession.Instance.GameObjectContainer.Tanks;

            ImageBrush[] playerImages = new ImageBrush[tanks.Length];
            for(int i = 0; i < playerImages.Length; i++)
            {
                playerImages[i] = new ImageBrush();
            }

            playerImages[0].ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/tankBlue.png"));
            playerImages[1].ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/tankRed.png"));

            for (int i = 0; i < tanks.Length; i++)
            {
                tanks[i].CanvasID = MyCanvas.Children.Add(tanks[i].Rectangle);
                Canvas.SetTop(tanks[i].Rectangle, tanks[i].Y);
                Canvas.SetLeft(tanks[i].Rectangle, tanks[i].X);
                tanks[i].Rectangle.Fill = playerImages[i];
            }

            Wall[] walls = GameSession.Instance.GameObjectContainer.Walls;
            for(int i = 0; i < walls.Length; i++)
            {
                Rectangle wall = walls[i].createWall();
                walls[i].CanvasID = MyCanvas.Children.Add(wall);
                Canvas.SetLeft(wall, walls[i].X);
                Canvas.SetTop(wall, walls[i].Y);
            }

            gameTimer.Tick += GameLoop;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);

            StartGame();
        }

        private void GameLoop(object sender, EventArgs e)
        {
            byte[] data;
            PlayerAction action = null;

            Packet packet = Packet.ReceiveDataFrom(socket, new IPEndPoint(IPAddress.Loopback, 8888));

            if (packet != null)
            {
                XmlSerializer ser = new XmlSerializer(typeof(GameObjectContainer));
                using (Stream stream = new MemoryStream(packet.Data))
                {
                    using (XmlReader reader = XmlReader.Create(stream))
                    {
                        GameObjectContainer gameObjectContainer = (GameObjectContainer)ser.Deserialize(reader);
                        GameSession.Instance.GameObjectContainer.Update(gameObjectContainer);

                        Tank[] tanks = GameSession.Instance.GameObjectContainer.Tanks;
                        for (int i = 0; i < tanks.Count(); i++)
                        {
                            Canvas.SetTop(tanks[i].Rectangle, tanks[i].Y);
                            Canvas.SetLeft(tanks[i].Rectangle, tanks[i].X);
                            tanks[i].Rectangle.RenderTransform = new RotateTransform(tanks[i].Rotation, tanks[i].Width / 2, tanks[i].Height / 2);
                        }
                    }
                }
            }

            Tank tank = GameSession.Instance.GameObjectContainer.Tanks[GameSession.Instance.self];
            Rectangle player = tank.Rectangle;

            PlayerHitBox = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.Width, player.Height);
            playerHitBoxObject = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.Width, player.Height);

            if (moveLeft == true && Canvas.GetLeft(player) > 0)
            {
                Canvas.SetLeft(player, Canvas.GetLeft(player) - tank.speed);
                action = new PlayerAction(ActionType.move, (int)MoveSide.Left);
            }
            if (moveRight == true && Canvas.GetLeft(player) + 90 < Application.Current.MainWindow.Width)
            {
                Canvas.SetLeft(player, Canvas.GetLeft(player) + tank.speed);
                action = new PlayerAction(ActionType.move, (int)MoveSide.Right);
            }
            if (moveUp == true && Canvas.GetTop(player) > 0)
            {
                Canvas.SetTop(player, Canvas.GetTop(player) - tank.speed);
                action = new PlayerAction(ActionType.move, (int)MoveSide.Up);
            }
            if (moveDown == true && Canvas.GetTop(player) + 90 < Application.Current.MainWindow.Height)
            {
                Canvas.SetTop(player, Canvas.GetTop(player) + tank.speed);
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

            SendKeepAlive();
        }

        private void SendKeepAlive()
        {
            byte[] data;
            if ((DateTime.Now - previous).Seconds > 30)
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
            Rectangle player = GameSession.Instance.GameObjectContainer.Tanks[GameSession.Instance.self].Rectangle;
            if (e.Key == Key.Left && noMoveLeft == false)
            {
                moveLeft = true;
                noMoveLeft = noMoveRight = noMoveUp = noMoveDown = false;
                player.RenderTransform = new RotateTransform(-90, player.Width / 2, player.Height / 2);
            }
            if (e.Key == Key.Right && noMoveRight == false)
            {
                moveRight = true;
                noMoveLeft = noMoveRight = noMoveUp = noMoveDown = false;
                player.RenderTransform = new RotateTransform(90, player.Width / 2, player.Height / 2);

            }
            if (e.Key == Key.Up && noMoveUp == false)
            {
                moveUp = true;
                noMoveLeft = noMoveRight = noMoveUp = noMoveDown = false;
                player.RenderTransform = new RotateTransform(0, player.Width / 2, player.Height / 2);

            }
            if (e.Key == Key.Down && noMoveDown == false)
            {
                moveDown = true;
                noMoveLeft = noMoveRight = noMoveUp = noMoveDown = false;
                player.RenderTransform = new RotateTransform(-180, player.Width / 2, player.Height / 2);

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
            gameTimer.Start();

            moveRight = false;
            moveLeft = false;
            moveUp = false;
            moveDown = false;
            gameOver = false;

            LivesText.Content = "Gyvybės: 3";
            livesImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/3Hearts.png"));


            MyCanvas.Background = Brushes.DarkGray;
        }


    }        
}
