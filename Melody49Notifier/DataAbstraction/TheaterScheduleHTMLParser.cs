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

            theaterSchedule.ScheduleDescription = GetScheduleDescription(htmlDocument);

            HtmlNodeCollection tableRows = GetTheaterShowingsTableRows(htmlDocument);

            string currentScreen = string.Empty;
            TheaterShowing currentTheaterShowing = null;
            theaterSchedule.Showings = new List<TheaterShowing>();

            foreach (HtmlNode tableRow in tableRows)
            {
                string column1Text = GetColumnText(tableRow, 0);
                string column2Text = GetColumnText(tableRow, 2);

                if (IsScreenRow(column1Text, column2Text))
                {
                    currentScreen = column1Text.Replace(":", "").Trim();
                }
                else if (!IsEmptyString(column1Text) && !IsEmptyString(column2Text))
                {
                    if (IsShowingScheduleRow(column2Text))
                    {
                        currentTheaterShowing.ShowingScheduleDescription = $"{currentTheaterShowing.ShowingScheduleDescription}{column1Text} {column2Text}\r\n";
                    }
                    else
                    {
                        currentTheaterShowing = new TheaterShowing()
                        {
                            Screen = currentScreen,
                            MovieDescription = column1Text,
                            ActorDescription = column2Text
                        };

                        theaterSchedule.Showings.Add(currentTheaterShowing);
                    }
                }
            }

            return theaterSchedule;
        }

        private static HtmlNodeCollection GetTheaterShowingsTableRows(HtmlDocument htmlDocument)
        {
            return htmlDocument.DocumentNode.SelectNodes("(//body/div/table)[2]//tr");
        }

        private static string GetScheduleDescription(HtmlDocument htmlDocument)
        {
            return htmlDocument.DocumentNode.SelectNodes("//div[@id=\"playweek\"]/p").First().InnerText;
        }

        private static bool IsShowingScheduleRow(string column2Text)
        {
            return TimeSpan.TryParse(column2Text.Trim(), out TimeSpan time);
        }

        private static bool IsScreenRow(string column1Text, string column2Text)
        {
            return column1Text.ToLower().Contains("screen") && IsEmptyString(column2Text);
        }

        private static string GetColumnText(HtmlNode tableRow, int index)
        {
            return tableRow.ChildNodes.Where(x => x.Name == "td").ElementAt(index).InnerText;
        }

        private static bool IsEmptyString(string columnText)
        {
            string trimmedLoweredColumnText = columnText?.Trim()?.ToLower();

            return string.IsNullOrEmpty(trimmedLoweredColumnText) || trimmedLoweredColumnText == "&nbsp;";
        }
    }
}
