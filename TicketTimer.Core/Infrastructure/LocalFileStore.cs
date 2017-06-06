using System;
using System.IO;
using System.Reflection;

namespace TicketTimer.Core.Infrastructure
{
    public class LocalFileStore : FileStore
    {
        public void WriteFile(string fileContent, string fileName)
        {
            // TODO make this look nicer.
            var pathToExecutable = new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase)).LocalPath;
            File.WriteAllText(Path.Combine(pathToExecutable, fileName), fileContent);
        }

        // TODO this is called too often.
        public string ReadFile(string fileName)
        {
            var pathToExecutable = new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase)).LocalPath;
            var statePath = Path.Combine(pathToExecutable, fileName);
            if (File.Exists(statePath))
            {
                return File.ReadAllText(statePath);
            }
            return string.Empty;
        }
    }
}
