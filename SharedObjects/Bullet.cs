using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects
{
    public class Bullet : GameObject
    {
        public double speed { get; set; }

        public Bullet(int x, int y, int width, int height, double speed) : base(x, y, width, height)
        {
            this.speed = speed;
        }

        public override void draw()
        {
            throw new NotImplementedException();
        }

        public override void getDimensions()
        {
            throw new NotImplementedException();
        }
    }
}
