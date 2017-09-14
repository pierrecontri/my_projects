using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ServeurPierre
{
    class Program
    {
        static int portServeur = 4055;
        static int tailleBuffer = 256;
        static bool arretServeur = false;

        static void Main(string[] args)
        {
            // Creation du socket serveur
            TcpListener server = null;
            try
            {
                server = new TcpListener(new IPEndPoint(IPAddress.Parse("127.0.0.1"), portServeur));
                // ecoute du socket
                server.Start();

                Console.WriteLine("Choisissez votre type de connexion (T ou M)");
                string typeConn = Console.ReadLine();

                Thread tArretServeur = new Thread(new ThreadStart(demandeArretServeur));
                tArretServeur.Start();

                // Enter the listening loop.
                while (!arretServeur)
                {

                    Console.Write("Attente d'une connexion ... ");
                    TcpClient client = server.AcceptTcpClient();
                    if ("T".Equals(typeConn))
                    {
                        ParameterizedThreadStart pStart = new ParameterizedThreadStart(dialogueClient);
                        Thread tClient = new Thread(pStart);
                        tClient.Start(client);
                    }
                    else
                        dialogueClient(client);
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                // Arret d'ecoute du client
                server.Stop();
            }

        }

        static void demandeArretServeur()
        {
            arretServeur = "bye".Equals(Console.ReadLine());
        }

        static void dialogueClient(object obj)
        {
            try
            {
                TcpClient client = (TcpClient)obj;
                string data = null;
                byte[] bytes = new Byte[tailleBuffer];

                // Perform a blocking call to accept requests.
                // You could also user server.AcceptSocket() here.
                Console.WriteLine("Connecté !");

                data = null;

                // Flux d'echange de donnees
                NetworkStream stream = client.GetStream();

                int i;
                while (!"BYE".Equals(data))
                {
                    // Boucle de reception des donnees
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        // transformations text => byte
                        data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                        Console.WriteLine("Reception de : {0}", data);

                        // Transformation pour le client
                        data = data.ToUpper();

                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

                        // ecriture de la reponse.
                        stream.Write(msg, 0, msg.Length);
                        stream.Flush();
                        Console.WriteLine("Envoie de : {0}", data);
                    }
                }
                // Arret du client
                client.Close();
                Console.WriteLine("Client déconnecté");
            }
            catch (Exception /*ioe*/) { }
        }
    }
}

