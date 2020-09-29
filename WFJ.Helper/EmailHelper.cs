using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace WFJ.Helper
{
    public class EmailHelper
    {
        public static void SendMail(string toEmail,string subject,string body)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress("nt26797@gmail.com");
            mail.To.Add("kapil.kumar.sharma475@gmail.com");
            mail.Subject = subject;
            mail.Body = body;

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("nt26797@gmail.com", "9219250730");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);
        }
    }
}
