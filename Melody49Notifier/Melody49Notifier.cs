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
            SetTraceLevel(log);

            log.Info($"C# Timer trigger function started at: {DateTime.Now}.");

            ICurrentTheaterScheduleDataFileManager currentTheaterScheduleDataFileManager = new CurrentTheaterScheduleDataFileManager(log);

            if (TheaterScheduleHasUpdated(log, currentTheaterScheduleDataFileManager, out TheaterSchedule currentTheaterSchedule))
            {
                log.Info($"The Theater Schedule has updated.  Sending Notifications.");
                SendNotification(log, currentTheaterSchedule);

                currentTheaterScheduleDataFileManager.UpdateCurrentTheaterSchedule(currentTheaterSchedule);
            }
            else
            {
                log.Info($"The Theater Schedule has not updated.");
            }

            log.Info($"C# Timer trigger function completed at: {DateTime.Now}.");
        }

        private static void SetTraceLevel(TraceWriter log)
        {
            switch (Environment.GetEnvironmentVariable("TraceLevel")?.ToLower())
            {
                case "error":
                    log.Level = System.Diagnostics.TraceLevel.Error;
                    break;
                case "warning":
                    log.Level = System.Diagnostics.TraceLevel.Warning;
                    break;
                case "verbose":
                    log.Level = System.Diagnostics.TraceLevel.Verbose;
                    break;
                case "info":
                default:
                    log.Level = System.Diagnostics.TraceLevel.Info;
                    break;
            }
        }

        private static bool TheaterScheduleHasUpdated(TraceWriter log, ICurrentTheaterScheduleDataFileManager currentTheaterScheduleDataFileManager, out TheaterSchedule currentTheaterSchedule)
    {
            ICurrentTheaterScheduleWebRequestManager currentTheaterScheduleWebRequestManager = new CurrentTheaterScheduleWebRequestManager(log, new TheaterScheduleHTMLParser(log));
            ITheaterScheduleComparer theaterScheduleComparer = new TheaterScheduleComparer(log);

            TheaterSchedule currentTheaterScheduleFromFile = currentTheaterScheduleDataFileManager.SelectCurrentTheaterSchedule();
            TheaterSchedule currentTheaterScheduleFromWebSite = currentTheaterScheduleWebRequestManager.GetCurrentTheaterSchedule();

            currentTheaterSchedule = currentTheaterScheduleFromWebSite;

            return !theaterScheduleComparer.AreEqual(currentTheaterScheduleFromFile, currentTheaterScheduleFromWebSite);
        }

        private static void SendNotification(TraceWriter log, TheaterSchedule currentTheaterSchedule)
        {
            INotificationEmailSender notificationEmailSender = new NotificationEmailSender(log, new NotificationEmailGenerator(log));

            notificationEmailSender.SendNotificationEmail(currentTheaterSchedule);
        }
    }
}