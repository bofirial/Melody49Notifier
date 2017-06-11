using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Melody49Notifier.Models;

namespace Melody49Notifier.Notification
{
    public interface INotificationEmailGenerator
    {
        string CreateFromTemplate(TheaterSchedule currentTheaterSchedule);
    }
}
