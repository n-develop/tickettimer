using ManyConsole;
using TicketTimer.Youtrack.Services;

namespace TicketTimer.Youtrack.Commands
{
    public class YoutrackAggregateCommand : ConsoleCommand
    {
        private readonly YoutrackService _youtrackService;

        public bool KeepWorkItems { get; set; }
        public string TicketNumber { get; set; }

        public YoutrackAggregateCommand(YoutrackService youtrackService)
        {
            _youtrackService = youtrackService;
            ConfigureCommand();
        }

        private void ConfigureCommand()
        {
            IsCommand("yt-aggregate", "Aggregate configured issues into one YouTrack-issue");
            HasRequiredOption("t|ticket=", "Target ticket to log the sum on", t => TicketNumber = t);
        }

        public override int Run(string[] remainingArguments)
        {
            _youtrackService.WriteAggregate(TicketNumber);
            return 0;
        }
    }
}
