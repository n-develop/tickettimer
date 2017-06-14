using System.Collections.Generic;

namespace TicketTimer.Core.Infrastructure
{
    public interface WorkItemStore
    {
        void AddToArchive(WorkItem workItem);

        TimerState GetState();

        void SetCurrent(WorkItem workItem);

        void ClearArchive();

        void RemoveFromArchive(string ticketNumber);

        void RemoveRangeFromArchive(IEnumerable<string> tickets);

        void Save();
    }
}
