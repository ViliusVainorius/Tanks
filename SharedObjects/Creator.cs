using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects
{
    public abstract class Creator
    {
        public abstract Tank factoryMethod(string UserInput, int x, int y, int width, int height);
    }
}
