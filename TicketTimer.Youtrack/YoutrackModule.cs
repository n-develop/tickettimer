using System.Configuration;
using Autofac;
using TicketTimer.Youtrack.Commands;
using TicketTimer.Youtrack.Services;

namespace TicketTimer.Youtrack
{
    public class YoutrackModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DefaultYoutrackService>().As<YoutrackService>();
            builder.RegisterType<YoutrackCommand>().AsSelf();

            RegisterYoutrackClient(builder);

            base.Load(builder);
        }

        private static void RegisterYoutrackClient(ContainerBuilder builder)
        {
            var youtrackUrl = ConfigurationManager.AppSettings["youtrackUrl"];
            var youtrackUser = ConfigurationManager.AppSettings["youtrackUser"];
            var youtrackPassword = ConfigurationManager.AppSettings["youtrackPassword"];
            if (!string.IsNullOrEmpty(youtrackUrl) && !string.IsNullOrEmpty(youtrackUser) &&
                !string.IsNullOrEmpty(youtrackPassword))
            {
                // TODO create and register client
            }
        }
    }
}
