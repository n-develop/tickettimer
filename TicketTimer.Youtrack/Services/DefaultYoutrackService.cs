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
        private const string AggregatePrefixSettingName = "youtrackAggregatePrefix";

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
            var prefixes = GetYoutrackIssuePrefixes();

            var youtrackIssues = _workItemStore.GetState().WorkItemArchive;
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

        public void WriteAggregate(string targetTicket)
        {
            var prefixes = GetYoutrackAggregatePrefixes();
            if (prefixes == null || !prefixes.Any())
            {
                Console.WriteLine("No prefixes for aggregation specified.");
                return;
            }

            var youtrackIssues = _workItemStore.GetState().WorkItemArchive;
            youtrackIssues = youtrackIssues.Where(issue => prefixes.Any(prefix => issue.TicketNumber.StartsWith(prefix))).ToList();

            var aggregatesPerDay = WorkItemAggregator.AggregateToSingleWorkItemPerDay(youtrackIssues, targetTicket);

            if (aggregatesPerDay == null || !aggregatesPerDay.Any())
            {
                Console.WriteLine("No aggregates to process.");
                return;
            }

            _successfullyLoggedItems.Clear();

            foreach (var workItem in aggregatesPerDay)
            {
                TrackTime(workItem);
            }

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

        private List<string> GetYoutrackAggregatePrefixes()
        {
            return GetListFromSettings(AggregatePrefixSettingName);
        }

        private List<string> GetYoutrackIssuePrefixes()
        {
            return GetListFromSettings(PrefixSettingName);
        }

        private List<string> GetListFromSettings(string settingsName)
        {
            if (ConfigurationManager.AppSettings.AllKeys.Contains(settingsName))
            {
                var settings = ConfigurationManager.AppSettings[settingsName];
                return settings.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            }
            return new List<string>();
        }
    }
}