using System;
using System.IO;
using System.Reflection;

namespace TicketTimer.Core.Infrastructure
{
    public class LocalFileStore : FileStore
    {
        public void WriteFile(string fileContent, string fileName)
        {
            var pathToExecutable = new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase)).LocalPath;
            File.WriteAllText(Path.Combine(pathToExecutable, fileName), fileContent);
        }

        public string ReadFile(string fileName)
        {
            var pathToExecutable = new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase)).LocalPath;
            var statePath = Path.Combine(pathToExecutable, fileName);
            Console.WriteLine("Try to read " + statePath);
            if (File.Exists(statePath))
            {
                return File.ReadAllText(statePath);
            }
            return string.Empty;
        }
    }
}
