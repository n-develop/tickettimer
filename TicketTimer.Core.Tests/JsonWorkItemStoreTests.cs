using System;
using TicketTimer.Core.Tests.Mocks;
using Xunit;

namespace TicketTimer.Core.Tests
{
    public class JsonWorkItemStoreTests
    {
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
    }
}
