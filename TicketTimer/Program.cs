using System;
using Autofac;
using ManyConsole;
using TicketTimer.Configuration;
using TicketTimer.Core.Commands;

namespace TicketTimer
{
    class Program
    {
        static int Main(string[] args)
        {
            var container = AutofacConfig.ConfigureContainer();

            var commands = CommandsConfiguration.GetCommandsFromConfig(container);

            commands.Add(container.Resolve<StartCommand>());
            commands.Add(container.Resolve<StopCommand>());
            commands.Add(container.Resolve<ShowCommand>());
            commands.Add(container.Resolve<StatusCommand>());
            commands.Add(container.Resolve<ClearCommand>());

            return ConsoleCommandDispatcher.DispatchCommand(commands, args, Console.Out);
        }
    }
}
