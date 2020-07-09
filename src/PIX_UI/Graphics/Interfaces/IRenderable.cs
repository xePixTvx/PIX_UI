namespace PIX_UI.Graphics.Interfaces
{
    public interface IRenderable
    {
        int RenderLayer { get; set; }
        bool IsActive { get; set; }
        bool IsVisible { get; set; }
        bool NeedsUpdate { get; set; }
        void Update();
        void Render();
    }
}
