namespace TicketTimer.Core.Services
{
    public interface RenameService
    {
        void RenameWorkItem(string currentTicketNumber, string newTicketNumber);
    }
}
