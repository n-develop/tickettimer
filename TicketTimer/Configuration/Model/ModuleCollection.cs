using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTimer.Configuration.Model
{
    public class ModuleCollection : ConfigurationElementCollection, IEnumerable<ModuleElement>
    {
        public void Add(ModuleElement element)
        {
            BaseAdd(element);
        }

        public void Clear()
        {
            BaseClear();
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ModuleElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ModuleElement)element).Type;
        }

        IEnumerator<ModuleElement> IEnumerable<ModuleElement>.GetEnumerator()
        {
            return this.OfType<ModuleElement>().GetEnumerator();
        }
    }
}
