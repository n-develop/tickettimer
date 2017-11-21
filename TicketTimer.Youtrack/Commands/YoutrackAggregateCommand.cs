using ManyConsole;
using TicketTimer.Youtrack.Services;

namespace TicketTimer.Youtrack.Commands
{
    public class YoutrackAggregateCommand : ConsoleCommand
    {
        private readonly YoutrackService _youtrackService;

        public YoutrackAggregateCommand(YoutrackService youtrackService)
        {
            _youtrackService = youtrackService;
            ConfigureCommand();
        }

        private void ConfigureCommand()
        {
            IsCommand("yt-aggregate", "Aggregate configured issues into one YouTrack-issue");
        }

        public override int Run(string[] remainingArguments)
        {
            _youtrackService.WriteAggregate();
            return 0;
        }
    }
}
