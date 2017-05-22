namespace TicketTimer.Core.Services
{
    public interface WorkItemService
    {
        void StartWorkItem(string ticketNumber, string comment);
        void StartWorkItem(string ticketNumber);
        void StopWorkItem();
    }
}
