#region Info/Author
//----------------------------------------------------------------------------
// Author:    Tazi Mehdi
// Source:    Chimera 2D GAMES ENGINE
// Info:      Enum
//-----------------------------------------------------------------------------
#endregion
namespace Chimera.Graphics
{
    namespace Enumeration
    {
        /// <summary>
        /// Define The Image Origins
        /// </summary>
       public enum EImageOrigin
        {
           /// <summary>
           /// Image Origin To The Left Up Corner
           /// </summary>
           LeftUpCorner,
           /// <summary>
           /// Image Origin To The Right Down Corner
           /// </summary>
           RightDownCorner, 
           /// <summary>
           /// Image Origin To The Center
           /// </summary>
           Center
        }
        /// <summary>
        /// Define The Directions
        /// </summary>
        public enum EDirection
        {
           /// <summary>
           /// Previous Direction
           /// </summary>
            Previous,
            /// <summary>
            /// Next Direction
            /// </summary>
            Next
        }

        /// <summary>
        /// Define Type Of Lines
        /// </summary>
        public enum ELine
        {
            /// <summary>
            /// Horizontal
            /// </summary>
             Horizontal,
            /// <summary>
            /// Vertical
            /// </summary>
            Vertical
        }
        /// <summary>
        /// Define Some Particule Effects Type
        /// </summary>
        public enum EParticleType
        {
            /// <summary>
            /// None Type
            /// </summary>
            None,
            /// <summary>
            /// Spray Type
            /// </summary>
            Spray,
            /// <summary>
            /// Line Type
            /// </summary>
            Line,
            /// <summary>
            /// Spiral Type
            /// </summary>
            Spiral,
            /// <summary>
            /// Circle Type
            /// </summary>
            Circle
        }
        /// <summary>
        /// Define Some ZOOM Type
        /// </summary>
        public enum EZOOM
        {
            /// <summary>
            /// Zoom In
            /// </summary>
            IN,
            /// <summary>
            /// Zoom Out
            /// </summary>
            OUT
        }      
    }
}
