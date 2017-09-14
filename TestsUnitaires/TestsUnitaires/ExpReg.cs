using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TestsUnitaires
{
    class ExpReg
    {
        public static void TesterExpression()
        {
            string[] tabStr = {"InfosDetailRisqueEntreprise","InfosSuiviRisqueEntreprise",
                                "InfosDetailRisqueParticulier","InfosSuiviRisqueParticulier"};
            Regex regExpr = new Regex(@"^Infos(?<vue>\w+)Risque(?<type>\w+)$", RegexOptions.ExplicitCapture);
            foreach (string mot in tabStr)
            {
                Console.WriteLine(mot + Environment.NewLine + "".PadRight(mot.Length,'='));
                foreach (string splitExpr in regExpr.Split(mot))
                    if(splitExpr.Length > 0) Console.WriteLine(splitExpr);
                Console.WriteLine(regExpr.Match(mot).Result("${vue} ${type}"));
                Console.WriteLine();
            }
        }
    }
}
