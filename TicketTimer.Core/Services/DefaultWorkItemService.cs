using System;
using System.Linq;
using TicketTimer.Core.Infrastructure;

namespace TicketTimer.Core.Services
{
    public class DefaultWorkItemService : WorkItemService
    {
        private readonly WorkItemStore _workItemStore;

        public DefaultWorkItemService(WorkItemStore workItemStore)
        {
            _workItemStore = workItemStore;
        }

        public void StartWorkItem(WorkItem workItem)
        {
            _workItemStore.Add(workItem);
            _workItemStore.Save();
        }

        public WorkItem GetCurrentWorkItem()
        {
            var currentState = _workItemStore.GetState();
            var currentWorkItem = currentState.WorkItems.SingleOrDefault(wi => wi.Stopped == DateTime.MinValue);
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