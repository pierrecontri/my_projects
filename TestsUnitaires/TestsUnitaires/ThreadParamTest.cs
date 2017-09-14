using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TestsUnitaires
{
    class ThreadParamTest
    {
        static void AfficheName(Object obj)
        {
            Console.WriteLine("Bonjour " + (string)obj);
        }

        public static void Test()
        {
            ParameterizedThreadStart paramThread = new ParameterizedThreadStart(AfficheName);
            Thread monThread = new Thread(paramThread);
            monThread.Start("Pierre");
        }
    }
}
