using PIX_UI.Graphics.Bases;

namespace PIX_UI.Graphics
{
    public class Rect : RenderableBase
    {
        public Rect()
        {
            App.RenderSys.AddToRenderList(this);
        }
    }
}
