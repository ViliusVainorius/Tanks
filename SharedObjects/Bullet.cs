using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Media;

namespace SharedObjects
{
    public class Bullet : GameObject
    {
        public int speed;
        public int bulletId;

        public Bullet(int x, int y, int width, int height, int speed, int bulletId) : base(x, y, width, height)
        {
            this.speed = speed;
            this.bulletId = bulletId;
        }

        public override void draw()
        {
            throw new NotImplementedException();
        }

        public override void getDimensions()
        {
            throw new NotImplementedException();
        }

        public override Rectangle GetNewRectangle()
        {
            Rectangle rec = new Rectangle()
            {
                Width = this.Width,
                Height = this.Height,
                Fill = Brushes.Green,
                Stroke = Brushes.Purple,
                StrokeThickness = 2,
                Tag = "Bullet",
            };

            return rec;
        }
    }
}
