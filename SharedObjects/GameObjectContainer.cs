using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects
{
    public class GameObjectContainer
    {
        public Tank[] Tanks;
        public Wall[] Walls;

        public void Update(GameObjectContainer gameObjectContainer)
        {
            for(int i = 0; i < Tanks.Count(); i++)
            {
                Tanks[i].X = gameObjectContainer.Tanks[i].X;
                Tanks[i].Y = gameObjectContainer.Tanks[i].Y;
                Tanks[i].Rotation = gameObjectContainer.Tanks[i].Rotation;
            }
        }
    }
}
