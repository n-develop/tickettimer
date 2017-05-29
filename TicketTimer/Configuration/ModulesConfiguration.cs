using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Autofac.Core;
using TicketTimer.Configuration.Model;

namespace TicketTimer.Configuration
{
    class ModulesConfiguration
    {
        public static List<IModule> GetModulesFromConfiguration()
        {
            var types = GetModuleTypesFromConfiguration();
            var moduleTypes = types.Where(t => t.IsSubclassOf(typeof(Module)) && !t.IsAbstract).ToList();
            var modules = GetModulesFromTypes(moduleTypes);
            return modules;
        }

        private static List<IModule> GetModulesFromTypes(List<Type> moduleTypes)
        {
            var modules = new List<IModule>();
            foreach (var type in moduleTypes)
            {
                var constructor = type.GetConstructor(new Type[0]);
                if (constructor != null)
                {
                    modules.Add((Module)constructor.Invoke(new object[0]));
                }
            }
            return modules;
        }

        public static List<Type> GetModuleTypesFromConfiguration()
        {
            var config = ConfigurationLoader.GetConfigurationFromFile("Commands.config");

            TicketTimerSection section = (TicketTimerSection)config.GetSection("ticketTimer");
            if (section == null)
            {
                return new List<Type>(0);
            }
            var moduleElements = section.Modules.ToList();
            var types = ConvertModuleElementsToTypes(moduleElements);
            return types;
        }

        private static List<Type> ConvertModuleElementsToTypes(List<ModuleElement> moduleElements)
        {
            var types = new List<Type>();
            foreach (var moduleElement in moduleElements)
            {
                try
                {
                    var moduleType = Type.GetType(moduleElement.Type);
                    types.Add(moduleType);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(
                        $"ERROR: Error while loading '{moduleElement}' as a type.\n{exception.Message}\n{exception.StackTrace}");
                }
            }
            return types;
        }
    }
}
