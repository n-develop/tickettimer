using YouTrackSharp.Infrastructure;

namespace TicketTimer.Youtrack.Services
{
    public class DefaultYoutrackService : YoutrackService
    {
        private readonly IConnection _connection;
        private readonly YoutrackService _youtrackService;

        public DefaultYoutrackService(IConnection connection, YoutrackService youtrackService)
        {
            _connection = connection;
            _youtrackService = youtrackService;
        }

        public void WriteEntireArchive()
        {
            throw new System.NotImplementedException();
        }
    }
}