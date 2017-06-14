using System.Configuration;
using Autofac;
using TicketTimer.Youtrack.Commands;
using TicketTimer.Youtrack.Services;
using YouTrackSharp.Infrastructure;

namespace TicketTimer.Youtrack
{
    public class YoutrackModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DefaultYoutrackService>().As<YoutrackService>();

            RegisterYoutrackClient(builder);

            builder.RegisterType<YoutrackCommand>().AsSelf();

            base.Load(builder);
        }

        private static void RegisterYoutrackClient(ContainerBuilder builder)
        {
            var youtrackUrl = ConfigurationManager.AppSettings["youtrackUrl"];
            var youtrackUser = ConfigurationManager.AppSettings["youtrackUser"];
            var youtrackPort = int.Parse(ConfigurationManager.AppSettings["youtrackPort"]);
            var youtrackPassword = ConfigurationManager.AppSettings["youtrackPassword"];

            if (!string.IsNullOrEmpty(youtrackUrl) && !string.IsNullOrEmpty(youtrackUser) &&
                !string.IsNullOrEmpty(youtrackPassword))
            {
                builder.Register(c =>
                {
                    var connection = new Connection(youtrackUrl, youtrackPort, true);
                    connection.Authenticate(youtrackUser, youtrackPassword);
                    return connection;
                }).As<IConnection>();
            }
        }
    }
}
