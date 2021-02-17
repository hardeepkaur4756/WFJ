using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace WFJ.Helper
{
    public class EmailHelper
    {
        public static void SendMail(string toEmail,string subject,string body,bool isReply = false)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(ConfigurationManager.AppSettings["SMTPHost"]);//"mail.tela.com");
            mail.From = new MailAddress(Convert.ToString(ConfigurationManager.AppSettings["EmailUserName"]));
            mail.To.Add(toEmail);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            if (isReply)
            {
                mail.ReplyTo = new MailAddress(Convert.ToString(ConfigurationManager.AppSettings["EmailUserName"]));
            }
            SmtpServer.Port = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"]);
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new System.Net.NetworkCredential(Convert.ToString(ConfigurationManager.AppSettings["EmailUserName"]), Convert.ToString(ConfigurationManager.AppSettings["EmailPassword"]));
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);
        }
    }
}
