using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedObjects;

namespace SharedObjects
{
    public class FactoryPattern
    {
        interface ITank
        {
            Tank GetTankByName();
        }

        class BlueHeavyTank : ITank
        {
            public Tank GetTankByName()
            {
                //Tank tank = new Tank();
                return null;
            }
        }
        class BlueLightTank : ITank
        {
            public Tank GetTankByName()
            {
                return null;
            }
        }

        class FactoryCreator
        {
            public ITank FactoryMethod(string name)
            {
                if(name == "Heavy")
                {
                    return new BlueHeavyTank();
                }
                else if(name == "Light")
                {
                    return new BlueLightTank();
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
