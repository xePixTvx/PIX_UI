using System;
using System.Collections.Generic;
using System.Text;

using PIX_UI;
using SFML.Window;

namespace PIX_UI_TEST
{
    class TestApp : App
    {
        public TestApp(string ResourceFolderName, string ConfigFileName, string WindowTitle, uint WindowWidth, uint WindowHeight) : base(ResourceFolderName, ConfigFileName, WindowTitle, WindowWidth, WindowHeight)
        {
        }

        protected override void AppUpdate()
        {
        }

        protected override void OnAppClosing()
        {
        }

        protected override void onKeyReleased(object sender, KeyEventArgs e)
        {
            if(e.Code == Keyboard.Key.A)
            {
            }
        }
    }
}
