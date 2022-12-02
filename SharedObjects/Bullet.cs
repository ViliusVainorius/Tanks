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
        public FacingSide side;

        public Bullet(int x, int y, int width, int height, int speed, int bulletId, FacingSide side = FacingSide.Right) : base(x, y, width, height)
        {
            this.speed = speed;
            this.bulletId = bulletId;
            this.side = side;
        }

        public override void Draw()
        {
            throw new NotImplementedException();
        }

        public override void GetDimensions()
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
                RadiusX = 50,
                RadiusY = 50,
            };

            return rec;
        }

        public int MoveX(FacingSide fs)
        {
            int x = 0;

            if (fs == FacingSide.Right)
            {
                x = 1;
            }
            else if (fs == FacingSide.Left)
            {
                x = -1;
            }

            return x;
        }

        public int MoveY(FacingSide fs)
        {
            int y = 0;

            if (fs == FacingSide.Up)
            {
                y = 1;
            }
            else if (fs == FacingSide.Down)
            {
                y = -1;
            }

            return y;
        }
    }
}
