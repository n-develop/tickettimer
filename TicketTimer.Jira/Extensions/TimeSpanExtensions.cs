using System;
using System.Text;

namespace TicketTimer.Jira.Extensions
{
    public static class TimeSpanExtensions
    {
        public static string ToJiraFormat(this TimeSpan timespan)
        {
            var jiraLog = new StringBuilder();

            if (timespan.Hours > 0)
            {
                jiraLog.Append($"{timespan.Hours}h ");
            }
            if (timespan.Minutes > 0)
            {
                jiraLog.Append($"{timespan.Minutes}m");
            }

            return jiraLog.ToString().Trim();
        }

        public static TimeSpan RoundUp(this TimeSpan timeSpan)
        {
            var differenceFromStep = timeSpan.Minutes % 5;
            if (differenceFromStep != 0)
            {
                return timeSpan.Add(new TimeSpan(0, 5 - differenceFromStep, 0));
            }
            return timeSpan;
        }
    }
}
