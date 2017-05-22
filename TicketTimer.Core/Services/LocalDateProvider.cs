using System;

namespace TicketTimer.Core.Services
{
    public class LocalDateProvider : DateProvider
    {
        public DateTime Now => DateTime.Now;
    }
}