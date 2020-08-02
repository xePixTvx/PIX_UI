using SFML.System;
using SFML.Graphics;

namespace PIX_UI.Graphics.Interfaces
{
    public interface IRenderable
    {
        string Name { get; set; }
        int RenderLayer { get; set; }
        bool IsActive { get; set; }
        bool IsVisible { get; set; }
        bool NeedsUpdate { get; set; }
        void Update();
        void Render();
        void Destroy();

        Alignment Origin_Alignment { get; set; }
        Vector2f Position { get; set; }
        Color Color { get; set; }
    }
}
