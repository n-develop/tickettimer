using System;
using System.Collections.Generic;
using System.Linq;
using TicketTimer.Core.Extensions;
using TicketTimer.Core.Infrastructure;

namespace TicketTimer.Core.Services
{
    public class ConsoleTable
    {
        private static readonly string TableSeparatorLine = new string('-', 80);

        public static void PrintTable(List<WorkItem> archive)
        {
            PrintTableHeader();
            PrintTableBody(archive);
            PrintTableFooter();
        }

        private static void PrintTableFooter()
        {
            Console.WriteLine(TableSeparatorLine);
        }

        private static void PrintTableBody(List<WorkItem> archive)
        {
            var itemsGroupedByDay = archive.GroupBy(item => item.Started.ToShortDateString());
            foreach (var grouping in itemsGroupedByDay)
            {
                PrintSectionHeader(grouping.Key);
                foreach (var workItem in grouping)
                {
                    PrintWorkItem(workItem);
                }
            }

        }

        private static void PrintSectionHeader(string sectionHeader)
        {
            Console.WriteLine(TableSeparatorLine);
            Console.WriteLine($"| {sectionHeader,-76} |");
            Console.WriteLine(TableSeparatorLine);

        }

        private static void PrintTableHeader()
        {
            Console.WriteLine(TableSeparatorLine);
            Console.WriteLine($"| {"Ticket",-20} | {"Comment",-40} | {"Duration",10} |");
        }

        private static void PrintWorkItem(WorkItem workItem)
        {
            var comment = workItem.Comment;
            if (comment.Length > 40)
            {
                comment = comment.Substring(0, 36) + "...";
            }
            var duration = workItem.Duration.ToShortString();
            Console.WriteLine($"| {workItem.TicketNumber,-20} | {comment,-40} | {duration,10} |");
        }
    }
}