using PIX_UI.Graphics.Bases;
using PIX_UI.Utilities;
using SFML.Graphics;
using SFML.System;
using System;

namespace PIX_UI.Graphics.Controls
{
    public class ProgressBar : RenderableBase
    {
        private RectangleShape BG_Shape;
        private RectangleShape Fill_Shape;

        private Vector2f _Size;
        private ProgressBarStyles _Style;
        private float _Value;
        private Color _BG_Color;
        private Color _Fill_Color;

        public Vector2f Size
        {
            get { return _Size; }
            set
            {
                _Size = value;
                NeedsUpdate = true;
            }
        }

        public ProgressBarStyles Style
        {
            get { return _Style; }
            set
            {
                _Style = value;
                NeedsUpdate = true;
            }
        }

        public float Value
        {
            get { return _Value; }
            set
            {
                _Value = (value > 100) ? 100 : (value < 0) ? 0 : value;
                NeedsUpdate = true;
            }
        }

        public Color BG_Color
        {
            get { return _BG_Color; }
            set 
            {
                _BG_Color = value;
                NeedsUpdate = true;
            }
        }

        public Color Fill_Color
        {
            get { return _Fill_Color; }
            set
            {
                _Fill_Color = value;
                NeedsUpdate = true;
            }
        }


        public ProgressBar(string _Name, float Width, float Height, float Pos_X, float Pos_Y, ProgressBarStyles ProgressBarStyle, Color BGColor, Color FillColor, float ProgressValue)
        {
            Name = _Name;
            BG_Shape = new RectangleShape();
            Fill_Shape = new RectangleShape();
            Size = new Vector2f(Width, Height);
            Position = new Vector2f(Pos_X, Pos_Y);
            Style = ProgressBarStyle;
            BG_Color = BGColor;
            Fill_Color = FillColor;
            Value = ProgressValue;

            App.RenderSys.AddToRenderList(this);
        }

        public override void Update()
        {
            //BG Size
            BG_Shape.Size = Size;

            if(Style == ProgressBarStyles.HORIZONTAL)
            {
                float fill_size_x = (Size.X / 100) * Value;
                Fill_Shape.Size = new Vector2f(fill_size_x, Size.Y);

                BG_Shape.Origin = Position_Utils.GetAlignOrigin(Alignment.CENTER_CENTER, BG_Shape);
                Fill_Shape.Origin = Position_Utils.GetAlignOrigin(Alignment.LEFT_CENTER, Fill_Shape);

                BG_Shape.Position = Position;
                Fill_Shape.Position = new Vector2f(Position.X - BG_Shape.Origin.X, Position.Y);
            }
            else if(Style == ProgressBarStyles.VERTICAL)
            {
                float fill_size_y = (Size.Y / 100) * Value;
                Fill_Shape.Size = new Vector2f(Size.X, fill_size_y);

                BG_Shape.Origin = Position_Utils.GetAlignOrigin(Alignment.CENTER_BOTTOM, BG_Shape);
                Fill_Shape.Origin = Position_Utils.GetAlignOrigin(Alignment.CENTER_BOTTOM, Fill_Shape);

                BG_Shape.Position = Position;
                Fill_Shape.Position = new Vector2f(Position.X, Position.Y);
            }
            else
            {
                App.Log.Print("Unknown ProgressBarStyle for " + this.Name, Logger.LoggerType.ERROR);
            }

            //Color
            BG_Shape.FillColor = BG_Color;
            Fill_Shape.FillColor = Fill_Color;


            NeedsUpdate = false;
        }

        public override void Render()
        {
            App.Window.Draw(BG_Shape);
            App.Window.Draw(Fill_Shape);
        }

        public override void Destroy()
        {
            App.RenderSys.RemoveFromRenderList(this);
            BG_Shape.Dispose();
            Fill_Shape.Dispose();
        }
    }
}
