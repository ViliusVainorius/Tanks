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

                Packet packet = ReceiveData();
                Player player = GetPlayer(packet);

                if(packet != null)
                {
                    string data = Encoding.ASCII.GetString(packet.Data);
                    try
                    {
                        PlayerAction action = JsonConvert.DeserializeObject<PlayerAction>(data);
                        Console.WriteLine("Player Action = {0}, Player Side = {1}", action.type, action.varied);
                    }
                    catch { }
                }
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

        private Packet ReceiveData()
        {
            byte[] data = new byte[1024];
            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            EndPoint Remote = (EndPoint)(sender);
            try
            {
                int recv = socket.ReceiveFrom(data, ref Remote);
            }
            catch
            {
                return null;
            }

            if (Remote == (EndPoint)sender)
                return null;

            return new Packet(Remote, data);
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
