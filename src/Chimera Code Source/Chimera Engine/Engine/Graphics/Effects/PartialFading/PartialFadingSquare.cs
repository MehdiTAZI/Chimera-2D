#region Using Statement
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Chimera.Graphics.Effects;
#endregion
namespace Chimera.Graphics.Effects.PartialFading
{   
    /// <summary>
    /// This Class Allow You Using Partial Fading Square Effect
    /// </summary>
    public class PartialFadingSquare : Chimera.Helpers.Interface.IUpdateable, Helpers.Interface.IDrawable, Helpers.Interface.IDrawables
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Image">The Image on what the effect will be applied</param>
        public PartialFadingSquare(Graphics.Image Image)
        {partialfading = new PartialFading(Image);}
        #region Fields & Properties


        /// <summary>
        /// Get Or Set The Image On What the effect will be applied
        /// </summary>
        public Image Image
        {
            get { return partialfading.Image; }
            set { partialfading.Image = value; }
        }
        /// <summary>
        /// Get Or Set The Effect Speed
        /// </summary>
        public float Speed
        {
            get { return partialfading.Speed; }
            set { partialfading.Speed = value; }
        }
        /// <summary>
        /// Get Or Set Rotation Speed
        /// </summary>
        public float SpeedRotation
        {
            get { return partialfading.SpeedRotation; }
            set { partialfading.SpeedRotation = value; }
        }
        /// <summary>
        /// Get Or Set The Effect Position
        /// </summary>
        public Vector2 Position
        {
            get { return partialfading.Position; }
            set { partialfading.Position = value; }
        }
        /// <summary>
        /// Enable Or Disable The Effect
        /// </summary>
        public bool Enable
        {
            get { return partialfading.Enable; }
            set { partialfading.Enable = value; }
        }
        private PartialFading partialfading;
        #endregion
        #region Initialization

        /// <summary>
        /// Initialize Rotation With Randomize Value
        /// </summary>
        
        public void InitRandomRotation()
        {
            partialfading.InitRandomRotation();
        }
        /// <summary>
        /// Initialize Rotation With Randomize Value
        /// </summary>
        /// <param name="origin">The Origin Of Rotation</param>
        public void InitRandomRotation(Enumeration.EImageOrigin origin)
        {
            partialfading.InitRandomRotation(origin);
        }
 
        #endregion
        #region Main Functions
        /// <summary>
        /// Initialize The Effect
        /// </summary>
        /// <param name="Size">Image Size</param>
        public void Initialize(float Size)
        { partialfading.Initialize(new Vector2(Size));  }
        /// <summary>
        /// Update the effect
        /// </summary>
        public void Update()
        {
            partialfading.Update();
        }
        /// <summary>
        /// Draw The Effect
        /// </summary>
        public void Draw()
        {
            partialfading.Draw();
        }
        /// <summary>
        /// Optimize Draw Method
        /// </summary>
        public void OptimizedDraw()
        {
            partialfading.OptimizedDraw();
        }
        /// <summary>
        /// Draw Without Stretch The Effect
        /// </summary>
        public void DefaultDraw()
        {
            partialfading.DefaultDraw();
        }
        /// <summary>
        /// Optimize DefaultDraw Method
        /// </summary>
        public void OptimizedDefaultDraw()
        {
            partialfading.OptimizedDefaultDraw();
        }
        /// <summary>
        /// Optimize Draw Method
        /// </summary>
        public void StretchDraw()
        {
            partialfading.StretchDraw();
        }
        /// <summary>
        /// Optimize StretchDraw Method
        /// </summary>
        public void OptimizedStretchDraw()
        {
            partialfading.OptimizedStretchDraw();
        }
        /// <summary>
        /// Reset The Effect
        /// </summary>
        public void Reset()
        {
            partialfading.Reset();
        }
        #endregion
    }
}
