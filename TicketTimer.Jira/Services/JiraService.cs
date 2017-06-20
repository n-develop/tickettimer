using System.Collections.Generic;

namespace TicketTimer.Jira.Services
{
    public interface JiraService
    {
        List<string> WriteEntireArchive();
    }
}