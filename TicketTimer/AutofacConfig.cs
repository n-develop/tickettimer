using Autofac;
using TicketTimer.Core;

namespace TicketTimer
{
    class AutofacConfig
    {
        public static IContainer ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<CoreModule>();

            return builder.Build();

        }
    }
}
