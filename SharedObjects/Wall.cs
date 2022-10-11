using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Media;

namespace SharedObjects
{
    public class Wall: Obstacle
    {
        public int height { get; set; }
        public int width { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public Wall(int height, int width, int x, int y)
        {
            this.height = height;
            this.width = width;
            this.X = x;
            this.Y = y;
        }

        public void draw()
        {
            throw new NotImplementedException();
        }

        public void getDimensions()
        {
            throw new NotImplementedException();
        }


        public Rectangle createWall()
        {
            Rectangle rec = new Rectangle()
            {
                Width = this.width,
                Height = this.height,
                Fill = Brushes.Black,
                Tag = "wall",
            };

            return rec;
        }
    }
}
