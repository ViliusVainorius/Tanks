using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using SharedObjects;
using Newtonsoft.Json;
using System.IO;
using System.Xml.Serialization;
using System.Threading;

namespace Server
{
    public class Server
    {
        private Socket _socket;
        private List<Player> _players = new List<Player>();
        private DateTime _previous;
        private int _bulletId = 0; //Unique bullet id
        private bool gameStarted = false;

        public Server() : this(8888) { }

        public Server(int port)
        {
            Console.WriteLine("Starting server on port {0}!", port);
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            _socket.Blocking = false;
            _socket.Bind(new IPEndPoint(IPAddress.Loopback, port));

            _previous = DateTime.Now;
        }

        public void CreateBullet(Tank t)
        {
            List<Bullet> bulletList = GameSession.Instance.GameObjectContainer.Bullets.ToList();
            int x = t.X;
            int y = t.Y;
            bool triple = t.hasTripleShoot;

            int width = t.Width / 3;
            int height = t.Height / 3;

            GetBulletCoordinates(t, ref x, ref y, width, height);

            List<Bullet> newbulletList;
            BulletContext context;
            if (triple)
            {
                TripleBullet tripleBullet = new TripleBullet(x, y, width, height, t.speed, _bulletId, t.side,
                    t.tripleshootstartime); 
                context = new BulletContext(tripleBullet);
            }
            else
            {
                SimpleBullet simpleBullet = new SimpleBullet(x, y, width, height, t.speed, _bulletId, t.side);
                context = new BulletContext(simpleBullet);
            }

            _bulletId += context.GetBulletsCount();
            newbulletList = context.RequestShoot(ref t);

            foreach (Bullet b in newbulletList)
            {
                bulletList.Add(b);
            }

            GameSession.Instance.GameObjectContainer.Bullets = bulletList.ToArray();
        }
        
        // offset - distance between two bullets, if triple shot
        public void GetBulletCoordinates(Tank t, ref int x, ref int y, int width, int height)
        {
            if (t.side == FacingSide.Left)
            {
                x -= (t.Width) - width;
                y += (t.Height / 2) - height / 2;
            }
            else if (t.side == FacingSide.Right)
            {
                x += (t.Width) + width;
                y += (t.Height / 2) - height / 2;
            }
            else if (t.side == FacingSide.Down)
            {
                y += (t.Height) + 1;
                x += t.Width / 2 - width / 2;
            }
            else if (t.side == FacingSide.Up)
            {
                y -= (height);
                x += t.Width / 2 - width / 2;
            }
        }

        public void Start()
        {
            Console.WriteLine("Server has been started!");
            Timer timer = new Timer(UpdateGame, "", TimeSpan.FromMilliseconds(50), TimeSpan.FromMilliseconds(50));

            while (true)
            {
                KeepAlive();
                TryStartGame();

                Packet packet = Packet.ReceiveData(_socket);
                Player player = GetPlayer(packet);

                if(packet == null)
                {
                    continue;
                }

                string data = Encoding.ASCII.GetString(packet.Data);
                try
                {
                    PlayerAction action = JsonConvert.DeserializeObject<PlayerAction>(data);

                    Tank[] tanks = GameSession.Instance.GameObjectContainer.Tanks; 
                    for(int i = 0; i < tanks.Length; i++)
                    {
                        if(tanks[i].player.EndPoint == player.EndPoint)
                        {
                            ActionController controller = new ActionController();

                            if (action.side == FacingSide.Right)
                            {
                                if (action.type == ActionType.Move)
                                {
                                    controller.SetCommand(new CommandMoveRight(tanks[i]));
                                    tanks[i].Rotation = 90;
                                }
                                else if(action.type == ActionType.Shoot)
                                {
                                    CreateBullet(tanks[i]);
                                }
                            }
                            else if (action.side == FacingSide.Left)
                            {
                                if (action.type == ActionType.Move)
                                {
                                    controller.SetCommand(new CommandMoveLeft(tanks[i]));
                                    tanks[i].Rotation = -90;
                                }
                                else if (action.type == ActionType.Shoot)
                                {
                                    CreateBullet(tanks[i]);
                                }
                            }
                            else if (action.side == FacingSide.Up)
                            {
                                if (action.type == ActionType.Move)
                                {
                                    controller.SetCommand(new CommandMoveUp(tanks[i]));
                                    tanks[i].Rotation = 0;
                                }
                                else if (action.type == ActionType.Shoot)
                                {
                                    CreateBullet(tanks[i]);
                                }
                            }
                            else if (action.side == FacingSide.Down)
                            {
                                if (action.type == ActionType.Move)
                                {
                                    controller.SetCommand(new CommandMoveDown(tanks[i]));
                                    tanks[i].Rotation = -180;
                                }
                                else if (action.type == ActionType.Shoot)
                                {
                                    CreateBullet(tanks[i]);
                                }
                            }

                            controller.Execute();
                        }

                        Powerup[] powerups = GameSession.Instance.GameObjectContainer.Powerups;
                        Powerup powerup = (Powerup)tanks[i].CheckCollision(GameSession.Instance.GameObjectContainer.Powerups);
                        if(powerup != null)
                        {
                            powerup.PickUp(ref tanks[i]);
                            for(int j = 0; j < powerups.Length; j++)
                            {
                                if(powerup == powerups[j])
                                {
                                    powerups[j] = new UsedPowerup();
                                }
                            }
                        }
                    }
                }
                catch { }
            }
        }

