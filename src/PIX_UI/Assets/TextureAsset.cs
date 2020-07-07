using System;
using System.IO;
using SFML.Graphics;
using PIX_UI.Logger;

namespace PIX_UI.Assets
{
    class TextureAsset
    {
        public string Name;
        public Texture Texture;

        public TextureAsset(string file, string name)
        {
            Name = name;
            try
            {
                Texture = new Texture(Path.Combine(App.ResourceFolder, App.TextureFolder, file));
                App.Log.Print("Texture: " + file + " Loaded as: " + name, LoggerType.ASSET);
            }
            catch (Exception e)
            {
                App.Log.Print("Failed to load Texture: " + file, LoggerType.ASSET);
                App.Log.Print(e.ToString(), LoggerType.ASSET);
                Texture = App.DefaultTexture;
            }
        }
    }
}
