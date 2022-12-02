using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects
{
    public class RedLightTank : Tank
    {
        const int DefaultSpeed = 2;
        new const string Skin = "pack://application:,,,/images/tankRed.png";
        public RedLightTank(int x, int y, int width, int height) : base(x, y, width, height, DefaultSpeed, Skin)
        {
            this.speed = speed + 3;
            this.lives = lives - 1;
        }
    }
}
