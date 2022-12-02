using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects
{
    public abstract class Unit : GameObject
    {
        protected Unit(int x, int y, int width, int height) : base(x, y, width, height)
        {
        }
    }
}
