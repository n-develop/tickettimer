using TicketTimer.Core.Infrastructure;

namespace TicketTimer.Core.Services
{
    public interface WorkItemService
    {
        void StartWorkItem(WorkItem workItem);
        void StopCurrentWorkItem();
        void ShowStatus();
        void ShowArchive();
        void Clear();
        void Rename(string oldName, string newName);
    }
}
