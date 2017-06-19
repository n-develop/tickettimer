using Autofac;
using TicketTimer.Core.Commands;
using TicketTimer.Core.Infrastructure;
using TicketTimer.Core.Services;

namespace TicketTimer.Core
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<JsonWorkItemStore>().As<WorkItemStore>().InstancePerLifetimeScope();
            builder.RegisterType<LocalFileStore>().As<FileStore>();
            builder.RegisterType<LocalDateProvider>().As<DateProvider>();
            builder.RegisterType<DefaultWorkItemService>().As<WorkItemService>();

            builder.RegisterType<StartCommand>().AsSelf();
            builder.RegisterType<StopCommand>().AsSelf();
            builder.RegisterType<CurrentCommand>().AsSelf();
            builder.RegisterType<ArchiveCommand>().AsSelf();
            builder.RegisterType<ClearCommand>().AsSelf();
            builder.RegisterType<RenameCommand>().AsSelf();

            base.Load(builder);
        }
    }
}
