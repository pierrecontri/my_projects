using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RetrieveParametresIni
{
    public class iniFile
    {
        private string _path;
        private string _machineName;

        private Dictionary<string, Dictionary<string, string>> _categories = new Dictionary<string, Dictionary<string, string>>();

        public Dictionary<string, Dictionary<string, string>> Categories
        {
            get { return _categories; }
        }

        public string MachineName
        {
            get { return _machineName; }
        }

        public iniFile(string name, string path)
        {
            _machineName = name;
            _path = path;
        }

        public void retrieveAllInfo()
        {
            foreach (string cat in IniReader.GetCategories(_path))
            {
                Dictionary<string, string> dicKeyValue = new Dictionary<string, string>();

                foreach (string key in IniReader.GetKeys(_path, cat))
                {
                    //Vérifie que la ligne ne soit pas un commentaire
                    if (!key.StartsWith("'") && !key.StartsWith(";") && !key.StartsWith("#"))
                    {
                        string tmp = IniReader.GetIniFileString(_path, cat, key, null);
                        dicKeyValue.Add(key.ToLower(), tmp);
                    }
                }

                _categories.Add(cat, dicKeyValue);

            }
        }
    }

   

}
