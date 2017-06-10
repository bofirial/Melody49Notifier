using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Melody49Notifier.DataAbstraction;
using Melody49Notifier.Models;

namespace Melody49Notifier
{
    public static class Melody49Notifier
    {

        [FunctionName("Melody49Notifier")]
        public static void Run([TimerTrigger("0/10 * * * * *")]TimerInfo myTimer, TraceWriter log) //[TimerTrigger("0 0 8-18 * * FRI")]TimerInfo myTimer, TraceWriter log)
        {
            log.Info($"C# Timer trigger function started at: {DateTime.Now}.");

            if (TheaterScheduleHasUpdated(log, out TheaterSchedule currentTheaterSchedule))
            {
                SendNotification(log, currentTheaterSchedule);
            }

            log.Info($"C# Timer trigger function completed at: {DateTime.Now}.");
        }

        private static bool TheaterScheduleHasUpdated(TraceWriter log, out TheaterSchedule currentTheaterSchedule)
        {
            throw new NotImplementedException();
        }

        private static void SendNotification(TraceWriter log, TheaterSchedule currentTheaterSchedule)
        {
            throw new NotImplementedException();
        }
    }
}