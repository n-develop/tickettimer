using ManyConsole;
using System;
using TicketTimer.Configuration;

namespace TicketTimer
{
    class Program
    {
        static int Main(string[] args)
        {
            var commands = CommandsConfiguration.GetCommandsFromConfig();

            return ConsoleCommandDispatcher.DispatchCommand(commands, args, Console.Out);
        }
    }
}
