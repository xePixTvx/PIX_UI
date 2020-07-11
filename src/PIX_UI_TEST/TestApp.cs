using SFML.Window;
using PIX_UI;
using PIX_UI.Graphics;
using PIX_UI.Graphics.Primitives;


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
        }

        protected override void AppUpdate()
        {
        }

        protected override void OnAppClosing()
        {
        }

        protected override void onKeyReleased(object sender, KeyEventArgs e)
        {
            if(e.Code == Keyboard.Key.Escape)
            {
                Exit();
            }
            if(e.Code == Keyboard.Key.A)
            {
                //GetElemByName("TestRect1")?.Destroy();
                //GetElemByName("TestCircle1").Position += new SFML.System.Vector2f(10, 0);
            }
        }
    }
}
