using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SharedObjects;

namespace Tanks
{
    public class BlueHeavyTank2 : Tank
    {
        public BlueHeavyTank2(int x, int y, int width, int height) : base(x, y, width, height, 1)
        {
            this.speed = speed - 2;
        }
    }
}