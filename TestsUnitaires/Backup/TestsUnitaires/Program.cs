using System;
using System.Collections.Generic;
using System.Text;

namespace TestsUnitaires
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestDate.TesterDate();
            //ExpReg.TesterExpression();
            //SerializationMemory.Test();
            //Console.WriteLine(MailError.SendMailError("smtp.orange.fr", "pierrotlefou@ttoto.com", "pierre.contri@free.fr", "Test mail by asynchronous", "Salut, ceci est un test d'envoie de mail asynchrone"));
            //ThreadsLimits.Tests();
            //ThreadParamTest.Test();
            //PerformancesTests.Test();
            //Securisation.TestCryptSynchrone();
            //Securisation.TestCryptAsynchrone();
            ReflectionTest.Test();
            //Securisation.TestCryptSynchrone();
            Console.WriteLine(Resource1.String1 + " " + Settings1.Default.ConnSqlExpress);

            Console.ReadKey();
        }
    }
}
