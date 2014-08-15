#region Info/Author
//----------------------------------------------------------------------------
// Author:    Tazi Mehdi
// Source:    Chimera 2D GAMES ENGINE
// Info:      Graphical Mouse
//-----------------------------------------------------------------------------
#endregion
#region Using Statement
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
#endregion
namespace Chimera.Graphics
{
    #if !XBOX
    /// <summary>
    /// This Class Allow You To Change Graphics Mouse Parameter
    /// </summary>
    public static class Mouse
    {
        #region Fields (cursor, mouseState)
        private static Graphics.Image cursor;
        
        private static MouseState mouseState;       
        
        #endregion
        #region Main Methods (Constructor, Initialize, LoadGraphicsContents, Update, Draw)
        /// <summary>
        /// the Default Constructor
        /// </summary>
        static Mouse()
        {
            cursor = new Graphics.Image();
        }
        /// <summary>
        /// Initialize The Mouse Image Size
        /// </summary>
        /// <param name="Size">Mouse Image Size</param>
        public static void Initialize(Vector2 Size)
        {
            cursor.Initialize(Size);
            cursor.Depth = 1;
        }
        /// <summary>
        /// Load Graphics Content
        /// </summary>
        /// <param name="spriteBatch">XNA SPRITE BATCH REFERENCE</param>
        /// <param name="texture">The Texture To Apply</param>
        public static void LoadGraphicsContents(SpriteBatch spriteBatch, Texture2D texture)
        {
            cursor.LoadGraphicsContent(spriteBatch, texture);
        }

       /// <summary>
       /// Update The Image Position
       /// </summary>      
        public static void Update()
        {
            mouseState = Microsoft.Xna.Framework.Input.Mouse.GetState();
            cursor.Position = new Vector2(mouseState.X, mouseState.Y);
        }
        /// <summary>
        /// Draw The Mouse Image
        /// </summary>
        
        public static void Draw()
        {
            cursor.DefaultDraw();
        }
        #endregion
        #region Additional Functions
        /// <summary>
        /// Set Mouse Image
        /// </summary>
        /// <param name="img">Set An Image For The Mouse Pointer</param>
        public static void SetImage(Graphics.Image img)
        {
            cursor = img;  
        }
        #endregion
    }
#endif
}
