using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Melody49Notifier.Models;
using Microsoft.Azure.WebJobs.Host;
using System.IO;
using Newtonsoft.Json;

namespace Melody49Notifier.DataAbstraction
{
    public class CurrentTheaterScheduleDataFileManager : ICurrentTheaterScheduleDataFileManager
    {
        private readonly TraceWriter log;

        public CurrentTheaterScheduleDataFileManager(TraceWriter log)
        {
            this.log = log;
        }

        static string HomeDirectory => Environment.GetEnvironmentVariable("HOME") ?? $"{Environment.GetEnvironmentVariable("HOMEDRIVE")}{Environment.GetEnvironmentVariable("HOMEPATH")}";
        static string ApplicationDirectory => $"{HomeDirectory}\\data\\Melody49Notifier";
        static string CurrentTheaterScheduleDataFile => $"{ApplicationDirectory}\\currentTheaterSchedule.json";

        public TheaterSchedule SelectCurrentTheaterSchedule()
        {
            if (File.Exists(CurrentTheaterScheduleDataFile))
            {
                string currentTheaterScheduleDataFileContents = File.ReadAllText(CurrentTheaterScheduleDataFile);

                log.Verbose($"Obtained Current Theater Schedule Data File Contents: {currentTheaterScheduleDataFileContents}");

                return JsonConvert.DeserializeObject<TheaterSchedule>(currentTheaterScheduleDataFileContents);
            }

            log.Verbose($"The Current Theater Schedule Data File Does Not Exist.");

            return null;
        }
        
        public void UpdateCurrentTheaterSchedule(TheaterSchedule theaterSchedule)
        {
            if (!Directory.Exists(ApplicationDirectory))
            {
                log.Verbose($"Creating Application Directory: ({ApplicationDirectory})");

                Directory.CreateDirectory(ApplicationDirectory);
            }

            string serializedTheaterSchedule = JsonConvert.SerializeObject(theaterSchedule);

            log.Verbose($"Saving the Current Theater Schedule: ({serializedTheaterSchedule})");

            File.WriteAllText(CurrentTheaterScheduleDataFile, serializedTheaterSchedule);
        }
    }
}
