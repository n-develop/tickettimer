using ManyConsole;
using TicketTimer.Core.Services;

namespace TicketTimer.Core.Commands
{
    public class RenameCommand : ConsoleCommand
    {
        private readonly WorkItemService _workItemService;
        private string _oldName;
        private string _newName;

        public RenameCommand(WorkItemService workItemService)
        {
            _workItemService = workItemService;
            ConfigureCommand();
        }

        private void ConfigureCommand()
        {
            IsCommand("rename", "Rename a ticket");
            HasRequiredOption("o|old=", "Old ticket number e.g. BDP-301", oldName => _oldName = oldName);
            HasRequiredOption("n|new=", "New ticket number e.g. BDP-302", newName => _newName = newName);
        }

        public override int Run(string[] remainingArguments)
        {
            _workItemService.Rename(_oldName, _newName);
            return 0;
        }
    }
}
