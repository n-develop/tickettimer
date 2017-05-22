using Autofac;
using TicketTimer.Core.Commands;
using TicketTimer.Core.Infrastructure;
using TicketTimer.Core.Services;

namespace TicketTimer.Core
{
    public class CoreModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<JsonWorkItemStore>().As<WorkItemStore>();
            builder.RegisterType<LocalFileStore>().As<FileStore>();
            builder.RegisterType<LocalDateProvider>().As<DateProvider>();
            builder.RegisterType<WorkItemServiceImpl>().As<WorkItemService>();

            builder.RegisterType<StartCommand>().AsSelf();

            base.Load(builder);
        }
    }
}
