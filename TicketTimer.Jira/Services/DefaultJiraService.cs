﻿using System;
using Atlassian.Jira;
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
            foreach (var workItem in archive)
            {
                try
                {
                    var jiraIssue = _jiraClient.Issues.GetIssueAsync(workItem.TicketNumber).Result;
                    if (jiraIssue != null)
                    {
                        TrackTime(workItem);
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine($"{workItem.TicketNumber} is not a jira ticket.");
                }
            }
        }

        private void TrackTime(WorkItem workItem)
        {
            var workLog = new Worklog(workItem.Duration.ToJiraFormat(), workItem.Started.Date, workItem.Comment);
            Console.WriteLine($"Work item {workItem.TicketNumber} written. {workItem.Duration.ToJiraFormat()} on {workItem.Started.ToShortDateString()}");
            //_jiraClient.Issues.AddWorklogAsync(workItem.TicketNumber, workLog);
        }
    }
}
