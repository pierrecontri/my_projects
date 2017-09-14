using System;

using System.Text;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using System.IO;

using System.Configuration;


namespace RetrieveParametresIni
{
    class Program
    {
        
        static List<DataTable> dtCategoryClient;

        static List<iniFile> iniServer;
        static List<iniFile> iniClient;

        static void Main(string[] args)
        {
            string[] strAr = null;
            string serverFileName = string.Empty;
            string clientFileName = string.Empty;

            string outClientFile = string.Empty;
            string outServerFile = string.Empty;

            //*******************************************************************
            //Parameters app.config
            //*******************************************************************
            try
            {
                strAr = readFile(ConfigurationManager.AppSettings["ListFile"]);

                serverFileName = ConfigurationManager.AppSettings["ServerFileName"];
                clientFileName = ConfigurationManager.AppSettings["ClientFileName"];

                outClientFile = ConfigurationManager.AppSettings["outClient"];
                outServerFile = ConfigurationManager.AppSettings["outServer"];
            }
            catch (Exception ex)
            {
                Console.WriteLine("0x01 " + ex.Message);
                Console.Read();
                return;
            }
            //*******************************************************************

            //*******************************************************************
            //List init
            //*******************************************************************
            dtCategoryClient = new List<DataTable>();
            iniServer = new List<iniFile>();
            iniClient = new List<iniFile>();
            //*******************************************************************

            foreach (string str in strAr)
            {
                //Déclaration recuperation liste.txt
                string[] s;
                string nameComputer = string.Empty;
                string directory = string.Empty;
                string directoryServer = string.Empty;
                string logicalName = string.Empty;
                List<string> directoryClient = new List<string>();

                //Fill var
                try
                {
                    s = str.Split(';');

                    nameComputer = s[0].TrimEnd(' ');
                    logicalName = s[1].TrimEnd(' ');
                    directory = s[2].TrimEnd(' ');
                    directoryServer = s[3].TrimEnd(' ');
                    directoryClient = new List<string>();
                    for (int i = 4; i < s.Length; i++) 
                    {
                        if (!s[i].Equals (string.Empty))
                        {
                            directoryClient.Add(s[i].TrimEnd(' '));
                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.Write("0x02 " + ex.Message);
                    Console.Read();
                    return;
                }

                //Serveur
                string tmp = "\\\\" + Format(nameComputer) + Format(directory) + Format(directoryServer) + serverFileName;
                iniServer.Add(new iniFile(nameComputer + " " + logicalName, tmp));

                //Client
                directoryClient.ForEach(x => iniClient.Add(new iniFile(nameComputer + " " + x,
                    "\\\\" + Format(nameComputer) + Format(directory) + Format(x) + clientFileName)));

                Console.WriteLine(nameComputer);
            }

            //Retrieve inf parameters for server file
            //No Error if file not exist
            for (int i = 0; i < iniServer.Count; i++)
            {
                iniServer[i].retrieveAllInfo();
            }

            //Retrieve inf parameters for client file
            //No Error if file not exist
            for (int i = 0; i < iniClient.Count; i++)
            {
                iniClient[i].retrieveAllInfo();
            }

            process(iniServer, outServerFile);
            process(iniClient, outClientFile);
            
        }

        /// <summary>
        /// Fill datatable and write to csv
        /// </summary>
        /// <param name="ini"></param>
        /// <param name="filename"></param>
        public static void process(List<iniFile> ini, string filename)
        {
            //Fill the datatable with the info and rotate it
            List<DataTable> dtCategory = RotateDataTableList(fillDataTable(ini));

            StringBuilder strb = new StringBuilder("\t");
            //ajout entête liste des möä^¨^¨^||||||chines
            ini.ForEach(x => strb.Append(x.MachineName + '\t'));
            strb.Replace("\t", Environment.NewLine, strb.Length - 1, 1);
            //ecrit les datatable en fichier csv
            writeToCSV(dtCategory, strb, filename);
        }

        /// <summary>
        /// Rotate a list of DataTisch
        /// </summary>
        /// <param name="ll"></param>
        /// <returns></returns>
        public static List<DataTable> RotateDataTableList(List<DataTable> ll)
        {
            List<DataTable> tmp = new List<DataTable>();
            ll.ForEach(x => tmp.Add(RotateTable(x)));
            return tmp;
        }

        /// <summary>
        /// Rotate DataTable
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DataTable RotateTable(DataTable dt)
        {
            DataTable table = new DataTable(dt.TableName);
            for (int i = 0; i <= dt.Rows.Count; i++)
            {
                table.Columns.Add(Convert.ToString(i));
            }
            DataRow r = null;
            for (int k = 0; k < dt.Columns.Count; k++)
            {
                r = table.NewRow();
                r[0] = dt.Columns[k].ToString();
                for (int j = 1; j <= dt.Rows.Count; j++)
                    r[j] = dt.Rows[j - 1][k];
                table.Rows.Add(r);
            }

            return table;
        }

        /// <summary>
        /// Write DataTable List to CSV
        /// </summary>
        /// <param name="path"></param>
        public static void writeToCSV(List<DataTable> list, StringBuilder strb, string path)
        {

            foreach (DataTable dt in list)
            {
                strb.AppendLine(dt.TableName);
                strb.AppendLine();

                foreach (DataRow dr in dt.Rows)
                {
                    Array.ForEach(dr.ItemArray, x => strb.Append(x.ToString() + "\t"));
                    strb.Replace("\t", Environment.NewLine, strb.Length - 1, 1);
                }
                strb.Append(Environment.NewLine);
            }
            try
            {
                using (StreamWriter sw = new FileInfo(path).CreateText()) { sw.Write(strb.ToString()); }
            }
            catch (Exception ex)
            {
                Console.WriteLine("0x03 " + ex.Message);
                Console.Read();
                return;
            }
        }

        /// <summary>
        /// Add \ at the end of string if besoin
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Format(string input)
        {
            if (!input.EndsWith("\\"))
                input += "\\";
            return input;
        }

        /// <summary>
        /// Create and Fill DataTable
        /// </summary>
        /// <param name="ll"></param>
        /// <returns></returns>
        public static List<DataTable> fillDataTable(List<iniFile> ll)
        {
            List<DataTable> ldt = new List<DataTable>();

            //pour chaque catégorie dans tt les catégorie de tout les fichiers nini
            foreach (string str in ll.SelectMany(x => x.Categories.Keys).ToList<string>().Distinct<string>().ToList<string>())
            {
                //*************************************************
                List<string> test = new List<string>();
                //pour tout les fichiers 
                foreach (iniFile inf in ll)
                {
                    try
                    { test.AddRange(inf.Categories[str].Keys); }
                    catch (Exception /*ex*/)
                    { }
                }

                //*************************************************
                test = test.Distinct<string>().ToList<string>();
                List<DataColumn> dc = new List<DataColumn>();
                test.ForEach(x => dc.Add(new DataColumn(x, typeof(string))));
                //*************************************************
                //crée la DataTable pour chaque catégorie
                DataTable dt = new DataTable(str);
                dt.Columns.AddRange(dc.ToArray());
                //dtCategoryServer.Add(dt);
                ldt.Add(dt);
                //*************************************************
            }

            //Remplissage des datatisch
            foreach (iniFile inf in ll)
            {
                foreach (DataTable dt in ldt)
                {
                    DataRow dr = dt.NewRow();
                    try
                    {
                        foreach (KeyValuePair<string, string> dic in inf.Categories[dt.TableName])
                        {
                            dr[dic.Key] = dic.Value;
                        }
                    }
                    catch (Exception /*ex*/)
                    { }
                    dt.Rows.Add(dr);
                }
            }
            return ldt;
        }

        /// <summary>
        /// Read Text File and return string[]
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string[] readFile(string file)
        {
            List<string> ll = new List<string>();

                StreamReader monStreamReader = new StreamReader(file);
                string line = monStreamReader.ReadLine();

                while (line != null)
                {
                    if (!(line.StartsWith ("#")) && !(line.Length == 0))
                        ll.Add(line);
                    line = monStreamReader.ReadLine();
                }
                monStreamReader.Close();

            return ll.ToArray();
        }
    }

}
