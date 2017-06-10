using Melody49Notifier.Models;

namespace Melody49Notifier.DataAbstraction
{
    public interface ICurrentTheaterScheduleDataFileManager
    {
        TheaterSchedule SelectCurrentTheaterSchedule();

        void UpdateCurrentTheaterSchedule(TheaterSchedule theaterSchedule);
    }
}