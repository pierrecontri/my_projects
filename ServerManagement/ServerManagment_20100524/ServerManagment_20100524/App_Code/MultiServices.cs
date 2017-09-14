#define TRACE

using System;
using System.Text.RegularExpressions;
using System.Data;
using System.IO;
using System.Collections;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Net.Mail;

/// <summary>
/// Description résumée de MultiServices
/// </summary>
public class MultiServices
{
	public MultiServices()
	{
	}

    public static string[] getMachineList(string fileNameMachines)
    {
        string[] machineList = null;
        try
        {
            if (System.IO.File.Exists(fileNameMachines))
            {
                // open file
                machineList = System.IO.File.ReadAllLines(fileNameMachines);
                // get it when there is ip address or machine name            
                machineList = machineList.Where(isMachineName).ToArray();
            }
        }
        catch (Exception iex)
        {
            System.Diagnostics.Trace.TraceWarning(iex.Source + "; " + iex.Message + "\n" + iex.StackTrace);
        }
        return machineList;
    }

    private static Func<string, bool> isMachineName = X => Regex.IsMatch(X, "^(\\w+|\\d{1,3})((\\.|-){0,1}(\\w+|\\d{1,3})*)*$");
    private static Func<string, bool> filterName = x => !(x.StartsWith(";") || String.Empty.Equals(x.Trim()));

    /// <summary>
    /// Send Mail for trace
    /// </summary>
    /// <param name="subject"></param>
    /// <param name="body"></param>
    public static void SendMailTrace(string subject, string body)
    {
        try
        {
            SmtpClient mail = new SmtpClient();
            mail.Send("WebServerManagment@thyssenkrupp.com", "to", subject, body);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Trace.TraceError(ex.Source + "; " + ex.Message + "\n" + ex.StackTrace);
        }
    }

    /// <summary>
    /// Analyse ZPointCS log files
    /// </summary>
    /// <param name="machineName"></param>
    /// <param name="zpPath"></param>
    /// <returns></returns>
    public static string getAnalyseZPLogs(string machineName, string zpPath)
    {
        try
        {
            string[] zpLog = System.IO.File.ReadAllLines(zpPath);
            /* for test */
            return string.Join("\n", zpLog);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Trace.TraceError(ex.Source + "; " + ex.Message + "\n" + ex.StackTrace);
        }
        return "Error in Analyse ZPointCS log files";
    }

    /// <summary>
    /// Analyse Oracle Log files
    /// </summary>
    /// <param name="machineName"></param>
    /// <param name="oraclePath"></param>
    /// <returns></returns>
    public static string getAnalyseOracleLogs(string machineName, string oraclePath)
    {
        try
        {
            string[] oracleLog = System.IO.File.ReadAllLines(oraclePath);
            /* for test */
            return string.Join("\n", oracleLog);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Trace.TraceError(ex.Source + "; " + ex.Message + "\n" + ex.StackTrace);
        }
        return "Error in Analyse Oracle log files";
    }
}

public class GenerateTextEventArgs : EventArgs
{
    private string myEventText = null;

    public GenerateTextEventArgs(string theEventText)
    {
        if (theEventText == null) throw new NullReferenceException();
        myEventText = theEventText;
    }

    public string EventText
    {
        get { return this.myEventText; }
    }
}