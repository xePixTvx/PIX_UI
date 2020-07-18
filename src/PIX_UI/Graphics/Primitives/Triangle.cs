using PIX_UI.Graphics.Bases;
using PIX_UI.Utilities;
using SFML.Graphics;
using SFML.System;

namespace PIX_UI.Graphics.Primitives
{
    public class Triangle : RenderableBase
    {
        private CircleShape Shape;
        private float _Radius;

        public float Radius
        {
            get { return _Radius; }
            set
            {
                _Radius = value;
                NeedsUpdate = true;
            }
        }

        public Triangle(string _Name, Alignment Origin_Align, float Size, float Pos_X, float Pos_Y, Color RGBA)
        {
            Name = _Name;
            Shape = new CircleShape(Size, 3);
            Radius = Size;
            Origin_Alignment = Origin_Align;
            Position = new Vector2f(Pos_X, Pos_Y);
            Color = RGBA;

            App.RenderSys.AddToRenderList(this);
        }

        public override void Update()
        {
            //Radius
            Shape.Radius = Radius;

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
