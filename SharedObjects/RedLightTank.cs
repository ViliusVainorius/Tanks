using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects
{
    public class RedLightTank : Tank
    {
        const int defaultspeed = 2;
        const string skin = "pack://application:,,,/images/tankRed.png";
        public RedLightTank(int x, int y, int width, int height) : base(x, y, width, height, defaultspeed, skin)
        {
            this.speed = speed + 3;
            this.lives = lives - 1;
        }
    }
}
