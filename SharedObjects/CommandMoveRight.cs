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
        public CommandMoveRight(Tank tank) : base(tank) { }

        public override void Execute()
        {
            tank.X += tank.speed;
            GameObject obstacle = null;
            tank.side = FacingSide.Right;

            obstacle = tank.CheckCollision(GameSession.Instance.GameObjectContainer.Walls);

            if (obstacle != null)
            {
                tank.X = obstacle.X - tank.Width;
            }

            obstacle = tank.CheckCollision(GameSession.Instance.GameObjectContainer.Tanks);

            if (obstacle != null)
            {
                tank.X = obstacle.X - tank.Width;
            }
        }
    }
}
