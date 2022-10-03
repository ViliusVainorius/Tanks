using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects
{
    public class Packet
    {
        public EndPoint Endpoint;
        public byte[] Data;

        public Packet(EndPoint endpoint, byte[] data)
        {
            Endpoint = endpoint;
            Data = data;
        }
    }
}
