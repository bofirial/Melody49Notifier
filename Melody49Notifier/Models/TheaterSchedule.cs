using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Melody49Notifier.Models
{
    public class TheaterSchedule
    {
        public string TheaterName { get; set; }

        public string ScheduleDescription { get; set; }

        public List<TheaterShowing> Showings { get; set; }
    }
}
