using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects
{
    public abstract class AbstractFactory
    {
        public abstract Tank createHeavyTank(int x, int y, int width, int height);
        public abstract Tank createLightTank(int x, int y, int width, int height);
    }
}
