using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects
{
    public class TankCreator : Creator
    {
        override
        public Tank factoryMethod(string input, int x, int y, int width, int height)
        {
            if (input.Equals("H"))
                return new BlueHeavyTank(x,y,width,height);
            if(input.Equals("L"))
                return new BlueLightTank(x, y, width, height);
            return null;
        }
    }
}
