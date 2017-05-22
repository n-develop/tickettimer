using System;

namespace TicketTimer.Core.Infrastructure
{
    public class WorkItem
    {
        public WorkItem(string ticketNumber)
        {
            TicketNumber = ticketNumber;
        }

        public string TicketNumber { get; set; }

        public DateTime Started { get; set; }

        public DateTime Stopped { get; set; }

        public string Comment { get; set; }

        public static WorkItem Empty
        {
            get
            {
                return new WorkItem("- no ticket -");
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var other = (WorkItem)obj;
            return TicketNumber.Equals(other.TicketNumber);
        }

        public override int GetHashCode()
        {
            return TicketNumber.GetHashCode();
        }
    }
}
