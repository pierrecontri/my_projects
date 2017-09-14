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
    private static String[] servicesToLoad = null;
    private Stack<String> lastError = new Stack<String>();

    public ServerManagment()
    {
        machineName = System.Environment.MachineName;
        this.LoadServiceList();
    }

    public ServerManagment(string strMachineName)
    {
        machineName = strMachineName;
        this.LoadServiceList();
    }

    public ServerManagment(string strMachineName, String[] lstServicesToLoad)
    {
        machineName = strMachineName;
        this.LoadServiceListFilter(lstServicesToLoad);
    }

    public override string ToString()
    {
        return "Server Managment for " + machineName;
    }

    public void LoadServiceList()
    {
        try
        {
            services = ServiceController.GetServices(machineName);
            Trace.TraceInformation("ServiceController.GetServices(\"" + machineName + "\")");
        }
        catch (Exception e)
        {
            Trace.TraceError(e.Source + "; " + e.Message + "\n" + e.StackTrace);
            lastError.Push(e.Message);
        }
    }

    public void LoadServiceListFilter(String[] lstServicesToLoad)
    {
        try
        {
            servicesToLoad = lstServicesToLoad;
            if (servicesToLoad == null)
                throw new NullReferenceException("Aucuns services à charger !");
            services = ServiceController.GetServices(machineName);
            if (services != null)
            {
                services = services.Where(filterServices).ToArray();
                Trace.TraceInformation("ServiceController.GetServices(\"" + machineName + "\")");
            }
        }
        catch (Exception e)
        {
            Trace.TraceError(e.Source + "; " + e.Message + "\r\n" + e.StackTrace);
            lastError.Push(e.Message);
        }
    }

    Func<ServiceController, bool> filterServices = x => servicesToLoad.Contains(x.ServiceName);

    public bool isExistsServicesList()
    {
        return services != null;
    }

    public ServiceController[] ServicesList
    {
        get { return services; }
    }

    public String MachineName
    {
        get {return machineName;}
    }

    public void StartAllServicesSelected()
    {
        if (isExistsServicesList())
            foreach (ServiceController srv in services)
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
                    lastError.Push(e.Message);
                }
            }
    }

    public void StopAllServicesSelected()
    {
        if (isExistsServicesList())
            foreach (ServiceController srv in services)
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
                    lastError.Push(e.Message);
                }
            }
    }

    public void StartOneService(String ServiceName)
    {
        if (isExistsServicesList())
                try
                {
                    ServiceController srvSelected = null;
                    foreach (ServiceController srv in services)
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
        if (isExistsServicesList())
                try
                {
                    ServiceController srvSelected = null;
                    foreach (ServiceController srv in services)
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

    public string GetLastError()
    {
        return (lastError.Count > 0)?lastError.Pop():"";
    }
}
