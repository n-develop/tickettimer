using System;
using System.Linq;
using TicketTimer.Core.Extensions;
using TicketTimer.Core.Infrastructure;

namespace TicketTimer.Core.Services
{
    public class DefaultWorkItemService : WorkItemService
    {
        private readonly WorkItemStore _workItemStore;
        private readonly DateProvider _dateProvider;

        public DefaultWorkItemService(WorkItemStore workItemStore, DateProvider dateProvider)
        {
            _workItemStore = workItemStore;
            _dateProvider = dateProvider;
        }

        public void StartWorkItem(WorkItem workItem)
        {
            if (_workItemStore.GetState().CurrentWorkItem != WorkItem.Empty)
            {
                StopCurrentWorkItem();
            }

            _workItemStore.SetCurrent(workItem);

            Console.WriteLine($"Starting work on ticket {workItem.TicketNumber} with comment '{workItem.Comment}' at '{workItem.Started.ToShortTimeString()}'");
        }

        public WorkItem GetCurrentWorkItem()
        {
            var currentState = _workItemStore.GetState();
            var currentWorkItem = currentState.WorkItemArchive.SingleOrDefault(item => item.Stopped == DateTime.MinValue);
            if (currentWorkItem == null)
            {
                return WorkItem.Empty;
            }
            return currentWorkItem;
        }

        public void StopCurrentWorkItem()
        {
            var currentWorkItem = _workItemStore.GetState().CurrentWorkItem;
            if (currentWorkItem != WorkItem.Empty)
            {
                currentWorkItem.Stopped = _dateProvider.Now;
                _workItemStore.AddToArchive(currentWorkItem);
                _workItemStore.SetCurrent(WorkItem.Empty);
                Console.WriteLine(
                    $"Stopped work on ticket {currentWorkItem.TicketNumber} with comment '{currentWorkItem.Comment}' at '{currentWorkItem.Stopped.ToShortTimeString()}' after {currentWorkItem.Duration.ToShortString()}");
            }
            else
            {
                Console.WriteLine("There is no ticket in progress.");
            }
        }

        public void ShowStatus()
        {
            var currentWorkItem = _workItemStore.GetState().CurrentWorkItem;
            if (currentWorkItem != WorkItem.Empty)
            {
                var duration = _dateProvider.Now - currentWorkItem.Started;
                Console.WriteLine($"You are working on {currentWorkItem.TicketNumber} ({currentWorkItem.Comment}) for {duration.ToShortString()}");
            }
            else
            {
                Console.WriteLine("There is no ticket in progress.");
            }
        }

        public void ShowArchive()
        {
            var archive = _workItemStore.GetState().WorkItemArchive;
            if (archive.Any())
            {
                Console.WriteLine($"| {"Ticket",-20} | {"Comment",-40} | {"Duration",10} |");
                Console.WriteLine(new string('-', 80));
                foreach (var workItem in archive)
                {
                    PrintWorkItem(workItem);
                }
            }
            else
            {
                Console.WriteLine("No items in the archive.");
            }
        }

        public void Clear()
        {
            _workItemStore.SetCurrent(WorkItem.Empty);
            _workItemStore.ClearArchive();
            Console.WriteLine("All tickets including the current work item are cleared.");
        }

        private void PrintWorkItem(WorkItem workItem)
        {
            var comment = workItem.Comment;
            if (comment.Length > 40)
            {
                comment = comment.Substring(0, 36) + "...";
            }
            var duration = workItem.Duration.ToShortString();
            Console.WriteLine($"| {workItem.TicketNumber,-20} | {comment,-40} | {duration,10} |");
        }
    }
}