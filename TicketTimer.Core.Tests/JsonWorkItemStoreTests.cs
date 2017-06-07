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
            workItemStore.AddToArchive(new WorkItem("BDP-1301")
            {
                Comment = "",
                Started = new DateTime(2017, 5, 1, 11, 0, 0),
                Stopped = new DateTime(2017, 5, 1, 12, 0, 0)
            });

            var content = memoryFileStore.ReadFile("unimportant-file-name.txt");
            var expected = "{\r\n  \"CurrentWorkItem\": {\r\n    \"TicketNumber\": \"- no ticket -\",\r\n    \"Started\": \"0001-01-01T00:00:00\",\r\n    \"Stopped\": \"0001-01-01T00:00:00\",\r\n    \"Comment\": \"\"\r\n  },\r\n  \"WorkItemArchive\": [\r\n    {\r\n      \"TicketNumber\": \"BDP-1301\",\r\n      \"Started\": \"2017-05-01T11:00:00\",\r\n      \"Stopped\": \"2017-05-01T12:00:00\",\r\n      \"Comment\": \"\"\r\n    }\r\n  ]\r\n}";
            Assert.Equal(expected, content);
        }

        [Fact]
        public void WorkStoreWithoutItems_CreatesCorrectJson()
        {
            var memoryFileStore = new MemoryFileStore();
            var workItemStore = new JsonWorkItemStore(memoryFileStore);
            workItemStore.SetCurrent(WorkItem.Empty);

            var content = memoryFileStore.ReadFile("unimportant-file-name.txt");
            var expected = "{\r\n  \"CurrentWorkItem\": {\r\n    \"TicketNumber\": \"- no ticket -\",\r\n    \"Started\": \"0001-01-01T00:00:00\",\r\n    \"Stopped\": \"0001-01-01T00:00:00\",\r\n    \"Comment\": \"\"\r\n  },\r\n  \"WorkItemArchive\": []\r\n}";
            Assert.Equal(expected, content);
        }

        [Fact]
        public void WorkStoreMultipleItems_CreatesCorrectJson()
        {
            var memoryFileStore = new MemoryFileStore();
            var workItemStore = new JsonWorkItemStore(memoryFileStore);
            workItemStore.AddToArchive(new WorkItem("BDP-1301")
            {
                Comment = "",
                Started = new DateTime(2017, 5, 1, 11, 0, 0),
                Stopped = new DateTime(2017, 5, 1, 12, 0, 0)
            });

            workItemStore.AddToArchive(new WorkItem("BDP-1302")
            {
                Comment = "abc",
                Started = new DateTime(2017, 5, 2, 13, 0, 0),
                Stopped = new DateTime(2017, 5, 2, 14, 0, 0)
            });

            var content = memoryFileStore.ReadFile("unimportant-file-name.txt");
            var expected =
                "{\r\n  \"CurrentWorkItem\": {\r\n    \"TicketNumber\": \"- no ticket -\",\r\n    \"Started\": \"0001-01-01T00:00:00\",\r\n    \"Stopped\": \"0001-01-01T00:00:00\",\r\n    \"Comment\": \"\"\r\n  },\r\n  \"WorkItemArchive\": [\r\n    {\r\n      \"TicketNumber\": \"BDP-1301\",\r\n      \"Started\": \"2017-05-01T11:00:00\",\r\n      \"Stopped\": \"2017-05-01T12:00:00\",\r\n      \"Comment\": \"\"\r\n    },\r\n    {\r\n      \"TicketNumber\": \"BDP-1302\",\r\n      \"Started\": \"2017-05-02T13:00:00\",\r\n      \"Stopped\": \"2017-05-02T14:00:00\",\r\n      \"Comment\": \"abc\"\r\n    }\r\n  ]\r\n}";

            Assert.Equal(expected, content);
        }
    }
}
