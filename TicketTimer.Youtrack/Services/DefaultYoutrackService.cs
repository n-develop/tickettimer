using System;
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

            foreach (var workItem in archive)
            {
                try
                {
                    var youtrackIssue = GetIssueById(workItem.TicketNumber);
                    if (youtrackIssue != null)
                    {
                        TrackTime(workItem);
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"{workItem.TicketNumber} could not be saved in Youtrack. Reason: '{exception.Message}'");
                }
            }
        }

        private void TrackTime(WorkItem workItem)
        {
            var xmlFormat = @"<workItem>
                                <date>{0}</date>
                                <duration>{1}</duration>
                                <description>{2}</description>
                            </workItem>";

            var xmlData = string.Format(xmlFormat, workItem.Started.ToYoutrackDate(), (int)workItem.Duration.RoundUp(5).TotalMinutes, workItem.Comment);
            _connection.Post($"issue/{workItem.TicketNumber}/timetracking/workitem", xmlData, HttpContentTypes.ApplicationXml, HttpContentTypes.ApplicationXml);

        }

        public Issue GetIssueById(string issueId)
        {
            var issuesManagement = new IssueManagement(_connection);
            var issue = issuesManagement.GetIssue(issueId);
            return issue;
        }
    }
}