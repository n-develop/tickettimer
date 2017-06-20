using ManyConsole;
using TicketTimer.Core.Infrastructure;
using TicketTimer.Youtrack.Services;

namespace TicketTimer.Youtrack.Commands
{
    public class YoutrackCommand : ConsoleCommand
    {
        private readonly YoutrackService _youtrackService;
        private readonly WorkItemStore _workItemStore;

        public bool KeepWorkItems { get; set; }

        public YoutrackCommand(YoutrackService youtrackService, WorkItemStore workItemStore)
        {
            _youtrackService = youtrackService;
            _workItemStore = workItemStore;
            ConfigureCommand();
        }

        private void ConfigureCommand()
        {
            IsCommand("youtrack", "Log time for all saved YouTrack-tickets");
            HasOption("k|keep:", "Keep work items", k => KeepWorkItems = k != null);
        }

        public override int Run(string[] remainingArguments)
        {
            var successfullyLogged = _youtrackService.WriteEntireArchive();
            if (!KeepWorkItems)
            {
                _workItemStore.RemoveRangeFromArchive(successfullyLogged);
            }
            return 0;
        }
    }
}
