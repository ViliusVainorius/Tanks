using SharedObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks
{
    public class ConcreteTeamFactory : TeamFactory
    {
        public override Tank GetTank(string name)
        {
            switch (name)
            {
                case "Heavy":
                    return new BlueHeavyTank();
                case "Light":
                    return new BlueLightTank();
                default:
                    throw new ApplicationException(String.Format
                        ("'{0}' Tank cannot be created", name));
            }
        }
    }
}
