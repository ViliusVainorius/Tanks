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
        public int Top { get; set; }
        public int Left { get; set; }

        public Tank(int width, int height, int top, int left)
        {
            this.Width = width;
            this.Height = height;
            this.Top = top;
            this.Left = left;

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
    }
}
