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
    internal class Tank: Unit
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int Top { get; set; }
        public int Left { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public Tank(int width, int height, 
            int top, int left,
            int x, int y)
        {
            this.Width = width;
            this.Height = height;
            this.Top = top;
            this.Left = left;
            this.X = x;
            this.Y = y;

            Rectangle rec = new Rectangle()
            {
                Width = width,
                Height = height,
                Fill = Brushes.Green,
                Stroke = Brushes.Red,
                StrokeThickness = 2,
            };

            MainWindow mainWindow = new MainWindow();
            mainWindow.MyCanvas.Children.Add(rec);
            Canvas.SetTop(rec, Top);
            Canvas.SetLeft(rec, Left);
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
