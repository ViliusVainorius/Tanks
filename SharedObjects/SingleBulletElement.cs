using Microsoft.JScript;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects
{
    public class SingleBulletElement : BulletElement
    {
        public double addBulletFire(Visitor bulletFire)
        {
            return bulletFire.AddPng(this);
        }

    }
}
