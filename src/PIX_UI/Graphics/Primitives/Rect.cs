using PIX_UI.Graphics.Bases;
using PIX_UI.Utilities;
using SFML.Graphics;
using SFML.System;

namespace PIX_UI.Graphics.Primitives
{
    public class Rect : RenderableBase
    {
        private RectangleShape Shape;
        private Vector2f _Size;

        public Vector2f Size
        {
            get { return _Size; }
            set
            {
                _Size = value;
                NeedsUpdate = true;
            }
        }

        public Rect(string _Name, Alignment Origin_Align, float Width, float Height, float Pos_X, float Pos_Y, Color RGBA)
        {
            Name = _Name;
            Shape = new RectangleShape();
            Size = new Vector2f(Width, Height);
            Origin_Alignment = Origin_Align;
            Position = new Vector2f(Pos_X, Pos_Y);
            Color = RGBA;

            App.RenderSys.AddToRenderList(this);
        }

        public override void Update()
        {
            //Size
            Shape.Size = Size;

            //Origin Align
            Vector2f Origin_Pos = Position_Utils.GetAlignOrigin(Origin_Alignment, Shape);
            Shape.Origin = Origin_Pos;

            //Position + Position Align
            Shape.Position = new Vector2f(Position.X, Position.Y);

            //Rotation
            Shape.Rotation = Rotation;

            //Color
            Shape.FillColor = Color;

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
