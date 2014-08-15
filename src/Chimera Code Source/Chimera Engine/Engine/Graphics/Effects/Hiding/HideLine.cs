#region Using Statement
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Chimera.Graphics.Effects;
#endregion
namespace Chimera.Graphics.Effects.Hiding
{
    /// <summary>
    /// This Class Allow You Using HidingLine Effect
    /// </summary>
    public class HidingLine : Helpers.Interface.IDrawable, Helpers.Interface.IDrawables
    {
        #region Fields & Properties
        /// <summary>
        /// Image On What the effect will be applied
        /// </summary>
        public Image Image
        {
            get { return hiding.Image; }
            set { hiding.Image = value; }
        }
        /// <summary>
        /// Effect Position
        /// </summary>
        public Vector2 Position
        {
            get { return hiding.Position; }
            set { hiding.Position = value; }
        }
        /// <summary>
        /// Enable Or Disable The Effect
        /// </summary>
        public bool Enable
        {
            get { return hiding.Enable; }
            set { hiding.Enable = value; }
        }
        /// <summary>
        /// Effect Time Interval betwen each animation : represented per milisecond
        /// </summary>
        public int Time
        {
            get { return hiding.Time; }
            set { hiding.Time = value; }
        }

        private GraphicsDeviceManager graphics;
        private Hiding hiding;
        #endregion

        #region Constructor
        /// <summary>
        /// A Constructor
        /// </summary>
        /// <param name="graphics">XNA GraphicsDeviceManager</param>
        public HidingLine(GraphicsDeviceManager graphics)
        {
            this.graphics = graphics;
            hiding = new Hiding(graphics);
        }
        /// <summary>
        /// A Constructor
        /// </summary>
        /// <param name="graphics">XNA GraphicsDeviceManager</param>
        /// <param name="Image">Image On What The Effect Will Be Applied</param>
        public HidingLine(GraphicsDeviceManager graphics, Graphics.Image Image)
        {
            this.graphics = graphics;
            hiding = new Hiding(graphics, Image);
        }
        #endregion
        #region Main Functions
        /// <summary>
        /// Initialize The Effect
        /// </summary>
        /// <param name="Type">Line Type</param>
        /// <param name="Size">Image Size</param>
        /// <param name="direction">Effect Direction</param>
        public void Initialize(Enumeration.ELine Type, float Size,Enumeration.EDirection direction)
        {
            if (Type == Chimera.Graphics.Enumeration.ELine.Horizontal)
                hiding.Initialize(new Vector2(graphics.GraphicsDevice.Viewport.Width, Size),direction);
            else if (Type == Chimera.Graphics.Enumeration.ELine.Vertical)
                hiding.Initialize(new Vector2(Size, graphics.GraphicsDevice.Viewport.Height),direction);
        }

        /// <summary>
        /// Update The Effect
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        public void Update(GameTime gameTime)
        {
            hiding.Update(gameTime);
        }
        /// <summary>
        /// Draw The Effect
        /// </summary>
        public void Draw()
        {
            hiding.Draw();
        }
        /// <summary>
        /// Optimize Draw Method
        /// </summary>
        public void OptimizedDraw()
        {
            hiding.OptimizedDraw();
        }
        /// <summary>
        /// Draw Without Stretch The Effect
        /// </summary>
        public void DefaultDraw()
        {
            hiding.DefaultDraw();
        }
        /// <summary>
        /// Optimize DefaultDraw Method
        /// </summary>
        public void OptimizedDefaultDraw()
        {
            hiding.OptimizedDefaultDraw();
        }
        /// <summary>
        /// Draw With Stretch The Effect
        /// </summary>
        public void StretchDraw()
        {
            hiding.StretchDraw();
        }
        /// <summary>
        /// Optimize StretchDraw Method
        /// </summary>
        public void OptimizedStretchDraw()
        {
            hiding.OptimizedStretchDraw();
        }
        /// <summary>
        /// Reset The Effect
        /// </summary>
        public void Reset()
        {
            hiding.Reset();
        }
        #endregion

    }
}
