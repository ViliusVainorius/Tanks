using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects
{
    public class ActionController
    {
        Command slot;

        public ActionController()
        {
            slot = new NoCommand();
        }

        public void SetCommand(Command command)
        {
            slot = command;
        }

        public void Execute()
        {
            slot.execute();
        }
    }
}
