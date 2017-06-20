using ManyConsole;
using TicketTimer.Jira.Services;

namespace TicketTimer.Jira.Commands
{
    public class JiraCommand : ConsoleCommand
    {
        private readonly JiraService _jiraService;

        public bool KeepWorkItems { get; set; }

        public JiraCommand(JiraService jiraService)
        {
            _jiraService = jiraService;
            ConfigureCommand();
        }

        private void ConfigureCommand()
        {
            IsCommand("jira", "Log time for all saved Jira-tickets");
            HasOption("k|keep:", "Keep work items after transfer", k => KeepWorkItems = k != null);
        }

        public override int Run(string[] remainingArguments)
        {
            // TODO process new "keep" parameter.
            _jiraService.WriteEntireArchive();
            return 0;
        }
    }
}
