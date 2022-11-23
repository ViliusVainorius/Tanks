using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects
{
    public abstract class Command
    {
        public abstract void execute();
        public void CheckCollisionWithEnemy(System.Drawing.Rectangle newPosition, ref GameObject obstacle, ref bool intersects)
        {
            // get other player coordinates and check for collision with my tank
            Tank[] tanks = GameSession.Instance.GameObjectContainer.Tanks;
            int myTankIndex = GameSession.Instance.self;
            for (int i = 0; i < tanks.Length; i++)
            {
                // if my tank then dont check for collisions
                if (myTankIndex == i)
                    continue;

                if (tanks[i].Intersect(newPosition))
                {
                    obstacle = tanks[i];
                    intersects = true;
                    break;
                }
            }
        }
        public void CheckCollisionWithWalls(System.Drawing.Rectangle newPosition, ref GameObject obstacle, ref bool intersects)
        {
            Wall[] walls = GameSession.Instance.GameObjectContainer.Walls;

            foreach (Wall wall in walls)
            {
                if (wall.Intersect(newPosition))
                {
                    obstacle = wall;
                    intersects = true;
                    break;
                }
            }
        }
    }
}
