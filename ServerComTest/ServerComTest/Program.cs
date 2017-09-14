using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace ServerComTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            IPAddress addr = new IPAddress(new byte[] { 127, 0, 0, 1});
            TcpListener listener = new TcpListener(addr, 2030);
            listener.Start();
            char escapeCar = ' ';
            while (escapeCar != 'q')
            {
                TcpClient client = listener.AcceptTcpClient();
                NetworkStream flux = client.GetStream();
                byte[] octets = new byte[512];
                int sizeReceive = 0;
                if (flux.CanRead)
                    sizeReceive = flux.Read(octets, 0, octets.Length);
                if (flux.CanWrite)
                    flux.Write(octets, 0, sizeReceive);
                flux.Close();
                escapeCar = (char)octets[0];
            }
            listener.Stop();
            Console.WriteLine("Arrêt du server, appuyez sur une touche pour quitter ...");
            Console.ReadKey();
        }
    }
}
