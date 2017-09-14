#define TRACE

using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class FactoryStateView : System.Web.UI.Page
{
    public List<ServerManagment> listServerManagment = null;
    // filter the requested services
    private static String[] listFilterServices = ConfigurationSettings.AppSettings["ListOfServicesToManage"].Split(new char[] { ';' });
    private static String pathMachineList = ConfigurationSettings.AppSettings["FileOfMachineList"];

    protected void Page_Load(object sender, EventArgs e)
    {
        string[] strMachineList = null;

        Page.Header.Title = "Managment of factory's view";
        if (Session["ListSrvManagment"] == null)
        {
            listServerManagment = new List<ServerManagment>();

            try
            {
                strMachineList = MultiServices.getMachineList(Server.MapPath(pathMachineList));
                if (strMachineList == null)
                {
                    Response.Write("No machine list present");
                    return;
                }
                foreach (string strMachine in strMachineList)
                {
                    try
                    {
                        listServerManagment.Add(new ServerManagment(strMachine, listFilterServices));
                    }
                    catch (Exception interneEx)
                    {
                        Response.Write("Impossible d'atteindre la cible " + strMachine + "\n");
                        System.Diagnostics.Trace.TraceWarning(interneEx.Source + "; " + interneEx.Message + "\n" + interneEx.StackTrace);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError(ex.Source + "; " + ex.Message + "\n" + ex.StackTrace);
            }

            Session["ListSrvManagment"] = listServerManagment;

        }
        else
        {
            listServerManagment = (List<ServerManagment>)Session["ListSrvManagment"];
        }

        try
        {
            foreach (ServerManagment svrManage in listServerManagment)
            {
                ServicesControl c = (ServicesControl)Page.LoadControl("ServicesControl.ascx");
                c.ServicesManagment = svrManage;
                c.IsAdmin = false;
                this.ContentPanel.Controls.Add(c);
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Trace.TraceError(ex.Source + "; " + ex.Message + "\n" + ex.StackTrace);
        }
    }
}
