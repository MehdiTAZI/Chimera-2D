//Mercury Particle Engine 2.0 Copyright (C) 2007 Matthew Davey

//This library is free software; you can redistribute it and/or modify it under the terms of
//the GNU Lesser General Public License as published by the Free Software Foundation; either
//version 2.1 of the License, or (at your option) any later version.

//This library is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
//without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
//See the GNU Lesser General Public License for more details.

//You should have received a copy of the GNU Lesser General Public License along with this
//library; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330,
//Boston, MA 02111-1307 USA 

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Chimera.Graphics.Effects.Particles.Engine
{
    public class ImageStrip
    {
        #region [ Private Fields ]

        private Game _game;
        private ContentManager _content;
        private string _asset;
        private bool _loaded;
        private Texture2D _texture;
        private Rectangle[] _source;
        private Vector2 _origin;

        #endregion

        #region [ Public Interface ]

        /// <summary>
        /// Returns an array of Microsoft.Xna.Framework.Rectangle objects defining the
        /// source rectangle of each frame in the strip.
        /// </summary>
        public Rectangle[] Source
        {
            get { return _source; }
        }

        /// <summary>
        /// Returns a Microsoft.Xna.Framework.Vector2 object defining the middle origin
        /// of each frame in the strip.
        /// </summary>
        public Vector2 Origin
        {
            get { return _origin; }
        }

        /// <summary>
        /// Returns true if the ImageStrips' texture asset has been loaded.
        /// </summary>
        public bool Loaded
        {
            get { return _loaded; }
        }

        /// <summary>
        /// Returns the Microsoft.Xna.Framework.Graphics.Texture2D object associated with
        /// the ImageStrip.
        /// </summary>
        public Texture2D Texture
        {
            get { return _texture; }
        }

        #endregion

        #region [ Constructors & Methods ]

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="game">A reference to your game object.</param>
        /// <param name="asset">The asset name as defined in the content pipeline.</param>
        /// <param name="width">The width in pixels of each frame in the strip.</param>
        /// <param name="height">The height in pixels of each frame in the strip.</param>
        /// <param name="frames">The total number of frames in the strip.</param>
        public ImageStrip(Game game, string asset, int width, int height, byte frames)
        {
            _game = game;
            _content = new ContentManager(_game.Services);
            _asset = asset;
            _loaded = false;
            _source = new Rectangle[frames];
            _origin = new Vector2((float)(width / 2), (float)(height / 2));

            IGraphicsDeviceService gds = (IGraphicsDeviceService)_game.Services.GetService(typeof(IGraphicsDeviceService));
            gds.DeviceCreated += new EventHandler(Load);
            gds.DeviceDisposing += new EventHandler(UnLoad);
            gds.DeviceReset += new EventHandler(Load);
            gds.DeviceResetting += new EventHandler(UnLoad);

            //Calculate a source rectangle for each frame in the image...
            for (byte i = 0; i < frames - 1; i++)
            {
                _source[i] = new Rectangle(i * width, 0, width, height);
            }
        }

        private void Load(object sender, EventArgs e)
        {
            if (!_loaded)
            {
                try
                {
                    _texture = _content.Load<Texture2D>(_asset);
                }
                catch (Exception ex)
                {
                    throw new Exception("Could not load asset!", ex);
                }

                _loaded = true;
            }
        }

        /// <summary>
        /// Forces the ImageStrip to load its resources. It is necessary to call this
        /// method if you create the ImageStrip after the graphics device has been created.
        /// </summary>
        public void ManualLoad()
        {
            Load(null, EventArgs.Empty);
        }

        private void UnLoad(object sender, EventArgs e)
        {
            if (_loaded)
            {
                _texture.Dispose();
                _content.Unload();
                _loaded = false;
            }
        }

        #endregion
    }
}
