using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace SharedObjects
{
    public class Powerup : Artifact
    {
        protected Powerup(int x, int y, int width, int height) : base(x, y, width, height)
        {

        }

        public override void draw()
        {
            throw new NotImplementedException();
        }

        public override void getDimensions()
        {
            throw new NotImplementedException();
        }

        public virtual void PickUp(ref Tank tank)
        {

        }

        public override Rectangle GetNewRectangle()
        {
            return null;
        }
    }
}
