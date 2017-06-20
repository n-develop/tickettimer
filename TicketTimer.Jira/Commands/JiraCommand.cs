using ManyConsole;
using TicketTimer.Core.Infrastructure;
using TicketTimer.Jira.Services;

namespace TicketTimer.Jira.Commands
{
    public class JiraCommand : ConsoleCommand
    {
        private readonly JiraService _jiraService;
        private readonly WorkItemStore _workItemStore;

        public bool KeepWorkItems { get; set; }

        public JiraCommand(JiraService jiraService, WorkItemStore workItemStore)
        {
            _jiraService = jiraService;
            _workItemStore = workItemStore;
            ConfigureCommand();
        }

        private void ConfigureCommand()
        {
            IsCommand("jira", "Log time for all saved Jira-tickets");
            HasOption("k|keep:", "Do not delete work items afterwards", k => KeepWorkItems = k != null);
        }

        public override int Run(string[] remainingArguments)
        {
            var successfullyLogged = _jiraService.WriteEntireArchive();
            if (!KeepWorkItems)
            {
                _workItemStore.RemoveRangeFromArchive(successfullyLogged);
            }
            return 0;
        }
    }
}
