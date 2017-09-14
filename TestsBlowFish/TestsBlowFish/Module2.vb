Imports System
Imports System.Text
Imports System.IO
Imports BlowfishNET
Imports BlowfishNET.JavaInterop
Imports System.Security.Cryptography

Module Module2
    Public Sub EncryptBlowFish()
        'Dim chaineClaire As String = "tbplc=souscription;prenom=davitggd;nom=brunet;rib1=11111111112223241222233;mail=david.brunet@experian.fr;rib2=11111111112224234222233;rib3=11111111112224234222233"
        'Dim chaineConvertie As String = Convert.ToBase64String(Encoding.Unicode.GetBytes(chaineClaire))
        'Dim bytesIn() As Byte = Encoding.Unicode.GetBytes(chaineClaire)
        'Dim bytesOut(bytesIn.Length - 1) As Byte


        Dim fs As New FileStream(".\BlowFishKey.key", FileMode.Open, FileAccess.Read)
        Dim maCle(fs.Length - 1) As Byte
        fs.Read(maCle, 0, maCle.Length)
        fs.Close()

        'Dim fs2 As New StreamReader(".\BlowFishKey.key")
        'Dim strKey As String = fs2.ReadToEnd()
        'fs2.Close()

        Dim strKey As String = Convert.ToBase64String(maCle)

        Dim fsChaine As New StreamReader(".\FichierClair.txt", Encoding.UTF8)
        Dim chaineClaire As String = fsChaine.ReadToEnd()
        fsChaine.Close()

        Dim easyTransform As New BlowfishEasy(strKey.ToCharArray())
        'Dim simpleTransform As New BlowfishSimple(strKey)

        Dim chaineEncrypt As String = easyTransform.EncryptString(chaineClaire)

        Dim ChaineDecrypt As String = easyTransform.DecryptString(chaineEncrypt)

        Dim sw As New StreamWriter(".\FichierCrypt.cryp", False, Encoding.UTF8)
        sw.Write(chaineEncrypt)
        sw.Close()

        Console.WriteLine(chaineEncrypt)

        Console.WriteLine(ChaineDecrypt)

        'Dim transform As New BlowfishECB(maCle, 0, maCle.Length)
        '' Recuperer le fichier
        'Dim fsr As New FileStream(".\FichierClair.txt", FileMode.Open, FileAccess.Read)
        'Dim fluxClair(fsr.Length - 1) As Byte
        'fsr.Read(fluxClair, 0, fluxClair.Length)
        'fsr.Close()
        'Dim bytesIn() As Byte = fluxClair
        'Dim bytesOut(bytesIn.Length - 1) As Byte
        '' Creer le flux de cryptage
        'transform.Encrypt(bytesIn, 0, bytesOut, 0, bytesOut.Length)
        '' Creer le nouveau fichier crypte
        'Dim fsw As New FileStream(".\FichierCrypt.cryp", FileMode.Create, FileAccess.Write)
        '' Crypter le fichier
        'fsw.Write(bytesOut, 0, bytesOut.Length)
        '' Fermer les flux
        'fsw.Close()

        'Console.WriteLine(Convert.ToBase64String(bytesOut) + Environment.NewLine)
    End Sub

    Public Sub DecryptBlowFish()

        'Dim fs As New FileStream(".\BlowFishKey.key", FileMode.Open, FileAccess.Read)
        'Dim maCle(fs.Length - 1) As Byte
        'fs.Read(maCle, 0, maCle.Length)
        'fs.Close()
        'Dim strKey = Encoding.Unicode.GetString(maCle)

        Dim fs2 As New StreamReader(".\BlowFishKey.key", Encoding.UTF8)
        Dim strKey2 As String = fs2.ReadToEnd()
        fs2.Close()

        Dim fsChaine As New StreamReader(".\BlowfishEasyCrypt.txt", Encoding.UTF8)
        Dim chaineCrypt As String = fsChaine.ReadToEnd()
        fsChaine.Close()

        Dim easyTransform As New BlowfishEasy(strKey2)
        Dim ChaineDecrypt As String = easyTransform.DecryptString(chaineCrypt)

        Dim sw As New StreamWriter(".\FichierDecrypt.txt", False, Encoding.Unicode)
        sw.Write(ChaineDecrypt)
        sw.Close()

        Console.WriteLine(ChaineDecrypt)

        'Dim fsChaine As New FileStream(".\BlowfishEasyCrypt.txt", FileMode.Open, FileAccess.Read)
        'Dim bFic(fsChaine.Length - 1) As Byte
        'fsChaine.Read(bFic, 0, bFic.Length)
        'fsChaine.Close()

        'Dim fic As New FileStream(".\BlowfishEasyCrypt.txt", FileMode.Open, FileAccess.Read)
        'Dim monFichier(fic.Length - 1) As Byte
        'fic.Read(monFichier, 0, monFichier.Length)
        'fic.Close()

        'Dim chaineCrypt2 As String = Convert.ToBase64String(monFichier)

        'Dim chaineCrypt2 As String = Convert.ToBase64String(monFichier)


        'Dim simpleTransform As New BlowfishSimple(strKey)
        'Console.WriteLine(easyTransform.EncryptString("test"))
        'Console.WriteLine(simpleTransform.Decrypt(chaineCrypt))




        'Dim fs As New FileStream(".\BlowFishKey.key", FileMode.Open, FileAccess.Read)
        'Dim maCle(fs.Length - 1) As Byte
        'fs.Read(maCle, 0, maCle.Length)
        'fs.Close()

        'Dim transform As New BlowfishECB(maCle, 0, maCle.Length)
        '' Recuperer le fichier
        'Dim fsr As New FileStream(".\FichierCrypt.cryp", FileMode.Open, FileAccess.Read)
        'Dim bytesIn(fsr.Length - 1) As Byte
        'fsr.Read(bytesIn, 0, bytesIn.Length)
        'fsr.Close()
        'Dim bytesOut(bytesIn.Length - 1) As Byte
        '' Creer le flux de cryptage
        'transform.Decrypt(bytesIn, 0, bytesOut, 0, bytesIn.Length)
        '' Affichage chaine decryptee
        'Console.WriteLine(Encoding.Unicode.GetString(bytesOut))
        '' Creer le nouveau fichier crypte
        'Dim fsw As New FileStream(".\FichierDecrypt.txt", FileMode.Create, FileAccess.Write)
        '' Crypter le fichier
        'fsw.Write(bytesOut, 0, bytesOut.Length)
        '' Fermer les flux
        'fsw.Close()
    End Sub
End Module
