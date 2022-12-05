using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace SharedObjects
{
    /// The 'Adapter' class
    public class CommandMoveUp : CommandMove
    {
        public CommandMoveUp(Tank tank) : base(tank) { }


        public override void MoveOneTime()
        {
            tank.Y -= tank.speed;
        }

        public override void ChangeDirection()
        {
            tank.side = FacingSide.Up;
        }

        public override void MoveBack(GameObject[] objects)
        {
            GameObject obstacle = null;
            obstacle = tank.CheckCollision(objects);
            if (obstacle != null)
            {
                tank.Y = obstacle.Y + obstacle.Height;
            }
        }
    }
}
