using System.Configuration;

namespace Download.Common.Extensions
{
   public class AppConfigExtension
    {
       public string Get<T>(string key)
       {
           return ConfigurationSettings.AppSettings[key];
       }

    }

    public class ConfigKey
    {
        public const string Version = "Version";

    }
}
