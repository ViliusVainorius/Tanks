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
        public bool Intersects { get; set; }
        public GameObject Obstacle { get;set;}

        public CommandMove()
        {
            Intersects = false;
            Obstacle = null;
        }

        public virtual void Execute() { }

    }
}
