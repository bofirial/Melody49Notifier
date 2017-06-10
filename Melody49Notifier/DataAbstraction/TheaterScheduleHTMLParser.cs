using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Melody49Notifier.Models;
using Microsoft.Azure.WebJobs.Host;

namespace Melody49Notifier.DataAbstraction
{
    public class TheaterScheduleHTMLParser : ITheaterScheduleHTMLParser
    {
        private readonly TraceWriter log;

        public TheaterScheduleHTMLParser(TraceWriter log)
        {
            this.log = log;
        }

        public TheaterSchedule ParseTheaterScheduleHTML(string html)
        {
            throw new NotImplementedException();
        }
    }
}
