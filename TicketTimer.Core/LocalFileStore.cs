using System.IO;

namespace TicketTimer.Core
{
    public class LocalFileStore : FileStore
    {
        public void WriteFile(string fileContent, string fileName)
        {
            File.WriteAllText(fileName, fileContent);
        }

        public string ReadFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                return File.ReadAllText(fileName);
            }
            return string.Empty;
        }
    }
}
