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

        // template pattern - method has its steps, which are implemented in subclasses
        public void Execute()
        {
            MoveOneTime();
            ChangeDirection();
            MoveBack(GameSession.Instance.GameObjectContainer.Walls);
            MoveBack(GameSession.Instance.GameObjectContainer.Tanks);
        }

        public virtual void MoveOneTime()
        {

        }
        public virtual void ChangeDirection()
        {

        }

        public virtual void MoveBack(GameObject[] objects)
        {

        }

    }
}
