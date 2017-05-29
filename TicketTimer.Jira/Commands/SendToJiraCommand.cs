using System;
using ManyConsole;
using TicketTimer.Jira.Services;

namespace TicketTimer.Jira.Commands
{
    public class SendToJiraCommand : ConsoleCommand
    {
        private readonly JiraService _jiraService;

        public SendToJiraCommand(JiraService jiraService)
        {
            _jiraService = jiraService;
            ConfigureCommand();
        }

        private void ConfigureCommand()
        {
            IsCommand("jira", "Log time for all saved Jira-tickets");
        }

        public override int Run(string[] remainingArguments)
        {
            // TODO use JiraService here.
            Console.WriteLine("You logged your time!");
            return 0;
        }
    }
}
