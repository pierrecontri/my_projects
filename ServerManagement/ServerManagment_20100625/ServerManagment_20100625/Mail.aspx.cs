#define TRACE

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
using System.Net.Mail;

public partial class Mail: System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    private void ButtonSend_Click(object sender, System.EventArgs e)
    {
       try
        {  
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(TBExpediteur.Text);
          
            mail.Subject = TBObjet.Text;
            mail.Body = TBMessage.Text;

            SmtpClient client = new SmtpClient();
            client.Send(mail);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Trace.TraceError(ex.Source + "; " + ex.Message + "\n" + ex.StackTrace);
        }
    }
}
