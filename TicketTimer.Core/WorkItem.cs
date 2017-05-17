using System;

namespace TicketTimer.Core
{
    public class WorkItem
    {
        public string TicketNumber { get; set; }

        public DateTime Started { get; set; }

        public DateTime Stopped { get; set; }

        public string Comment { get; set; }
    }
}
