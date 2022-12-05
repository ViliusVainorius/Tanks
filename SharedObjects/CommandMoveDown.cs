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
    public class CommandMoveDown : CommandMove
    {
        public CommandMoveDown(Tank tank) : base(tank)
        {
        }

        public override void MoveOneTime()
        {
            tank.Y += tank.speed;
        }

        public override void ChangeDirection()
        {
            tank.side = FacingSide.Down;
        }

        public override void MoveBack(GameObject[] objects)
        {
            GameObject obstacleWalls = null;
            obstacleWalls = tank.CheckCollision(objects);
            if (obstacleWalls != null)
            {
                tank.Y = obstacleWalls.Y - tank.Height;
            }
        }

    }
}
