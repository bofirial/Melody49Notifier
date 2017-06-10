using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Melody49Notifier.Models
{
    public interface ITheaterScheduleComparer
    {
        bool AreEqual(TheaterSchedule firstTheaterSchedule, TheaterSchedule secondTheaterSchedule);
    }
}
