using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects
{
    public class RedFactory : AbstractFactory
    {
        override
            public Tank CreateHeavyTank(int x, int y, int width, int height)
        {
            return new RedHeavyTank(x, y, width, height);
        }

        override
            public Tank CreateLightTank(int x, int y, int width, int height)
        {
            return new RedLightTank(x, y, width, height);
        }
    }
}
