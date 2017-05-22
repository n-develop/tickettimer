using ManyConsole;
using TicketTimer.Core.Infrastructure;
using TicketTimer.Core.Services;

namespace TicketTimer.Core.Commands
{
    public class StartCommand : ConsoleCommand
    {
        private readonly WorkItemService _workItemService;
        private readonly DateProvider _dateProvider;
        private string _ticket;
        private string _comment;

        public StartCommand(WorkItemService workItemService, DateProvider dateProvider)
        {
            ConfigureCommand();

            _workItemService = workItemService;
            _dateProvider = dateProvider;
        }

        private void ConfigureCommand()
        {
            IsCommand("start", "Starts the Timer on a given ticket");
            HasRequiredOption("t|ticket=", "Ticket number e.g. BDP-301", ticket => _ticket = ticket);
            HasOption("c|comment=", "What are you working on?", comment => _comment = comment);
        }

        public override int Run(string[] remainingArguments)
        {

            var workItem = new WorkItem(_ticket)
            {
                Comment = _comment,
                Started = _dateProvider.Now
            };

            _workItemService.StartWorkItem(workItem);
            return 0;
        }
    }
}
