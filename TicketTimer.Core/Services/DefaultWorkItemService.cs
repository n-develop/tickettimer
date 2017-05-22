using System;
using System.Linq;
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
            StopCurrentWorkItem();

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
                Console.WriteLine($"Stopped work on ticket {currentWorkItem.TicketNumber} with comment '{currentWorkItem.Comment}' at '{currentWorkItem.Stopped.ToShortTimeString()}' after {currentWorkItem.Duration}");
            }

        }
    }
}