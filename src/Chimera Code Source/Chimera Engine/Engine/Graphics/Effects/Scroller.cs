#region Using Statement
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace Chimera.Graphics.Effects
{
    /// <summary>
    /// This Class Allow You To Scroll Some Graphics Components
    /// </summary>
    public class Scroller : Chimera.Helpers.Interface.IUpdateable
    {
        #region Fields
        private Rectangle reclimite;
        private Graphics.Image img;
        private Graphics.TextWriter txt;

        private bool enable;
        private Vector2 speed;
        #endregion
        #region Properties
       /// <summary>
       /// Set Or Get Speed
       /// </summary>
        public Vector2 Speed
        {
            get { return speed; }
            set {speed=value ;}
        }
       /// <summary>
       /// Enable Or Disable The Effect
       /// </summary>
        public bool Enable
        {
            get { return enable;}
            set {enable=value ;}
        }
        #endregion
        #region Constructor
       /// <summary>
       /// The Default Constructor
       /// </summary>
        public Scroller() {}
        #endregion
        #region Initialize

       /// <summary>
       /// Initialize The Effect
       /// </summary>
       /// <param name="graphics">XNA GraphicsDevice Reference</param>
       /// <param name="img">The Image To Scrool</param>
        public void Initialize(GraphicsDeviceManager graphics, Graphics.Image img)
        {
            enable = true;
            reclimite = new Rectangle(graphics.GraphicsDevice.Viewport.X, graphics.GraphicsDevice.Viewport.Y, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);
            this.img = img;
            txt = null;
            speed = new Vector2(1f);
        }
        /// <summary>
        /// Initialize The Effect
        /// </summary>
        /// <param name="rec">The Rectangle Where The Effect Will Happen</param>
        /// <param name="img">The Image To Scroll</param>
        public void Initialize(Rectangle rec, Graphics.Image img)
        {
            enable = true;
            reclimite = rec;
            this.img = img;
            txt = null;
            speed = new Vector2(1f);
        }
        /// <summary>
        /// Initialize The Effect
        /// </summary>
        /// <param name="graphics">XNA GraphicsDevice Reference</param>
        /// <param name="txt">The Text To Scroll</param>
        public void Initialize(GraphicsDeviceManager graphics, Graphics.TextWriter txt)
        {
            enable = true;
            reclimite = new Rectangle(graphics.GraphicsDevice.Viewport.X, graphics.GraphicsDevice.Viewport.Y, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);
            this.txt = txt;
            this.img = null;
            speed = new Vector2(1f);
        }
        /// <summary>
        /// Initialize The Effect
        /// </summary>
        /// <param name="rec">The Rectangle Where The Effect Will Happen</param>
        /// <param name="txt">The Text To Scroll</param>
        public void Initialize(Rectangle rec, Graphics.TextWriter txt)
        {
            enable = true;
            reclimite = rec;
            this.txt = txt;
            this.img = null;
            speed = new Vector2(1f);
        }


        #endregion
        #region Main Functions
       /// <summary>
       /// The Update Function ,Must Be Called Every Frame
       /// </summary>
        public void Update()
        {
            if (img != null)
            {
                img.Position += speed;
                if ( img.Position.X >= reclimite.Width) img.Position = new Vector2(reclimite.X,img.Position.Y);
                else if (img.Position.X+img.Size.X <= reclimite.X) img.Position = new Vector2(reclimite.Width, img.Position.Y);
                if (img.Position.Y >= reclimite.Height) img.Position = new Vector2(img.Position.X, reclimite.Y);
                else if (img.Position.Y + img.Size.Y <= reclimite.Y) img.Position = new Vector2(img.Position.X, reclimite.Height);
            }
            else if (txt != null)
            {
                txt.Position += speed;
                if (txt.Position.X >= reclimite.Width) txt.Position = new Vector2(reclimite.X, txt.Position.Y);
                else if (txt.Position.X + txt.Text.Length <= reclimite.X) txt.Position = new Vector2(reclimite.Width, txt.Position.Y);
                if (txt.Position.Y >= reclimite.Height) txt.Position = new Vector2(txt.Position.X, reclimite.Y);
                else if (txt.Position.Y + 1 <= reclimite.Y) txt.Position = new Vector2(txt.Position.X, reclimite.Height);
            }
        }
       /// <summary>
       /// Reset The Effect
       /// </summary>
        public void Reset()
        {
            enable = true;
        }
        #endregion
    }
}
