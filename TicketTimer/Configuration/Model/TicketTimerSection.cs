﻿using System.Configuration;

namespace TicketTimer.Configuration.Model
{
    public class TicketTimerSection : ConfigurationSection
    {
        [ConfigurationProperty("commands", DefaultValue = null, IsRequired = true)]
        [ConfigurationCollection(typeof(CommandsCollection), AddItemName = "add", ClearItemsName = "clear", RemoveItemName = "remove")]
        public CommandsCollection Commands
        {
            get
            {
                return (CommandsCollection)this["commands"];
            }
            set
            {
                this["commands"] = value;
            }
        }

        [ConfigurationProperty("modules", DefaultValue = null, IsRequired = true)]
        [ConfigurationCollection(typeof(ModuleCollection), AddItemName = "add", ClearItemsName = "clear", RemoveItemName = "remove")]
        public ModuleCollection Modules
        {
            get
            {
                return (ModuleCollection)this["modules"];
            }
            set
            {
                this["modules"] = value;
            }
        }
    }
}
