using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects
{
    public class RedHeavyTank : Tank
    {
        const int DefaultSpeed = 2;
        new const string Skin = "pack://application:,,,/images/tankRed.png";
        public RedHeavyTank(int x, int y, int width, int height) : base(x, y, width, height, DefaultSpeed, Skin)
        {
            this.lives = lives + 5;
        }
    }
}
