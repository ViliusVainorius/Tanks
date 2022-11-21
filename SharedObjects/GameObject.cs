using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace SharedObjects
{
    public abstract class GameObject
    {
        Rectangle rect;
        [XmlAttribute]
        public int X { get; set; }
        [XmlAttribute]
        public int Y { get; set; }
        [XmlAttribute]
        public int Width { get; set; }
        [XmlAttribute]
        public int Height { get; set; }
        public int CanvasID { get; set; }

        protected GameObject() { }

        protected GameObject(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public abstract void draw();
        public abstract void getDimensions();
        public abstract Rectangle GetNewRectangle();

        public bool Intersect(GameObject other)
        {
            System.Drawing.Rectangle selfRect = new System.Drawing.Rectangle(X, Y, Width, Height);
            System.Drawing.Rectangle otherRect = new System.Drawing.Rectangle(other.X, other.Y, other.Width, other.Height);

            return selfRect.IntersectsWith(otherRect);
        }

        public bool Intersect(System.Drawing.Rectangle other)
        {
            System.Drawing.Rectangle selfRect = new System.Drawing.Rectangle(X, Y, Width, Height);

            return selfRect.IntersectsWith(other);
        }

        public Rectangle Rectangle
        {
            get
            {
                if (rect == null)
                {
                    rect = GetNewRectangle();
                }

                return rect;
            }
        }
    }
}
