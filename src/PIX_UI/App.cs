﻿using System;
using System.IO;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using PIX_UI.Logger;
using PIX_UI.Config;
using PIX_UI.Assets;
using PIX_UI.Render;
using PIX_UI.Graphics;
using PIX_UI.Graphics.Interfaces;
using PIX_UI.Graphics.Primitives;
using PIX_UI.Utilities;

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
        public static Config.Config Config;

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

        //FPS Text
        private SimpleText FPS_Text;



        protected App(string ResourceFolderName, string ConfigFileName, string WindowTitle, uint WindowWidth, uint WindowHeight)
        {
            //Set Resource Folder Path/Name
            ResourceFolder = Path.Combine(Environment.CurrentDirectory, ResourceFolderName);

            //Create/Load Config
            Config = new Config.Config(Path.Combine(ResourceFolder, "config", ConfigFileName));

            //Create Logger
            Log = new AppLogger(Config.LOGGER_USE_CONSOLE, Config.LOGGER_BACKUP_FULL_LOGS);

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

            //Create Fps Text
            Vector2f FPS_POS = Position_Utils.GetPositionOnScreen(Alignment.LEFT_TOP);
            FPS_Text = new SimpleText("APP_FPS_TEXT_MAIN", Alignment.LEFT_TOP, FPS_POS.X, FPS_POS.Y, new Color(255, 255, 255, 255), "default", 16, Text.Styles.Regular, "");
            FPS_Text.RenderLayer = 999;
            if(!Config.SHOW_FPS)
            {
                FPS_Text.IsActive = false;
            }
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

                //Update FPS Text
                if(Config.SHOW_FPS && FPS_Text.IsActive)
                {
                    FPS_Text.String = "FPS: " + GetFPS() + " ------ " + GetFrameTime() + " MS";
                }

                AppUpdate();
                RenderSys.Render();
                Window.Display();
                FrameTime = FrameTimeClock.Restart().AsMilliseconds();
                if (DefaultAssetLoadFailed)
                {
                    Exit();
                }
            }
            OnAppClosing();
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
            Window.MouseButtonReleased += new EventHandler<MouseButtonEventArgs>(OnMouseButtonReleased);
            Window.MouseButtonPressed += new EventHandler<MouseButtonEventArgs>(OnMouseButtonPressed);
            Window.MouseButtonPressed += new EventHandler<MouseButtonEventArgs>(OnMouseButtonPressed_ClickableElems);
            Window.KeyReleased += new EventHandler<KeyEventArgs>(OnKeyReleased);
            Window.KeyPressed += new EventHandler<KeyEventArgs>(OnKeyPressed);
        }
        protected virtual void OnMouseButtonReleased(object sender, MouseButtonEventArgs e)
        {
        }
        protected virtual void OnMouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
        }
        protected virtual void OnKeyReleased(object sender, KeyEventArgs e)
        {
        }
        protected virtual void OnKeyPressed(object sender, KeyEventArgs e)
        {
        }
        public void OnMouseButtonPressed_ClickableElems(object sender, MouseButtonEventArgs e)
        {
            if(e.Button == Mouse.Button.Left)
            {
                foreach(IRenderable elem in RenderSys.GetRenderList())
                {
                    if (elem.IsActive && elem is IClickable clickElem)
                    {
                        if (clickElem.IsSelected)
                        {
                            clickElem.ExecuteAction();
                        }
                    }
                }
            }
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



        //Test
        public static IRenderable GetElemByName(string ElemName)
        {
            foreach(IRenderable elem in RenderSys.GetRenderList())
            {
                if(elem.Name == ElemName)
                {
                    return elem;
                }
            }
            return null;
        }


    }
}
