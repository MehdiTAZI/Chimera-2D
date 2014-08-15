#region Using Statement
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace Chimera.Graphics.Effects
{
    /// <summary>
    /// This Class Allow You To Scale Your Components
    /// </summary>
    public class Scaling : Chimera.Helpers.Interface.IUpdateable, Helpers.Interface.IDrawable, Helpers.Interface.IDrawables
    {
        #region Fields
        private Image image;
        private TextWriter text;
        private bool enable;
        private GraphicsDeviceManager graphics;
        private Enumeration.EZOOM type;
        private float speed;
        private Vector2 maxscal,minscal;
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
            get { return type;}
            set { type = value;}
        }

        /// <summary>
        /// Get Or Set The Image To Be Scaled
        /// </summary>
        public Image Image
        {
            get { return image;}
            set { image = value;}      
        }
        /// <summary>
        /// Get Or Set The Text To Be Scaled
        /// </summary>
        public TextWriter TextWriter
        {
            get { return text; }
            set {text = value; }
        }
        /// <summary>
        /// Get Or Set The Effect Speed
        /// </summary>
        public float Speed
        {
            get { return speed*100; }
            set { speed = value/100; }
        }
        #endregion
        #region Helpers Functions
        private Vector2 calculatepos()
        {
            float x=0,y=0;
            if (image != null)
            {
                x = image.Size.X * speed * 0.5f;
                y = image.Size.Y * speed * 0.5f;
            }
            else if (text != null)
            {
                x = text.Text.Length * speed * 0.5f;
                y = speed * 0.5f;
            }
            return new Vector2(x, y);
        }
        private void calculcenter()
        {
            float x, y;
            float xx=0, yy=0;
            if (image != null)
            {
                xx = image.Size.X;
                yy = image.Size.Y;
            }
            else if (text != null)
            {
                xx = text.Text.Length;
                yy = 1;
            }
            if (type == Chimera.Graphics.Enumeration.EZOOM.IN)
            {
                x = xx* minscal.X * 0.5f;
                y = yy * minscal.Y * 0.5f;
            }
            else
            {
                x = (xx * maxscal.X * 0.5f);
                y = (yy * maxscal.Y * 0.5f);
            }
            Vector2 pos = new Vector2((graphics.GraphicsDevice.Viewport.Width / 2) - x, (graphics.GraphicsDevice.Viewport.Height / 2) - y);
            if (image != null) image.Position = pos;
            else if (text != null) text.Position = pos;
        }
        private void eback()
        {
            if (type == Enumeration.EZOOM.OUT)
            {
                image.Scale -= new Vector2(speed);
                image.Position += calculatepos();
                if ((image.Scale.X <= minscal.X) && image.Scale.Y <= minscal.Y) enable = false;
            }
            else
            {
                image.Scale += new Vector2(speed);
                image.Position -= calculatepos();
                if ((image.Scale.X >= maxscal.X) && image.Scale.Y >= maxscal.Y) enable = false;
            }
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
        /// A Constuctor
        /// </summary>
        /// <param name="graphics">XNA GraphicsDevice Reference</param>
        public Scaling(GraphicsDeviceManager graphics)
        {
            this.graphics = graphics;
        }
        /// <summary>
        /// A Constructor
        /// </summary>
        /// <param name="graphics">XNA GraphicsDevice Reference</param>
        /// <param name="Image">The Image To Be Scalled</param>
        public Scaling(GraphicsDeviceManager graphics,Image Image)
        {
            Initialize(graphics, image, Chimera.Graphics.Enumeration.EZOOM.IN);
        }
        /// <summary>
        /// A Constructor
        /// </summary>
        /// <param name="graphics">XNA GraphicsDevice Reference</param>
        /// <param name="text">The Text To Be Scalled</param>
        public Scaling(GraphicsDeviceManager graphics, TextWriter text)
        {
            Initialize(graphics, text, Chimera.Graphics.Enumeration.EZOOM.IN);
        }
        #endregion
        #region Main Functions
        /// <summary>
        /// Initialize The Effect
        /// </summary>
        /// <param name="graphics">XNA GraphicsDevice Reference</param>
        /// <param name="Image">The Image To Be Scalled</param>
        /// <param name="Type">Effect Type</param>
        public void Initialize(GraphicsDeviceManager graphics, Image Image, Enumeration.EZOOM Type)
        {
            this.image = Image;
            this.graphics = graphics;
            text = null;
            this.type = Type;
            maxscal = new Vector2(1);
            minscal = new Vector2(0);
            speed = 0.01f;
            Reset();
        }
        /// <summary>
        /// Initialize The Effect
        /// </summary>
        /// <param name="graphics">XNA GraphicsDevice Reference</param>
        /// <param name="text">The Text To Be Scalled</param>
        /// <param name="Type">Effect Type</param>
        public void Initialize(GraphicsDeviceManager graphics, TextWriter text, Enumeration.EZOOM Type)
        {
            this.image = null;
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
        public void RestoreDefaultSize()
        {
            float xx = 0, yy = 0;
            if (image != null)
            {
                xx = image.Size.X;
                yy = image.Size.Y;
            }
            else if (text != null)
            {
                xx = text.Text.Length;
                yy = 1;
            }
            float x = (xx  * 0.5f);
            float y = (yy * 0.5f);
            Vector2 pos = new Vector2((graphics.GraphicsDevice.Viewport.Width / 2) - x, (graphics.GraphicsDevice.Viewport.Height / 2) - y);
            if (image != null)
            {
                image.Position = pos;
                image.Scale = new Vector2(1f);
            }
            else if (text != null)
            {
                text.Position = pos;
                text.Scale = new Vector2(1f);
            }
        }
        /// <summary>
        /// Reset The Effect
        /// </summary>

        public void Reset()
        {
            
            enable = true;
            if (type == Enumeration.EZOOM.OUT)
            {
                if (image != null) image.Scale = maxscal;
                else if (text != null) text.Scale = maxscal;
            }
            else
            {
                if (image != null) image.Scale = minscal;
                else if (text != null) text.Scale = minscal;
            }
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
        /// UpDate The Effect
        /// </summary>
        public void Update()
        {
            if (enable)
            {

                if (image != null)
                    eback();
                else if (text != null)
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
                if (image != null) image.Draw();
                else if (text != null) text.Draw();
            }
        }
        /// <summary>
        /// Draw The Effect Without Stretching The Image
        /// </summary>
        public void DefaultDraw()
        {
            if (enable)
            {
                if (image != null) image.DefaultDraw();
                else if (text != null) text.Draw();
            }
        }

        /// <summary>
        /// Draw The Effect With Stretching The Image
        /// </summary>
        public void StretchDraw()
        {
            if (enable)
            {
                if (image != null) image.StretchDraw();
                else if (text != null) text.Draw();
            }
        }
        #endregion
    }
}
