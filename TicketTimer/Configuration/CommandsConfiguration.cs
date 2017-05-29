using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using ManyConsole;
using TicketTimer.Configuration.Model;

namespace TicketTimer.Configuration
{
    class CommandsConfiguration
    {
        // TODO remove "static" and make this a proper class
        public static IList<ConsoleCommand> GetCommandsFromConfig(IContainer container)
        {
            var types = GetTypesFromConfiguration();

            var commandTypes = types.Where(t => t.IsSubclassOf(typeof(ConsoleCommand)) && !t.IsAbstract).ToList();
            var commands = GetCommandsFromContainer(container, commandTypes);
            return commands;
        }

        private static IList<ConsoleCommand> GetCommandsFromContainer(IContainer container, IEnumerable<Type> commandTypes)
        {
            var consoleCommands = new List<ConsoleCommand>();
            foreach (var type in commandTypes)
            {
                var command = container.Resolve(type) as ConsoleCommand;
                if (command != null)
                {
                    consoleCommands.Add(command);
                }
            }
            return consoleCommands;
        }

        private static List<Type> GetTypesFromConfiguration()
        {
            var config = ConfigurationLoader.GetConfigurationFromFile("Commands.config");

            TicketTimerSection section = (TicketTimerSection)config.GetSection("ticketTimer");
            if (section == null)
            {
                return new List<Type>(0);
            }
            var commandElements = section.Commands.ToList();
            var types = ConvertCommandElementsToTypes(commandElements);
            return types;
        }

        private static List<Type> ConvertCommandElementsToTypes(List<CommandElement> commandElements)
        {
            var types = new List<Type>();
            foreach (var commandElement in commandElements)
            {
                try
                {
                    var commandType = Type.GetType(commandElement.Type);
                    if (commandType != null)
                    {
                        types.Add(commandType);
                    }
                    else
                    {
                        Console.WriteLine($"WARNING: Could not load '{commandElement.Type}' as a type.");
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(
                        $"ERROR: Error while loading '{commandElement}' as a type.\n{exception.Message}\n{exception.StackTrace}");
                }
            }
            return types;
        }
    }
}
