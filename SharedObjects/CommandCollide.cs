using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects
{
    /// The 'Adaptee' class
    /// here we have methods, related to moving. It doesnt matter what kind of object moves
    public class CommandCollide
    {
        public void CheckCollisionWithEnemy(System.Drawing.Rectangle newPosition,
            ref GameObject obstacle, ref bool intersects, Player player = null)
        {
            // get other player coordinates and check for collision with my tank
            Tank[] tanks = GameSession.Instance.GameObjectContainer.Tanks;
            if (player != null)
            {
                int myTankIndex = GameSession.Instance.GetPlayerIndex(player);
                int enemyTankIndex = myTankIndex == 0 ? 1 : 0; // enemy index is opposite to 'my' index

                if (tanks[enemyTankIndex].Intersect(newPosition))
                {
                    obstacle = tanks[enemyTankIndex];
                    intersects = true;
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
