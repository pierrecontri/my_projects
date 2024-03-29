﻿#define TRACE

using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Threading;

public partial class FactoryView : System.Web.UI.Page
{
    public List<ServerManagment> listServerManagment = null;
    // filter the requested services
    private static String[] listFilterServices = ConfigurationSettings.AppSettings["ListOfServicesToManage"].Split(new char[] { ';' });
    private static String pathMachineList = ConfigurationSettings.AppSettings["FileOfMachineList"];

    protected void Page_Load(object sender, EventArgs e)
    {
        string[] strMachineList = null;

        Page.Header.Title = "Managment of factory's view";
        
        List<Thread> thList = new List<Thread>();
        
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
                        //Thread th = new Thread(new ParameterizedThreadStart(ConnectAndAddMachine));
                        //th.Start(strMachine);
                        //thList.Add(th);
                        ConnectAndAddMachine(strMachine);
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

        //try
        //{
        //    foreach (Thread tmpThreadJoin in thList)
        //    {
        //        tmpThreadJoin.Join();
        //    }
        //}
        //catch (Exception /* ex */) { }

        try
        {
            listServerManagment.Sort();
            foreach (ServerManagment svrManage in listServerManagment)
            {
                ServicesControl c = (ServicesControl)Page.LoadControl("ServicesControl.ascx");
                c.ServicesManagment = svrManage;
                c.IsAdmin = true.Equals(Session["isAuthor"]);
                this.ContentPanel.Controls.Add(c);
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Trace.TraceError(ex.Source + "; " + ex.Message + "\n" + ex.StackTrace);
        }
    }

    private void ConnectAndAddMachine(object machineName)
    {
        listServerManagment.Add(new ServerManagment((string)machineName, listFilterServices));
    }
}
