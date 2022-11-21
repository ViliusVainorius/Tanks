using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedObjects;

namespace Tanks
{
    public class RedHeavyTank : Tank
    {
        public RedHeavyTank(int x, int y, int width, int height) : base(x, y, width, height, 1)
        {
        }
    }
}
