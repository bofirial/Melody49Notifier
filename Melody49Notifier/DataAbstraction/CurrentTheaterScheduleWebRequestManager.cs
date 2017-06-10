using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Melody49Notifier.Models;
using System.Net;
using System.IO;
using Microsoft.Azure.WebJobs.Host;

namespace Melody49Notifier.DataAbstraction
{
    public class CurrentTheaterScheduleWebRequestManager : ICurrentTheaterScheduleWebRequestManager
    {
        private readonly TraceWriter log;
        private readonly ITheaterScheduleHTMLParser theaterScheduleHTMLParser;

        public CurrentTheaterScheduleWebRequestManager(TraceWriter log, ITheaterScheduleHTMLParser theaterScheduleHTMLParser)
        {
            this.log = log;
            this.theaterScheduleHTMLParser = theaterScheduleHTMLParser;
        }

        public string TheaterUrl => "http://www.chakerestheatres.com/melody49di.php";

        public TheaterSchedule GetCurrentTheaterSchedule()
        {
            string currentTheaterHTML = GetCurrentTheaterScheduleHTML();

            log.Verbose($"Received Current Theater Schedule HTML.  ({currentTheaterHTML.Length} Characters)");

            return theaterScheduleHTMLParser.ParseTheaterScheduleHTML(currentTheaterHTML);
        }

        private string GetCurrentTheaterScheduleHTML()
        {
            string currentTheaterHTML;
            WebRequest webRequest = WebRequest.Create(TheaterUrl);
            WebResponse webResponse = webRequest.GetResponse();

            using (StreamReader streamReader = new StreamReader(webResponse.GetResponseStream()))
            {
                currentTheaterHTML = streamReader.ReadToEnd();
            }

            return currentTheaterHTML;
        }
    }
}
