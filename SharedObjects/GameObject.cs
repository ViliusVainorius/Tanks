using Microsoft.JScript;
using System;
using System.Collections;
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
        Rectangle _rect;
        [XmlAttribute]
        public int X { get; set; }
        [XmlAttribute]
        public int Y { get; set; }
        [XmlAttribute]
        public int Width { get; set; }
        [XmlAttribute]
        public int Height { get; set; }
        public int CanvasId { get; set; }

        protected GameObject() { }

        protected GameObject(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public abstract void Draw();
        public abstract void GetDimensions();
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

        public GameObject CheckCollision(GameObject[] gameObjects)
        {
            Collection collection = new Collection(gameObjects);
            Iterator iterator = collection.CreateIterator();

            for(GameObject gameObject = iterator.First(); !iterator.IsDone; gameObject = iterator.Next())
            {
                if (this != gameObject && gameObject.Intersect(this))
                {
                    return gameObject;
                }
            }

            return null;
        }

        public Rectangle Rectangle => _rect ?? (_rect = GetNewRectangle());
    }
}
