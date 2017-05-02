using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTimer.Core
{
    public interface FileStore
    {
        void WriteFile(string fileContent, string fileName);

        string ReadFile(string fileName);
    }
}
