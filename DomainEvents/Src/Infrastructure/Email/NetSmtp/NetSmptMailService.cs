using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace DomainEvents.Src.Infrastructure.Email.NetSmtp
{
    public class NetSmptMailService : IEmailSender
    {
        public void SendEmail(string from, string to, string message, string subject)
        {
            MailMessage msg = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            msg.From = new MailAddress(from);
            msg.To.Add(new MailAddress(to));
            msg.Subject = subject;
            msg.IsBodyHtml = true; //to make message body as html  
            msg.Body = message;
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com"; //for gmail host  
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(from, "Nbuy12345");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(msg);
        }
    }
}
