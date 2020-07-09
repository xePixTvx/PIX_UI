using PIX_UI.Graphics.Bases;
using SFML.Graphics;
using SFML.System;

namespace PIX_UI.Graphics
{
    public class Rect : RenderableBase
    {
        private RectangleShape Shape;

        public Rect(Alignment Origin_Align, Alignment Position_Align, float Pos_X, float Pos_Y, Color RGBA)
        {
            Shape = new RectangleShape
            {
                Size = new Vector2f(400, 60)//Create a ISizeable Interface + Base??
            };

            Origin_Alignment = Origin_Align;
            Position_Alignment = Position_Align;
            Position = new Vector2f(Pos_X, Pos_Y);
            Color = RGBA;//Create my own color stuff??

            App.RenderSys.AddToRenderList(this);
        }


        public override void Update()
        {
            //Origin Align

            //Position Align

            //Position
            //x = pos_align_pos_x + Position.X ----- same for Y

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
    }
}
