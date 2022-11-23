using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects
{
    public class ActionController
    {
        CommandMove slot;

        public ActionController()
        {
            slot = new NoCommand();
        }

        public void SetCommand(CommandMove command)
        {
            slot = command;
        }

        public void Execute()
        {
            slot.execute();
        }
    }
}
