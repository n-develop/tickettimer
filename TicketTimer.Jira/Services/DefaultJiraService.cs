using System;
using System.Collections.Generic;
using Atlassian.Jira;
using TicketTimer.Core.Extensions;
using TicketTimer.Core.Infrastructure;
using TicketTimer.Jira.Extensions;

namespace TicketTimer.Jira.Services
{
    public class DefaultJiraService : JiraService
    {
        private readonly WorkItemStore _workItemStore;
        private readonly Atlassian.Jira.Jira _jiraClient;

        public DefaultJiraService(WorkItemStore workItemStore, Atlassian.Jira.Jira jiraClient)
        {
            _workItemStore = workItemStore;
            _jiraClient = jiraClient;
        }

        public void WriteEntireArchive()
        {
            var archive = _workItemStore.GetState().WorkItemArchive;
            var successfullyLogged = new List<string>();
            foreach (var workItem in archive)
            {
                try
                {
                    var jiraIssue = _jiraClient.Issues.GetIssueAsync(workItem.TicketNumber).Result;
                    if (jiraIssue != null)
                    {
                        TrackTime(workItem);
                        if (!successfullyLogged.Contains(workItem.TicketNumber))
                        {
                            successfullyLogged.Add(workItem.TicketNumber);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"{workItem.TicketNumber} is not a jira ticket.");

                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"{workItem.TicketNumber} could not be saved in Jira. Reason: '{exception.Message}'");
                }
            }
            _workItemStore.RemoveRangeFromArchive(successfullyLogged);
        }

        

        private void TrackTime(WorkItem workItem)
        {
            var duration = workItem.Duration.RoundUp(5);
            var workLog = new Worklog(duration.ToJiraFormat(), workItem.Started.Date, workItem.Comment);

            _jiraClient.Issues.AddWorklogAsync(workItem.TicketNumber, workLog);
            Console.WriteLine($"Work item {workItem.TicketNumber} written. {duration.ToJiraFormat()} on {workItem.Started.ToShortDateString()}");
        }
    }
}
