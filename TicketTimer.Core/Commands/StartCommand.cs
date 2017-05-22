using ManyConsole;
using System;

namespace TicketTimer.Core.Commands
{
    public class StartCommand : ConsoleCommand
    {
        private string _ticket;
        private string _comment;

        public StartCommand()
        {
            IsCommand("start", "Starts the Timer on a given ticket");

            HasRequiredOption("t|ticket=", "Ticket number e.g. BDP-301", ticket => _ticket = ticket);

            HasOption("c|comment=", "What are you working on?", comment => _comment = comment);
        }

        public override int Run(string[] remainingArguments)
        {
            Console.WriteLine($"Starting work on ticket {_ticket} with comment '{_comment}'");

            return 0;
        }
    }
}
