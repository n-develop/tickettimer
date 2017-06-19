using ManyConsole;
using TicketTimer.Core.Services;

namespace TicketTimer.Core.Commands
{
    public class CurrentCommand : ConsoleCommand
    {
        private readonly WorkItemService _workItemService;

        public CurrentCommand(WorkItemService workItemService)
        {
            _workItemService = workItemService;
            ConfigureCommand();
        }

        private void ConfigureCommand()
        {
            IsCommand("current", "Shows the current work item.");
        }

        public override int Run(string[] remainingArguments)
        {
            _workItemService.ShowCurrentWorkItem();
            return 0;
        }
    }
}
