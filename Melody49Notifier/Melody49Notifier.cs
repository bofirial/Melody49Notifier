using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;

namespace Melody49Notifier
{
    public static class Melody49Notifier
    {
        [FunctionName("Melody49Notifier")]
        public static void Run([TimerTrigger("0 0 8-18 * * FRI")]TimerInfo myTimer, TraceWriter log)
        {
            log.Info($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}