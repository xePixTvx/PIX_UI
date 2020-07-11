using SFML.System;
using SFML.Graphics;
using PIX_UI.Graphics;

namespace PIX_UI.Utilities
{
    public class Position_Utils
    {
        public static Vector2f GetAlignPosition(Alignment Align)
        {
            uint win_width = App.WindowSize.Width;
            uint win_height = App.WindowSize.Height;
            string[] _Align = Align.ToString().Split('_');
            float x = (_Align[0] == "CENTER") ? (win_width / 2) : (_Align[0] == "RIGHT") ? win_width : 0;
            float y = (_Align[1] == "CENTER") ? (win_height / 2) : (_Align[1] == "BOTTOM") ? win_height : 0;
            return new Vector2f(x, y);
        }


        public static Vector2f GetAlignOrigin(Alignment Align, RectangleShape Shape)
        {
            string[] _Align = Align.ToString().Split('_');
            float x = (_Align[0] == "CENTER") ? (Shape.Size.X * (float)0.5) : (_Align[0] == "RIGHT") ? Shape.Size.X : 0;
            float y = (_Align[1] == "CENTER") ? (Shape.Size.Y * (float)0.5) : (_Align[1] == "BOTTOM") ? Shape.Size.Y : 0;
            return new Vector2f(x, y);
        }

        public static Vector2f GetAlignOrigin(Alignment Align, CircleShape Shape)
        {
            string[] _Align = Align.ToString().Split('_');
            float x = (_Align[0] == "CENTER") ? Shape.Radius : (_Align[0] == "RIGHT") ? (Shape.Radius * 2) : 0;
            float y = (_Align[1] == "CENTER") ? Shape.Radius : (_Align[1] == "BOTTOM") ? (Shape.Radius * 2) : 0;
            return new Vector2f(x, y);
        }
    }
}
