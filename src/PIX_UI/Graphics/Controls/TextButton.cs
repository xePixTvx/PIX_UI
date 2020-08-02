using PIX_UI.Graphics.Bases;
using PIX_UI.Utilities;
using SFML.Graphics;
using SFML.System;
using System;


//TEXT ALIGNMENT UNFINISHED!!!!!

namespace PIX_UI.Graphics.Controls
{
    public class TextButton : ClickableBase
    {
        private RectangleShape BG_Shape;
        private Text Text_Shape;
        private Vector2f _Size;
        private string _FontName;
        private uint _CharSize;
        private string _String;
        private Color _BG_Color;
        private Color _FG_Color;
        private ButtonTextStyles _TextStyle;

        //private CircleShape show_origin_BG;
        //private CircleShape show_origin_Text;

        public Vector2f Size
        {
            get { return _Size; }
            set
            {
                _Size = value;
                NeedsUpdate = true;
            }
        }

        public ButtonTextStyles TextStyle
        {
            get { return _TextStyle; }
            set
            {
                _TextStyle = value;
                NeedsUpdate = true;
            }
        }

        public string FontName
        {
            get { return _FontName; }
            set
            {
                _FontName = value;
                NeedsUpdate = true;
            }
        }

        public uint CharSize
        {
            get { return _CharSize; }
            set
            {
                _CharSize = value;
                NeedsUpdate = true;
            }
        }

        public string String
        {
            get { return _String; }
            set
            {
                _String = value;
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

        public Color FG_Color
        {
            get { return _FG_Color; }
            set
            {
                _FG_Color = value;
                NeedsUpdate = true;
            }
        }


        public TextButton(string _Name, Alignment Origin_Align, float Width, float Height, float Pos_X, float Pos_Y, string Font_Name, uint Char_Size, string TextToDisplay, Color Background_Color, Color Foreground_Color, ButtonTextStyles ButtonTextStyle = ButtonTextStyles.CENTER, Action Exec = null)
        {
            Name = _Name;
            BG_Shape = new RectangleShape();
            Size = new Vector2f(Width, Height);
            Origin_Alignment = Origin_Align;
            Position = new Vector2f(Pos_X, Pos_Y);
            ExecAction = Exec;

            Text_Shape = new Text();
            Text_Shape.Style = Text.Styles.Regular;
            FontName = Font_Name;
            CharSize = Char_Size;
            String = TextToDisplay;
            TextStyle = ButtonTextStyle;

            BG_Color = Background_Color;
            FG_Color = Foreground_Color;

            /*show_origin_BG = new CircleShape
            {
                Radius = 5,
                FillColor = new Color(0, 0, 255, 255)
            };

            show_origin_Text = new CircleShape
            {
                Radius = 5,
                FillColor = new Color(0, 255, 0, 255)
            };*/

            App.RenderSys.AddToRenderList(this);
        }

        public override void Update()
        {
            //Font
            Text_Shape.Font = App.AssetManager.GetFont(FontName);

            //Size
            BG_Shape.Size = Size;
            Text_Shape.CharacterSize = CharSize;

            //Displayed String
            Text_Shape.DisplayedString = String;

            //Origin Align ---- BG
            Vector2f Origin_Pos = Position_Utils.GetAlignOrigin(Origin_Alignment, BG_Shape);
            BG_Shape.Origin = Origin_Pos;

            //Position + Position Align ---- BG
            BG_Shape.Position = new Vector2f(Position.X, Position.Y);

            Vector2f Text_Origin_Pos = Position_Utils.GetAlignOrigin(Alignment.CENTER_CENTER, Text_Shape);
            Text_Shape.Origin = Text_Origin_Pos;
            Text_Shape.Position = new Vector2f(BG_Shape.Position.X, BG_Shape.Position.Y - (Text_Shape.GetGlobalBounds().Height / 2));

            /*show_origin_BG.Origin = Position_Utils.GetAlignOrigin(Alignment.CENTER_CENTER, show_origin_BG);
            show_origin_BG.Position = BG_Shape.Position;
            show_origin_Text.Origin = Position_Utils.GetAlignOrigin(Alignment.CENTER_CENTER, show_origin_Text);
            show_origin_Text.Position = Text_Shape.Position;*/

            //Color
            Text_Shape.FillColor = FG_Color;

            NeedsUpdate = false;
        }

        public override void Render()
        {
            App.Window.Draw(BG_Shape);
            App.Window.Draw(Text_Shape);

            //App.Window.Draw(show_origin_BG);
            //App.Window.Draw(show_origin_Text);
        }

        public override void Destroy()
        {
            App.RenderSys.RemoveFromRenderList(this);
            BG_Shape.Dispose();
            Text_Shape.Dispose();
        }





        public override void UpdateSelection()
        {
            IsSelected = (Position_Utils.IsHovered(BG_Shape) == true) ? true : false;

            if (IsSelected)
            {
                BG_Shape.FillColor = new Color(BG_Color.R, BG_Color.G, BG_Color.B, 255);
            }
            else
            {
                BG_Shape.FillColor = new Color(BG_Color.R, BG_Color.G, BG_Color.B, 130);
            }
        }
    }
}
