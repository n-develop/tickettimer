using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
