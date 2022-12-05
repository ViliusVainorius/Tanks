﻿using System;
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
        public CommandMoveDown(Tank tank) : base(tank) { }

        public override void Execute()
        {
            tank.Y += tank.speed;
            GameObject obstacle = null;
            tank.side = FacingSide.Down;

            obstacle = tank.CheckCollision(GameSession.Instance.GameObjectContainer.Walls);

            if (obstacle != null)
            {
                tank.Y = obstacle.Y - tank.Height;
            }

            obstacle = tank.CheckCollision(GameSession.Instance.GameObjectContainer.Tanks);

            if(obstacle != null)
            {
                tank.Y = obstacle.Y - tank.Height;
            }
        }
    }
}
