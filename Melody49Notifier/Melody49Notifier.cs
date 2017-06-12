using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Melody49Notifier.DataAbstraction;
using Melody49Notifier.Models;
using Melody49Notifier.Notification;

namespace Melody49Notifier
{
    public static class Melody49Notifier
    {

        [FunctionName("Melody49Notifier")]
        public static void Run([TimerTrigger("0 0 8-18 * * FRI")]TimerInfo myTimer, TraceWriter log)
        {
            log.Info($"C# Timer trigger function started at: {DateTime.Now}.");

            if (TheaterScheduleHasUpdated(log, out TheaterSchedule currentTheaterSchedule))
            {
                log.Info($"Theater Schedule has updated.  Sending Notifications.");
                SendNotification(log, currentTheaterSchedule);
            }
            else
            {
                log.Info($"Theater Schedule has not updated.");
            }

            log.Info($"C# Timer trigger function completed at: {DateTime.Now}.");
        }

        private static bool TheaterScheduleHasUpdated(TraceWriter log, out TheaterSchedule currentTheaterSchedule)
        {
            ICurrentTheaterScheduleDataFileManager currentTheaterScheduleDataFileManager = new CurrentTheaterScheduleDataFileManager(log);
            ICurrentTheaterScheduleWebRequestManager currentTheaterScheduleWebRequestManager = new CurrentTheaterScheduleWebRequestManager(log, new TheaterScheduleHTMLParser(log));
            ITheaterScheduleComparer theaterScheduleComparer = new TheaterScheduleComparer(log);

            TheaterSchedule currentTheaterScheduleFromFile = currentTheaterScheduleDataFileManager.SelectCurrentTheaterSchedule();
            TheaterSchedule currentTheaterScheduleFromWebSite = currentTheaterScheduleWebRequestManager.GetCurrentTheaterSchedule();

            currentTheaterSchedule = currentTheaterScheduleFromWebSite;

            if (!theaterScheduleComparer.AreEqual(currentTheaterScheduleFromFile, currentTheaterScheduleFromWebSite))
            {
                currentTheaterScheduleDataFileManager.UpdateCurrentTheaterSchedule(currentTheaterScheduleFromWebSite);

                return true;
            }

            return false;
        }

        private static void SendNotification(TraceWriter log, TheaterSchedule currentTheaterSchedule)
        {
            INotificationEmailSender notificationEmailSender = new NotificationEmailSender(log, new NotificationEmailGenerator(log));

            notificationEmailSender.SendNotificationEmail(currentTheaterSchedule);
        }
    }
}