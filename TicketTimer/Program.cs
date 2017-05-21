using ManyConsole;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using TicketTimer.Configuration;

namespace TicketTimer
{
    class Program
    {
        static int Main(string[] args)
        {
            var commands = GetCommandsFromConfig();

            return ConsoleCommandDispatcher.DispatchCommand(commands, args, Console.Out);
        }


        public static IList<ConsoleCommand> GetCommandsFromConfig()
        {
            var types = GetTypesFromConfiguration();

            var commandTypes = types.Where(t => t.IsSubclassOf(typeof(ConsoleCommand)) && !t.IsAbstract).ToList();
            var commands = GetCommandsFromTypes(commandTypes);
            return commands;
        }

        private static IList<ConsoleCommand> GetCommandsFromTypes(List<Type> commandTypes)
        {
            List<ConsoleCommand> consoleCommandList = new List<ConsoleCommand>();
            foreach (Type type in (IEnumerable<Type>)commandTypes)
            {
                ConstructorInfo constructor = type.GetConstructor(new Type[0]);
                if (constructor != null)
                    consoleCommandList.Add((ConsoleCommand)constructor.Invoke(new object[0]));
            }
            return consoleCommandList;
        }

        private static List<Type> GetTypesFromConfiguration()
        {
            var types = new List<Type>();
            ExeConfigurationFileMap configFileMap =
                new ExeConfigurationFileMap { ExeConfigFilename = "Commands.config" };

            System.Configuration.Configuration config =
                ConfigurationManager.OpenMappedExeConfiguration(
                    configFileMap, ConfigurationUserLevel.None);

            TicketTimerSection section = (TicketTimerSection)config.GetSection("ticketTimer");
            if (section == null)
            {
                return new List<Type>(0);
            }
            foreach (var element in section.Commands.ToList())
            {
                try
                {
                    var commandType = Type.GetType(element.Type);
                    types.Add(commandType);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(
                        $"ERROR: Error while loading '{element}' as a type.\n{exception.Message}\n{exception.StackTrace}");
                }
            }
            return types;
        }
    }
}
