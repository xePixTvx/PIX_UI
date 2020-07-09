using System;
using System.IO;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using PIX_UI.Logger;
using PIX_UI.Config;
using PIX_UI.Assets;
using PIX_UI.Render;

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

        //Assets
        public static AssetManager AssetManager;
        public static string FontFolder = "fonts";
        public static string TextureFolder = "textures";

        //Default Assets
        private bool DefaultAssetLoadFailed = false;
        public static Font DefaultFont { get; private set; }
        public static Texture DefaultTexture { get; private set; }

        //Render System
        public static RenderSystem RenderSys;



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

            //Create AssetManager
            AssetManager = new AssetManager();

            //Load Default Assets
            if (!LoadDefaultAssets())
            {
                Log.Print("Loading Default Assets Failed!", LoggerType.ERROR);
                DefaultAssetLoadFailed = true;
            }

            //Create Render System
            RenderSys = new RenderSystem();
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
                RenderSys.Render();
                Window.Display();
                FrameTime = FrameTimeClock.Restart().AsMilliseconds();
                if (DefaultAssetLoadFailed)
                {
                    Exit();
                }
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

        #region Default Assets
        private bool LoadDefaultAssets()
        {
            //Default Font
            try
            {
                DefaultFont = new Font(Path.Combine(ResourceFolder, FontFolder, "default.ttf"));
                Log.Print("Default Font Loaded", LoggerType.ASSET);
            }
            catch (Exception e)
            {
                Log.Print(e.Message, LoggerType.ERROR);
                return false;
            }

            //Default Texture
            try
            {
                DefaultTexture = new Texture(Path.Combine(ResourceFolder, TextureFolder, "default.png"));
                DefaultTexture.Repeated = true;
                Log.Print("Default Texture Loaded", LoggerType.ASSET);
            }
            catch (Exception e)
            {
                Log.Print(e.Message, LoggerType.ERROR);
                return false;
            }
            return true;
        }
        #endregion Default Assets

    }
}