        private void UpdateGame(object state)
        {
            if (!gameStarted)
                return;

            foreach (Bullet bullet in GameSession.Instance.GameObjectContainer.Bullets)
            {
                if (bullet != null)
                {
                    bullet.Move(ref _bulletId);
                }
            }

            foreach (Tank tank in GameSession.Instance.GameObjectContainer.Tanks)
            {
                XmlSerializer ser = new XmlSerializer(typeof(GameObjectContainer));
                using (MemoryStream ms = new MemoryStream())
                {
                    ser.Serialize(ms, GameSession.Instance.GameObjectContainer);
                    Packet packet = new Packet(tank.player.EndPoint, ms.ToArray());
                    packet.SendToEndPoint(_socket);
                }
            }
        }

        private void TryStartGame()
        {
            if(gameStarted)
                return;

            if(_players.Count >= 2)
            {
                int player1 = -1;
                int player2 = -1;

                for (int i = 0; i < _players.Count; i++)
                {
                    if(!_players[i].isInGame)
                    {
                        if(player1 == -1)
                        {
                            player1 = i;
                            continue;
                        }

                        player2 = i;
                        break;
                    }
                }

                if(player1 == -1 || player2 == -1)
                {
                    return;
                }

                GameSession.xmlFileName = "..\\..\\..\\SharedObjects\\Maps\\Map1.xml";

                Player player = _players[player1];
                StartGamePacket startGamePacket = new StartGamePacket("..\\..\\..\\SharedObjects\\Maps\\Map1.xml", 0);
                
                GameSession.Instance.GameObjectContainer.Tanks[0].player = player;
                XmlSerializer ser = new XmlSerializer(typeof(StartGamePacket));
                using(MemoryStream ms = new MemoryStream())
                {
                    ser.Serialize(ms, startGamePacket);
                    Packet packet = new Packet(player.EndPoint, ms.ToArray());
                    packet.SendToEndPoint(_socket);
                }
                _players[player1].isInGame = true;

                startGamePacket.self = 1;
                player = _players[player2];
                GameSession.Instance.GameObjectContainer.Tanks[1].player = player;
                using (MemoryStream ms = new MemoryStream())
                {
                    ser.Serialize(ms, startGamePacket);
                    Packet packet = new Packet(player.EndPoint, ms.ToArray());
                    packet.SendToEndPoint(_socket);
                }
                _players[player2].isInGame = true;

                gameStarted = true;
            }
        }

        private Player GetPlayer(Packet packet)
        {
            if (packet == null)
            {
                return null;
            }

            Player player = FindPlayer(packet.Endpoint);
            if (player == null)
            {
                player = new Player(packet.Endpoint);
                _players.Add(player);
                Console.WriteLine("Player at endpoint {0} added!", player.EndPoint);
            }

            player.LastKeepAlive = DateTime.Now;

            return player;
        }

        private Player FindPlayer(EndPoint endPoint)
        {
            foreach(Player player in _players)
            {
                if (player.EndPoint.ToString() == endPoint.ToString())
                    return player;
            }

            return null;
        }

        private void KeepAlive()
        {
            TimeSpan span = DateTime.Now - _previous;

            if(span.Minutes < 1)
            {
                return;
            }

            List<int> playerId = new List<int>();

            for(int i = 0; i < _players.Count; i++)
            {
                if(!_players[i].IsAlive())
                    playerId.Add(i);
            }

            for(int i = playerId.Count - 1; i >= 0; i--)
            {
                Console.WriteLine("Player at endpoint {0} removed!", _players[playerId[i]].EndPoint);
                _players.RemoveAt(playerId[i]);
            }

            _previous = DateTime.Now;
        }
    }
}
