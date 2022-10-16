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
        public Wall() { }
        public Wall(int x, int y, int width, int height) : base(x, y, width, height) { }

        public override void draw()
        {
            throw new NotImplementedException();
        }

        public override void getDimensions()
        {
            throw new NotImplementedException();
        }

        public Rectangle createWall()
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
