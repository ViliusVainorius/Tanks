using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects
{
    public class Player
    {
        public EndPoint EndPoint;
        public DateTime LastKeepAlive;
        private int _keepAliveMinutes;
        public bool isInGame;

        public Player(EndPoint endPoint) : this(endPoint, 1)
        {
        }

        public Player(EndPoint endPoint, int keepAliveMinutes)
        {
            this.EndPoint = endPoint;
            this.LastKeepAlive = DateTime.Now;
            this._keepAliveMinutes = keepAliveMinutes;
            this.isInGame = false;
        }

        public bool IsAlive()
        {
            return !((DateTime.Now - LastKeepAlive).Minutes >= _keepAliveMinutes);
        }
    }
}
