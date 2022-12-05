using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using SharedObjects;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using Newtonsoft.Json;
using System.Threading;
using System.Windows.Markup;
using Server;

namespace Tanks
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Socket _socket;

        DispatcherTimer _gameTimer = new DispatcherTimer();

        ImageBrush _livesImage = new ImageBrush();
        Rect _playerHitBox;

        Rect _playerHitBoxObject;

        DateTime _previous;

        bool _moveLeft, _moveRight, _moveUp, _moveDown, _gameOver, _shoot;

        public MainWindow()
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            _socket.Blocking = false;
            _socket.Connect(new IPEndPoint(IPAddress.Loopback, 8888));

            Packet packet = new Packet(new byte[0]);
            packet.SendData(_socket);
            _previous = DateTime.Now;

            InitializeComponent();

            MyCanvas.Focus();

            while(true)
            {
                packet = Packet.ReceiveData(_socket);
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

            //GameSession.Instance.GameObjectContainer.Tanks.Append(heavyTank);
            Tank[] tanks = GameSession.Instance.GameObjectContainer.Tanks;

            ImageBrush[] playerImages = new ImageBrush[tanks.Length];
            for(int i = 0; i < playerImages.Length; i++)
            {
                playerImages[i] = new ImageBrush();
            }

            playerImages[0].ImageSource = new BitmapImage(new Uri(tanks[0].Skin));
            playerImages[1].ImageSource = new BitmapImage(new Uri(tanks[1].Skin));

            for (int i = 0; i < tanks.Length; i++)
            {
                tanks[i].CanvasId = MyCanvas.Children.Add(tanks[i].Rectangle);
                Canvas.SetTop(tanks[i].Rectangle, tanks[i].Y);
                Canvas.SetLeft(tanks[i].Rectangle, tanks[i].X);
                tanks[i].Rectangle.Fill = playerImages[i];
            }

            Wall[] walls = GameSession.Instance.GameObjectContainer.Walls;
            foreach (var w in walls)
            {
                Rectangle wall = w.Rectangle;
                w.CanvasId = MyCanvas.Children.Add(wall);
                Canvas.SetLeft(wall, w.X);
                Canvas.SetTop(wall, w.Y);
            }

            Powerup[] powerups = GameSession.Instance.GameObjectContainer.Powerups;
            for (int i = 0; i < powerups.Length; i++)
            {
                Rectangle powerup = powerups[i].Rectangle;
                walls[i].CanvasId = MyCanvas.Children.Add(powerup);
                Canvas.SetLeft(powerup, powerups[i].X);
                Canvas.SetTop(powerup, powerups[i].Y);
            }

            _gameTimer.Tick += GameLoop;
            _gameTimer.Interval = TimeSpan.FromMilliseconds(20);

            StartGame();
        }

        private void GameLoop(object sender, EventArgs e)
        {
            PlayerAction action = null;

            Packet packet = Packet.ReceiveDataFrom(_socket, new IPEndPoint(IPAddress.Loopback, 8888));
            List<Bullet> bullets = new List<Bullet>();

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

                        foreach(Powerup powerup in GameSession.Instance.GameObjectContainer.Powerups)
                        {
                            if(powerup.GetType() == new UsedPowerup().GetType())
                            {
                                MyCanvas.Children.Remove(powerup.Rectangle);
                            }
                        }
                        foreach (Bullet bullet in GameSession.Instance.GameObjectContainer.Bullets)
                        {
                            try
                            {
                                MyCanvas.Children.Add(bullet.Rectangle);
                            }
                            catch { }

                            Canvas.SetLeft(bullet.Rectangle, bullet.X);
                            Canvas.SetTop(bullet.Rectangle, bullet.Y);
                            
                        }

                        foreach (Bullet bullet in GameSession.Instance.GameObjectContainer.remove)
                        {
                            MyCanvas.Children.Remove(bullet.Rectangle);
                        }
                    }
                }
            }

            Tank tank = GameSession.Instance.GameObjectContainer.Tanks[GameSession.Instance.self];
            Rectangle player = tank.Rectangle;

            
            LivesText.Content = "Gyvybės: " + tank.lives;

            _playerHitBox = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.Width, player.Height);
            _playerHitBoxObject = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.Width, player.Height);
            ActionController controller = new ActionController();

            if (_moveLeft)
            {
                controller.SetCommand(new CommandMoveLeft(tank));
                action = new PlayerAction(ActionType.Move, FacingSide.Left);
            }
            if (_moveRight)
            {
                controller.SetCommand(new CommandMoveRight(tank));
                action = new PlayerAction(ActionType.Move, FacingSide.Right);
            }
            if (_moveUp)
            {
                controller.SetCommand(new CommandMoveUp(tank));
                action = new PlayerAction(ActionType.Move, FacingSide.Up);
            }
            if (_moveDown)
            {
                controller.SetCommand(new CommandMoveDown(tank));
                action = new PlayerAction(ActionType.Move, FacingSide.Down);
            }
            if (_shoot)
            {
                Thread.Sleep(100);
                action = new PlayerAction(ActionType.Shoot, tank.side);
            }

            controller.Execute();
            Canvas.SetTop(player, tank.Y);
            Canvas.SetLeft(player, tank.X);

            if (action != null)
            {
                string json = JsonConvert.SerializeObject(action);
                byte [] data = Encoding.ASCII.GetBytes(json);
                _socket.Send(data);
            }

            SendKeepAlive();
        }

        private void SendKeepAlive()
        {
            if ((DateTime.Now - _previous).Seconds > 30)
            {
                byte[] data = new byte[0];
                try
                {
                    _socket.Receive(data);
                    _socket.Send(data, SocketFlags.None);
                }
                catch { }
            }
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            Rectangle player = GameSession.Instance.GameObjectContainer.Tanks[GameSession.Instance.self].Rectangle;
            if (e.Key == Key.Left)
            {
                _moveLeft = true;
                player.RenderTransform = new RotateTransform(-90, player.Width / 2, player.Height / 2);
            }
            if (e.Key == Key.Right)
            {
                _moveRight = true;
                player.RenderTransform = new RotateTransform(90, player.Width / 2, player.Height / 2);

            }
            if (e.Key == Key.Up)
            {
                _moveUp = true;
                player.RenderTransform = new RotateTransform(0, player.Width / 2, player.Height / 2);

            }
            if (e.Key == Key.Down)
            {
                _moveDown = true;
                player.RenderTransform = new RotateTransform(-180, player.Width / 2, player.Height / 2);
            }
            if (e.Key == Key.Space)
            {
                _shoot = true;
            }
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                _moveLeft = false;
            }
            if (e.Key == Key.Right)
            {
                _moveRight = false;
            }
            if (e.Key == Key.Up)
            {
                _moveUp = false;
            }
            if (e.Key == Key.Down)
            {
                _moveDown = false;
            }
            if (e.Key == Key.Space)
            {
                _shoot = false;
            }

            if (e.Key == Key.Enter && _gameOver)
            {
                StartGame();
            }
        }

        private void StartGame()
        {
            _gameTimer.Start();

            _moveRight = false;
            _moveLeft = false;
            _moveUp = false;
            _moveDown = false;
            _shoot = false;
            _gameOver = false;

            _livesImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/3Hearts.png"));
            MyCanvas.Background = Brushes.DarkGray;
        }
    }        
}
