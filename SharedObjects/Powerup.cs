using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects
{
    public class Powerup : Artifact
    {
        public PowerupType type { get; set; }

        public Powerup(int x, int y, int width, int height, PowerupType type) : base(x, y, width, height)
        {
            this.type = type;
        }

        public enum PowerupType
        {
            Live, 
            TripleShoot,
            Mine
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
