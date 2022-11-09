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
        private int KeepAliveMinutes;

        public Player(EndPoint EndPoint) : this(EndPoint, 1)
        {
        }

        public Player(EndPoint endPoint, int KeepAliveMinutes)
        {
            this.EndPoint = endPoint;
            this.LastKeepAlive = DateTime.Now;
            this.KeepAliveMinutes = KeepAliveMinutes;
        }

        public bool IsAlive()
        {
            return !((DateTime.Now - LastKeepAlive).Minutes >= KeepAliveMinutes);
        }

        public void PickPowerup(Powerup p)
        {
            if (p.type is Powerup.PowerupType.Live)
            {
                GameSession.Instance.lives++;
            }

            else if (p.type is Powerup.PowerupType.TripleShoot)
            {
                // set player's tank hasTripleShoot value to true
            }
        }
    }
}
