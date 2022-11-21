using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedObjects;

namespace Tanks
{
    public class RedLightTank : Tank
    {
        public RedLightTank(int x, int y, int width, int height) : base(x, y, width, height, 5)
        {
        }
    }
}
