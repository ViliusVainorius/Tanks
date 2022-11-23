using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects
{
    public class CommandShoot : CommandCollide
    {
        Tank tank;

        public CommandShoot(Tank tank)
        {
            this.tank = tank;
        }

        public override void execute()
        {
            System.Drawing.Rectangle newPosition = new System.Drawing.Rectangle(tank.X, tank.Y + tank.speed, tank.Width, tank.Height);
            GameObject obstacle = null;
            bool intersects = false;

            CheckCollisionWithWalls(newPosition, ref obstacle, ref intersects);
            CheckCollisionWithEnemy(newPosition, ref obstacle, ref intersects);

            int y = -1;

            if (intersects)
            {
                y = obstacle.Y - tank.Height;
            }
            else
            {
                y = tank.Y + tank.speed;
            }

            tank.Y = y;
            tank.side = FacingSide.Down;
        }
    }
}
