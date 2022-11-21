using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Media;

namespace SharedObjects
{
    public class TripleShotPowerup : Powerup
    {
        public TripleShotPowerup(int x, int y, int width, int height) : base(x, y, width, height)
        {

        }

        public override void PickUp(ref Tank tank)
        {
            tank.hasTripleShoot = true;
        }

        public override Rectangle GetNewRectangle()
        {
            Rectangle rec = new Rectangle()
            {
                Width = this.Width,
                Height = this.Height,
                Fill = Brushes.Green,
                Stroke = Brushes.Orange,
                StrokeThickness = 2,
                Tag = "Powerup",
            };

            return rec;
        }
    }
}
