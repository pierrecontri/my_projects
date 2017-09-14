using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Management;
using Microsoft.Win32;
using System.Data;


    public struct HardDetails
    {
        public string Caption;
        public string Manufacturer;
        public string DeviceID;
        public string ClassGUID;
    }

    public struct HardwareClass
    {
        public string Class;
        public string Name;
    }

    public partial class HardwareDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string remoteMachine = Request.Params["remoteMachine"];
            if (remoteMachine != null)
                DisplayWmi(remoteMachine);
		}

        /// <summary>
        /// Display Hardware info
        /// One GridView per Hardware Class
        /// </summary>
        /// <param name="name">Machine Network Name</param>
        public void DisplayWmi(string name)
        {
            Dictionary<string, HardDetails[]> hs;

            //Get HW info Dictionary <class name,HardwareInfo[]>
            hs = HardwareDiscovery(name);
            hs.OrderBy(key => key.Key);

            //Create DataTable foreach dictionary key
            foreach (var tt in hs)
            {
                DataTable dt = new DataTable((tt.Key));
                dt.Columns.Add("Caption");
                dt.Columns.Add("Manufacturer");
                dt.Columns.Add("DeviceID");

                //Create DataRow
                foreach (HardDetails hd in tt.Value)
                {
                    DataRow dr = dt.NewRow();
                    dr["Caption"] = hd.Caption;
                    dr["Manufacturer"] = hd.Manufacturer;
                    dr["DeviceID"] = hd.DeviceID;
                    dt.Rows.Add(dr);
                }


                //Create Display GridView and Display Label
                if (dt.Rows.Count > 0)
                {
                    GridView gw = new GridView();
                    gw.DataSource = dt;
                    gw.DataBind();
                    gw.BorderStyle = BorderStyle.Double;
                    gw.BorderWidth = 5;
                    gw.BorderColor = System.Drawing.Color.Black;

                    form1.Controls.Add(new LiteralControl("<H2>" + tt.Key + "</H2>"));
                    form1.Controls.Add(new LiteralControl("<br />"));
                    form1.Controls.Add(gw);
                    form1.Controls.Add(new LiteralControl("<br />"));

                }
            }
        }

        /// <summary>
        /// Get Machine Full Hardware
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Dictionary<Hardware class name,HardwareDetails[]></returns>
        public Dictionary<string, HardDetails[]> HardwareDiscovery(string name)
        {
            List<HardwareClass> ll = new List<HardwareClass>();
            List<HardDetails> dtll = new List<HardDetails>();
            Dictionary<string,HardDetails[]> dic= new Dictionary<string,HardDetails[]>();

            RegistryKey environmentKey = null;
            RegistryKey key = null;

            try
            {
                // Open HKEY_CURRENT_USER\Environment 
                // on a remote computer.
                environmentKey = RegistryKey.OpenRemoteBaseKey(
                    RegistryHive.LocalMachine, name).OpenSubKey(
                    "SYSTEM\\CurrentControlSet\\Control\\class");
            }
            catch (Exception e)
            {
                Console.WriteLine("{0}: {1}",
                    e.GetType().Name, e.Message);
            }
            try
            {
                string[] classs = environmentKey.GetSubKeyNames();


                foreach (string cl in classs)
                {
                    HardwareClass hc;

                    try
                    {
                        key = RegistryKey.OpenRemoteBaseKey(
                        RegistryHive.LocalMachine, name).OpenSubKey(
                        "SYSTEM\\CurrentControlSet\\Control\\class\\" + cl);

                        hc.Name = key.GetValue("").ToString();
                        hc.Class = cl;

                        ll.Add(hc);
                    }
                    catch (Exception /*ex*/)
                    {
                    }
                }
            }
            catch (Exception /*ex*/)
            {
            }

            ManagementScope sc = new ManagementScope(@"\\" + name + @"\root\cimv2");
            sc.Connect();
            SelectQuery Info_bios = new SelectQuery("Win32_PnpEntity");
            ManagementObjectSearcher Bios_Information = new ManagementObjectSearcher(sc, Info_bios);


            foreach (ManagementObject fo in Bios_Information.Get())
            {

                HardDetails hd = new HardDetails();
                try { hd.Caption = fo["Caption"].ToString(); }
                catch (Exception /*ex*/)
                { }
                try { hd.Manufacturer = fo["Manufacturer"].ToString(); }
                catch (Exception /*ex*/)
                { }

                try { hd.ClassGUID = fo["ClassGuid"].ToString(); }
                catch (Exception /*ex*/)
                { }

                try { hd.DeviceID = fo["DeviceID"].ToString(); }
                catch (Exception /*ex*/)
                { }


                dtll.Add(hd);
            }

            foreach (HardwareClass hhc in ll)
            {
                List<HardDetails> huhu = dtll.FindAll(x => x.ClassGUID == hhc.Class);
                dic.Add(hhc.Name, huhu.ToArray());
                Console.WriteLine();
            }
            return dic;

        }


    }


