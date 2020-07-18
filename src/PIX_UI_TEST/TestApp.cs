using SFML.Window;
using SFML.System;
using PIX_UI;
using PIX_UI.Graphics;
using PIX_UI.Graphics.Primitives;
using PIX_UI.Graphics.Controls;
using PIX_UI.Utilities;

namespace PIX_UI_TEST
{
    class TestApp : App
    {
        public TestApp(string ResourceFolderName, string ConfigFileName, string WindowTitle, uint WindowWidth, uint WindowHeight) : base(ResourceFolderName, ConfigFileName, WindowTitle, WindowWidth, WindowHeight)
        {
            Vector2f POS_CENTER_CENTER = Position_Utils.GetPositionOnScreen(Alignment.CENTER_CENTER);
            Vector2f POS_CENTER_TOP = Position_Utils.GetPositionOnScreen(Alignment.CENTER_TOP);
            //Vector2f POS_CENTER_BOTTOM = Position_Utils.GetPositionOnScreen(Alignment.CENTER_BOTTOM);
            Vector2f POS_LEFT_BOTTOM = Position_Utils.GetPositionOnScreen(Alignment.LEFT_BOTTOM);
            Vector2f POS_RIGHT_TOP = Position_Utils.GetPositionOnScreen(Alignment.RIGHT_TOP);


            new Rect("TestRect1", Alignment.CENTER_CENTER, 300, 100, POS_CENTER_CENTER.X, POS_CENTER_CENTER.Y, new SFML.Graphics.Color(255, 0, 0, 255));
            new Circle("TestCircle1", Alignment.CENTER_CENTER, 50, POS_CENTER_CENTER.X - 240, POS_CENTER_CENTER.Y, new SFML.Graphics.Color(0, 255, 0, 255));
            new Triangle("TestTriangle1", Alignment.CENTER_CENTER, 50, POS_CENTER_CENTER.X + 240, POS_CENTER_CENTER.Y, new SFML.Graphics.Color(0, 0, 255, 255));
            new Line("TestLine1", Position_Utils.GetPositionOnScreen(Alignment.LEFT_TOP), Position_Utils.GetPositionOnScreen(Alignment.RIGHT_BOTTOM), SFML.Graphics.Color.Cyan, SFML.Graphics.Color.Magenta);
            new SimpleText("TestText1", Alignment.CENTER_TOP, POS_CENTER_TOP.X, POS_CENTER_TOP.Y + 20, new SFML.Graphics.Color(255, 255, 255, 255), "default", 20, SFML.Graphics.Text.Styles.Regular, "Test Text");
            new SimpleSprite("TestSprite1", Alignment.RIGHT_TOP, POS_RIGHT_TOP.X, POS_RIGHT_TOP.Y, "default");

            new SpriteButton("TestSpriteButton1", Alignment.LEFT_BOTTOM, POS_LEFT_BOTTOM.X, POS_LEFT_BOTTOM.Y, "default");
            new TextButton("TestTextButton1", Alignment.LEFT_TOP, 200, 30, 0, 0, "default", 16, "Test Button 1", ButtonTextStyles.CENTER);
        }

        protected override void AppUpdate()
        {
        }

        protected override void OnAppClosing()
        {
        }

        protected override void OnKeyReleased(object sender, KeyEventArgs e)
        {
            if(e.Code == Keyboard.Key.Escape)
            {
                Exit();
            }
            if(e.Code == Keyboard.Key.A)
            {
                /*** TESTING ***/
                //GetElemByName("TestRect1")?.Destroy();
                //GetElemByName("TestCircle1").Position += new SFML.System.Vector2f(10, 0);
                /*SimpleText t = GetElemByName("TestText1") as SimpleText;
                t.String += "fkhsdkhfsdhjasdhas\n";
                TextButton tb = GetElemByName("TestTextButton1") as TextButton;
                tb.Origin_Alignment = Alignment.CENTER_CENTER;*/
            }
        }
    }
}
