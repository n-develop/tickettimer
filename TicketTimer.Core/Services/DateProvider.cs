using System;

namespace TicketTimer.Core.Services
{
    public interface DateProvider
    {
        DateTime Now { get; }
    }
}