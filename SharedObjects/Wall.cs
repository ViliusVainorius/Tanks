using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Xml.Serialization;

namespace SharedObjects
{
    public class Wall: Obstacle
    {
        public Wall(int x, int y, int width, int height) : base(x, y, width, height) { }

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
                Fill = Brushes.Black,
                Tag = "wall",
            };

            return rec;
        }
    }
}
