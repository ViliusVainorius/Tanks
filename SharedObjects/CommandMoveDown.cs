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
    public class CommandMoveDown : Command
    {
        Tank tank;

        public CommandMoveDown(Tank tank)
        {
            this.tank = tank;
        }

        public void execute()
        {
            Wall[] walls = GameSession.Instance.GameObjectContainer.Walls;
            System.Drawing.Rectangle newPosition = new System.Drawing.Rectangle(tank.X, tank.Y + tank.speed, tank.Width, tank.Height);
            bool intersects = false;
            Wall obstacle = null;

            foreach (Wall wall in walls)
            {
                if (wall.Intersect(newPosition))
                {
                    obstacle = wall;
                    intersects = true;
                    break;
                }
            }

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
        }
    }
}
