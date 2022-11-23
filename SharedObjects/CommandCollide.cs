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
        public virtual void execute() { }
        public void CheckCollisionWithEnemy(System.Drawing.Rectangle newPosition, ref GameObject obstacle, ref bool intersects)
        {
            // get other player coordinates and check for collision with my tank
            Tank[] tanks = GameSession.Instance.GameObjectContainer.Tanks;
            int myTankIndex = GameSession.Instance.self;

            StreamWriter writer;
            using (writer = new StreamWriter(@"C:\Users\vytau\Documents\KTU\7 pusmetis\Objektinis programų projektavimas\Temporary.txt"))
            {
                writer.WriteLine("Index: " + myTankIndex);
            }

            for (int i = 0; i < tanks.Length; i++)
            {
                // if my tank, then dont check for collisions
                if (GameSession.Instance.self == i)
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
