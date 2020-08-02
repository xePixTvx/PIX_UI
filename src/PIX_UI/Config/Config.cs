using System.IO;
using System.Text;
using System;
using Newtonsoft.Json;

namespace PIX_UI.Config
{
    public class Config
    {
        private string ConfigFilePath;
        private Settings AppSettings;

        /// <summary>
        /// Create Config
        /// </summary>
        public Config(string FilePath)
        {
            ConfigFilePath = FilePath;
            if (!File.Exists(ConfigFilePath))
            {
                AppSettings = new Settings();
                SaveConfig();
            }
            else
            {
                AppSettings = LoadConfig();
            }
        }

        /// <summary>
        /// Show Fps
        /// </summary>
        public bool SHOW_FPS
        {
            get => AppSettings.SHOW_FPS;
            set => AppSettings.SHOW_FPS = value;
        }

        /// <summary>
        /// Logger Prints are shown in the console
        /// </summary>
        public bool LOGGER_USE_CONSOLE
        {
            get => AppSettings.LOGGER_USE_CONSOLE;
            set
            {
                AppSettings.LOGGER_USE_CONSOLE = value;
                App.Log.UseConsole = value;
            }
        }

        /// <summary>
        /// Logger backups full log files
        /// </summary>
        public bool LOGGER_BACKUP_FULL_LOGS
        {
            get => AppSettings.LOGGER_BACKUP_FULL_LOGS;
            set
            {
                AppSettings.LOGGER_BACKUP_FULL_LOGS = value;
                App.Log.BackupFullLogs = value;
            }
        }



        /// <summary>
        /// Save Config to File
        /// </summary>
        public void SaveConfig()
        {
            string settings_data = JsonConvert.SerializeObject(AppSettings, Formatting.Indented);
            File.WriteAllText(ConfigFilePath, settings_data);
        }

        private Settings LoadConfig()
        {
            string settings_data = File.ReadAllText(ConfigFilePath, Encoding.ASCII);
            object settings_tmp = JsonConvert.DeserializeObject<Settings>(settings_data);
            return (Settings)Convert.ChangeType(settings_tmp, typeof(Settings));
        }

    }
}
