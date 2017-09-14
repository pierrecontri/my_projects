using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace TestCopyFile
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                File.Copy("C:\\pagefile.sys", "C:\\Temp\\pagefile2.sys");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            try
            {
                FileStream ficLect = new FileStream("C:\\pagefile.sys", FileMode.Open, FileAccess.Read);
                FileStream ficEcri = new FileStream("C:\\Temp\\pagefile3.sys", FileMode.CreateNew, FileAccess.Write);
                while (ficLect.Position < ficLect.Length)
                {
                    ficEcri.WriteByte((byte) ficLect.ReadByte());
                }
                ficLect.Close();
                ficEcri.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }

            Console.ReadKey();
        }
    }
}
