using PIX_UI.Graphics.Bases;
using PIX_UI.Utilities;
using SFML.Graphics;
using SFML.System;

namespace PIX_UI.Graphics.Primitives
{
    public class SimpleSprite : RenderableBase
    {
        private Sprite Shape;
        private string _TextureName;

        public string TextureName
        {
            get { return _TextureName; }
            set
            {
                _TextureName = value;
                NeedsUpdate = true;
            }
        }

        public SimpleSprite(string _Name, Alignment Origin_Align, float Pos_X, float Pos_Y, string Texture_Name)
        {
            Name = _Name;
            Shape = new Sprite();
            TextureName = Texture_Name;
            Origin_Alignment = Origin_Align;
            Position = new Vector2f(Pos_X, Pos_Y);

            App.RenderSys.AddToRenderList(this);
        }

        public override void Update()
        {
            //Texture
            Shape.Texture = App.AssetManager.GetTexture(TextureName);

            //Origin Align
            Vector2f Origin_Pos = Position_Utils.GetAlignOrigin(Origin_Alignment, Shape);
            Shape.Origin = Origin_Pos;

            //Position + Position Align
            Shape.Position = new Vector2f(Position.X, Position.Y);

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
