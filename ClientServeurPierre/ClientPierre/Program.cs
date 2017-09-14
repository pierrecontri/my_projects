using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace ClientPierre
{
    class Program
    {
        static int portServeur = 4055;
        static int tailleBuffer = 256;

        static void Main(string[] args)
        {
            // Creation du socket
            TcpClient client = null;
            try
            {
                client = new TcpClient();
                // connexion du socket
                client.Connect("localhost", portServeur);
                // boucler tant que pas "bye"
                string data = string.Empty;
                NetworkStream stream = client.GetStream();
                Console.WriteLine("Client connecté");
                while (!"BYE".Equals(data))
                {
                    // recuperation de la commande clavier
                    data = Console.ReadLine();
                    byte[] bufferQuestion = new byte[tailleBuffer];
                    bufferQuestion = System.Text.ASCIIEncoding.ASCII.GetBytes(data);
                    stream.Write(bufferQuestion, 0, bufferQuestion.Length);
                    stream.Flush();
                    byte[] bufferReponse = new byte[tailleBuffer];
                    int i = 0;
                    //while ((i = stream.Read(bufferReponse, 0, bufferReponse.Length)) != 0)
                    //{
                    i = stream.Read(bufferReponse, 0, bufferReponse.Length);
                    // transformations text => byte
                    data = System.Text.Encoding.ASCII.GetString(bufferReponse, 0, i);
                    //}
                    Console.WriteLine("Réponse serveur : " + data);
                }
                // arret du tube
                stream.Close();
            }
            catch (SocketException /*sockEx*/)
            { }
            catch (Exception /*ex*/)
            { }
            finally
            {
                // arret du client
                if(client != null) client.Close();
            }
        }
    }
}
