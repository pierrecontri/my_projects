using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security;
using System.Security.Cryptography;

namespace TestsUnitaires
{
    class Securisation
    {
        public static void CreeFichierClair()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Bonjour,");
            sb.AppendLine();
            sb.AppendLine("Ceci est un test de cryptage.");
            sb.AppendLine();
            sb.AppendLine("Cdt,");
            sb.Append("Pierre");
            StreamWriter sw = new StreamWriter(@".\FichierClair.txt");
            sw.Write(sb.ToString());
            sw.Close();
        }

        public static void CreationPrivateKey()
        {
            //RijndaelManaged generatKey = new RijndaelManaged();
            BlowfishNET.BlowfishAlgorithm generatKey = new BlowfishNET.BlowfishAlgorithm();
            // sauvegarde de la cle synchrone
            generatKey.GenerateKey();
            byte[] privateKey = generatKey.Key;
            FileStream sauvKey = new FileStream(@".\KeyTestSynchrone.key", FileMode.Create, FileAccess.Write);
            sauvKey.Write(privateKey, 0, privateKey.Length);
            sauvKey.Close();
            generatKey.GenerateIV();
            byte[] privateVect = generatKey.IV;
            FileStream sauvVect = new FileStream(@".\IVTestSynchrone.key", FileMode.Create, FileAccess.Write);
            sauvVect.Write(privateVect, 0, privateVect.Length);
            sauvVect.Close();
        }

        public static void TestCryptSynchrone()
        {
            CreeFichierClair();
            CreationPrivateKey();
            EncryptSynch();
            DecryptSynch();
        }

        public static void EncryptSynch()
        {
            // Creation de l'objet de cryptage
            //RijndaelManaged encrypt = new RijndaelManaged();
            BlowfishNET.BlowfishAlgorithm encrypt = new BlowfishNET.BlowfishAlgorithm();

            // Recuperer de la cle synchrone et le salt
            byte[] privateKey = null;
            FileStream readKey = new FileStream(@".\KeyTestSynchrone.key", FileMode.Open, FileAccess.Read);
            privateKey = new byte[readKey.Length];
            readKey.Read(privateKey, 0, privateKey.Length);
            readKey.Close();
            encrypt.Key = privateKey;
            byte[] privateVect = null;
            FileStream readVect = new FileStream(@".\IVTestSynchrone.key", FileMode.Open, FileAccess.Read);
            privateVect = new byte[readVect.Length];
            readVect.Read(privateVect, 0, privateVect.Length);
            readVect.Close();
            encrypt.IV = privateVect;

            // Recuperer le fichier
            FileStream fsr = new FileStream(@".\FichierClair.txt", FileMode.Open, FileAccess.Read);
            byte[] fluxClair = new byte[fsr.Length];
            fsr.Read(fluxClair, 0, fluxClair.Length);
            fsr.Close();
            // Creer le nouveau fichier crypte
            FileStream fsw = new FileStream(@".\FichierCrypt.cryp", FileMode.Create, FileAccess.Write);
            // Creer le flux de cryptage
            CryptoStream cs = new CryptoStream(fsw, encrypt.CreateEncryptor(), CryptoStreamMode.Write);
            // Crypter le fichier
            cs.Write(fluxClair, 0, fluxClair.Length);
            // Fermer les flux
            cs.Close();
            fsw.Close();
        }

        public static void DecryptSynch()
        {
            // Creation de l'objet de cryptage
            BlowfishNET.BlowfishAlgorithm decrypt = new BlowfishNET.BlowfishAlgorithm();

            // Recuperer de la cle synchrone et le salt
            byte[] privateKey = null;
            FileStream readKey = new FileStream(@".\KeyTestSynchrone.key", FileMode.Open, FileAccess.Read);
            privateKey = new byte[readKey.Length];
            readKey.Read(privateKey, 0, privateKey.Length);
            readKey.Close();
            decrypt.Key = privateKey;
            byte[] privateVect = null;
            FileStream readVect = new FileStream(@".\IVTestSynchrone.key", FileMode.Open, FileAccess.Read);
            privateVect = new byte[readVect.Length];
            readVect.Read(privateVect, 0, privateVect.Length);
            readVect.Close();
            decrypt.IV = privateVect;

            // Recuperer le fichier crypte
            FileStream fsr = new FileStream(@".\FichierCrypt.cryp", FileMode.Open, FileAccess.Read);
            byte[] fluxCrypte = new byte[fsr.Length];
            fsr.Read(fluxCrypte, 0, fluxCrypte.Length);
            fsr.Close();
            // Creer le nouveau fichier decrypte
            FileStream fsw = new FileStream(@".\FichierDeCrypt.txt", FileMode.Create, FileAccess.Write);
            // Creer le flux de decryptage
            CryptoStream cs = new CryptoStream(fsw, decrypt.CreateDecryptor(), CryptoStreamMode.Write);
            // Crypter le fichier
            cs.Write(fluxCrypte, 0, fluxCrypte.Length);
            // Fermer les flux
            cs.Close();
            fsw.Close();
        }

