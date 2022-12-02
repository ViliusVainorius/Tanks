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
    public class CommandMoveRight : CommandMove
    {
        Tank _tank;
        Player _player;

        public CommandMoveRight(Tank tank, Player player = null)
        {
            this._tank = tank;
            this._player = player;
        }

        public override void Execute()
        {
            System.Drawing.Rectangle newPosition = new System.Drawing.Rectangle(_tank.X + _tank.speed, _tank.Y, _tank.Width, _tank.Height);
            CommandCollide collisions = new CommandCollide();
            GameObject obst = Obstacle;
            bool inter = Intersects;
            collisions.CheckCollisionWithWalls(newPosition, ref obst, ref inter);
            collisions.CheckCollisionWithEnemy(newPosition, ref obst, ref inter, _player);
            Obstacle = obst;
            Intersects = inter;

            int x;

            if (Intersects)
            {
                x = Obstacle.X - _tank.Width;
            }
            else
            {
                x = _tank.X + _tank.speed;
            }

            _tank.X = x;
            _tank.side = FacingSide.Right;
        }
    }
}
