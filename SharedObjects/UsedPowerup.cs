using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Media;

namespace SharedObjects
{
    public class UsedPowerup : Powerup
    {
        public Rectangle _rectangle;
        public UsedPowerup() : base(0, 0, 0, 0)
        {

        }

        public override void PickUp(ref Tank tank)
        {
            
        }

        public override Rectangle GetNewRectangle()
        {
            return _rectangle;
        }
    }
}
