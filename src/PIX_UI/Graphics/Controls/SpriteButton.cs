using PIX_UI.Graphics.Bases;
using PIX_UI.Utilities;
using SFML.Graphics;
using SFML.System;
using System;

namespace PIX_UI.Graphics.Controls
{
    public class SpriteButton : ClickableBase
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

        public SpriteButton(string _Name, Alignment Origin_Align, Alignment Position_Align, float Pos_X, float Pos_Y, string Texture_Name, Action Exec = null)
        {
            Name = _Name;
            Shape = new Sprite();
            TextureName = Texture_Name;
            Origin_Alignment = Origin_Align;
            Position_Alignment = Position_Align;
            Position = new Vector2f(Pos_X, Pos_Y);
            ExecAction = Exec;

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
            Vector2f Align_Pos = Position_Utils.GetAlignPosition(Position_Alignment);
            Shape.Position = new Vector2f(Align_Pos.X + Position.X, Align_Pos.Y + Position.Y);

            //Rotation
            Shape.Rotation = Rotation;

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





        public override void UpdateSelection()
        {
            IsSelected = (Position_Utils.IsHovered(Shape) == true) ? true : false;

            if(IsSelected)
            {
                Shape.Color = new Color(255, 255, 255, 255);
            }
            else
            {
                Shape.Color = new Color(255, 255, 255, 180);
            }
        }

    }
}
