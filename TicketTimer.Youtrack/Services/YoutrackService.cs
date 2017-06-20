using System.Collections.Generic;

namespace TicketTimer.Youtrack.Services
{
    public interface YoutrackService
    {
        List<string> WriteEntireArchive();
        void WriteAggregate(string targetTicket);
    }
}
