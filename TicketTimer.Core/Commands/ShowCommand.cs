using System;
using ManyConsole;
using TicketTimer.Core.Services;

namespace TicketTimer.Core.Commands
{
    public class ShowCommand : ConsoleCommand
    {
        private readonly WorkItemService _workItemService;

        public ShowCommand(WorkItemService workItemService)
        {
            _workItemService = workItemService;
            ConfigureCommand();
        }

        private void ConfigureCommand()
        {
            IsCommand("show", "Shows all unsaved work items.");
        }

        public override int Run(string[] remainingArguments)
        {
            //TODO correct service call
            Console.WriteLine("Show them all!");
            return 0;
        }
    }
}
