using SharedObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Tanks
{
    public class Tank: Unit
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public Tank(int width, int height,
            int x, int y)
        {
            this.Width = width;
            this.Height = height;
            this.X = x;
            this.Y = y;

        }

        public Rectangle createTank()
        {
            Rectangle rec = new Rectangle()
            {
                Width = this.Width,
                Height = this.Height,
                Fill = Brushes.Green,
                Stroke = Brushes.Red,
                StrokeThickness = 2,
                Tag = "Player",
            };

            return rec;
        }

        public void draw()
        {
            throw new NotImplementedException();
        }

        public void getDimensions()
        {
            throw new NotImplementedException();
        }
        public void shoot()
        {
            throw new NotImplementedException();
        }
    }
}
