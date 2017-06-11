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
            bool areEqual = true;

            areEqual = areEqual && firstTheaterSchedule?.ScheduleDescription == secondTheaterSchedule?.ScheduleDescription;
            areEqual = areEqual && firstTheaterSchedule?.TheaterName == secondTheaterSchedule?.TheaterName;
            areEqual = areEqual && firstTheaterSchedule?.Showings?.Count == secondTheaterSchedule?.Showings?.Count;

            for (int i = 0; i < firstTheaterSchedule?.Showings?.Count; i++)
            {
                areEqual = areEqual && firstTheaterSchedule?.Showings?[i]?.MovieDescription == secondTheaterSchedule?.Showings?[i]?.MovieDescription;
                //areEqual = areEqual && firstTheaterSchedule?.Showings?[i]?.Screen == secondTheaterSchedule?.Showings?[i]?.Screen;
                //areEqual = areEqual && firstTheaterSchedule?.Showings?[i]?.ShowingScheduleDescription == secondTheaterSchedule?.Showings?[i]?.ShowingScheduleDescription;
                //areEqual = areEqual && firstTheaterSchedule?.Showings?[i]?.ActorDescription == secondTheaterSchedule?.Showings?[i]?.ActorDescription;
            }

            return areEqual;
        }
    }
}
