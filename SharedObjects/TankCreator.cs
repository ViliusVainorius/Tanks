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
        public Team factoryMethod(string input)
        {
            if (input.Equals("R"))
                return new RedTeam();
            if(input.Equals("B"))
                return new BlueTeam();
            return null;
        }
    }
}
