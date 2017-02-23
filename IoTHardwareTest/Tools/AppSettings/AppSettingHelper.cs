using System.Collections.Generic;
using Windows.Storage;

namespace IoTHardwareTest.Tools.AppSettings
{
    public class AppSettingHelper
    {
        public static ApplicationDataContainer Current
        {
            get
            {
                return ApplicationData.Current.LocalSettings;
            }
        }

        public static void SetValue(string key, object value)
        {
            if (Current.Values.ContainsKey(key))
            {
                Current.Values[key] = value;
            }
            else
            {
                Current.Values.Add(new KeyValuePair<string, object>(key, value));
            }
        }

        public static object GetValue(string key)
        {
            if (Current.Values.ContainsKey(key))
            {
                return Current.Values[key];
            }
            return null;
        }

        public static bool DeleteValue(string key)
        {
            if (Current.Containers.ContainsKey(key))
            {
                Current.Values.Remove(key);
                return true;
            }

            return false;
        }
    }
}
