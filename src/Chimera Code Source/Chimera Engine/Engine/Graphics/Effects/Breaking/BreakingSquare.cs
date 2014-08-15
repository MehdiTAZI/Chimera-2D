#region Using Statement
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Chimera.Graphics.Effects;
#endregion
namespace Chimera.Graphics.Effects.Breaking
{
    /// <summary>
    /// This Class Allow You Using Breaking Line Effect 
    /// </summary>
    public class BreakingSquare : Chimera.Helpers.Interface.IUpdateable, Helpers.Interface.IDrawable, Helpers.Interface.IDrawables
    {
        #region Fields & Properties
        /// <summary>
        /// Get Or Set The Image On What The Effect Will Be Applied
        /// </summary>
        public Image Image
        {
            get { return breaking.Image; }
            set { breaking.Image = value; }
        }
        /// <summary>
        /// Get Or Set The Effect Position
        /// </summary>
        public Vector2 Position
        {
            get { return breaking.Position; }
            set { breaking.Position = value; }
        }
        /// <summary>
        /// Get Or Set The Effect Speed Rotation
        /// </summary>
        public float SpeedRotation
        {
            get { return breaking.SpeedRotation ; }
            set { breaking.SpeedRotation = value; }
        }
        /// <summary>
        /// Get Or Set The Effect Speed
        /// </summary>
        public float Speed
        {
            get { return breaking.Speed; }
            set { breaking.Speed = value; }
        }
        /// <summary>
        /// Enable Or Disable The Effect
        /// </summary>
        public bool Enable
        {
            get { return breaking.Enable; }
            set { breaking.Enable = value; }
        }

        private GraphicsDeviceManager graphics;
        private Breaking breaking;
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="graphics">XNA GraphicsDeviceManager</param>
        public BreakingSquare(GraphicsDeviceManager graphics)
        {
            this.graphics = graphics;
            breaking = new Breaking(graphics);
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="graphics">XNA GraphicsDeviceManager</param>
        /// <param name="Image">The Image On What The Effect Will Be Applied</param>
        public BreakingSquare(GraphicsDeviceManager graphics, Graphics.Image Image)
        {
            this.graphics = graphics;
            breaking = new Breaking(graphics, Image);
        }
        #endregion
        #region Initilization
        /// <summary>
        /// Initalize Randomize Rotatio
        /// </summary>
        public void InitRandomRotation()
        {
            breaking.InitRandomRotation();
        }
        /// <summary>
        /// Initialize Randomize Rotation
        /// </summary>
        /// <param name="origin">Rotation Origin</param>
        public void InitRandomRotation(Enumeration.EImageOrigin origin)
        {
            breaking.InitRandomRotation(origin);
        }
        /// <summary>
        /// Initiaze The Effect
        /// </summary>
        /// <param name="Size">Set Size Of Each Image In The Collection Effect</param>
        public void Initialize(float Size)
        {
            breaking.Initialize(new Vector2(Size, Size));
        }
        /// <summary>
        /// Initialize Randomize Destionation Of Image Each Image
        /// </summary>
        /// <param name="start">The Start Vector</param>
        /// <param name="end">The Final Vector</param>
        public void InitRandomDest(Vector2 start, Vector2 end)
        {
            breaking.InitRandomDest(start, end);

        }
        #endregion
        #region Main Functions

        /// <summary>
        /// Update The Effect
        /// </summary>
        public void Update()
        {
            breaking.Update();
        }
        /// <summary>
        /// Draw The Effect
        /// </summary>
        public void Draw()
        {
            breaking.Draw();
        }
        /// <summary>
        /// Optimize Draw Method
        /// </summary>
        public void OptimizedDraw()
        {
            breaking.OptimizedDraw();
        }
        /// <summary>
        /// Draw Without Stretch The Effect
        /// </summary>
        public void DefaultDraw()
        {
            breaking.DefaultDraw();
        }
        /// <summary>
        /// Optimize DefaultDraw Method
        /// </summary>
        public void OptimizedDefaultDraw()
        {
            breaking.OptimizedDefaultDraw();
        }
        /// <summary>
        /// Draw With Stretch The Effect
        /// </summary>
        public void StretchDraw()
        {
            breaking.StretchDraw();
        }
        /// <summary>
        /// Optimize StrechDraw Method
        /// </summary>
        public void OptimizedStretchDraw()
        {
            breaking.OptimizedStretchDraw();
        }
        /// <summary>
        /// Reset The Effect
        /// </summary>
        public void Reset()
        {
            breaking.Reset();
        }
        #endregion

    }
}
