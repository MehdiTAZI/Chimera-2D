#region Info/Author
//----------------------------------------------------------------------------
// Author:    Tazi Mehdi
// Source:    Chimera 2D GAMES ENGINE
// Info:      Title
//-----------------------------------------------------------------------------
#endregion
#region Using Statement
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace Chimera.Graphics
{
    /// <summary>
    /// A Title public class
    /// </summary>
    public class Title : Graphics.Image, Helpers.Interface.IDrawable
    {
        #region Main Methods (Initialize)
        /// <summary>
        /// Initialize The Title
        /// </summary>
        /// <param name="size">Title Size</param>
        public override void Initialize(Vector2 size)
        {
            base.Initialize(size);
            this.depth = 0.7f;
        }
        #endregion
    }
}
