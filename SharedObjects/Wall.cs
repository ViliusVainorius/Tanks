﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects
{
    public class Wall: Obstacle
    {
        public int height { get; set; }
        public int width { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public void draw()
        {
            throw new NotImplementedException();
        }

        public void getDimensions()
        {
            throw new NotImplementedException();
        }
    }
}
