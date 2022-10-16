using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects
{
    public abstract class Obstacle: GameObject
    {
        protected Obstacle() { }
        protected Obstacle(int x, int y, int width, int height) : base(x, y, width, height) { }
    }
}
