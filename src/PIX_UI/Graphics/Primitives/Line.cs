using PIX_UI.Graphics.Bases;
using PIX_UI.Utilities;
using SFML.Graphics;
using SFML.System;

namespace PIX_UI.Graphics.Primitives
{
    public class Line : RenderableBase
    {
        private VertexArray Shape;

        public Line(string _Name, Vector2f Start_Pos, Vector2f End_Pos, Color Start_RGBA, Color End_RGBA)
        {
            Name = _Name;
            Shape = new VertexArray(PrimitiveType.Lines, 2);
            Shape[0] = new Vertex(Start_Pos, Start_RGBA);
            Shape[1] = new Vertex(End_Pos, End_RGBA);

            App.RenderSys.AddToRenderList(this);
        }

        public override void Update()
        {
            NeedsUpdate = false;
        }

        public override void Render()
        {
            App.Window.Draw(Shape);
        }

        public override void Destroy()
        {
            App.RenderSys.RemoveFromRenderList(this);
            Shape.Dispose();
        }
    }
}
