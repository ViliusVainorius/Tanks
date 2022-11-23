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
        Tank tank;

        public CommandMoveDown(Tank tank): base()
        {
            this.tank = tank;
        }

        public override void execute()
        {
            System.Drawing.Rectangle newPosition = new System.Drawing.Rectangle(tank.X, tank.Y + tank.speed, tank.Width, tank.Height);
            
            CommandCollide collisions = new CommandCollide();
            GameObject obst = obstacle;
            bool inter = intersects;
            collisions.CheckCollisionWithWalls(newPosition, ref obst, ref inter);
            collisions.CheckCollisionWithEnemy(newPosition, ref obst, ref inter);
            obstacle = obst;
            intersects = inter;

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
