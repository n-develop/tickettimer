using System;
using TicketTimer.Core.Infrastructure;
using YouTrackSharp.Infrastructure;
using YouTrackSharp.Issues;

namespace TicketTimer.Youtrack.Services
{
    public class DefaultYoutrackService : YoutrackService
    {
        private readonly IConnection _connection;
        private readonly WorkItemStore _workItemStore;

        public DefaultYoutrackService(IConnection connection, WorkItemStore workItemStore)
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
                catch (Exception)
                {
                    Console.WriteLine($"{workItem.TicketNumber} is not a YouTrack ticket.");
                }
            }
        }

        private void TrackTime(WorkItem workItem)
        {
            throw new NotImplementedException();
        }

        public Issue GetIssueById(string issueId)
        {
            var issuesManagement = new IssueManagement(_connection);
            var issue = issuesManagement.GetIssue(issueId);
            return issue;
        }
    }
}