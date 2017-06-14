using System;
using System.Collections.Generic;
using EasyHttp.Http;
using TicketTimer.Core.Extensions;
using TicketTimer.Core.Infrastructure;
using TicketTimer.Youtrack.Extensions;
using YouTrackSharp.Issues;

namespace TicketTimer.Youtrack.Services
{
    public class DefaultYoutrackService : YoutrackService
    {
        private readonly CustomConnection _connection;
        private readonly WorkItemStore _workItemStore;

        public DefaultYoutrackService(CustomConnection connection, WorkItemStore workItemStore)
        {
            _connection = connection;
            _workItemStore = workItemStore;
        }

        public void WriteEntireArchive()
        {
            var archive = _workItemStore.GetState().WorkItemArchive;
            var successfullyLogged = new List<string>();

            foreach (var workItem in archive)
            {
                try
                {
                    var youtrackIssue = GetIssueById(workItem.TicketNumber);
                    if (youtrackIssue != null)
                    {
                        TrackTime(workItem);
                        if (!successfullyLogged.Contains(workItem.TicketNumber))
                        {
                            successfullyLogged.Add(workItem.TicketNumber);
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

            _workItemStore.RemoveRangeFromArchive(successfullyLogged);
        }

        private void TrackTime(WorkItem workItem)
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
    }
}