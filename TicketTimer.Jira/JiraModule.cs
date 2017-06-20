using System.Configuration;
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
            builder.RegisterType<JiraCommand>().AsSelf();

            RegisterJiraClient(builder);

            base.Load(builder);
        }

        private static void RegisterJiraClient(ContainerBuilder builder)
        {
            var jiraUrl = ConfigurationManager.AppSettings["jiraUrl"];
            var jiraUser = ConfigurationManager.AppSettings["jiraUser"];
            var jiraPassword = ConfigurationManager.AppSettings["jiraPassword"];
            if (!string.IsNullOrEmpty(jiraUrl) && !string.IsNullOrEmpty(jiraUser) &&
                !string.IsNullOrEmpty(jiraPassword))
            {
                builder.Register(c => Atlassian.Jira.Jira.CreateRestClient(jiraUrl, jiraUser, jiraPassword))
                    .As<Atlassian.Jira.Jira>();
            }
        }
    }
}
