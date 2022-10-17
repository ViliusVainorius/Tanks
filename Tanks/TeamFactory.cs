using SharedObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks
{
    public abstract class TeamFactory : Tank
    {
        public abstract Tank GetTank(string name);
    }
}
