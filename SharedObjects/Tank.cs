﻿using System;
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
        public int lives;
        [XmlIgnore]
        public Player player;
        public int Rotation;


        public Tank(int x, int y, int width, int height, int speed) : base(x, y, width, height) {

            this.hasTripleShoot = false;
            this.lives = 3;
            this.speed = speed;
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

        public override void draw()
        {
            throw new NotImplementedException();
        }

        public override void getDimensions()
        {
            throw new NotImplementedException();
        }

        public int getLives()
        {
            return this.lives;
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
