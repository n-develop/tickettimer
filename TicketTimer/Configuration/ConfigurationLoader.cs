using System.Configuration;

namespace TicketTimer.Configuration
{
    public class ConfigurationLoader
    {
        public static System.Configuration.Configuration GetConfigurationFromFile(string configFilename)
        {
            ExeConfigurationFileMap configFileMap =
                new ExeConfigurationFileMap { ExeConfigFilename = configFilename };

            System.Configuration.Configuration config =
                ConfigurationManager.OpenMappedExeConfiguration(
                    configFileMap, ConfigurationUserLevel.None);

            return config;
        }
    }
}
