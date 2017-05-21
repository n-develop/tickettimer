using ManyConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using TicketTimer.Configuration.Model;

namespace TicketTimer.Configuration
{
    class CommandsConfiguration
    {
        public static IList<ConsoleCommand> GetCommandsFromConfig()
        {
            var types = GetTypesFromConfiguration();

            var commandTypes = types.Where(t => t.IsSubclassOf(typeof(ConsoleCommand)) && !t.IsAbstract).ToList();
            var commands = GetCommandsFromTypes(commandTypes);
            return commands;
        }

        private static IList<ConsoleCommand> GetCommandsFromTypes(List<Type> commandTypes)
        {
            var consoleCommands = new List<ConsoleCommand>();
            foreach (var type in commandTypes)
            {
                var constructor = type.GetConstructor(new Type[0]);
                if (constructor != null)
                {
                    consoleCommands.Add((ConsoleCommand)constructor.Invoke(new object[0]));
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
                    types.Add(commandType);
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
