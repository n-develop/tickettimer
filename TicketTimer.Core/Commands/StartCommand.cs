using ManyConsole;
using System;
using TicketTimer.Core.Services;

namespace TicketTimer.Core.Commands
{
    public class StartCommand : ConsoleCommand
    {
        private readonly WorkItemService _workItemService;
        private string _ticket;
        private string _comment;

        public StartCommand(WorkItemService workItemService)
        {
            ConfigureCommand();

            _workItemService = workItemService;
        }

        private void ConfigureCommand()
        {
            IsCommand("start", "Starts the Timer on a given ticket");
            HasRequiredOption("t|ticket=", "Ticket number e.g. BDP-301", ticket => _ticket = ticket);
            HasOption("c|comment=", "What are you working on?", comment => _comment = comment);
        }

        public override int Run(string[] remainingArguments)
        {
            Console.WriteLine($"Starting work on ticket {_ticket} with comment '{_comment}'");
            _workItemService.StartWorkItem(_ticket, _comment);
            return 0;
        }
    }
}
