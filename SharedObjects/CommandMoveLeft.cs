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
    public class CommandMoveLeft : Command
    {
        Tank tank;

        public CommandMoveLeft(Tank tank)
        {
            this.tank = tank;
        }

        public void execute()
        {
            Wall[] walls = GameSession.Instance.GameObjectContainer.Walls;
            System.Drawing.Rectangle newPosition = new System.Drawing.Rectangle(tank.X - tank.speed, tank.Y, tank.Width, tank.Height);
            bool intersects = false;
            Wall obstacle = null;

            foreach (Wall wall in walls)
            {
                if(wall.Intersect(newPosition))
                {
                    obstacle = wall;
                    intersects = true;
                    break;
                }
            }

            int x = -1;

            if (intersects)
            {
                x = obstacle.X + obstacle.Width;
            }
            else
            {
                x = tank.X - tank.speed;
            }

            tank.X = x;
        }
    }
}
