﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects
{
    public interface BulletElement
    {
        Tank addBulletFire(Visitor bulletFire);
    }
}
