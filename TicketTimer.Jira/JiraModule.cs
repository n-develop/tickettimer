using Autofac;
using TicketTimer.Jira.Commands;
using TicketTimer.Jira.Services;

namespace TicketTimer.Jira
{
    public class JiraModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DefaultJiraService>().As<JiraService>();

            builder.RegisterType<SendToJiraCommand>().AsSelf();

            base.Load(builder);
        }
    }
}
