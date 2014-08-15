#region Info/Author
//----------------------------------------------------------------------------
// Author:    Tazi Mehdi
// Source:    Chimera 2D GAMES ENGINE
// Info:      ___Interface___
//-----------------------------------------------------------------------------
#endregion
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
namespace Chimera.Helpers.Interface
{
    
    /// <summary>
    /// Define An Interface For Countable Class
    /// </summary>
    public interface ICountalbe
    {
    
    }
    /// <summary>
    /// Define An Interface For Loadable Class
    /// </summary>
    public interface ILoadable
    {
        /// <summary>
        /// Load Graphics Content
        /// </summary>
        /// <param name="graphicsDevice">XNA GraphicsDevice Reference</param>
        /// <param name="contentManager">XNA ContentManager Reference</param>
        void LoadGraphicsContent(GraphicsDevice graphicsDevice, ContentManager contentManager);
    }
    /// <summary>
    /// Define An Interface For Drawable Class With Diferent Options"
    /// </summary>
    public interface IDrawables:Interface.IDrawable
    {
        /// <summary>
        /// Draw Without Stretch
        /// </summary>
        void DefaultDraw();
        /// <summary>
        /// Draw Without Stretch
        /// </summary>
        void StretchDraw();
    }
    /// <summary>
    /// Define An Interface For Drawable Class
    /// </summary>
    public interface IDrawable
    {
        void Draw();
    }
    /// <summary>
    /// Define An Interface For Optimized Drawable Class
    /// </summary>
    public interface IOptimizedDrawable : IDrawables
    {
        /// <summary>
        /// Optimize DefaultDraw Method
        /// </summary>
        void OptimizeDefaultDraw();
        /// <summary>
        /// Optimize Draw Method
        /// </summary>
        void OptimizeDraw();
        /// <summary>
        /// Optimize StretchDraw Method
        /// </summary>
        void OptimizeStretchDraw();
    }
    /// <summary>
    /// Define An Interface For Updateable Class
    /// </summary>
    public interface IUpdateable
    {
        void Update();
    }

}
