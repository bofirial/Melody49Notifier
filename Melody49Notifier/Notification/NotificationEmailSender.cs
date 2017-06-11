using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Melody49Notifier.Models;
using Microsoft.Azure.WebJobs.Host;

namespace Melody49Notifier.Notification
{
    public class NotificationEmailSender : INotificationEmailSender
    {
        public NotificationEmailSender(TraceWriter log)
        {
            Log = log;
        }

        public TraceWriter Log { get; }

        public void SendNotificationEmail(TheaterSchedule currentTheaterSchedule)
        {
            throw new NotImplementedException();
        }
    }
}
