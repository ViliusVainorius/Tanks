using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks
{
    public class RedHeavyTank : Tank
    {
        public RedHeavyTank(int width, int height, int top, int left, int x, int y) 
            : base(width, height, top, left, x, y)
        {
        }
    }
}
