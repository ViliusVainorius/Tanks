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
    public class CommandMoveRight : Command
    {
        Tank tank;

        public CommandMoveRight(Tank tank)
        {
            this.tank = tank;
        }

        public override void execute()
        {
            System.Drawing.Rectangle newPosition = new System.Drawing.Rectangle(tank.X + tank.speed, tank.Y, tank.Width, tank.Height);
            bool intersects = false;
            GameObject obstacle = null;

            CheckCollisionWithWalls(newPosition, ref obstacle, ref intersects);
            CheckCollisionWithEnemy(newPosition, ref obstacle, ref intersects);

            int x = -1;

            if (intersects)
            {
                x = obstacle.X - tank.Width;
            }
            else
            {
                x = tank.X + tank.speed;
            }

            tank.X = x;
            tank.side = FacingSide.Right;
        }
    }
}
