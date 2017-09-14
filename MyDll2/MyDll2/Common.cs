using System;

namespace MyDll2
{
    public class PiRCommon
    {
        string personName = "World";

        public PiRCommon()
        {
        }

        public PiRCommon(string personName)
        {
            this.personName = personName;
        }

        public string SayHello()
        {
            return string.Format("Hello {0} !", personName);
        }

        public double PiR_2()
        {
            return Math.Pow(Math.PI, 2);
        }

        public string SayBye()
        {
            return string.Format("Bye {0} !", personName);
        }
    }
}
