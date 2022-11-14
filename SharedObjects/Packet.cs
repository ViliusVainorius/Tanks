using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace SharedObjects
{
    public class Packet
    {
        public EndPoint Endpoint;
        public byte[] Data;
        private const int PacketSize = 1256;

        public Packet(byte[] data)
        {
            Endpoint = null;
            Data = data;
        }

        public Packet(EndPoint endpoint, byte[] data)
        {
            Endpoint = endpoint;
            Data = data;
        }

        public static Packet ReceiveData(Socket socket)
        {
            byte[] data = new byte[PacketSize];
            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            EndPoint Remote = (EndPoint)(sender);
            try
            {
                int recv = socket.ReceiveFrom(data, ref Remote);
            }
            catch
            {
                return null;
            }

            if (Remote == (EndPoint)sender)
                return null;

            for(int i = 0; i < PacketSize; i++)
            {
                if (data[i] == 0x00)
                {
                    byte[] temp = new byte[i];
                    Array.Copy(data, temp, i);
                    data = temp;
                    break;
                }
            }

            return new Packet(Remote, data);
        }

        public static Packet ReceiveDataFrom(Socket socket, EndPoint endpoint)
        {
            byte[] data = new byte[PacketSize];
            try
            {
                int recv = socket.ReceiveFrom(data, ref endpoint);
            }
            catch
            {
                return null;
            }

            for (int i = 0; i < PacketSize; i++)
            {
                if (data[i] == 0x00)
                {
                    byte[] temp = new byte[i];
                    Array.Copy(data, temp, i);
                    data = temp;
                    break;
                }
            }

            return new Packet(endpoint, data);
        }

        public void SendData(Socket socket)
        {
            socket.Send(Data);
        }

        public void SendToEndPoint(Socket socket)
        {
            socket.SendTo(Data, Endpoint);
        }
    }
}
