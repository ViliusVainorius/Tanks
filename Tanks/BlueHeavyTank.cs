using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SharedObjects;

namespace Tanks
{
    public class BlueHeavyTank : Tank
    {
        public BlueHeavyTank(int x, int y, int width, int height) : base(x, y, width, height)
        {
            speed = speed / 2;
        }
    }
}