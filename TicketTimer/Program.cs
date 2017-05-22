using ManyConsole;
using System;
using TicketTimer.Configuration;

namespace TicketTimer
{
    class Program
    {
        static int Main(string[] args)
        {
            var container = AutofacConfig.ConfigureContainer();

            var commands = CommandsConfiguration.GetCommandsFromConfig(container);

            return ConsoleCommandDispatcher.DispatchCommand(commands, args, Console.Out);
        }
    }
}
