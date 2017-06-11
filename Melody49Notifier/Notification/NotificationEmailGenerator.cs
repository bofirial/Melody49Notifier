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
        private readonly TraceWriter log;

        public NotificationEmailGenerator(TraceWriter log)
        {
            this.log = log;
        }

        public string CreateFromTemplate(TheaterSchedule currentTheaterSchedule)
        {
            string email = File.ReadAllText("EmailTemplate.html");
            string showingTemplate = File.ReadAllText("EmailShowingTemplate.html");

            string showings = string.Empty;

            foreach (TheaterShowing theaterShowing in currentTheaterSchedule.Showings)
            {
                string showing = string.Copy(showingTemplate);

                showing = showing.Replace("%Screen%", theaterShowing.Screen);
                showing = showing.Replace("%MovieDescription%", theaterShowing.MovieDescription);
                showing = showing.Replace("%ActorDescription%", theaterShowing.ActorDescription);
                showing = showing.Replace("%ShowingScheduleDescription%", theaterShowing.ShowingScheduleDescription);

                showings += showing;
            }

            email = email.Replace("%ScheduleDescription%", currentTheaterSchedule.ScheduleDescription);
            email = email.Replace("%Showings%", showings);

            return email;
        }
    }
}
