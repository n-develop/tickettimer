using System;
using System.IO;
using System.Reflection;

namespace TicketTimer.Core.Infrastructure
{
    public class LocalFileStore : FileStore
    {
        private static readonly string CurrentDirectory =
            new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase)).LocalPath;

        public void WriteFile(string fileContent, string fileName)
        {
            var pathToFile = Path.Combine(CurrentDirectory, fileName);
            File.WriteAllText(pathToFile, fileContent);
        }

        public string ReadFile(string fileName)
        {
            var statePath = Path.Combine(CurrentDirectory, fileName);

            if (File.Exists(statePath))
            {
                return File.ReadAllText(statePath);
            }
            return string.Empty;
        }
    }
}