        public static void EncryptAndDecryptSynch()
        {
            // Creation de l'objet de cryptage
            RijndaelManaged encryptAndDecrypt = new RijndaelManaged();

            // Recuperer de la cle synchrone et le salt
            byte[] privateKey = null;
            FileStream readKey = new FileStream(@".\KeyTestSynchrone.key", FileMode.Open, FileAccess.Read);
            privateKey = new byte[readKey.Length];
            readKey.Read(privateKey, 0, privateKey.Length);
            readKey.Close();
            encryptAndDecrypt.Key = privateKey;
            byte[] privateVect = null;
            FileStream readVect = new FileStream(@".\IVTestSynchrone.key", FileMode.Open, FileAccess.Read);
            privateVect = new byte[readVect.Length];
            readVect.Read(privateVect, 0, privateVect.Length);
            readVect.Close();
            encryptAndDecrypt.IV = privateVect;

            // Recuperer le fichier
            FileStream fsr = new FileStream(@".\FichierClair.txt", FileMode.Open, FileAccess.Read);
            byte[] fluxClair = new byte[fsr.Length];
            fsr.Read(fluxClair, 0, fluxClair.Length);
            fsr.Close();
            // Creer le nouveau fichier crypte
            FileStream fsw = new FileStream(@".\FichierCrypt.cryp", FileMode.Create, FileAccess.Write);
            // Creer le flux de cryptage
            CryptoStream cs = new CryptoStream(fsw, encryptAndDecrypt.CreateEncryptor(), CryptoStreamMode.Write);
            // Crypter le fichier
            cs.Write(fluxClair, 0, fluxClair.Length);
            // Fermer les flux
            cs.Close();
            fsw.Close();

            // Recuperer de la cle synchrone
            privateKey = null;
            readKey = new FileStream(@".\KeyTestSynchrone.key", FileMode.Open, FileAccess.Read);
            privateKey = new byte[readKey.Length];
            readKey.Read(privateKey, 0, privateKey.Length);
            readKey.Close();
            encryptAndDecrypt.Key = privateKey;

            // Recuperer le fichier crypte
            fsr = new FileStream(@".\FichierCrypt.cryp", FileMode.Open, FileAccess.Read);
            byte[] fluxCrypte = new byte[fsr.Length];
            fsr.Read(fluxCrypte, 0, fluxCrypte.Length);
            fsr.Close();
            // Creer le nouveau fichier decrypte
            fsw = new FileStream(@".\FichierDeCrypt.txt", FileMode.Create, FileAccess.Write);
            // Creer le flux de decryptage
            cs = new CryptoStream(fsw, encryptAndDecrypt.CreateDecryptor(), CryptoStreamMode.Write);
            // Crypter le fichier
            cs.Write(fluxCrypte, 0, fluxCrypte.Length);
            // Fermer les flux
            cs.Close();
            fsw.Close();
        }

        public static void TestCryptAsynchrone()
        {
            CreeFichierClair();
            EncryptAsynch();
            DecryptAsynch();
        }

        public static void EncryptAsynch()
        {
            RSACryptoServiceProvider transform = new RSACryptoServiceProvider();
            FileStream fsr = new FileStream(@".\FichierClair.txt", FileMode.Open, FileAccess.Read);
            byte[] bytesNormal = new byte[fsr.Length];
            fsr.Read(bytesNormal,0, bytesNormal.Length);
            fsr.Close();
            byte[] bytesEncrypts = transform.Encrypt(bytesNormal, false);
            FileStream fsw = new FileStream(@".\FichierAsynchCrypt.txt", FileMode.Create, FileAccess.Write);
            fsw.Write(bytesEncrypts, 0, bytesEncrypts.Length);
            fsw.Close();
            StreamWriter publicKey = new StreamWriter(@".\PublicKeyTestASynchrone.key",false,Encoding.Unicode);
            publicKey.Write(transform.ToXmlString(true));
            publicKey.Close();
        }

        public static void DecryptAsynch()
        {
            RSACryptoServiceProvider transform = new RSACryptoServiceProvider();
            StreamReader publicKey = new StreamReader(@".\PublicKeyTestASynchrone.key", Encoding.Unicode);
            transform.FromXmlString(publicKey.ReadToEnd());
            publicKey.Close();
            FileStream fsr = new FileStream(@".\FichierAsynchCrypt.txt", FileMode.Open, FileAccess.Read);
            byte[] bytesEncrypts = new byte[fsr.Length];
            fsr.Read(bytesEncrypts, 0, bytesEncrypts.Length);
            fsr.Close();
            byte[] bytesNormal = transform.Decrypt(bytesEncrypts, false);
            FileStream fsw = new FileStream(@".\FichierAsynchDecrypt.txt", FileMode.Create, FileAccess.Write);
            fsw.Write(bytesEncrypts, 0, bytesEncrypts.Length);
            fsw.Close();
        }

        public static void TestHash()
        {
        }
    }
}
