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
        public bool intersects { get; set; }
        public GameObject obstacle { get;set;}

        public CommandMove()
        {
            intersects = false;
            obstacle = null;
        }

        public virtual void execute() { }

    }
}
