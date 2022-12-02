using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects
{
    public class StartGamePacket
    {
        public string fileName;
        public int self;

        public StartGamePacket()
        {
        }

        public StartGamePacket(string fileName, int self)
        {
            this.fileName = fileName;
            this.self = self;
        }
    }
}
