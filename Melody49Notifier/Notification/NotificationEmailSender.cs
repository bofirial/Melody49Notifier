using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Melody49Notifier.Models;
using Microsoft.Azure.WebJobs.Host;
using System.Net.Mail;
using System.Net;
using System.IO;

namespace Melody49Notifier.Notification
{
    public class NotificationEmailSender : INotificationEmailSender
    {
        private readonly INotificationEmailGenerator notificationEmailGenerator;

        public NotificationEmailSender(TraceWriter log, INotificationEmailGenerator notificationEmailGenerator)
        {
            Log = log;
            this.notificationEmailGenerator = notificationEmailGenerator;
        }

        public TraceWriter Log { get; }

        public void SendNotificationEmail(TheaterSchedule currentTheaterSchedule)
        {
            MailMessage message = new MailMessage()
            {
                From = new MailAddress(Environment.GetEnvironmentVariable("FromEmailAddress"), "Melody 49 Drive-In"),
                Subject = "New Showings at the Melody 49 Drive-In",
                Body = notificationEmailGenerator.CreateFromTemplate(currentTheaterSchedule),
                IsBodyHtml = true
            };

            string[] emailAddresses = Environment.GetEnvironmentVariable("ToEmailAddresses").Split(',');

            foreach (string emailAddress in emailAddresses)
            {
                message.To.Add(emailAddress);
            }

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(Environment.GetEnvironmentVariable("SMTPUserName"), Environment.GetEnvironmentVariable("SMTPPassword"), "")
            };

            smtpClient.Send(message);
        }
    }
}
