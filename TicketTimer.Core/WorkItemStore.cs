namespace TicketTimer.Core
{
    public interface WorkItemStore
    {
        void Add(WorkItem workItem);

        TimerState GetState();

        void Save();

        void Load();
    }
}
