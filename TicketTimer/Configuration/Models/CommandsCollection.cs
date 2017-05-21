using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace TicketTimer.Configuration.Model
{
    public class CommandsCollection : ConfigurationElementCollection, IEnumerable<CommandElement>
    {

        public void Add(CommandElement element)
        {
            BaseAdd(element);
        }

        public void Clear()
        {
            BaseClear();
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new CommandElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((CommandElement)element).Type;
        }

        IEnumerator<CommandElement> IEnumerable<CommandElement>.GetEnumerator()
        {
            return this.OfType<CommandElement>().GetEnumerator();
        }
    }
}
