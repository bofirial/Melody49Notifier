using Microsoft.Azure.WebJobs.Host;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Melody49Notifier.Models
{
    public class TheaterScheduleComparer : ITheaterScheduleComparer
    {
        public TheaterScheduleComparer(TraceWriter log)
        {
            Log = log;
        }

        public TraceWriter Log { get; }

        public bool AreEqual(TheaterSchedule firstTheaterSchedule, TheaterSchedule secondTheaterSchedule)
        {
            throw new NotImplementedException();
        }
    }
}
