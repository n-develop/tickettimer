using System;
using ManyConsole;
using TicketTimer.Core.Services;

namespace TicketTimer.Core.Commands
{
    public class StatusCommand : ConsoleCommand
    {
        private readonly WorkItemService _workItemService;

        public StatusCommand(WorkItemService workItemService)
        {
            _workItemService = workItemService;
            ConfigureCommand();
        }

        private void ConfigureCommand()
        {
            IsCommand("status", "Shows the current work item.");
        }

        public override int Run(string[] remainingArguments)
        {
            _workItemService.ShowStatus();
            return 0;
        }
    }
}
