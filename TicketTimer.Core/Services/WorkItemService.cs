using TicketTimer.Core.Infrastructure;

namespace TicketTimer.Core.Services
{
    public interface WorkItemService
    {
        void StartWorkItem(WorkItem workItem);
        void StopCurrentWorkItem();
        void ShowCurrentWorkItem();
        void ShowArchive();
        void Clear();
        void Rename(string oldName, string newName);
    }
}
