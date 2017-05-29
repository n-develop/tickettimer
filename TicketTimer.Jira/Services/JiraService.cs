using Atlassian.Jira;
using TicketTimer.Core.Infrastructure;
using TicketTimer.Jira.Extensions;

namespace TicketTimer.Jira.Services
{
    // TODO introduce interface for better testability
    public class JiraService
    {
        private readonly WorkItemStore _workItemStore;

        // TODO Insert correct parameters
        public Atlassian.Jira.Jira JiraClient => Atlassian.Jira.Jira.CreateRestClient("JiraUrl", "JiraUserName", "JiraPassword");

        public JiraService(WorkItemStore workItemStore)
        {
            _workItemStore = workItemStore;
        }

        public void WriteEntireArchive()
        {
            var archive = _workItemStore.GetState().WorkItemArchive;
            foreach (var workItem in archive)
            {
                // TODO Check if work item is jira-item.
                TrackTime(workItem);
            }
        }

        private void TrackTime(WorkItem workItem)
        {
            var workLog = new Worklog(workItem.Duration.ToJiraFormat(), workItem.Started.Date, workItem.Comment);

            JiraClient.Issues.AddWorklogAsync(workItem.TicketNumber, workLog);
        }
    }
}
