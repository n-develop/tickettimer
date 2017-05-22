using System;
using System.IO;
using TicketTimer.Core.Commands;
using TicketTimer.Core.Infrastructure;
using TicketTimer.Core.Services;
using TicketTimer.Core.Tests.Mocks;
using Xunit;

namespace TicketTimer.Core.Tests
{
    public class StartCommandTests
    {
        [Fact(Skip = "This test is not usefull, yet...")]
        public void NoParameters_PrintsHelpText()
        {
            using (StringWriter consoleOutput = new StringWriter())
            {
                var command = new StartCommand(new DefaultWorkItemService(new JsonWorkItemStore(new MemoryFileStore())), new LocalDateProvider());
                Console.SetOut(consoleOutput);
                command.Run(null);
                Assert.Equal("You are '' on ticket ''\r\n", consoleOutput.ToString());
            }
        }
    }
}
