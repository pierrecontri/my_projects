using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace TestsUnitaires
{
    class PerformancesTests
    {
        public static void Test()
        {
            Process[] processus = Process.GetProcesses();
            foreach (Process proc in processus)
            {
                Console.WriteLine(proc.ToString());
            }
        }
    }
}
