#region Info/Author
//----------------------------------------------------------------------------
// Author:    Tazi Mehdi
// Source:    Chimera 2D GAMES ENGINE
// Info:      Image public class
//-----------------------------------------------------------------------------
#endregion
#region Using Statement
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace Chimera.Graphics
{
    /// <summary>
    /// This Class Able You To Create a Image
    /// </summary>
    public class BackGround : Graphics.Image, Helpers.Interface.IDrawable
    {
        /// <summary>
        /// The Default Constructor
        /// </summary>
        public BackGround()
        {
        //    SetOrigin(Chimera.Graphics.Enumeration.EImageOrigin.LeftUpCorner);
        }
        #region Main Methods (Initialize)
        /// <summary>
        /// Initialize The Image
        /// </summary>
        /// <param name="size">Image Size</param>
        public override void Initialize(Vector2 size)
        {
            base.Initialize(size);
            this.depth = 0;
        }
        #endregion
    }
}
