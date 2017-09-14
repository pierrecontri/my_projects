using System;
using System.Collections.Generic;
using System.Text;

namespace TestsUnitaires
{
    class TestDate
    {
        static public void TesterDate()
        {
            DateTime dt = new DateTime(2006, 04, 05);
            Console.WriteLine(dt.ToString("dddd dd MMMM yyyy"));
            Console.Read();
        }
    }
}
