using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Melody49Notifier.Models;
using Microsoft.Azure.WebJobs.Host;
using System.IO;

namespace Melody49Notifier.Notification
{
    public class NotificationEmailGenerator : INotificationEmailGenerator
    {
        public NotificationEmailGenerator(TraceWriter log)
        {
            Log = log;
        }

        public TraceWriter Log { get; }

        public string CreateFromTemplate(TheaterSchedule currentTheaterSchedule)
        {
            return File.ReadAllText("EmailTemplate.html");
        }
    }
}
