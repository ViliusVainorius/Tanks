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
        CommandShoot slot_shoot;

        public ActionController()
        {
            slot = new NoCommand();
        }

        public void SetCommand(CommandMove command)
        {
            slot = command;
        }
        public void SetCommand(CommandShoot command)
        {
            slot_shoot = command;
        }

        public void Execute()
        {
            if (slot != null)
                slot.execute();
            if (slot_shoot != null)
                slot_shoot.execute();
        }
    }
}
