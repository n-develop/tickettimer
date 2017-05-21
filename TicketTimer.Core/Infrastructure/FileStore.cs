namespace TicketTimer.Core.Infrastructure
{
    public interface FileStore
    {
        void WriteFile(string fileContent, string fileName);

        string ReadFile(string fileName);
    }
}
