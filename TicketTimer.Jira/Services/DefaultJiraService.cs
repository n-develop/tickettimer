using System;
using System.Collections.Generic;
using Atlassian.Jira;
using TicketTimer.Core.Extensions;
using TicketTimer.Core.Infrastructure;
using TicketTimer.Core.Utils;
using TicketTimer.Jira.Extensions;

namespace TicketTimer.Jira.Services
{
    public class DefaultJiraService : JiraService
    {
        private readonly WorkItemStore _workItemStore;
        private readonly Atlassian.Jira.Jira _jiraClient;
        private readonly List<string> _successfullyLoggedItems;

        public DefaultJiraService(WorkItemStore workItemStore, Atlassian.Jira.Jira jiraClient)
        {
            _workItemStore = workItemStore;
            _jiraClient = jiraClient;
            _successfullyLoggedItems = new List<string>();
        }

        public void WriteEntireArchive()
        {
            var archive = _workItemStore.GetState().WorkItemArchive;
            var aggregatedWorkItems = WorkItemAggregator.AggregateWorkItems(archive);
            _successfullyLoggedItems.Clear();

            foreach (var workItem in aggregatedWorkItems)
            {
                TrackTime(workItem);
            }

            _workItemStore.RemoveRangeFromArchive(_successfullyLoggedItems);
        }

        private void TrackTime(WorkItem workItem)
        {
            try
            {
                var jiraIssue = _jiraClient.Issues.GetIssueAsync(workItem.TicketNumber).Result;
                if (jiraIssue != null)
                {
                    WriteTimeToJira(workItem);
                    if (!_successfullyLoggedItems.Contains(workItem.TicketNumber))
                    {
                        _successfullyLoggedItems.Add(workItem.TicketNumber);
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


        private void WriteTimeToJira(WorkItem workItem)
        {
            var duration = workItem.Duration.RoundUp(5);
            var workLog = new Worklog(duration.ToJiraFormat(), workItem.Started.Date, workItem.Comment);

            _jiraClient.Issues.AddWorklogAsync(workItem.TicketNumber, workLog);
            Console.WriteLine($"Work item {workItem.TicketNumber} written. {duration.ToJiraFormat()} on {workItem.Started.ToShortDateString()}");
        }
    }
}
