using PIX_UI.Graphics.Interfaces;
using SFML.Graphics;
using SFML.System;

namespace PIX_UI.Graphics.Bases
{
    public class RenderableBase : IRenderable
    {
        private string _Name = "UNDEFINED_NAME";
        private int _RenderLayer = 0;
        private bool _IsActive = true;
        private bool _IsVisible = true;
        private bool _NeedsUpdate = false;

        private Alignment _Origin_Alignment = Alignment.LEFT_TOP;
        private Vector2f _Position = new Vector2f(0, 0);
        private Color _Color = new Color(255, 255, 255, 255);

        public virtual string Name
        {
            get 
            { 
                if(_Name == "UNDEFINED_NAME")
                {
                    App.Log.Print("Name not defined for " + this, Logger.LoggerType.ERROR);
                }
                return _Name; 
            }
            set { _Name = value; }
        }

        public virtual int RenderLayer
        {
            get { return _RenderLayer; }
            set 
            { 
                _RenderLayer = value;
                App.RenderSys.SortRenderList();
            }
        }

        public virtual bool IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }

        public virtual bool IsVisible
        {
            get { return _IsVisible; }
            set { _IsVisible = value; }
        }

        public virtual bool NeedsUpdate
        {
            get { return _NeedsUpdate; }
            set { _NeedsUpdate = value; }
        }

        public virtual void Update()
        {
            App.Log.Print("Update() not defined for " + Name, Logger.LoggerType.ERROR);
        }

        public virtual void Render()
        {
            App.Log.Print("Render() not defined for " + Name, Logger.LoggerType.ERROR);
        }

        public virtual void Destroy()
        {
            App.Log.Print("Destroy() not defined for " + Name, Logger.LoggerType.ERROR);
        }



        public virtual Alignment Origin_Alignment
        {
            get { return _Origin_Alignment; }
            set 
            { 
                _Origin_Alignment = value;
                _NeedsUpdate = true;
            }
        }

        public virtual Vector2f Position
        {
            get { return _Position; }
            set 
            { 
                _Position = value;
                _NeedsUpdate = true;
            }
        }

        public virtual Color Color
        {
            get { return _Color; }
            set 
            {
                _Color = value;
                _NeedsUpdate = true;
            }
        }
    }
}
