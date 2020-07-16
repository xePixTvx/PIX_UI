using SFML.Window;
using PIX_UI;
using PIX_UI.Graphics;
using PIX_UI.Graphics.Primitives;
using PIX_UI.Graphics.Controls;

namespace PIX_UI_TEST
{
    class TestApp : App
    {
        public TestApp(string ResourceFolderName, string ConfigFileName, string WindowTitle, uint WindowWidth, uint WindowHeight) : base(ResourceFolderName, ConfigFileName, WindowTitle, WindowWidth, WindowHeight)
        {
            new Rect("TestRect1", Alignment.CENTER_CENTER, Alignment.CENTER_CENTER, 300, 100, 0, 0, new SFML.Graphics.Color(255, 0, 0, 255));
            new Circle("TestCircle1", Alignment.CENTER_CENTER, Alignment.CENTER_CENTER, 50, -240, 0, new SFML.Graphics.Color(0, 255, 0, 255));
            new Triangle("TestTriangle1", Alignment.CENTER_CENTER, Alignment.CENTER_CENTER, 50, 240, 0, new SFML.Graphics.Color(0, 0, 255, 255));
            new Line("TestLine1", PIX_UI.Utilities.Position_Utils.GetAlignPosition(Alignment.LEFT_TOP), PIX_UI.Utilities.Position_Utils.GetAlignPosition(Alignment.RIGHT_BOTTOM), SFML.Graphics.Color.Cyan, SFML.Graphics.Color.Magenta);
            new SimpleText("TestText1", Alignment.CENTER_TOP, Alignment.CENTER_TOP, 0, 20, new SFML.Graphics.Color(255, 255, 255, 255), "default", 20, SFML.Graphics.Text.Styles.Regular, "Test Text");
            new SimpleSprite("TestSprite1", Alignment.RIGHT_TOP, Alignment.RIGHT_TOP, 0, 0, "default");

            new SpriteButton("TestSpriteButton1", Alignment.LEFT_BOTTOM, Alignment.LEFT_BOTTOM, 0, 0, "default");
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
                t.String += "fkhsdkhfsdhjasdhas\n";*/
            }
        }
    }
}
