﻿using System;
using Newtonsoft.Json;

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


        [JsonIgnore]
        public TimeSpan Duration
        {
            get
            {
                if (Stopped != DateTime.MinValue && Started != DateTime.MinValue && Stopped > Started)
                {
                    return Stopped - Started;
                }
                throw new NotSupportedException("Inconsistent state of work item");
            }
            set => Stopped = Started + value;
        }

        public static WorkItem Empty => new WorkItem("- no ticket -") { Comment = string.Empty };

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

        public static bool operator ==(WorkItem first, WorkItem second)
        {
            if ((object)first == null || (object)second == null)
            {
                return false;
            }

            return first.Equals(second);
        }
        public static bool operator !=(WorkItem first, WorkItem second)
        {
            return !(first == second);
        }
    }
}
