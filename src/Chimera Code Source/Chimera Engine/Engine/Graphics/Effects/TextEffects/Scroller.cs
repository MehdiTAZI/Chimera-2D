using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Chimera.Graphics.Effects.TextEffects
{
    /// <summary>
    /// This Class Allow You To Scroll Text Graphic Component
    /// </summary>
    public class Scroller : Chimera.Helpers.Interface.IUpdateable
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public Scroller()
        {
            scr = new Chimera.Graphics.Effects.Scroller();
        }
        #region Fields
        private Graphics.TextWriter txt;
        private Graphics.Effects.Scroller scr;
        #endregion
        #region Properties
        /// <summary>
        /// Set Or Get Speed
        /// </summary>
        public Vector2 Speed
        {
            get { return this.scr.Speed; }
            set { this.scr.Speed = value; }
        }
        /// <summary>
        /// Enable Or Disable The Effect
        /// </summary>
        public bool Enable
        {
            get { return this.scr.Enable ; }
            set { this.scr.Enable = value; }
        }
        #endregion
        #region Initialize
        /// <summary>
        /// Initialize The Effect
        /// </summary>
        /// <param name="graphics">XNA GraphicsDevice Reference</param>
        /// <param name="txt">The Text To Scroll</param>
        public void Initialize(GraphicsDeviceManager graphic, TextWriter txt)
        {
            scr.Initialize(graphic, txt);
            this.txt = txt;
        }
        /// <summary>
        /// Initialize The Effect
        /// </summary>
        /// <param name="rec">The Rectangle Where The Effect Will Happen</param>
        /// <param name="txt">The Text To Scroll</param>
        public void Initialize(Rectangle rec, TextWriter txt)
        {
            scr.Initialize(rec, txt);
            this.txt = txt;
        }
        #endregion
        #region Main Functions
        /// <summary>
        /// The Update Function ,Must Be Called Every Frame
        /// </summary>
        public void Update()
        {
            scr.Update();
        }
        /// <summary>
        /// Reset The Effect
        /// </summary>
        public void Reset()
        {
            scr.Reset();
        }
        #endregion

    }
}
