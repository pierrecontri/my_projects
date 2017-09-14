using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace TestsUnitaires
{
    class SerializationMemory : IDisposable
    {
        MemoryStream ms = null;
        public const int TAILLE_MAX_MEM = 800000000;
        // Constructeur
        public SerializationMemory()
        {
            ms = new MemoryStream();
        }

        public void SerializeMemoryObject()
        {
            Random rnd = new Random();

            BinaryFormatter bf = new BinaryFormatter();
            try
            {
                // se positionner en fin de fichier
                ms.Position = ms.Length;
                for (UInt16 i = UInt16.MinValue; i < UInt16.MaxValue; i++)
                {
                    // si la taille memoire est atteinte sur le fichier,
                    // passer a un autre fichier
                    Article LivreDotNet = new Article(rnd.Next().ToString().PadLeft(15) + " : Les bases du Framework .Net 2.0", 38.54, 5.5);
                    Article HautParleurs = new Article(rnd.Next().ToString().PadLeft(15) + " : Haut-Parleurs M-Audio", 82.78, 19.6);
                    bf.Serialize(ms, LivreDotNet);
                    bf.Serialize(ms, HautParleurs);
                    //Console.WriteLine(i);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Source + " " + ex.Message);
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.TargetSite);
                Console.WriteLine(ms.Position + " " + ms.Length + " " + ms.Capacity);
                Console.WriteLine("Tappez une touche ...");
                Console.ReadKey();
            }
        }

        public void DeserializeMemoryObject()
        {
            BinaryFormatter bf = new BinaryFormatter();
            // Retourner au début du fichier
            ms.Position = 0;
            while (ms.Position < ms.Length)
                Console.WriteLine(((Article)bf.Deserialize(ms)).ToString());
        }

        public void SaveOnFile(String nameFile)
        {
            FileStream fs = new FileStream(nameFile, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            BinaryFormatter bf = new BinaryFormatter();
            // Retourner au début du fichier
            ms.Position = 0;
            while (ms.Position < ms.Length)
                sw.WriteLine(((Article)bf.Deserialize(ms)).ToString());
            sw.WriteLine("Happy End");
            sw.Close();
            fs.Close();
        }

        // Sauvegarde de la serialisation sur fichier XML()
        public Boolean trySaveOnFile()
        {
            try
            {
                // creation du fichier
                FileStream fs = new FileStream(@"C:\Temp\testSerializationXml.xml", FileMode.Create);
                // creation d'un fichier memoire
                MemoryStream msXml = new MemoryStream();
                BinaryFormatter bf = new BinaryFormatter();
                XmlSerializer serialXml = new XmlSerializer(typeof(Article));

                ms.Position = 0;
                while (ms.Position < ms.Length)
                {
                    Article art = (Article)bf.Deserialize(ms);
                    serialXml.Serialize(msXml, art);
                    Console.WriteLine(art.ToString());
                }

                // Ecriture sur fichier physique
                msXml.Flush();
                msXml.WriteTo(fs);
                msXml.Close();
                fs.Close();

                // reponse
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Source + " " + ex.Message);
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.TargetSite);
                Console.WriteLine("Tappez une touche ...");
                // reponse
                return false;
            }
        }

        public static void Test(object o)
        {
            SerializationMemory sm = new SerializationMemory();
            sm.SerializeMemoryObject();
            sm.DeserializeMemoryObject();
            //sm.SaveOnFile(@"C:\Temp\testArtcles.txt");
            //sm.trySaveOnFile();
            sm.Dispose();
        }

        // Destructeur
        #region IDisposable Membres

        public void Dispose()
        {
            if (ms != null)
                ms.Close();
        }

        #endregion
    }

    [Serializable]
    class Article : IDeserializationCallback
    {
        public String Name = "";
        public Double PrixHT = 0.0;
        public Double TVA = 0.0;
        [NonSerialized]
        public Double PrixTTC = 0.0;

        public Article(String name, Double prixHT, Double tva)
        {
            this.Name = name;
            this.PrixHT = prixHT;
            this.TVA = tva;
            this.PrixTTC = CalculPrixTTC();
        }

        public Double CalculPrixTTC()
        {
            return (this.PrixHT * (1 + this.TVA / 100));
        }

        public override string ToString()
        {
            return this.Name + " : " + this.PrixTTC.ToString("F2") + " euros";
        }

        #region IDeserializationCallback Membres

        public void OnDeserialization(object sender)
        {
            this.PrixTTC = this.CalculPrixTTC();
        }

        #endregion
    }
}
