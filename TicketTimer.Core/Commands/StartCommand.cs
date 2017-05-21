using ManyConsole;
using System;

namespace TicketTimer.Core.Commands
{
    public class StartCommand : ConsoleCommand
    {
        public StartCommand()
        {
            IsCommand("start", "Starts the Timer on a given ticket");
        }

        public override int Run(string[] remainingArguments)
        {
            throw new NotImplementedException();
        }
    }
}
