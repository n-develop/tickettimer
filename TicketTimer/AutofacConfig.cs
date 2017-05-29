using Autofac;
using TicketTimer.Configuration;
using TicketTimer.Core;

namespace TicketTimer
{
    class AutofacConfig
    {
        public static IContainer ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<CoreModule>();

            RegisterModulesFromConfig(builder);

            return builder.Build();
        }

        private static void RegisterModulesFromConfig(ContainerBuilder builder)
        {
            var modules = ModulesConfiguration.GetModulesFromConfiguration();

            foreach (var module in modules)
            {
                builder.RegisterModule(module);
            }
        }
    }
}
