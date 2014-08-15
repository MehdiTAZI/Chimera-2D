#region Info/Author
//----------------------------------------------------------------------------
// Author:    Tazi Mehdi
// Source:    Chimera 2D GAMES ENGINE
// Info:      Pixel Perfect Collision Test
//-----------------------------------------------------------------------------
#endregion
#region Using Statement
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace Chimera.Physics
{
    /// <summary>
    /// This Class Allow You The Posibility Of Using The Pixel Perfect Collision 
    /// </summary>
    public static class Pixel_Perfect
    {
        #region Other Functions
        private static bool TestCollision(Texture2D texture1, Rectangle texture1Intersection,
                                            Texture2D texture2, Rectangle texture2Intersection)
        {


            int pixelCount = texture1Intersection.Width * texture1Intersection.Height;
            if (pixelCount != 0)
            {
                
                uint[] texture1Pixels = new uint[pixelCount];
                uint[] texture2Pixels = new uint[pixelCount];
                //unsafe mode for optimization
                texture1.GetData(0, texture1Intersection, texture1Pixels, 0, pixelCount);
                texture2.GetData(0, texture2Intersection, texture2Pixels, 0, pixelCount);
            
                for (int i = 0; i < pixelCount; ++i)
                    if (((texture1Pixels[i] & 0xff000000) > 0) && ((texture2Pixels[i] & 0xff000000) > 0))
                        return true;
            }
            return false;
        }
        /// <summary>
        /// Calculate The Intersection Rectangle Of Two Images
        /// </summary>
        /// <param name="img1"></param>
        /// <param name="img2"></param>
        /// <returns>Return The Intersection Rectangle Of Two Images</returns>
        public static Rectangle Intersection(Graphics.Image img1, Graphics.Image img2)
        {
            int x1 = Math.Max((int)img1.Position.X, (int)img2.Position.X);
            int y1 = Math.Max((int)img1.Position.Y, (int)img2.Position.Y);
            int x2 = Math.Min((int)(img1.Position.X + img1.Size.X), (int)(img2.Position.X + img2.Size.X));
            int y2 = Math.Min((int)(img1.Position.Y + img1.Size.Y), (int)(img2.Position.Y + img2.Size.Y));

            if ((x2 >= x1) && (y2 >= y1))
            {
                return new Rectangle(x1, y1, x2 - x1, y2 - y1);
            }
            return Rectangle.Empty;
        }
        /// <summary>
        /// Calculate The Normalize Of The Rectangle
        /// </summary>
        /// <param name="reference">The Rectangle To Be Normalized</param>
        /// <param name="rectangle">The Reference</param>
        /// <returns>Return The Normalize Of The Rectangle In Parameter</returns>
        public static Rectangle Normalize(Rectangle reference, Rectangle rectangle)
        {
            return new Rectangle(
              rectangle.X - reference.X,
              rectangle.Y - reference.Y,
              rectangle.Width,
              rectangle.Height);
        }
        #endregion
        #region  Main Function
        /// <summary>
        /// Check For A Potential Collision
        /// </summary>
        /// <param name="img1">The First Image To Check</param>
        /// <param name="img2">The Second Image To Check</param>
        /// <returns>Return True If The Two Image Are In Collision)</returns>
        public static bool IsCollid(Graphics.Image img1, Graphics.Image img2)
        {
           Rectangle rec= Intersection(img1, img2);
           Rectangle rec1, rec2;
           rec1 = Normalize(new Rectangle((int)img1.Position.X, (int)img1.Position.Y, (int)(img1.Size.X + img1.Position.X), (int)(img1.Size.Y+img1.Position.Y)), rec);
           rec2 = Normalize(new Rectangle((int)img2.Position.X, (int)img2.Position.Y, (int)(img2.Size.X + img2.Position.X), (int)(img2.Size.Y + img2.Position.Y)), rec);
           return TestCollision(img1.Texture, rec1, img2.Texture, rec2);
       }
        /// <summary>
        /// Check For A Potential Collision (Using Boding Box Test , If It's The
        /// </summary>
       /// <param name="img1">The First Image To Check</param>
       /// <param name="img2">The Second Image To Check</param>
        /// <returns>Return True If The Two Image Are In Collision,Testing First With The Bouding Box Technic ( Optimized )</returns>
        public static bool IsBoxedCollid(Graphics.Image img1, Graphics.Image img2)
        {
            BoundingBox b1, b2;
            b1 = new BoundingBox();
            b2 = new BoundingBox();
            b1.Initialize(img1);
            b2.Initialize(img2);

            if (b1.IsCollid(b2))
            {
                Rectangle rec = Intersection(img1, img2);
                Rectangle rec1, rec2;
                rec1 = Normalize(new Rectangle((int)img1.Position.X, (int)img1.Position.Y, (int)(img1.Size.X + img1.Position.X), (int)(img1.Size.Y + img1.Position.Y)), rec);
                rec2 = Normalize(new Rectangle((int)img2.Position.X, (int)img2.Position.Y, (int)(img2.Size.X + img2.Position.X), (int)(img2.Size.Y + img2.Position.Y)), rec);
                return TestCollision(img1.Texture, rec1, img2.Texture, rec2);
            }
            else
            {
                return false;
            }
            
        }
        #endregion
        //the above does not address scaled or rotated sprites. 

}
}
