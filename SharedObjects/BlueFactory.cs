using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects
{
    public class BlueFactory : AbstractFactory
    {
        override
            public Tank createHeavyTank(int x, int y, int width, int height)
        {
            return new BlueHeavyTank(x, y, width, height);
        }

        override
            public Tank createLightTank(int x, int y, int width, int height)
        {
            return new BlueLightTank(x,y,width,height);
        }
    }
}
