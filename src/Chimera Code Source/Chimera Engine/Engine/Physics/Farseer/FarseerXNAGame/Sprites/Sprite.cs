using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

using Chimera.Physics.Farseer.FarseerGames.FarseerXNAGame.Scene;

namespace Chimera.Physics.Farseer.FarseerGames.FarseerXNAGame.Sprites {
    public class Sprite : SceneObject, IRenderable, ILoadable  {
        private string _assetName;
        private Texture2D _texture2D;
        private Vector2 _origin;
        private float _layer = 0f;
        private Color _tint = Color.White;

        public Sprite(string textureAssetName) {
            _assetName = textureAssetName;
        }

        public Sprite(string textureAssetName, float layer) {
            _assetName = textureAssetName;
            _layer = layer;            
        }

        public Sprite(string textureAssetName, float layer, Color tint) {
            _assetName = textureAssetName;
            _layer = layer;
            _tint = tint;          
        }

        public Sprite(Sprite sprite) {
            _assetName = sprite._assetName;
            _layer = sprite.Layer;
            _tint = sprite.Tint;
            _texture2D = sprite._texture2D;
            _origin = sprite._origin;
        }

        public void LoadGraphicsContent(GraphicsDevice graphicsDevice, ContentManager contentManager) {
            _texture2D = contentManager.Load<Texture2D>(_assetName);
            _origin = new Vector2( _texture2D.Width /2, _texture2D.Height/2);
        }

        public void Render(GraphicsDevice graphicsDevice) {
            FarseerGame.FarseerSpriteBatch.Draw(_texture2D, ConvertUnits.ToPixels(Position),null, _tint, Orientation, _origin, 1f, SpriteEffects.None,_layer);
        }

        public float Layer {
            get { return _layer; }
            set { _layer = value; }
        }

        public Color Tint {
            get { return _tint; }
            set { _tint = value; }
        }

        public string AssetName{
            get{return _assetName;}
            set{_assetName = value;}
        }



    }
}
