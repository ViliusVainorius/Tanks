using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects
{
    internal class BlueLightTank : Tank
    {
        public BlueLightTank(int x, int y, int width, int height) : base(x, y, width, height, 5)
        {
            this.speed = speed + 3;
            this.lives = lives - 1;
        }
    }
}
