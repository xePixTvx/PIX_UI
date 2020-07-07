using System.IO;
using System.Collections.Generic;
using SFML.Graphics;
using PIX_UI.Logger;

namespace PIX_UI.Assets
{
    public class AssetManager
    {
        List<FontAsset> FontList = new List<FontAsset>();
        List<TextureAsset> TextureList = new List<TextureAsset>();

        public AssetManager()
        {
            if(!Directory.Exists(Path.Combine(App.ResourceFolder,App.FontFolder)))
            {
                Directory.CreateDirectory(Path.Combine(App.ResourceFolder, App.FontFolder));
            }
            if (!Directory.Exists(Path.Combine(App.ResourceFolder, App.TextureFolder)))
            {
                Directory.CreateDirectory(Path.Combine(App.ResourceFolder, App.TextureFolder));
            }
            FontList.Clear();
            TextureList.Clear();
        }

        //Load Asset
        public void Load(AssetType Type, string File, string Name)
        {
            if (Type == AssetType.Font)
            {
                FontList.Add(new FontAsset(File, Name));
            }
            else if (Type == AssetType.Texture)
            {
                TextureList.Add(new TextureAsset(File, Name));
            }
            else
            {
                App.Log.Print("Tried to load a not supported AssetType!", LoggerType.ERROR);
            }
        }

        //Get Loaded Font Asset
        public Font GetFont(string Name)
        {
            if (Name == "default")
            {
                return App.DefaultFont;
            }
            foreach (FontAsset _font in FontList)
            {
                if (_font.Name == Name)
                {
                    return _font.Font;
                }
            }
            return App.DefaultFont;
        }

        //Get Loaded Texture Asset
        public Texture GetTexture(string Name)
        {
            if (Name == "default")
            {
                return App.DefaultTexture;
            }
            foreach (TextureAsset _texture in TextureList)
            {
                if (_texture.Name == Name)
                {
                    return _texture.Texture;
                }
            }
            return App.DefaultTexture;
        }
    }
}
