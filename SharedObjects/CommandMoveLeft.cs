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
    public class CommandMoveLeft: CommandMove
    {
        Tank tank;
        Player player;

        public CommandMoveLeft(Tank tank, Player player = null) : base()
        {
            this.tank = tank;
            this.player = player;
        }

        public override void execute()
        {
            System.Drawing.Rectangle newPosition = new System.Drawing.Rectangle(tank.X - tank.speed, tank.Y, tank.Width, tank.Height);

            CommandCollide collisions = new CommandCollide();
            GameObject obst = obstacle;
            bool inter = intersects;
            collisions.CheckCollisionWithWalls(newPosition, ref obst, ref inter);
            collisions.CheckCollisionWithEnemy(newPosition, ref obst, ref inter, player);
            obstacle = obst;
            intersects = inter;

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
            tank.side = FacingSide.Left;
        }
    }
}
