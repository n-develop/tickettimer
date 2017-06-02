using System;
using TicketTimer.Core.Services;

namespace TicketTimer.Rename
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
            throw new NotImplementedException();
        }
    }
}
