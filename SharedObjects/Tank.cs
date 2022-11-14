using System;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows;
using System.Windows.Shapes;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace SharedObjects
{
    public class Tank : Unit
    {
        private Rectangle rect;
        public bool hasTripleShoot;

        [XmlAttribute]
        public int speed;
        public int lives;
        [XmlIgnore]
        public Player player;
        public int Rotation;

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
            this.lives = 3;
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
        public void PickPowerup(Powerup p)
        {
            if (p.type is Powerup.PowerupType.Live)
            {
                this.lives++;
            }

            else if (p.type is Powerup.PowerupType.TripleShoot)
            {
                // set player's tank hasTripleShoot value to true
            }
        }
        public void GetShot()
        {
            this.lives--;
            if (lives == 0)
            {
                Console.Write("Game over!");
            }
        }

        public void TankCollision(Tank tank2)
        {
            Rect Tank1Hitbox = new Rect(this.X, this.Y, this.Width, this.Height);
            Rect Tank2Hitbox = new Rect(tank2.X, tank2.Y, tank2.Width, tank2.Height);

            


        }

        public void MaxSpawnedItems(List<Powerup> powerups)
        {
            if(powerups.Count < 10)
            {
                Powerup powerup = new Powerup(1, 1, 1, 1, Powerup.PowerupType.Live);

            }
            else if (powerups.Count < 11)
            {
                return;
            }
            else
            {
                powerups.RemoveAt(powerups.Count - 1);
            }
        }
    }
}
