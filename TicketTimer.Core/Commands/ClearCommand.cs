using ManyConsole;
using TicketTimer.Core.Services;

namespace TicketTimer.Core.Commands
{
    public class ClearCommand : ConsoleCommand
    {
        private readonly WorkItemService _workItemService;

        public ClearCommand(WorkItemService workItemService)
        {
            _workItemService = workItemService;
            ConfigureCommand();
        }

        private void ConfigureCommand()
        {
            IsCommand("clear", "Clear all your unsaved tickets.");
        }

        public override int Run(string[] remainingArguments)
        {
            _workItemService.Clear();
            return 0;
        }
    }
}
