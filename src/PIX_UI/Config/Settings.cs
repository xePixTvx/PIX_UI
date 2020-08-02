using System;
using System.Collections.Generic;
using System.Text;

namespace PIX_UI.Config
{
    internal class Settings
    {
        public bool SHOW_FPS { get; set; } = false;
        public bool LOGGER_USE_CONSOLE { get; set; } = true;
        public bool LOGGER_BACKUP_FULL_LOGS { get; set; } = true;
    }
}
