using System;
using ManyConsole;
using TicketTimer.Configuration;
using TicketTimer.Core;

namespace TicketTimer
{
    class Program
    {
        static int Main(string[] args)
        {
            var container = AutofacConfig.ConfigureContainer();

            var commands = CommandsConfiguration.GetCommandsFromConfig(container);

            commands.AddRange(ConsoleCommandDispatcher.FindCommandsInSameAssemblyAs(typeof(CoreModule)));

            return ConsoleCommandDispatcher.DispatchCommand(commands, args, Console.Out);
        }
    }
}
