using System.ComponentModel;
using System.Configuration;

namespace Jopalesha.Common.Infrastructure.Configuration.Xml
{
    internal static class ConfigurationManagerHelper
    {
        public static T ReadAppSetting<T>(string searchKey, T defaultValue = default)
        {
            var result = defaultValue;
            var valueAsString = ConfigurationManager.AppSettings[searchKey];

            if (!string.IsNullOrEmpty(valueAsString))
            {
                try
                {
                    var converter = TypeDescriptor.GetConverter(typeof(T));
                    result = (T) converter.ConvertFromString(valueAsString);
                }
                catch
                {
                    // ignored
                    // Return default value
                }
            }

            return result;
        }
    }
}
