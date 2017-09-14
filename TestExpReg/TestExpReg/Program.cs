using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TestExpReg
{
    class Program
    {
        static void Main(string[] args)
        {
            string [] tabStr = {"InfosDetailRisqueEntreprise","InfosSuiviRisqueEntreprise",
                                "InfosDetailRisqueParticulier","InfosSuiviRisqueParticulier"};
            string strExpr = @"^(Infos)\w*(Risque\w*)$";
            Regex regExpr = new Regex(@"^Infos(?<vue>\w+)Risque(?<type>\w+)$", RegexOptions.ExplicitCapture);
            foreach(string mot in tabStr)
            {
                Console.WriteLine(mot);
                foreach(string splitExpr in System.Text.RegularExpressions.Regex.Split(mot,strExpr))
                    Console.WriteLine("\t==> " + splitExpr);
                foreach(Match matchExpr in System.Text.RegularExpressions.Regex.Matches(mot, strExpr))
                    Console.WriteLine("\t--> " + matchExpr.Value);
                Console.WriteLine("\t*** " + regExpr.Match(mot).Result("${vue} : ${type} ***"));
                Console.WriteLine();
            }
            Console.ReadKey();
        }
    }
}
