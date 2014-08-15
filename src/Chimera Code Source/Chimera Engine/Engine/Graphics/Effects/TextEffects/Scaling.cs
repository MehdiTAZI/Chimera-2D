#region Using Statement
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace Chimera.Graphics.Effects.TextEffects
{
    /// <summary>
    /// This Class Allow You To Scale Your Text Component
    /// </summary>
    public class Scaling : Chimera.Helpers.Interface.IUpdateable, Helpers.Interface.IDrawable 
    {
        #region Fields
        private TextWriter text;
        private bool enable;
        private GraphicsDeviceManager graphics;
        private Enumeration.EZOOM type;
        private float speed;
        private Vector2 maxscal, minscal;
        #endregion
        #region Properties
        /// <summary>
        /// Get Or Set The Minimal Scalling Value
        /// </summary>
        public Vector2 MinScale
        {
            get { return minscal; }
            set { minscal = value; }
        }
        /// <summary>
        /// Get Or Set The Maximal Scalling Value
        /// </summary>
        public Vector2 MaxScale
        {
            get { return maxscal; }
            set { maxscal = value; }
        }
        /// <summary>
        /// Enable Or Disable The Effect
        /// </summary>
        public bool Enable
        {
            get { return enable; }
            set { enable = value; }
        }
        /// <summary>
        /// Get Or Set The Zoom Type
        /// </summary>
        public Enumeration.EZOOM Type
        {
            get { return type; }
            set { type = value; }
        }
        /// <summary>
        /// Get Or Set The Text To Be Scaled
        /// </summary>
        public TextWriter TextWriter
        {
            get { return text; }
            set { text = value; }
        }
        /// <summary>
        /// Get Or Set The Effect Speed
        /// </summary>
        public float Speed
        {
            get { return speed * 100; }
            set { speed = value / 100; }
        }
        #endregion
        #region Helper Functions
        private Vector2 calculatepos()
        {
              float  x = text.Text.Length * speed * 0.5f;
              float y = speed * 0.5f;
           
            return new Vector2(x, y);
        }
        private void calculcenter()
        {
            float x, y;

            float  xx = text.Text.Length;
            float yy = 1;

            if (type == Chimera.Graphics.Enumeration.EZOOM.IN)
            {
                x = xx * minscal.X * 0.5f;
                y = yy * minscal.Y * 0.5f;
            }
            else
            {
                x = (xx * maxscal.X * 0.5f);
                y = (yy * maxscal.Y * 0.5f);
            }

            Vector2 pos = new Vector2((graphics.GraphicsDevice.Viewport.Width / 2) - x, (graphics.GraphicsDevice.Viewport.Height / 2) - y);
            text.Position = pos;
        }
        private void etext()
        {
            if (type == Enumeration.EZOOM.OUT)
            {
                text.Scale -= new Vector2(speed);
                text.Position += calculatepos();
                if ((text.Scale.X <= minscal.X) && text.Scale.Y <= minscal.Y) enable = false;
            }
            else
            {
                text.Scale += new Vector2(speed);
                text.Position -= calculatepos();
                if ((text.Scale.X >= maxscal.X) && text.Scale.Y >= maxscal.Y) enable = false;
            }
        }
        #endregion
        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="graphics">XNA GraphicsManager Reference</param>
        public Scaling(GraphicsDeviceManager graphics)
        {
            Initialize(graphics, Chimera.Graphics.Enumeration.EZOOM.OUT);
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="graphics">XNA GraphicsManager Reference</param>
        /// <param name="text">Text On What The Effect Will Be Applied</param>
        public Scaling(GraphicsDeviceManager graphics, TextWriter text)
        {
            Initialize(graphics, text, Chimera.Graphics.Enumeration.EZOOM.OUT);
        }
        
        #endregion
        #region Main Functions
        /// <summary>
        /// Initialize The Effect
        /// </summary>
        /// <param name="graphics">XNA GraphicsManager Reference</param>
        /// <param name="Type">Effect Type</param>
        public void Initialize(GraphicsDeviceManager graphics, Enumeration.EZOOM Type)
        {
            this.graphics = graphics;
            this.type = Type;
            maxscal = new Vector2(1);
            minscal = new Vector2(0);
            speed = 0.01f;
            Reset();
        }
        /// <summary>
        /// Initialize The Effect
        /// </summary>
        /// <param name="graphics">XNA GraphicsManager Reference</param>
        /// <param name="text">Text On What The Effect Will Be Applied</param>
        /// <param name="Type">Effect Type</param>
        public void Initialize(GraphicsDeviceManager graphics, TextWriter text, Enumeration.EZOOM Type)
        {
            this.graphics = graphics;
            this.text = text;
            this.type = Type;
            maxscal = new Vector2(1);
            minscal = new Vector2(0);
            speed = 0.01f;
            Reset();
        }

        /// <summary>
        /// Restore The Default Size
        /// </summary>
        public void SetDefaultSize()
        {
            float xx = 0, yy = 0;

                xx = text.Text.Length;
                yy = 1;
  
            float x = (xx * 0.5f);
            float y = (yy * 0.5f);
            Vector2 pos = new Vector2((graphics.GraphicsDevice.Viewport.Width / 2) - x, (graphics.GraphicsDevice.Viewport.Height / 2) - y);

                text.Position = pos;
                text.Scale = new Vector2(1f);
        }
        /// <summary>
        /// Reset The Effect
        /// </summary>
        public void Reset()
        {

            enable = true;
            if (type == Enumeration.EZOOM.OUT)
            text.Scale = maxscal;
            else
              text.Scale = minscal;
        }
        /// <summary>
        /// Reset The Effect An Put The Image In Center
        /// </summary>
        public void ResetInCenter()
        {
            Reset();
            calculcenter();
        }

        /// <summary>
        /// Update The Effect
        /// </summary>
        public void Update()
        {
            if (enable)
            {
                    etext();

            }
        }
        /// <summary>
        /// Draw The Effect
        /// </summary>
        public void Draw()
        {
            if (enable)
            {
                text.Draw();
            }
        }
        #endregion
    }
}
