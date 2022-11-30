using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Xml;
using SharedObjects;
using System.ComponentModel;
using Newtonsoft.Json;
using System.IO;
using System.Xml.Serialization;

namespace Server
{
    public class Server
    {
        private Socket socket;
        private List<Player> players = new List<Player>();
        private DateTime previous;
        private int bulletCount = 0; // total bullets in a field

        public Server() : this(8888) { }

        public Server(int port)
        {
            Console.WriteLine("Starting server on port {0}!", port);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.Blocking = false;
            socket.Bind(new IPEndPoint(IPAddress.Loopback, port));

            previous = DateTime.Now;
        }

        public void CreateBullet(ref List<Bullet> bullets, Tank t)
        {
            // create bullet
            Bullet b = new Bullet(t.X, t.Y, t.Width / 3,
                t.Height / 3, t.speed, bulletCount);
            bulletCount++;

            // and to list
           //bullets.Append(b);
            bullets.Add(b);
        }

        public void Start()
        {
            Console.WriteLine("Server has been started!");

            while (true)
            {
                KeepAlive();
                TryStartGame();

                Packet packet = Packet.ReceiveData(socket);
                Player player = GetPlayer(packet);

                if(packet == null)
                {
                    continue;
                }

                string data = Encoding.ASCII.GetString(packet.Data);
                try
                {
                    List<Bullet> bulletsList = GameSession.Instance.GameObjectContainer.Bullets.ToList();
                    PlayerAction action = JsonConvert.DeserializeObject<PlayerAction>(data);

                    Tank[] tanks = GameSession.Instance.GameObjectContainer.Tanks;
                    for(int i = 0; i < tanks.Length; i++)
                    {
                        if(tanks[i].player.EndPoint == player.EndPoint)
                        {
                            ActionController controller = new ActionController();

                            if (action.side == FacingSide.Right)
                            {
                                if (action.type == ActionType.move)
                                {
                                    controller.SetCommand(new CommandMoveRight(tanks[i], player));
                                    tanks[i].Rotation = 90;
                                }
                                else if(action.type == ActionType.shoot)
                                {
                                    CreateBullet(ref bulletsList, tanks[i]);
                                    controller.SetCommand(new CommandShoot(tanks[i], bulletsList));
                                }
                            }
                            else if (action.side == FacingSide.Left)
                            {
                                if (action.type == ActionType.move)
                                {
                                    controller.SetCommand(new CommandMoveLeft(tanks[i], player));
                                    tanks[i].Rotation = -90;
                                }
                                else if (action.type == ActionType.shoot)
                                {
                                    CreateBullet(ref bulletsList, tanks[i]);
                                    //controller.SetCommand(new CommandShoot(tanks[i]));
                                }
                            }
                            else if (action.side == FacingSide.Up)
                            {
                                if (action.type == ActionType.move)
                                {
                                    controller.SetCommand(new CommandMoveUp(tanks[i], player));
                                    tanks[i].Rotation = 0;
                                }
                                else if (action.type == ActionType.shoot)
                                {
                                    CreateBullet(ref bulletsList, tanks[i]);
                                    //controller.SetCommand(new CommandShoot(tanks[i]));
                                }
                            }
                            else if (action.side == FacingSide.Down)
                            {
                                if (action.type == ActionType.move)
                                {
                                    controller.SetCommand(new CommandMoveDown(tanks[i], player));
                                    tanks[i].Rotation = -180;
                                }
                                else if (action.type == ActionType.shoot)
                                {
                                    CreateBullet(ref bulletsList, tanks[i]);
                                    //controller.SetCommand(new CommandShoot(tanks[i]));
                                }
                            }

                            controller.Execute();
                        }

                        Powerup[] powerups = GameSession.Instance.GameObjectContainer.Powerups;
                        for (int j = 0; j < powerups.Length; j++)
                        {
                            if (powerups[j] != null && tanks[i].Intersect(powerups[j]))
                            {
                                powerups[j].PickUp(ref tanks[i]);
                                powerups[j] = new UsedPowerup();
                            }
                        }
                    }

                    GameSession.Instance.GameObjectContainer.Bullets = bulletsList.ToArray();
                    GameSession.Instance.GameObjectContainer.Tanks = tanks.ToArray();

                    foreach (Tank tank in GameSession.Instance.GameObjectContainer.Tanks)
                    {
                        XmlSerializer ser = new XmlSerializer(typeof(GameObjectContainer));
                        using (MemoryStream ms = new MemoryStream())
                        {
                            ser.Serialize(ms, GameSession.Instance.GameObjectContainer);
                            packet = new Packet(tank.player.EndPoint, ms.ToArray());
                            packet.SendToEndPoint(socket);
                        }
                    }
                }
                catch { }
            }
        }

        private void TryStartGame()
        {
            if(players.Count >= 2)
            {
                int player1 = -1;
                int player2 = -1;

                for (int i = 0; i < players.Count; i++)
                {
                    if(!players[i].isInGame)
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

                Player player;
                StartGamePacket startGamePacket = new StartGamePacket("..\\..\\..\\SharedObjects\\Maps\\Map1.xml", 0);
                
                player = players[player1];
                GameSession.Instance.GameObjectContainer.Tanks[0].player = player;
                XmlSerializer ser = new XmlSerializer(typeof(StartGamePacket));
                using(MemoryStream ms = new MemoryStream())
                {
                    ser.Serialize(ms, startGamePacket);
                    Packet packet = new Packet(player.EndPoint, ms.ToArray());
                    packet.SendToEndPoint(socket);
                }
                players[player1].isInGame = true;

                startGamePacket.self = 1;
                player = players[player2];
                GameSession.Instance.GameObjectContainer.Tanks[1].player = player;
                using (MemoryStream ms = new MemoryStream())
                {
                    ser.Serialize(ms, startGamePacket);
                    Packet packet = new Packet(player.EndPoint, ms.ToArray());
                    packet.SendToEndPoint(socket);
                }
                players[player2].isInGame = true;
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
                players.Add(player);
                Console.WriteLine("Player at endpoint {0} added!", player.EndPoint);
            }

            player.LastKeepAlive = DateTime.Now;

            return player;
        }

        private Player FindPlayer(EndPoint endPoint)
        {
            foreach(Player player in players)
            {
                if (player.EndPoint.ToString() == endPoint.ToString())
                    return player;
            }

            return null;
        }

        private void KeepAlive()
        {
            TimeSpan span = DateTime.Now - previous;

            if(span.Minutes < 1)
            {
                return;
            }

            List<int> playerId = new List<int>();

            for(int i = 0; i < players.Count; i++)
            {
                if(!players[i].IsAlive())
                    playerId.Add(i);
            }

            for(int i = playerId.Count - 1; i >= 0; i--)
            {
                Console.WriteLine("Player at endpoint {0} removed!", players[playerId[i]].EndPoint.ToString());
                players.RemoveAt(playerId[i]);
            }

            previous = DateTime.Now;
        }
    }
}
