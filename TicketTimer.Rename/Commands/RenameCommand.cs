using System;
using ManyConsole;

namespace TicketTimer.Rename.Commands
{
    public class RenameCommand : ConsoleCommand
    {
        private string _oldName;
        private string _newName;

        public RenameCommand()
        {
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
            // TODO use RenameService here
            Console.WriteLine($"You renamed it! {_oldName} -> {_newName}");
            return 0;
        }
    }
}
