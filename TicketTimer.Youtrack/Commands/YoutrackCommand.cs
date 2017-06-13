using ManyConsole;
using TicketTimer.Youtrack.Services;

namespace TicketTimer.Youtrack.Commands
{
    public class YoutrackCommand : ConsoleCommand
    {
        private readonly YoutrackService _youtrackService;

        public YoutrackCommand(YoutrackService youtrackService)
        {
            _youtrackService = youtrackService;
            ConfigureCommand();
        }

        private void ConfigureCommand()
        {
            IsCommand("youtrack", "Log time for all saved YouTrack-tickets");
        }

        public override int Run(string[] remainingArguments)
        {
            _youtrackService.WriteEntireArchive();
            return 0;
        }
    }
}
