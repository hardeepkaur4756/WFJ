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
            SmtpClient SmtpServer = new SmtpClient();
            mail.To.Add(toEmail);
            if (isReply)
            {
                mail.ReplyTo = new MailAddress(Convert.ToString(ConfigurationManager.AppSettings["EmailUserName"]));
            }
            mail.From = new MailAddress("servicerequests@wfjlawfirm.com");
            mail.Subject = subject;
            mail.IsBodyHtml = true;
            mail.Body = body;
            SmtpServer.Host = "clientmailrelay.tela.com";
            SmtpServer.Port = 25;
            SmtpServer.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            try
            {
                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
