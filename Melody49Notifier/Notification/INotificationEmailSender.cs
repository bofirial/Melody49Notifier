using Melody49Notifier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Melody49Notifier.Notification
{
    public interface INotificationEmailSender
    {
        void SendNotificationEmail(TheaterSchedule currentTheaterSchedule);
    }
}
