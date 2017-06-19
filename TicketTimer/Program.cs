using System;
using System.IO;
using System.Reflection;
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
            Environment.CurrentDirectory = GetCurrentDirectory();

            var container = AutofacConfig.ConfigureContainer();

            var commands = CommandsConfiguration.GetCommandsFromConfig(container);

            commands.Add(container.Resolve<StartCommand>());
            commands.Add(container.Resolve<StopCommand>());
            commands.Add(container.Resolve<ArchiveCommand>());
            commands.Add(container.Resolve<CurrentCommand>());
            commands.Add(container.Resolve<ClearCommand>());
            commands.Add(container.Resolve<RenameCommand>());

            return ConsoleCommandDispatcher.DispatchCommand(commands, args, Console.Out);
        }

        private static string GetCurrentDirectory()
        {
            return new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase)).LocalPath;
        }
    }
}
