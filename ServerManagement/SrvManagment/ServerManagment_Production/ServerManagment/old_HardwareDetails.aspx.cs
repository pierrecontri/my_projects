using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Management;
using System.Data;
using System.Data.SqlClient;

public partial class HardwareDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string remoteMachine = Request.Params["remoteMachine"];
        if (remoteMachine != null)
            getWMIInfoFrom(remoteMachine);
    }

    protected void getWMIInfoFrom(string machineName)
    {
        try
        {
            DataTable dt = new DataTable("Essai");
            dt.Columns.Add("Caption");
            dt.Columns.Add("Manufacturer");

            ManagementScope sc = new ManagementScope(@"\\" + machineName + @"\root\cimv2");
            sc.Connect();
            SelectQuery Info_bios = new SelectQuery("Win32_PnpEntity");
            ManagementObjectSearcher Bios_Information = new ManagementObjectSearcher(sc, Info_bios);
            foreach (ManagementObject fo in Bios_Information.Get())
            {
                DataRow dr = dt.NewRow();

                try { dr["Caption"] = fo["Caption"].ToString(); }
                catch (Exception /*ex*/)
                { }
                try { dr["Manufacturer"] = fo["Manufacturer"].ToString(); }
                catch (Exception /*ex*/)
                { }

                dt.Rows.Add(dr);
            }
            this.GridView1.DataSource = dt;
            this.GridView1.DataBind();
            //dt.WriteXml(@"c:\testInventory.xml");

        }
        catch (Exception /*ex*/)
        {
            Response.Write("Connection to " + machineName + " error !");
        }
    }
}
