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

namespace Server
{
    internal class Server
    {
        Socket socket;
        List<Player> players = new List<Player>();

        public Server() : this(8888)
        {

        }
        public Server(int port)
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.Blocking = false;
            socket.Bind(new IPEndPoint(IPAddress.Loopback, port));
        }

        public void Start()
        {
            while (true)
            {
                KeepAlive();

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

                Player player = new Player(Remote);
                if (player != null && !PlayerExists(player))
                {
                    players.Add(player);
                    Console.WriteLine("Player at endpoint {0} added!", player.EndPoint);
                }

                string msg = Encoding.ASCII.GetString(data);
                Console.WriteLine(msg);
            }
        }

        private bool PlayerExists(Player obj)
        {
            foreach(Player player in players)
            {
                if (player.ToString() == obj.ToString())
                    return true;
            }

            return false;
        }

        private void KeepAlive()
        {
            List<int> playerId = new List<int>();

            for(int i = 0; i < players.Count; i++)
            {
                byte[] data = new byte[0];

                socket.SendTo(data, players[i].EndPoint);
                try
                {
                    socket.ReceiveFrom(data, ref players[i].EndPoint);
                    players[i].LastKeepAlive = DateTime.Now;
                }
                catch
                {
                    if(players[i].IsAlive())
                        playerId.Add(i);
                }
            }

            for(int i = playerId.Count - 1; i >= 0; i--)
            {
                Console.WriteLine("Player at endpoint {0} removed!", players[playerId[i]].EndPoint.ToString());
                players.RemoveAt(playerId[i]);
            }
        }
    }
}
