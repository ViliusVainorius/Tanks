﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Media;

namespace SharedObjects
{
    public abstract class Bullet : GameObject
    {
        public double speed { get; set; }

        public Bullet(int x, int y, int width, int height, double speed) : base(x, y, width, height)
        {
            this.speed = speed;
        }

        public override void draw()
        {
            throw new NotImplementedException();
        }

        public override void getDimensions()
        {
            throw new NotImplementedException();
        }

        public override Rectangle GetNewRectangle()
        {
            Rectangle rec = new Rectangle()
            {
                Width = this.Width,
                Height = this.Height,
                Fill = Brushes.Green,
                Stroke = Brushes.Purple,
                StrokeThickness = 2,
                Tag = "Bullet",
            };

            return rec;
        }
    }
}
