using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
