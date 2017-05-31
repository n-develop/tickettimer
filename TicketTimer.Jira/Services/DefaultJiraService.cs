using Atlassian.Jira;
using TicketTimer.Core.Infrastructure;
using TicketTimer.Jira.Extensions;

namespace TicketTimer.Jira.Services
{
    public class DefaultJiraService : JiraService
    {
        private readonly WorkItemStore _workItemStore;

        // TODO Insert correct parameters
        public Atlassian.Jira.Jira JiraClient => Atlassian.Jira.Jira.CreateRestClient("JiraUrl", "JiraUserName", "JiraPassword");

        public DefaultJiraService(WorkItemStore workItemStore)
        {
            _workItemStore = workItemStore;
        }

        public async void WriteEntireArchive()
        {
            var archive = _workItemStore.GetState().WorkItemArchive;
            foreach (var workItem in archive)
            {
                var jiraIssue = await JiraClient.Issues.GetIssueAsync(workItem.TicketNumber);
                if (jiraIssue != null)
                {
                    TrackTime(workItem);
                }
            }
        }

        private void TrackTime(WorkItem workItem)
        {
            var workLog = new Worklog(workItem.Duration.ToJiraFormat(), workItem.Started.Date, workItem.Comment);

            JiraClient.Issues.AddWorklogAsync(workItem.TicketNumber, workLog);
        }
    }
}
