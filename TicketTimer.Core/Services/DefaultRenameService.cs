namespace TicketTimer.Core.Services
{
    public class DefaultRenameService : RenameService
    {
        private readonly WorkItemService _workItemService;

        public DefaultRenameService(WorkItemService workItemService)
        {
            _workItemService = workItemService;
        }
        public void RenameWorkItem(string currentTicketNumber, string newTicketNumber)
        {

        }
    }
}
