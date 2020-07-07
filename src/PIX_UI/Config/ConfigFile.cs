using System;
using System.IO;

namespace PIX_UI.Config
{
    public class ConfigFile
    {
        private static string FileName;
        private static IniFile Ini;

        public ConfigFile(string _FileName = "config.ini")
        {
            FileName = _FileName;
            string FileDirectory = Path.Combine(App.ResourceFolder, "config");
            if (!Directory.Exists(FileDirectory))
            {
                Directory.CreateDirectory(FileDirectory);
            }
            Ini = new IniFile(Path.Combine(FileDirectory, FileName));
        }

        public string GetConfigSetting(string section, string key, string default_value)
        {
            string setting = Ini.IniReadValue(section, key);
            if (setting == "")
            {
                setting = default_value;
                SetConfigSetting(section, key, default_value);
            }
            return setting;
        }

        public void SetConfigSetting(string section, string key, string value)
        {
            Ini.IniWriteValue(section, key, value);
        }
    }
}
