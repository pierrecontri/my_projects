using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace TestsUnitaires
{
    public struct Identity
    {
        public string prenom;
        public DateTime anneeNaissance;

        public override string ToString()
        {
            StringBuilder strResponse = new StringBuilder();
            foreach (FieldInfo finfo in this.GetType().GetFields())
                strResponse.Append(finfo.Name + " : " + finfo.GetValue(this).ToString() + "; ");
            return strResponse.ToString();
        }
    }

    class ReflectionTest
    {
        public string famille = "";
        public List<Identity> memberList = new List<Identity>();
        
        public ReflectionTest(string nom)
        {
            this.famille = nom;
        }

        public void NouvelleNaissange(string prenom, DateTime dateNaissance)
        {
            Identity nouveauBebe;
            nouveauBebe.prenom = prenom;
            nouveauBebe.anneeNaissance = dateNaissance;
            memberList.Add(nouveauBebe);
        }

        public static string GetColonnes(Type typeTest, string strCol)
        {
            foreach (FieldInfo finfo in typeof(ReflectionTest).GetFields())
                strCol += finfo.Name + "; ";
            return strCol;
        }

        public override sealed string ToString()
        {
            StringBuilder strReflection = new StringBuilder();
            foreach (FieldInfo finfo in this.GetType().GetFields())
            {
                if (typeof(IList).IsAssignableFrom(finfo.FieldType))
                {
                    foreach (Object obj in (IList)finfo.GetValue(this))
                        strReflection.AppendLine(obj.ToString());
                }
                else
                    strReflection.AppendLine(finfo.Name + " : " + finfo.GetValue(this).ToString());
            }
            return strReflection.ToString();
        }

        public static void Test()
        {
            ReflectionTest tty = new ReflectionTest("Contri");
            tty.NouvelleNaissange("Pierre", DateTime.Parse("25/02/1981"));
            tty.NouvelleNaissange("Bernard", DateTime.Parse("31/08/1957"));
            tty.NouvelleNaissange("Françoise", DateTime.Parse("30/12/1951"));
            Console.WriteLine(GetColonnes(typeof(ReflectionTest), ""));
            Console.WriteLine(tty.ToString());
        }
    }
}
