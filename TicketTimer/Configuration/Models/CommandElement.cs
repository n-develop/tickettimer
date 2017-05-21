using System.Configuration;

namespace TicketTimer.Configuration.Model
{
    public class CommandElement : ConfigurationElement
    {
        [ConfigurationProperty("type", IsRequired = true)]
        public string Type
        {
            get => (string)this["type"];
            set => this["type"] = value;
        }
    }
}
