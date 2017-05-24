using ManyConsole;
using TicketTimer.Core.Services;

namespace TicketTimer.Core.Commands
{
    public class StopCommand : ConsoleCommand
    {
        private readonly WorkItemService _workItemService;

        public StopCommand(WorkItemService workItemService)
        {
            ConfigureCommand();
            _workItemService = workItemService;
        }

        private void ConfigureCommand()
        {
            IsCommand("stop", "Stops the timer on the current ticket.");
        }

        public override int Run(string[] remainingArguments)
        {
            _workItemService.StopCurrentWorkItem();
            return 0;
        }
    }
}
