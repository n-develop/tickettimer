using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using EasyHttp.Http;
using TicketTimer.Core.Extensions;
using TicketTimer.Core.Infrastructure;
using TicketTimer.Core.Utils;
using TicketTimer.Youtrack.Extensions;
using YouTrackSharp.Issues;

namespace TicketTimer.Youtrack.Services
{
    public class DefaultYoutrackService : YoutrackService
    {
        private const string PrefixSettingName = "youtrackIssuePrefix";

        private readonly CustomConnection _connection;
        private readonly WorkItemStore _workItemStore;
        private readonly List<string> _successfullyLoggedItems;

        public DefaultYoutrackService(CustomConnection connection, WorkItemStore workItemStore)
        {
            _connection = connection;
            _workItemStore = workItemStore;
            _successfullyLoggedItems = new List<string>();
        }

        public List<string> WriteEntireArchive()
        {
            var youtrackIssues = _workItemStore.GetState().WorkItemArchive;
            var prefixes = GetYoutrackIssuePrefixes();
            if (prefixes != null && prefixes.Any())
            {
                youtrackIssues = youtrackIssues.Where(issue => prefixes.Any(prefix => issue.TicketNumber.StartsWith(prefix))).ToList();
            }
            var aggregatedWorkItems = WorkItemAggregator.AggregateWorkItems(youtrackIssues);
            _successfullyLoggedItems.Clear();

            foreach (var workItem in aggregatedWorkItems)
            {
                TrackTime(workItem);
            }

            return _successfullyLoggedItems;
        }

        private void TrackTime(WorkItem workItem)
        {
            try
            {
                var youtrackIssue = GetIssueById(workItem.TicketNumber);
                if (youtrackIssue != null)
                {
                    WriteTimeToYoutrack(workItem);
                    if (!_successfullyLoggedItems.Contains(workItem.TicketNumber))
                    {
                        _successfullyLoggedItems.Add(workItem.TicketNumber);
                    }
                }
                else
                {
                    Console.WriteLine($"{workItem.TicketNumber} is not a Youtrack issue.");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{workItem.TicketNumber} could not be saved in Youtrack. Reason: '{exception.Message}'");
            }
        }

        private void WriteTimeToYoutrack(WorkItem workItem)
        {
            var xmlFormat = @"<workItem>
                                <date>{0}</date>
                                <duration>{1}</duration>
                                <description>{2}</description>
                            </workItem>";
            var duration = workItem.Duration.RoundUp(5);

            var xmlData = string.Format(xmlFormat, workItem.Started.ToYoutrackDate(), (int)duration.TotalMinutes, workItem.Comment);
            _connection.Post($"issue/{workItem.TicketNumber}/timetracking/workitem", xmlData, HttpContentTypes.ApplicationXml, HttpContentTypes.ApplicationXml);

            Console.WriteLine($"Work item {workItem.TicketNumber} written. {duration.ToShortString()} on {workItem.Started.ToShortDateString()}");

        }

        public Issue GetIssueById(string issueId)
        {
            var issuesManagement = new IssueManagement(_connection);
            var issue = issuesManagement.GetIssue(issueId);
            return issue;
        }

        private List<string> GetYoutrackIssuePrefixes()
        {
            if (ConfigurationManager.AppSettings.AllKeys.Contains(PrefixSettingName))
            {
                var prefixSettings = ConfigurationManager.AppSettings[PrefixSettingName];
                return prefixSettings.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            }
            return new List<string>();
        }
    }
}