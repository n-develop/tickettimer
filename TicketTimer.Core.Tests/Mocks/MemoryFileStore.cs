using System.Text;
using TicketTimer.Core.Infrastructure;

namespace TicketTimer.Core.Tests.Mocks
{
    public class MemoryFileStore : FileStore
    {
        private readonly StringBuilder _content = new StringBuilder();

        public void WriteFile(string fileContent, string fileName)
        {
            _content.Clear();
            _content.Append(fileContent);
        }

        public string ReadFile(string fileName)
        {
            return _content.ToString();
        }
    }
}
