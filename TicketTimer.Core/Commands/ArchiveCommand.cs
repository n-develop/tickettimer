using ManyConsole;
using TicketTimer.Core.Services;

namespace TicketTimer.Core.Commands
{
    public class ArchiveCommand : ConsoleCommand
    {
        private readonly WorkItemService _workItemService;

        public ArchiveCommand(WorkItemService workItemService)
        {
            _workItemService = workItemService;
            ConfigureCommand();
        }

        private void ConfigureCommand()
        {
            IsCommand("archive", "Shows all unsaved work items.");
        }

        public override int Run(string[] remainingArguments)
        {
            _workItemService.ShowArchive();
            return 0;
        }
    }
}
