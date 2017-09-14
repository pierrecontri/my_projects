using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Reflection;
using CarsCostSimulator.modules.Modelizing;
using CarsCostSimulator.modules.Modelizing.CarModelizing;

namespace CarsCostSimulator.modules
{
    class XmlCarManage
    {
        //getName = lambda classObj : classObj.__name__
        //clsmembers = map(getName, listClassSimulator)

        static string[] clsmembers = new string[] { "Car", "Fuel", "Wearpart", "Driver", "Drivertype" };

        //int indent = 0;
        object tmpObject = null;
        object tmpParentObject = null;
        string actual_element = string.Empty;

        public XmlCarManage(string databasefile, bool demomode)
        {
            System.Data.DataSet _ds = new System.Data.DataSet();
            //_ds.ReadXmlSchema(".\\dbschema.xsl");
            _ds.ReadXml(databasefile);
            string tty2 = _ds.Tables["Fuel"].TableName;

            /*XmlTextReader reader = new XmlTextReader(databasefile);
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element: // The node is an element.
                        Console.Write("<" + reader.Name);
                        Console.WriteLine(">");
                        Dictionary<string, object> attrs = new Dictionary<string, object>();
                        if (reader.HasAttributes)
                        {
                            // move to attributes
                            if (reader.MoveToFirstAttribute())
                            {
                                do
                                {
                                    attrs.Add(reader.Name, reader.Value);
                                } while (reader.MoveToNextAttribute());
                                // move to element
                                reader.MoveToElement();
                            }
                        }
                        start_element(reader.Name, attrs);
                        break;
                    case XmlNodeType.Text: //Display the text in each element.
                        Console.WriteLine(reader.Value);
                        char_data(reader.Value.ToString());
                        break;
                    case XmlNodeType.EndElement: //Display the end of the element.
                        Console.Write("</" + reader.Name);
                        Console.WriteLine(">");
                        end_element(reader.Name);
                        break;
                }
            }*/
        }

        protected void start_element(string name, Dictionary<string, object> attrs)
        {

            this.actual_element = name;

            bool saveObj = !(attrs.ContainsKey("visibility") && "hidden".Equals(attrs["visibility"].ToString().ToLower()));

            if (!(attrs.ContainsKey("name")))
                return;

            object tmpObject2 = null;
            try
            {
                //string formulEval = name + "(\"" + attrs["name"].ToString() + "\")";
                //tmpObject2 = eval(formulEval);

                //tmpObject2 = Activator.CreateInstance("CarsCostSimulator.modules.Modelizing", name, new object[] { attrs["name"].ToString() });
                Type objType = Type.GetType("CarsCostSimulator.modules.Modelizing." + name, true);
                tmpObject2 = Activator.CreateInstance(objType, new object[] { attrs["name"].ToString() });
            }
            catch (Exception /* ex */)
            {
                return;
            }


            if (XmlCarManage.clsmembers.Contains(name))
            {
                // root object, save into parent
                this.tmpParentObject = tmpObject2;

                if (saveObj)
                    try
                    {
                        if (hasattr(tmpObject2, "appendObj"))
                            tmpObject2.GetType().InvokeMember("appendObj", System.Reflection.BindingFlags.SetProperty, null, this, new object[] { tmpObject });
                    }
                    catch (Exception /* ex */)
                    {
                        Console.WriteLine("Error in appening object");
                    }

            }
            else if (this.tmpParentObject != null && hasattr(this.tmpParentObject, name + "s"))
            {
                if (saveObj)
                {
                    PropertyInfo propName_s = this.tmpParentObject.GetType().GetProperty(name + "s");
                    var valueProp = propName_s.GetValue(this.tmpParentObject, null);
                    valueProp.GetType().GetMethod("append", BindingFlags.InvokeMethod).Invoke(valueProp, new object[] { tmpObject });
                    // eval("this.tmpParentObject." + name + "s.append(tmpObject)");
                }
            }
            // save temporary object
            this.tmpObject = tmpObject2;
            return;
        }

        protected void end_element(string name)
        {
            if (this.tmpObject == null)
                return;
            // if we leave Wearparts objects, please back to Car
            if ("Wearpart".Equals(name) && this.tmpObject is Wearpart)
                this.tmpObject = this.tmpParentObject;
            // quit object pointer if it's the end of this object
            if (this.tmpObject.GetType().Name.Equals(name))
                this.tmpObject = null;
            return;
        }

        protected void char_data(string data)
        {
            if (data.ToString().Trim().Length > 0)
                if (this.tmpObject != null && hasattr(this.tmpObject, this.actual_element))
                    setattr(this.tmpObject, this.actual_element, data);
        }

        private bool hasattr(object p, string p_2)
        {
            List<object> ttylst = new List<object>();
            ttylst.AddRange(p.GetType().GetProperties());
            ttylst.AddRange(p.GetType().GetMethods());
            ttylst.AddRange(p.GetType().GetMembers());

            foreach (MemberInfo tty in ttylst)
            {
                if (tty.Name.Equals(p_2))
                    return true;
            }
            return false;
        }

        private bool setattr(object p, string p_2, string data)
        {
            try
            {
                PropertyInfo propInfo = p.GetType().GetProperty(p_2);
                if (propInfo != null)
                {
                    switch (propInfo.PropertyType.Name)
                    {
                        case "Single":
                            float tmpFloat = 0.0F;
                            float.TryParse(data, out tmpFloat);
                            propInfo.SetValue(p, tmpFloat, null);
                            break;
                        case "String":
                            propInfo.SetValue(p, data, null);
                            break;
                        default :
                            Console.WriteLine(propInfo.PropertyType.Name + " not defined into setattr() function");
                            break;
                    }
                }
//                    p.GetType().InvokeMember(p_2,BindingFlags.SetProperty, null, p, new object[] { data });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            try
            {
                if (p.GetType().GetMethod(p_2) != null)
                    p.GetType().InvokeMember(p_2, BindingFlags.InvokeMethod, null, p, new object[] { data });
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return false;
        }

        /*
                self.p = xml.parsers.expat.ParserCreate()
                if demomode:
                    self.p.StartElementHandler = self.start_element_show
                    self.p.EndElementHandler = self.end_element_show
                    self.p.CharacterDataHandler = self.char_data_show
                else:
                    self.p.StartElementHandler = self.start_element
                    self.p.EndElementHandler = self.end_element
                    self.p.CharacterDataHandler = self.char_data

                if databaseFile != "" and pth.exists(databaseFile):
                    self.p.ParseFile(open(databaseFile))

         */
    }
}
