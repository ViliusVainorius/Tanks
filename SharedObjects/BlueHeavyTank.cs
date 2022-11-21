using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects
{
    public class BlueHeavyTank : Tank
    {
        public BlueHeavyTank(int x, int y, int width, int height) : base(x, y, width, height, 1)
        {
            this.speed = speed - 2;
            this.lives = lives + 5;
        }
    }
}
