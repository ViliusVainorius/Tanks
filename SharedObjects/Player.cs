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
        private int KeepAliveTimeoutSeconds;

        public Player(EndPoint EndPoint) : this(EndPoint, 10)
        {
        }

        public Player(EndPoint endPoint, int KeepAliveTimeoutSeconds)
        {
            this.EndPoint = endPoint;
            this.LastKeepAlive = DateTime.Now;
            this.KeepAliveTimeoutSeconds = KeepAliveTimeoutSeconds;
        }

        public bool IsAlive()
        {
            return (DateTime.Now - LastKeepAlive).Seconds > KeepAliveTimeoutSeconds;
        }
    }
}
