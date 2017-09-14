#define TRACE

using System;
using System.Data;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Timers;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;

/// <summary>
/// Description résumée de ServerManagment
/// </summary>
[Serializable]
public class ServerManagment
{
    private string machineName = "";
    private ServiceController[] services = null;
    private Stack<String> lastError = new Stack<String>();
    private static string[] servicesToLoad = null;
    private static string[] logFilesList = null;

    public ServerManagment()
    {
        this.machineName = System.Environment.MachineName;
        this.LoadServiceList();
    }

    public ServerManagment(string strMachineName)
    {
        this.machineName = strMachineName;
        this.LoadServiceList();
    }

    public ServerManagment(string strMachineName, String[] lstServicesToLoad)
    {
        this.machineName = strMachineName;
        this.listServicesToLoad = lstServicesToLoad;
    }

    public ServerManagment(string strMachineName, string[] lstServicesToLoad, string[] logFilesList)
    {
        this.machineName = strMachineName;
        this.listServicesToLoad = lstServicesToLoad;
        this.getLogsFiles(logFilesList);
    }

    public override string ToString()
    {
        return "Server Managment for " + this.machineName;
    }

    public ServiceController[] ServicesList
    {
        get { return this.services; }
    }

    public String MachineName
    {
        get { return this.machineName; }
        set
        {
            this.machineName = value;
            this.LoadServiceList();
        }
    }

    public string[] listServicesToLoad
    {
        get
        {
            return servicesToLoad;
        }
        set
        {
            servicesToLoad = value;
            LoadServiceList();
        }
    }

    private void LoadServiceList()
    {
        try
        {
            // remove all old error list
            if (this.lastError.Count > 0)
                this.lastError.Clear();

            this.services = ServiceController.GetServices(this.machineName);
            if (this.listServicesToLoad != null)
                this.services = services.Where(filterServices).ToArray();
            if (this.services.Count() == 0)
                this.lastError.Push("Aucun service disponible pour cette machine");
            Trace.TraceInformation("ServiceController.GetServices(\"" + machineName + "\")");
        }
        catch (Exception e)
        {
            Trace.TraceError(e.Source + "; " + e.Message + "\r\n" + e.StackTrace);
            this.services = null;
            this.lastError.Push(e.Message);
        }
    }

    private Func<ServiceController, bool> filterServices = x => servicesToLoad.Contains(x.ServiceName);

    public bool isExistsServicesList()
    {
        return this.services != null;
    }

    protected void getLogsFiles(string[] logsFilesList)
    {
        ServerManagment.logFilesList = logsFilesList;
    }

    public void StartAllServicesSelected()
    {
        if (this.isExistsServicesList())
            foreach (ServiceController srv in this.services.Reverse())
            {
                try
                {
                    if (srv.Status != ServiceControllerStatus.Running)
                    {
                        srv.Start();
                        srv.WaitForStatus(ServiceControllerStatus.Running);
                    }
                }
                catch (Exception e)
                {
                    Trace.TraceError(e.Source + "; " + e.Message + "\n" + e.StackTrace);
                    this.lastError.Push(e.Message);
                }
            }
    }

    public void StopAllServicesSelected()
    {
        if (this.isExistsServicesList())
            foreach (ServiceController srv in this.services)
            {
                try
                {
                    if (srv.Status != ServiceControllerStatus.Stopped && srv.CanStop)
                    {
                        srv.Stop();
                        srv.WaitForStatus(ServiceControllerStatus.Stopped);
                    }
                }
                catch (Exception e)
                {
                    Trace.TraceError(e.Source + "; " + e.Message + "\n" + e.StackTrace);
                    this.lastError.Push(e.Message);
                }
            }
    }

    public void StartOneService(String ServiceName)
    {
        if (this.isExistsServicesList())
                try
                {
                    ServiceController srvSelected = null;
                    foreach (ServiceController srv in this.services)
                    {
                        if( srv.ServiceName == ServiceName)
                            srvSelected = srv;
                    }
                    if (srvSelected != null && srvSelected.Status != ServiceControllerStatus.Running)
                    {
                        srvSelected.Start();
                        srvSelected.WaitForStatus(ServiceControllerStatus.Running);
                    }
                }
                catch (Exception e)
                {
                    Trace.TraceError(e.Source + "; " + e.Message + "\n" + e.StackTrace);
                    lastError.Push(e.Message);
                }
    }

    public void StopOneService(String ServiceName)
    {
        if (this.isExistsServicesList())
                try
                {
                    ServiceController srvSelected = null;
                    foreach (ServiceController srv in this.services)
                    {
                        if (srv.ServiceName == ServiceName)
                            srvSelected = srv;
                    }
                    if (srvSelected != null && srvSelected.Status != ServiceControllerStatus.Stopped && srvSelected.CanStop)
                    {
                        srvSelected.Stop();
                        srvSelected.WaitForStatus(ServiceControllerStatus.Stopped);
                    }
                }
                catch (Exception e)
                {
                    Trace.TraceError(e.Source + "; " + e.Message + "\n" + e.StackTrace);
                    lastError.Push(e.Message);
                }
    }

    public string GetLastErrors()
    {
        string strErrors = "";
        while (this.lastError.Count > 0)
            strErrors += this.lastError.Pop() + "\n";
        if (strErrors == "") strErrors = "Pas d'erreurs détectées";
        return strErrors;
    }

    public string GetAllErrors()
    {
        return (this.lastError != null && this.lastError.Count > 0)?
            string.Join("\n",this.lastError.ToArray()) :
            "Pas d'erreurs détectées";
    }
}
