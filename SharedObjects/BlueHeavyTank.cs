using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects
{
    public class BlueHeavyTank : Tank
    {
        const int defaultspeed = 2;
        const string skin = "pack://application:,,,/images/tankBlue.png";
        public BlueHeavyTank(int x, int y, int width, int height) : base(x, y, width, height, defaultspeed,skin)
        {
            this.lives = lives + 5;
        }
    }
}
