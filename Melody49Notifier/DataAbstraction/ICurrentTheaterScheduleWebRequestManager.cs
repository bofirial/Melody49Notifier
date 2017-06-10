using Melody49Notifier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Melody49Notifier.DataAbstraction
{
    public interface ICurrentTheaterScheduleWebRequestManager
    {
        TheaterSchedule GetCurrentTheaterSchedule();
    }
}
