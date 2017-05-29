using Autofac;
using TicketTimer.Rename.Commands;

namespace TicketTimer.Rename
{
    public class RenameModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RenameCommand>().AsSelf();

            builder.RegisterType<DefaultRenameService>().As<RenameService>();

            base.Load(builder);
        }
    }
}
