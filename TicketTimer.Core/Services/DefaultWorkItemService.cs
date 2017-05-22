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
            var currentWorkItem = GetCurrentWorkItem();
            if (currentWorkItem != null)
            {
                currentWorkItem.Stopped = _dateProvider.Now;
            }
            _workItemStore.Add(workItem);
            _workItemStore.Save();
        }

        public WorkItem GetCurrentWorkItem()
        {
            var currentState = _workItemStore.GetState();
            var currentWorkItem = currentState.WorkItems.SingleOrDefault(item => item.Stopped == DateTime.MinValue);
            if (currentWorkItem == null)
            {
                return WorkItem.Empty;
            }
            return currentWorkItem;
        }

        public void StopWorkItem()
        {
            throw new System.NotImplementedException();
        }
    }
}