#region Using Statement
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace Chimera.Graphics.Effects.EarthQuake
{
    /// <summary>
    /// This Class Allow Using The EarthQuake Effect On The Screen
    /// </summary>
    ///<remarks>Call Update Method  If The Effect Is Based On ViewPort,otherwise Call Draw Method</remarks>
    public class Screen : Chimera.Helpers.Interface.IUpdateable, Helpers.Interface.IDrawable 
    {
        #region Fields
        private Vector2 min, max;
        private SpriteBatch _sprite;
        private Viewport viewport;
        private Viewport saveviewport;
        private bool enable;
        private GraphicsDeviceManager _graphics;        
        #endregion

        #region Properties

        /// <summary>
        /// Set Or Get Maximal Power Of The EarthQuake Effect
        /// </summary>
        public Vector2 Max
        {
            get { return this.max; }
            set { this.max = value; }
        }

        /// <summary>
        /// Set Or Get Minimal Power Of The EarthQuake Effect
        /// </summary>
        public Vector2 Min
        {
            get { return this.min; }
            set { this.min = value; }
        }
        /// <summary>
        /// Enable Or Disable The Effect
        /// </summary>
        public bool Enable
        {
            get { return this.enable; }
            set { this.enable = value; }
        }
        private bool basedonViewPort=true;
        /// <summary>
        /// If The Effect Is Based On
        /// </summary>
        public bool BasedOnViewPort
        {
            get {return basedonViewPort ;}
            set {basedonViewPort =value;}
        }

        #endregion
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public Screen()
        {
  
        }
        #endregion
        #region Main Function
        /// <summary>
        /// Initialize The Effect
        /// </summary>
        /// <param name="Sprite">XNA SpriteBatch Reference</param>
        public void Initialize(SpriteBatch Sprite)
        {
            this._sprite = Sprite;
            this.Reset();
        }
        /// <summary>
        /// Initialize The Effect
        /// </summary>
        /// <param name="Sprite">XNA SpriteBatch Reference</param>
        /// <param name="graphics"></param>
        public void Initialize(SpriteBatch Sprite,GraphicsDeviceManager graphics)
        {
            this._sprite = Sprite;
            this.Reset();
            this.Set(graphics);
        }
        /// <summary>
        /// Set Graphics
        /// </summary>
        /// <param name="graphics">XNA GraphicsDeviceManager Reference</param>
        public void Set(GraphicsDeviceManager graphics)
        {
            _graphics = graphics;
            this.viewport = graphics.GraphicsDevice.Viewport;
            this.saveviewport = this.viewport;
        }
        /// <summary>
        /// Set Effect Parameters
        /// </summary>
        /// <param name="min">Minimal Power EarthQuake Value</param>
        /// <param name="max">Maximal Power EarthQuake Value</param>
        public void SetEffectParameters(Vector2 min, Vector2 max)
        {
            this.min = min;
            this.max = max;
        }
        /// <summary>
        /// Restore The Default ViewPort 
        /// </summary>
        public void RestoreViewPort()
        {
            if(!_graphics.GraphicsDevice.Viewport.Equals(saveviewport))
            {
            _graphics.GraphicsDevice.Viewport = saveviewport;
            _graphics.ApplyChanges();
            }
        }
        /// <summary>
        /// Reset The Effect
        /// </summary>
        public void Reset()
        {
            this.enable = true;
        }
        /// <summary>
        /// Update The Effect(ViewPort) Call If The Effect Is Based On ViewPort
        /// </summary>
        public void Update()
        {
            if (enable)
                if (this.basedonViewPort)
                {
                    viewport.X = Helpers.Randomize.GenerateInteger((int)min.X, (int)max.X);
                    viewport.Y = Helpers.Randomize.GenerateInteger((int)min.Y, (int)max.Y);
                    _graphics.GraphicsDevice.Viewport = viewport;
                    _graphics.ApplyChanges();
                }
                
        }
        /// <summary>
        /// Draw The Effect(Screen Image)If The Effect Is Not Based On ViewPort
        /// </summary>
        public void Draw()
        {
            if (enable)
                if (!this.basedonViewPort)
                Graphics.Screen.DrawScreen(new Vector2(Helpers.Randomize.GenerateInteger((int)min.X, (int)max.X), Helpers.Randomize.GenerateInteger((int)min.Y, (int)max.Y)), _sprite);
        }

        #endregion

    }
}
