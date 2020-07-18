﻿using PIX_UI.Graphics.Bases;
using PIX_UI.Utilities;
using SFML.Graphics;
using SFML.System;

namespace PIX_UI.Graphics.Primitives
{
    public class SimpleText : RenderableBase
    {
        private Text Shape;
        private string _FontName;
        private uint _CharSize;
        private Text.Styles _TextStyle;
        private string _String;

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

        public Text.Styles TextStyle
        {
            get { return _TextStyle; }
            set
            {
                _TextStyle = value;
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

        public SimpleText(string _Name, Alignment Origin_Align, float Pos_X, float Pos_Y, Color RGBA, string Font_Name, uint Char_Size, Text.Styles Text_Style, string TextToDisplay = "")
        {
            Name = _Name;
            Shape = new Text();
            FontName = Font_Name;
            CharSize = Char_Size;
            TextStyle = Text_Style;
            String = TextToDisplay;
            Origin_Alignment = Origin_Align;
            Position = new Vector2f(Pos_X, Pos_Y);
            Color = RGBA;

            App.RenderSys.AddToRenderList(this);
        }

        public override void Update()
        {
            //Font
            Shape.Font = App.AssetManager.GetFont(FontName);

            //Char Size
            Shape.CharacterSize = CharSize;

            //Text Style
            Shape.Style = TextStyle;

            //Displayed String
            Shape.DisplayedString = String;

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
