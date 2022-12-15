using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects
{
    public class TripleBulletElement : BulletElement
    {
        public Tank addBulletFire(Visitor bulletFire)
        {
            return bulletFire.AddPng(this);
        }
    }
}
