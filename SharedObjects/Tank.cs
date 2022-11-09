using System;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace SharedObjects
{
    public class Tank : Unit
    {
        private Rectangle rect;
        public bool hasTripleShoot;

        [XmlAttribute]
        public int speed;

        public Rectangle Rectangle
        {
            get
            {
                if(rect == null)
                {
                    rect = GetRectangle();
                }

                return rect;
            }
        }

        public Tank() { }
        public Tank(int x, int y, int width, int height) : base(x, y, width, height) {

            this.hasTripleShoot = false;
        }

        private Rectangle GetRectangle()
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

        public override void draw()
        {
            throw new NotImplementedException();
        }

        public override void getDimensions()
        {
            throw new NotImplementedException();
        }

        public void shoot()
        {
            if (hasTripleShoot)
            { 
                Console.WriteLine("triple shoot!");
            }
            else
            {
                Console.WriteLine("simple shoot!");
            }
        }
    }
}
