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
        public bool hasTripleShoot;

        [XmlAttribute]
        public int speed;
        [XmlAttribute]
        public int lives;
        [XmlIgnore]
        public Player player;
        public string Skin;
        public int Rotation;
        public FacingSide side;
        public DateTime tripleshootstartime { get; set; }
        public string State { get; set; }

        public Tank(int x, int y, int width, int height, int speed, string skin) : base(x, y, width, height) {

            this.hasTripleShoot = false;
            this.lives = 3;
            this.speed = speed;
            this.Skin = skin;
        }

        public override Rectangle GetNewRectangle()
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

        public override void Draw()
        {
            throw new NotImplementedException();
        }

        public override void GetDimensions()
        {
            throw new NotImplementedException();
        }

        public void Shoot()
        {
            Console.WriteLine(hasTripleShoot ? "triple shoot!" : "simple shoot!");
        }
        /*public void PickPowerup(Powerup p)
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

        public void MaxSpawnedItems(List<Powerup> powerups)
        {
            if(powerups.Count < 10)
            {
                Powerup powerup = new Powerup(1, 1, 1, 1, Powerup.PowerupType.Live);
                powerups.Add(powerup);
            }
            else if (powerups.Count < 11)
            {
                return;
            }
            else
            {
                powerups.RemoveAt(powerups.Count - 1);
            }
        }*/

        public bool MineDamage()
        {
            bool p = true;
            this.lives = this.lives-1;
            
            return p;
        }
    }
}
