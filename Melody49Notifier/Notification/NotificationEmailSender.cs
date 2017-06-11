using System;
using Melody49Notifier.Models;
using Microsoft.Azure.WebJobs.Host;
using System.Net.Mail;
using System.Net;

namespace Melody49Notifier.Notification
{
    public class NotificationEmailSender : INotificationEmailSender
    {
        private readonly TraceWriter log;
        private readonly INotificationEmailGenerator notificationEmailGenerator;

        public NotificationEmailSender(TraceWriter log, INotificationEmailGenerator notificationEmailGenerator)
        {
            this.log = log;
            this.notificationEmailGenerator = notificationEmailGenerator;
        }
        
        public void SendNotificationEmail(TheaterSchedule currentTheaterSchedule)
        {
            MailMessage message = new MailMessage()
            {
                From = new MailAddress(Environment.GetEnvironmentVariable("FromEmailAddress"), "Melody 49 Drive-In"),
                Subject = $"New Showings at the Melody 49 Drive-In - {currentTheaterSchedule.ScheduleDescription}",
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

            log.Info($"Sending the Current Theater Schedule Notification to: {string.Join(", ", emailAddresses)}.");

            smtpClient.Send(message);
        }
    }
}
