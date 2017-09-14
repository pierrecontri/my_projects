using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TestsUnitaires
{
    class ThreadsLimits
    {
        static List<Thread> listThreads = new List<Thread>();

        public static void Tests()
        {
            int nbrThreads = 30;

            // creation des threads snas lancement
            for (int i = 0; i < nbrThreads; i++)
                listThreads.Add(new Thread(SerializationMemory.Test));

            // affichage des IDs
            Action<Thread> actionAfficheID = new Action<Thread>(TestAfficheID);
            listThreads.ForEach(actionAfficheID);

            // lancement des threads
            foreach (Thread tTmp in listThreads)
                tTmp.Start();

            // attente des threads
            foreach (Thread tTmp in listThreads)
                tTmp.Join();
        }

        public static void TestAfficheID(Thread obj)
        {
            Console.WriteLine(obj.ManagedThreadId.ToString());
        }
    }
}
