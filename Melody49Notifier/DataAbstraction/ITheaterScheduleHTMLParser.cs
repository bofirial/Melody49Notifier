using Melody49Notifier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Melody49Notifier.DataAbstraction
{
    public interface ITheaterScheduleHTMLParser
    {
        TheaterSchedule ParseTheaterScheduleHTML(string html);
    }
}
