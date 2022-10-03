using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects
{
    public class Bullet : GameObject
    {
        public int X { get; set; }
        public int Y { get; set; }
        public double speed { get; set; }

        public void draw()
        {
            throw new NotImplementedException();
        }

        public void getDimensions()
        {
            throw new NotImplementedException();
        }
    }
}
