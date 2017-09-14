using System;
using System.Net.Mail;

namespace TestsUnitaires
{
    class MailError
    {
        public static string SendMailError(string smpt, string from, string recipients, string subject, string body)
        {
            try
            {
                SmtpClient clientMail = new SmtpClient(smpt);
                clientMail.SendAsync(from, recipients, subject, body, null);
                return "ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
