using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;

namespace SharedObjects
{
    public class HealthPowerup : Powerup
    {
        public HealthPowerup(int x, int y, int width, int height) : base(x, y, width, height)
        {

        }

        public override void PickUp(ref Tank tank)
        {
            tank.lives = tank.lives + 1;// tank.lives + 1 >= 3 ? 3 : tank.lives + 1;
        }

        public override Rectangle GetNewRectangle()
        {
            Rectangle rec = new Rectangle()
            {
                Width = this.Width,
                Height = this.Height,
                Fill = Brushes.Red,
                Stroke = Brushes.Orange,
                StrokeThickness = 2,
                Tag = "Powerup",
            };

            return rec;
        }
    }
}
