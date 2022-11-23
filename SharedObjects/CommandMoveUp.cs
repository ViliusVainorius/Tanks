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
    public class CommandMoveUp : Command
    {
        Tank tank;

        public CommandMoveUp(Tank tank)
        {
            this.tank = tank;
        }

        public override void execute()
        {
            System.Drawing.Rectangle newPosition = new System.Drawing.Rectangle(tank.X, tank.Y - tank.speed, tank.Width, tank.Height);
            bool intersects = false;
            GameObject obstacle = null;

            CheckCollisionWithWalls(newPosition, ref obstacle, ref intersects);
            CheckCollisionWithEnemy(newPosition, ref obstacle, ref intersects);

            int y = -1;

            if (intersects)
            {
                y = obstacle.Y + obstacle.Height;
            }
            else
            {
                y = tank.Y - tank.speed;
            }

            tank.Y = y;
            tank.side = FacingSide.Up;
        }
    }
}
