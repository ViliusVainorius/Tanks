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
    internal class Tank
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public Tank(int width, int height, int top, int left)
        {
            this.Width = width;
            this.Height = height;
            this.X = top;
            this.Y = left;

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
            Canvas.SetTop(rec, X);
            Canvas.SetLeft(rec, Y);
        }
    }
}
