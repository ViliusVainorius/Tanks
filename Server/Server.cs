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


        public Server() : this(8888) { }

        public Server(int port)
        {
            Console.WriteLine("Starting server on port {0}!", port);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.Blocking = false;
            socket.Bind(new IPEndPoint(IPAddress.Loopback, port));

            previous = DateTime.Now;
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
                    PlayerAction action = JsonConvert.DeserializeObject<PlayerAction>(data);
                    Console.WriteLine("Player Action = {0}, Player Side = {1}", action.type, action.varied);

                    foreach(Tank tank in GameSession.Instance.GameObjectContainer.Tanks)
                    {
                        if(tank.player.EndPoint == player.EndPoint)
                        {
                            if(action.varied == (int)MoveSide.Right)
                                tank.X = tank.X + tank.speed;
                            else if (action.varied == (int)MoveSide.Left)
                                tank.X = tank.X - tank.speed;
                            else if (action.varied == (int)MoveSide.Up)
                                tank.Y = tank.Y - tank.speed;
                            else if (action.varied == (int)MoveSide.Down)
                                tank.Y = tank.Y + tank.speed;
                        }
                    }

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
