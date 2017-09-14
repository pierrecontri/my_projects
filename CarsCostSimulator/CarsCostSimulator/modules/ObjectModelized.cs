using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace CarsCostSimulator.modules
{
    public class XmlExp
    {
        public String XmlExport(int indent, int objID)
        {
            // calcul the unique identifier of objID if there is not
            if (objID == -1)
                objID = this.GetHashCode();
            // first ligne about this object
            string data = "<" + this.GetType().Name + " id=\"" + objID.ToString().PadRight(2, '0') + "\" name=\"" + this.ToString() + "\">";
            string xmltxt = data.PadRight(data.Length + indent, ' ') + "\n";

            // Get properties for writing it into xmlfile
            PropertyInfo[] tmpProperties = this.GetType().GetProperties();
            foreach (PropertyInfo tmpProperty in tmpProperties)
            {
                Object propertyContent = tmpProperty.GetValue(this, null);

                if (propertyContent is System.Collections.IList)
                {
                    string tmpData = "";
                    int idxSubObj = 1;
                    foreach (var subobj in (IList)propertyContent)
                    {
                        if (subobj is XmlExp)
                        {
                            tmpData += ((XmlExp)subobj).XmlExport(indent + 4, idxSubObj) + "\n";
                            idxSubObj++;
                        }
                    }

                    if (!"".Equals(tmpData))
                    {
                        // start of parent ancrer
                        data = "<" + tmpProperty.Name + ">";
                        xmltxt += data.PadRight(data.Length + (indent + 2), ' ') + "\n";
                        // child ancrer
                        xmltxt += tmpData;
                        // end of parent ancrer
                        data = "</" + tmpProperty.Name + ">";
                        xmltxt += data.PadRight(data.Length + (indent + 2), ' ') + "\n";
                    }
                }
                else
                {
                    string strPropContent = (propertyContent != null) ? propertyContent.ToString() : "";
                    data = "<" + tmpProperty.Name + ">" + strPropContent + "</" + tmpProperty.Name + ">";
                    xmltxt += data.PadRight(data.Length + (indent + 2), ' ') + "\n";
                }
            }
            data = "</" + this.GetType().Name + ">";
            xmltxt += data.PadRight(data.Length + indent, ' ');

            return xmltxt;
        }
    }

    public class ObjectModelized : XmlExp, IComparable
    {

        protected static Dictionary<string, Dictionary<String, ObjectModelized>> arrayObj = new Dictionary<string, Dictionary<String, ObjectModelized>>();
        String _name = String.Empty;

        public static Dictionary<String, ObjectModelized> getListObjectModelizedByType(String nameModelType)
        {
            if (!arrayObj.ContainsKey(nameModelType))
            {
                arrayObj[nameModelType] = new Dictionary<String, ObjectModelized>();
            }
            return arrayObj[nameModelType];
        }
        
        public static Dictionary<String, ObjectModelized> ListObjectModelized
        {
            get
            {
                return ObjectModelized.getListObjectModelizedByType("ObjectModelized");
            }
        }

        public ObjectModelized()
        {
        }

        public ObjectModelized(string name)
        {
            this.name = name;
        }

        public override string ToString()
        {
            return this.name;
        }

        public String name
        {
            get { return this._name; }
            set
            {
                if (ObjectModelized.arrayObj.ContainsKey(this.name))
                {
                    // remove object with old key
                    ObjectModelized.arrayObj.Remove(this.name);
                    // rename object
                    this._name = value;
                    // add object with new key
                    ObjectModelized.ListObjectModelized[this._name] = this;
                }
                else
                    this._name = value;
            }
        }

        public virtual String getInfos()
        {
            return this.name;
        }

        public void appendObj(ObjectModelized newObj)
        {
            ObjectModelized.ListObjectModelized.Add(newObj.ToString(), newObj);
        }

        public static bool removeObj(ObjectModelized objToRemove)
        {
            try
            {
                string objname = objToRemove.ToString();
                if (ObjectModelized.arrayObj.ContainsKey(objname))
                    ObjectModelized.arrayObj.Remove(objname);
                return true;
            }
            catch (Exception /*ex*/)
            {
                return false;
            }
        }

        public bool clearObj()
        {
            if (ObjectModelized.arrayObj.Count == 0)
                return false;

            ObjectModelized.ListObjectModelized.Clear();
            return true;
        }

        public bool updateObj(ObjectModelized oldObj, ObjectModelized newObj)
        {
            try
            {

                if (oldObj.ToString() == newObj.ToString())
                    ObjectModelized.ListObjectModelized[oldObj.ToString()] = newObj;
                else
                {
                    ObjectModelized.ListObjectModelized.Remove(oldObj.name);
                    ObjectModelized.ListObjectModelized.Add(newObj.name, newObj);
                }
                return true;
            }
            catch (Exception /*ex*/)
            {
                return false;
            }
        }

        #region IComparable Membres

        public int CompareTo(object obj)
        {
            if (obj == null)
                return -1;
            return this.ToString().ToLower().CompareTo(obj.ToString().ToLower());
        }

        #endregion

    }
}
