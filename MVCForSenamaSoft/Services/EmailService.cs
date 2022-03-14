using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System.Linq;
using System;
using System.Threading.Tasks;
using System.Text;
using System.Net.Mail;
using System.Net;

namespace MVCForSenamaSoft.Services
{
    public class EmailService
    { 
        private readonly ILogger<EmailService> logger; // for tracking the process of sending a message or errors

        public EmailService(ILogger<EmailService> logger)
        {
            this.logger = logger;
        }

        public void SendEmail(string domain, string userName, string password) 
        {
            try
            {
                MailMessage message = new MailMessage();
                message.IsBodyHtml = true;
                message.From = new MailAddress("admin@mycompany.com", "My company");
                message.To.Add("davidmikut@mail.ru");
                message.Subject = "Message from the System.Net.Mail";
                message.Body = 
                    String.Format("<div>Domain: {0}<br>User name: {1}<br>Password: {2}</div>", domain, userName, password);
                //message.Attachments.Add(new Attachment("...path to file..."));

                using(SmtpClient client = new SmtpClient("smtp.gmail.com"))
                {
                    client.Credentials = new NetworkCredential("davidmikutishvili@gmail.com", "my12345lifeisGREAT");
                    client.Port = 587;
                    client.EnableSsl = true;

                    client.Send(message);
                    logger.LogInformation("Sending the message was successful!");
                }
            }
            catch (Exception e)
            {
                logger.LogError(e.GetBaseException().Message);
            }
        }
    }
}
