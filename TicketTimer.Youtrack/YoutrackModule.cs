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
            var jiraUrl = ConfigurationManager.AppSettings["youtrackUrl"];
            var jiraUser = ConfigurationManager.AppSettings["youtrackUser"];
            var jiraPassword = ConfigurationManager.AppSettings["youtrackPassword"];
            if (!string.IsNullOrEmpty(jiraUrl) && !string.IsNullOrEmpty(jiraUser) &&
                !string.IsNullOrEmpty(jiraPassword))
            {
                // TODO create and register client
            }
        }
    }
}
