using System;
using System.IO;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using PIX_UI.Logger;
using PIX_UI.Config;

namespace PIX_UI
{
    public abstract class App
    {
        //Window
        public static RenderWindow Window { get; private set; }
        public static VideoMode WindowSize { get; private set; }
        public Color WindowBackgroundColor { get; set; } = new Color(0, 0, 0);

        //Frame Time/Rate
        private Clock FrameTimeClock;
        private uint FrameRateLimit = 60;
        private float FrameTime;

        //Is App Active
        public bool IsActive { get; private set; } = false;

        //App Update
        protected abstract void AppUpdate();

        //On App Close
        protected abstract void OnAppClosing();

        //Logger
        public static AppLogger Log;

        //Resource Folder
        public static string ResourceFolder { get; private set; } = Path.Combine(Environment.CurrentDirectory, "data");

        //Config
        public static ConfigFile Config;

        //Basic/Main App Settings
        public static bool Setting_ShowLogInConsole;
        public static bool Setting_BackupFullLogFiles;



        protected App(string ResourceFolderName, string ConfigFileName, string WindowTitle, uint WindowWidth, uint WindowHeight)
        {
            //Set Resource Folder Path/Name
            ResourceFolder = Path.Combine(Environment.CurrentDirectory, ResourceFolderName);

            //Create/Load Config
            Config = new ConfigFile(ConfigFileName);

            //Load Basic/Main App Settings from Config File
            Setting_ShowLogInConsole = (Config.GetConfigSetting("MAIN", "ShowLogInConsole", "false") == "true") ? true : false;
            Setting_BackupFullLogFiles = (Config.GetConfigSetting("MAIN", "BackupFullLogFiles", "true") == "true") ? true : false;

            //Create Logger
            Log = new AppLogger(Setting_ShowLogInConsole, Setting_BackupFullLogFiles);

            //Set Window Size
            WindowSize = new VideoMode(WindowWidth, WindowHeight);

            //Create Window
            InitWindow(WindowTitle, Styles.Close);
        }

        //Start App
        public void Start()
        {
            Log.Print("Start Main Loop");
            IsActive = true;
            MainLoop();
        }

        //Exit App
        public void Exit()
        {
            Log.Print("---------- APP CLOSED ----------");
            OnAppClosing();
            IsActive = false;
        }


        //App Main Loop
        private void MainLoop()
        {
            FrameTimeClock = new Clock();
            while (IsActive)
            {
                Window.DispatchEvents();
                Window.Clear(WindowBackgroundColor);
                AppUpdate();
                Window.Display();
                FrameTime = FrameTimeClock.Restart().AsMilliseconds();
            }

            Log.LoggerDispose();
            Window.Close();
        }



        #region Frametime
        //Get Frametime
        public float GetFrameTime()
        {
            return FrameTime;
        }

        //Get Fps
        public uint GetFPS()
        {
            uint fps = FrameRateLimit * ((uint)FrameTime * FrameRateLimit) / 1000;
            return fps;
        }
        #endregion Frametime

        #region Init Window Stuff
        private void InitWindow(string title, Styles style)
        {
            Log.Print("Init Window");
            Window = new RenderWindow(WindowSize, title, style);
            Window.SetVerticalSyncEnabled(false);
            Window.SetFramerateLimit(FrameRateLimit);
            InitWindowEventHandlers();
        }

        private void InitWindowEventHandlers()
        {
            Log.Print("Init Event Handlers");
            Window.Closed += (_, __) => Exit();
            Window.MouseButtonReleased += new EventHandler<MouseButtonEventArgs>(onMouseButtonReleased);
            Window.MouseButtonPressed += new EventHandler<MouseButtonEventArgs>(onMouseButtonPressed);
            Window.KeyReleased += new EventHandler<KeyEventArgs>(onKeyReleased);
            Window.KeyPressed += new EventHandler<KeyEventArgs>(onKeyPressed);
        }
        protected virtual void onMouseButtonReleased(object sender, MouseButtonEventArgs e)
        {
        }
        protected virtual void onMouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
        }
        protected virtual void onKeyReleased(object sender, KeyEventArgs e)
        {
        }
        protected virtual void onKeyPressed(object sender, KeyEventArgs e)
        {
        }
        #endregion

    }
}
