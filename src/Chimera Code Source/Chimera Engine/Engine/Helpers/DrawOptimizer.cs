#region Info/Author
//----------------------------------------------------------------------------
// Author:    Tazi Mehdi
// Source:    Chimera 2D GAMES ENGINE
// Info:      __________Optimize The Drawing______________
//-----------------------------------------------------------------------------
#endregion
#region Using Statement
using System.Collections.Generic;
using Chimera;
using Microsoft.Xna.Framework;
#endregion
namespace Chimera.Helpers
{
    /// <summary>
    /// This Class Allow You To Optimize The Rendring On The Screen
    /// </summary>
    public static class DrawOptimizer
    {
        #region Fields
        private static Microsoft.Xna.Framework.Graphics.GraphicsDevice graphics;
        private static List<Graphics.Image> list;
        /// <summary>
        /// Get Or Set The Images List To Draw
        /// </summary>
        public static List<Graphics.Image> List
        {
            get { return list; }
            set { list = value; }
        }
        #endregion
        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="_graphics">Reference To Graphics</param>
        public static void Initialize(Microsoft.Xna.Framework.Graphics.GraphicsDevice _graphics)
        {
            graphics = _graphics;
        }
        #region Draw Functions
        /// <summary>
        /// Optimize Drawing Of List Of Images
        /// </summary>
        public static void DrawList()
        {
            foreach (Graphics.Image img in list)
            {
                DrawElement(img);
            }
        }
        /// <summary>
        /// Optimize Drawing Of An Element Of List
        /// </summary>
        /// <param name="index">Image Index</param>
        public static void DrawElementOfList(int index)
        {
            Graphics.Image img = list[index];
            DrawElement(img);
        }
        /// <summary>
        /// Optimize Drawing Of The Image
        /// </summary>
        /// <param name="img">Image To Optimize</param>
        public static void DrawElement(Graphics.Image img)
        {
            if ((img.Position.X <= graphics.Viewport.Width) &&
                           (img.Position.Y <= graphics.Viewport.Height) &&
                           (img.Position.X + img.Size.X >= graphics.Viewport.X) &&
                           (img.Position.Y + img.Size.Y >= graphics.Viewport.Y))
                img.Draw();
        }
        #endregion
        #region DefaultDraw Functions
        /// <summary>
        /// Optimize Drawing Of List Of Images
        /// </summary>
        public static void DefaultDrawList()
        {
            foreach (Graphics.Image img in list)
            {
                DefaultDrawElement(img);
            }
        }
        /// <summary>
        /// Optimize Drawing Of An Element Of List
        /// </summary>
        /// <param name="index">Image Index</param>
        public static void DefaultDrawlementOfList(int index)
        {
            Graphics.Image img = list[index];
            DefaultDrawElement(img);
        }
        /// <summary>
        /// Optimize Drawing Of The Image
        /// </summary>
        /// <param name="img">Image To Optimize</param>
        public static void DefaultDrawElement(Graphics.Image img)
        {
            if ((img.Position.X <= graphics.Viewport.Width) &&
                           (img.Position.Y <= graphics.Viewport.Height) &&
                           (img.Position.X + img.Size.X >= graphics.Viewport.X) &&
                           (img.Position.Y + img.Size.Y >= graphics.Viewport.Y))
                img.DefaultDraw();
        }
         #endregion
        #region StretchDraw Functions
        /// <summary>
        /// Optimize Drawing Of List Of Images
        /// </summary>
        public static void StretchDrawList()
        {
            foreach (Graphics.Image img in list)
            {
                StretchDrawElement(img);
            }
        }
        /// <summary>
        /// Optimize Drawing Of An Element Of List
        /// </summary>
        /// <param name="index">Image Index</param>
        public static void StretchDrawlementOfList(int index)
        {
            Graphics.Image img = list[index];
            StretchDrawElement(img);
        }
        /// <summary>
        /// Optimize Drawing Image
        /// </summary>
        /// <param name="img">Image</param>
        
        public static void StretchDrawElement(Graphics.Image img)
        {
            if ((img.Position.X <= graphics.Viewport.Width) &&
                           (img.Position.Y <= graphics.Viewport.Height) &&
                           (img.Position.X + img.Size.X >= graphics.Viewport.X) &&
                           (img.Position.Y + img.Size.Y >= graphics.Viewport.Y))
                img.StretchDraw();
        }
         #endregion
    }
}

