using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects
{
    /// The 'Target' class
    public class CommandMove
    {
        public Tank tank;

        public CommandMove(Tank tank)
        {
            this.tank = tank;
        }

        public virtual void Execute() { }

    }
}
