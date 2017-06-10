using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Melody49Notifier.Models;
using Microsoft.Azure.WebJobs.Host;
using HtmlAgilityPack;

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
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            TheaterSchedule theaterSchedule = new TheaterSchedule();

            //theaterSchedule.ScheduleDescription = htmlDocument.DocumentNode.Descendants("div").Where(x => x?.Attributes?["id"]?.Value == "playweek").First().ChildNodes.First().InnerText;

            theaterSchedule.ScheduleDescription = htmlDocument.DocumentNode.SelectNodes("//div[@id=\"playweek\"]/p").First().InnerText;

            return theaterSchedule;
        }
    }
}
