using System;
using System.IO;
using SFML.Graphics;
using PIX_UI.Logger;

namespace PIX_UI.Assets
{
    class FontAsset
    {
        public string Name;
        public Font Font;

        public FontAsset(string file, string name)
        {
            Name = name;
            try
            {
                Font = new Font(Path.Combine(App.ResourceFolder, App.FontFolder, file));
                App.Log.Print("Font: " + file + " Loaded as: " + name, LoggerType.ASSET);
            }
            catch (Exception e)
            {
                App.Log.Print("Failed to load Font: " + file, LoggerType.ASSET);
                App.Log.Print(e.ToString(), LoggerType.ASSET);
                Font = App.DefaultFont;
            }
        }
    }
}
