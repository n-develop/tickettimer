using System;
using TicketTimer.Core.Infrastructure;
using TicketTimer.Core.Tests.Mocks;
using Xunit;

namespace TicketTimer.Core.Tests
{
    public class JsonWorkItemStoreTests
    {
        // TODO Tests for I/O-Errors

        [Fact]
        public void WorkStoreWithOneItem_CreatesCorrectJson()
        {
            var memoryFileStore = new MemoryFileStore();
            var workItemStore = new JsonWorkItemStore(memoryFileStore);
            workItemStore.Add(new WorkItem
            {
                Comment = "",
                Started = new DateTime(2017, 5, 1, 11, 0, 0),
                Stopped = new DateTime(2017, 5, 1, 12, 0, 0),
                TicketNumber = "BDP-1301"
            });
            workItemStore.Save();

            var content = memoryFileStore.ReadFile("unimportant-file-name.txt");
            var expected = "{\"WorkItems\":[{\"TicketNumber\":\"BDP-1301\",\"Started\":\"2017-05-01T11:00:00\",\"Stopped\":\"2017-05-01T12:00:00\",\"Comment\":\"\"}]}";
            Assert.Equal(expected, content);
        }

        [Fact]
        public void WorkStoreWithoutItems_CreatesCorrectJson()
        {
            var memoryFileStore = new MemoryFileStore();
            var workItemStore = new JsonWorkItemStore(memoryFileStore);

            workItemStore.Save();

            var content = memoryFileStore.ReadFile("unimportant-file-name.txt");
            var expected = "{\"WorkItems\":[]}";
            Assert.Equal(expected, content);
        }

        [Fact]
        public void WorkStoreMultipleItems_CreatesCorrectJson()
        {
            var memoryFileStore = new MemoryFileStore();
            var workItemStore = new JsonWorkItemStore(memoryFileStore);
            workItemStore.Add(new WorkItem
            {
                Comment = "",
                Started = new DateTime(2017, 5, 1, 11, 0, 0),
                Stopped = new DateTime(2017, 5, 1, 12, 0, 0),
                TicketNumber = "BDP-1301"
            });

            workItemStore.Add(new WorkItem
            {
                Comment = "abc",
                Started = new DateTime(2017, 5, 2, 13, 0, 0),
                Stopped = new DateTime(2017, 5, 2, 14, 0, 0),
                TicketNumber = "BDP-1302"
            });

            workItemStore.Save();

            var content = memoryFileStore.ReadFile("unimportant-file-name.txt");
            var expected = "{\"WorkItems\":[";
            expected +=
                "{\"TicketNumber\":\"BDP-1301\",\"Started\":\"2017-05-01T11:00:00\",\"Stopped\":\"2017-05-01T12:00:00\",\"Comment\":\"\"}";
            expected +=
                ",{\"TicketNumber\":\"BDP-1302\",\"Started\":\"2017-05-02T13:00:00\",\"Stopped\":\"2017-05-02T14:00:00\",\"Comment\":\"abc\"}";
            expected += "]}";
            Assert.Equal(expected, content);
        }
    }
}
