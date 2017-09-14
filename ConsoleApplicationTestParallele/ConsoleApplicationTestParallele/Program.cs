using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleApplicationTestParallele
{
    class Program
    {
        static void Main(string[] args)
        {
            var tty = "test";
            Console.WriteLine(tty + " : " + tty.GetType());
            Parallel.For(0, 100, delegate(int i)
            {
                Console.WriteLine("Hello " + i.ToString());
            });

            Console.ReadKey();
        }
    }
}
