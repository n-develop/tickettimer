using System;
using System.Collections.Generic;
using System.Linq;
using TicketTimer.Core.Infrastructure;

namespace TicketTimer.Core.Utils
{
    public class WorkItemAggregator
    {
        public static List<WorkItem> AggregateWorkItems(List<WorkItem> archive)
        {
            var aggregates = new List<WorkItem>();

            var itemsGroupedByDay = archive.GroupBy(item => item.Started.Date);
            foreach (var itemsPerDay in itemsGroupedByDay)
            {
                var aggregatesPerDay = SumUpDurationsPerTicket(itemsPerDay);

                aggregates.AddRange(aggregatesPerDay);
            }
            return aggregates;
        }

        private static IEnumerable<WorkItem> SumUpDurationsPerTicket(IGrouping<DateTime, WorkItem> workItems)
        {
            var perTicketNumber = workItems.GroupBy(wi => wi.TicketNumber);

            return perTicketNumber.Select(itemsPerTicket =>
                new WorkItem(itemsPerTicket.Key)
                {
                    Comment = string.Join(" | ", itemsPerTicket.Select(item => item.Comment).Distinct()),
                    Started = workItems.Key,
                    Duration = SumOf(itemsPerTicket.Select(item => item.Duration).ToList())
                }).ToList();
        }

        private static TimeSpan SumOf(List<TimeSpan> durations)
        {
            var sum = durations.FirstOrDefault();
            for (var i = 1; i < durations.Count; i++)
            {
                sum = sum + durations[i];
            }
            return sum;
        }
    }
}