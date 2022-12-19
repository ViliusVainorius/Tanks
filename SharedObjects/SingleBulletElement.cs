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
        DateTime timeShot = DateTime.Now;
        public Tank addBulletFire(Visitor bulletFire)
        {
            return bulletFire.AddPng(this);
        }

    }
}
