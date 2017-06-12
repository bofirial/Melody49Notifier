using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Melody49Notifier.Models;
using Microsoft.Azure.WebJobs.Host;
using System.IO;
using System.Reflection;

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
            string email = GetEmbeddedResourceText("Melody49Notifier.EmailTemplate.html");
            string showingTemplate = GetEmbeddedResourceText("Melody49Notifier.EmailShowingTemplate.html");

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

        private static string GetEmbeddedResourceText(string resourceName)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
