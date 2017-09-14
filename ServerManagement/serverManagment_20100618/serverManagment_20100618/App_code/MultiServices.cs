#define TRACE

using System;
using System.Text.RegularExpressions;
using System.Data;
using System.Collections.Generic;
using System.Collections;
using System.IO;
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
    public static string getErrorZPLogs(string machineName, string zpPath, string lettre)
    {
        try
        {
            if (!System.IO.File.Exists(zpPath)) return "File not found! ";
            List<string> erreur = new List<string>();
            string[] zpLog = System.IO.File.ReadAllLines(zpPath);
            foreach (string s in zpLog)
            {
                if (s.Contains(lettre))
                {
                    erreur.Add(s);
                }
            }
            string[] error = erreur.ToArray();
            return string.Join("\n", error);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Trace.TraceError(ex.Source + "; " + ex.Message + "\n" + ex.StackTrace);
        }
        return "Error in Analyse ZPointCS log files";
    }

    public static string getWarningZPLogs(string machineName, string zpPath, string lettre2)
    {
        try
        {
            if (!System.IO.File.Exists(zpPath)) return "File not found! ";
            List<string> warning = new List<string>();
            string[] zpLog = System.IO.File.ReadAllLines(zpPath);
            foreach (string s in zpLog)
            {
                if (s.Contains(lettre2))
                {
                    warning.Add(s);
                }
            }
            string[] war = warning.ToArray();
            return string.Join("\n", war);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Trace.TraceError(ex.Source + "; " + ex.Message + "\n" + ex.StackTrace);
        }
        return "Error in Analyse ZPointCS log files";
    }

    public static string[] filterDateZPLogs(string[] zpLog, int filterTimeLogs)
    {
        List<string> zpLogDating = new List<string>();
        try
        {
            if ("".Equals(zpLog) || filterTimeLogs <= 0) return zpLog;

            // trouver la date la plus vieille
            // en fonction de l'heure actuelle
            //et du filtrage des heures demandees "filterTimeLogs"
            // la date se trouve en position 0 de chaque ligne (ou pas)

            // recuperer la date et l'heure actuelle
            // soustraire le nombre d'heures a filtrer
            DateTime compareDate = DateTime.Now.AddHours(-1 * filterTimeLogs);
            // parcourir le tableau pour trouver la bonne date
            // et le scinder en deux
            IEnumerator handleLog = zpLog.GetEnumerator();
            // aller au debut du tableau
            handleLog.Reset();
            // s'arreter si la date correspond
            while (handleLog.MoveNext())
            {
                string tmpLigne = (string)handleLog.Current;
                string tmpDateLigne = "";
                // recuperation de la date par rapport a la ligne
                if (tmpLigne.Length > 18)
                    tmpDateLigne = tmpLigne.Substring(0, 19);

                DateTime tmpDate;

                if (!DateTime.TryParse(tmpDateLigne, out tmpDate))
                    continue;
                else if (compareDate.CompareTo(tmpDate) < 0)
                    break;
            }


            // on est correctement positionne
            // remplir le tableau pre-filtre
            if (handleLog.Current != null)
                do
                {
                    zpLogDating.Add((string)handleLog.Current);
                } while (handleLog.MoveNext());
        }
        catch (Exception ex)
        {
            System.Diagnostics.Trace.TraceError(ex.Source + "; " + ex.Message + "\n" + ex.StackTrace);
        }
        return zpLogDating.ToArray();
    }

    public static string[] filterTraceTypeZPLogs(string[] zpLog, string traceType)
    {
        List<string> zpLogTracing = new List<string>();
        string strExprFilter = @"^.* \[(\d)\] .*$";
        try
        {
            if ("".Equals(zpLog) || "".Equals(traceType) || "0".Equals(traceType)) return zpLog;
            // for each ligne, check if it appartient to the needing tracing type
            bool isNeeded = false;
            Regex checkFilter = new Regex(strExprFilter);
            foreach (string strLigne in zpLog)
            {
                Match valueGetting = checkFilter.Match(strLigne);
                if (valueGetting.Success && valueGetting.Groups.Count > 1)
                    isNeeded = traceType.Equals(valueGetting.Groups[1].Value);
                if (isNeeded)
                    zpLogTracing.Add(strLigne);
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Trace.TraceError(ex.Source + "; " + ex.Message + "\n" + ex.StackTrace);
        }
        return zpLogTracing.ToArray();
    }

    public static string getAnalyseZPLogs(string machineName, string zpPath, string traceTypeLogs, int filterTimeLogs)
    {
        try
        {

            if (!System.IO.File.Exists(zpPath)) return "File not found! ";
            string[] zpLog = System.IO.File.ReadAllLines(zpPath);

            /* filter by Date */
            zpLog = filterDateZPLogs(zpLog, filterTimeLogs);

            /* filter by error type */
            zpLog = filterTraceTypeZPLogs(zpLog, traceTypeLogs);

            return string.Join("\n", zpLog);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Trace.TraceError(ex.Source + "; " + ex.Message + "\n" + ex.StackTrace);
            return "Error in Analyse Oracle log files";
        }
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