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
    }
}
