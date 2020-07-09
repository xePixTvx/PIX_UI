using SFML.Window;
using PIX_UI;
using PIX_UI.Graphics;

namespace PIX_UI_TEST
{
    class TestApp : App
    {
        private Rect TestRect;

        public TestApp(string ResourceFolderName, string ConfigFileName, string WindowTitle, uint WindowWidth, uint WindowHeight) : base(ResourceFolderName, ConfigFileName, WindowTitle, WindowWidth, WindowHeight)
        {
            TestRect = new Rect();
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
            }
        }
    }
}
