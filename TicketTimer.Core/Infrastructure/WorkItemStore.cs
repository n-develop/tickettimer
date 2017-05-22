namespace TicketTimer.Core.Infrastructure
{
    public interface WorkItemStore
    {
        void AddToArchive(WorkItem workItem);

        TimerState GetState();

        void SetCurrent(WorkItem workItem);
    }
}
