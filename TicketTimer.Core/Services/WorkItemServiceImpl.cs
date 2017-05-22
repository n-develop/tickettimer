using TicketTimer.Core.Infrastructure;

namespace TicketTimer.Core.Services
{
    // TODO this should have a better name
    public class WorkItemServiceImpl : WorkItemService
    {
        private readonly WorkItemStore _workItemStore;
        private readonly DateProvider _dateProvider;

        public WorkItemServiceImpl(WorkItemStore workItemStore, DateProvider dateProvider)
        {
            _workItemStore = workItemStore;
            _dateProvider = dateProvider;
        }

        public void StartWorkItem(string ticketNumber)
        {
            StartWorkItem(ticketNumber, string.Empty);
        }

        public void StartWorkItem(string ticketNumber, string comment)
        {
            var workItem = new WorkItem(ticketNumber)
            {
                Comment = comment,
                Started = _dateProvider.Now
            };
            _workItemStore.Add(workItem);
            _workItemStore.Save();
        }

        public void StopWorkItem()
        {
            throw new System.NotImplementedException();
        }
    }
}