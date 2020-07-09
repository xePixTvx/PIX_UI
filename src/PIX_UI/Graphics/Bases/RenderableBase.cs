using PIX_UI.Graphics.Interfaces;

namespace PIX_UI.Graphics.Bases
{
    public class RenderableBase : IRenderable
    {
        private int _RenderLayer = 0;
        private bool _IsActive = true;
        private bool _IsVisible = true;
        private bool _NeedsUpdate = false;

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
            App.Log.Print("Update() not defined for " + this, Logger.LoggerType.ERROR);
        }

        public virtual void Render()
        {
            App.Log.Print("Render() not defined for " + this, Logger.LoggerType.ERROR);
        }
    }
}
