using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects
{
    public class Powerup : Artefact
    {
        public enum PowerupType
        {
            Live, 
            TripleShoot
        }
        public int X { get; set; }
        public int Y { get; set; }
        public PowerupType type { get; set; }

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
