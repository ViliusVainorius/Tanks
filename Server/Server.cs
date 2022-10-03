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

namespace Server
{
    internal class Server
    {
        private Socket socket;
        private List<Player> players = new List<Player>();
        private DateTime previous;
        private static Server instance;
        public static int port = 8888;
        
        public static Server Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new Server(port);
                }
                return instance;
            }
        }

        private Server(int port)
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
                TimeSpan span = DateTime.Now - previous;

                if(span.Minutes >= 1)
                {
                    KeepAlive();
                    previous = DateTime.Now;
                }

                byte[] data = new byte[1024];
                IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
                EndPoint Remote = (EndPoint)(sender);
                try
                {
                    int recv = socket.ReceiveFrom(data, ref Remote);
                }
                catch
                {
                    continue;
                }

                if((Remote != (EndPoint)sender))
                {
                    Player player = FindPlayer(Remote);
                    if(player == null)
                    {
                        player = new Player(Remote);
                        players.Add(player);
                        Console.WriteLine("Player at endpoint {0} added!", player.EndPoint);
                    }

                    player.LastKeepAlive = DateTime.Now;
                }

                string msg = Encoding.ASCII.GetString(data);
                if(msg != "")
                    Console.WriteLine(msg);
            }
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
        }
    }
}
