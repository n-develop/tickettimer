using System.Collections.Generic;

namespace TicketTimer.Core.Infrastructure
{
    public class TimerState
    {
        public TimerState()
        {
            WorkItemArchive = new List<WorkItem>();
            CurrentWorkItem = WorkItem.Empty;
        }

        public WorkItem CurrentWorkItem { get; set; }

        public List<WorkItem> WorkItemArchive { get; set; }
    }
}
