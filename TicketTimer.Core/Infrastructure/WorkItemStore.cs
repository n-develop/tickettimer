namespace TicketTimer.Core.Infrastructure
{
    public interface WorkItemStore
    {
        void Add(WorkItem workItem);

        TimerState GetState();

        void Save();

        void Load();
    }
}
