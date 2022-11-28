using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects
{
    public class BlueTeam : Team
    {
        
        override public AbstractFactory getAbstractFactory()
        {
            return new BlueFactory();
        }
    }
}
