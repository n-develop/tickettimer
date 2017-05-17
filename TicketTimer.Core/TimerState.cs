using System.Collections.Generic;

namespace TicketTimer.Core
{
    public class TimerState
    {
        public TimerState()
        {
            WorkItems = new List<WorkItem>();
        }

        public List<WorkItem> WorkItems { get; set; }
    }
}
