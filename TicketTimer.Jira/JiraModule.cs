using Autofac;
using TicketTimer.Jira.Commands;
using TicketTimer.Jira.Services;

namespace TicketTimer.Jira
{
    public class JiraModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<JiraService>().AsSelf();

            builder.RegisterType<SendToJiraCommand>().AsSelf();

            base.Load(builder);
        }
    }
}
