#region Info/Author
//----------------------------------------------------------------------------
// Author:    Tazi Mehdi
// Source:    Chimera 2D GAMES ENGINE
// Info:      Physics Effect
//-----------------------------------------------------------------------------
#endregion
#region Using Statement
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion


namespace Chimera.Physics
{
    
    namespace Effects
    {
        /// <summary>
        /// This Class Offer Some Personal Physic Effects
        /// </summary>
       public static class VelocityEffects
        {
            /// <summary>
           /// Apply The Null Vector To The Vectors In The Parameter
           /// </summary>
           /// <param name="vect1">First Vector</param>
           /// <param name="vect2">Seconde Vector</param>
            public static void NullVect(ref Vector2 vect1, ref Vector2 vect2)
            {
                vect1 = new Vector2(0, 0);
                vect2 = new Vector2(0, 0);
            }
            #region Effects Img
            /// <summary>
            /// A Bouncing Effect
            /// </summary>
            /// <param name="img1">First Image</param>
            /// <param name="img2">Second Image</param>
            /// <param name="vect1">Return Vector For The First Image</param>
            /// <param name="vect2">Return Vector For The Second Image</param>
            public static void Bounce(Graphics.Image img1, Graphics.Image img2, ref Vector2 vect1, ref Vector2 vect2)
            {
                Vector2 velocity = Velocity(img1, img2);
                vect1 = (velocity);
                vect2 = (-velocity);
            }
            /// <summary>
            /// A Attract Effect
            /// </summary>
            /// <param name="img1">First Image</param>
            /// <param name="img2">Second Image</param>
            /// <param name="vect1">Return Vector For The First Image</param>
            /// <param name="vect2">Return Vector For The Second Image</param>
            public static void Attract(Graphics.Image img1, Graphics.Image img2, ref Vector2 vect1, ref Vector2 vect2)
            {
                Vector2 velocity = Velocity(img1, img2);
                vect1 = (-velocity);
                vect2 = (velocity);
            }
            /// <summary>
            /// An Absorbation Effect
            /// </summary>
            /// <param name="img1">First Image</param>
            /// <param name="img2">Second Image</param>
            /// <param name="vect1">Return Vector For The First Image</param>
            /// <param name="vect2">Return Vector For The Second Image</param>
            public static void Absorb(Graphics.Image img1, Graphics.Image img2, ref Vector2 vect1, ref Vector2 vect2)
            {
                Vector2 velocity = Velocity(img1, img2);
                vect1 = (velocity);
                vect2 = (velocity);
            }
            /// <summary>
            /// A WalkWith Effect
            /// </summary>
            /// <param name="img1">First Image</param>
            /// <param name="img2">Second Image</param>
            /// <param name="vect1">Return Vector For The First Image</param>
            /// <param name="vect2">Return Vector For The Second Image</param>
            public static void WalkWith(Graphics.Image img1, Graphics.Image img2, ref Vector2 vect1, ref Vector2 vect2)
            {
                Vector2 velocity = Velocity(img1, img2);
                vect1 = (-velocity);
                vect2 = (velocity);
            }
            /// <summary>
            /// A Half Bouncing Effect
            /// </summary>
            /// <param name="img1">First Image</param>
            /// <param name="img2">Second Image</param>
            /// <param name="vect1">Return Vector For The First Image</param>
            /// <param name="vect2">Return Vector For The Second Image</param>
            public static void HalfBounce(Graphics.Image img1, Graphics.Image img2, ref Vector2 vect1, ref Vector2 vect2)
            {
                Vector2 velocity = Velocity(img1, img2);
                vect1 = new Vector2(0,0);
                vect2 = (-velocity);
            }
            /// <summary>
            /// Get Randomize Velocity Effects
            /// </summary>
            /// <param name="img1">First Image</param>
            /// <param name="img2">Second Image</param>
            /// <param name="vect1">Return Vector For The First Image</param>
            /// <param name="vect2">Return Vector For The Second Image</param>
            public static void RandomVelocityEffects(Graphics.Image img1, Graphics.Image img2, ref Vector2 vect1, ref Vector2 vect2)
            {
                Vector2 velocity = Velocity(img1, img2);
                Random rnd = new Random(DateTime.Now.Millisecond);
                vect1 = (velocity * rnd.Next(-1, 1));
                vect2 = (velocity * rnd.Next(-1, 1));
            }
            #endregion
            #region Main Function Img
           /// <summary>
           /// Calculate a Velocity Vector
           /// </summary>
           /// <param name="img1">First Image</param>
           /// <param name="img2">Second Image</param>
           /// <returns></returns>
            public static Vector2 Velocity(Graphics.Image img1, Graphics.Image img2)
            {
                Vector2 center1 = new Vector2((img1.Position.X + img1.Size.X) / 2, (img1.Position.Y + img1.Size.Y) / 2);
                Vector2 center2 = new Vector2((img2.Position.X + img2.Size.X) / 2, (img2.Position.Y + img2.Size.Y) / 2);
                Vector2 velocity = center1 - center2;
                velocity.Normalize();
                return velocity;
            }
            #endregion

            #region Effects txt
            /// <summary>
            /// A Bouncing Effect
            /// </summary>
            /// <param name="img1">First Text public class</param>
            /// <param name="img2">Second Text public class</param>
            /// <param name="vect1">Return Vector For The First Text public class</param>
            /// <param name="vect2">Return Vector For The Second Text public class</param>
            public static void Bounce(Graphics.TextWriter txt1, Graphics.TextWriter txt2, ref Vector2 vect1, ref Vector2 vect2)
            {
                Vector2 velocity = Velocity(txt1, txt2);
                vect1 = (velocity);
                vect2 = (-velocity);
            }
            /// <summary>
            /// A Attract Effect
            /// </summary>
            /// <param name="img1">First Text public class</param>
            /// <param name="img2">Second Text public class</param>
            /// <param name="vect1">Return Vector For The First Text public class</param>
            /// <param name="vect2">Return Vector For The Second Text public class</param>
            public static void Attract(Graphics.TextWriter txt1, Graphics.TextWriter txt2, ref Vector2 vect1, ref Vector2 vect2)
            {
                Vector2 velocity = Velocity(txt1, txt2);
                vect1 = (-velocity);
                vect2 = (velocity);
            }
            /// <summary>
            /// An Absorbation Effect
            /// </summary>
            /// <param name="txt1">First Text public class</param>
            /// <param name="txt2">Second Text public class</param>
            /// <param name="vect1">Return Vector For The First Text public class</param>
            /// <param name="vect2">Return Vector For The Second Text public class</param>
            public static void Absorb(Graphics.TextWriter txt1, Graphics.TextWriter txt2, ref Vector2 vect1, ref Vector2 vect2)
            {
                Vector2 velocity = Velocity(txt1, txt2);
                vect1 = (velocity);
                vect2 = (velocity);
            }
            /// <summary>
            /// A WalkWith Effect
            /// </summary>
            /// <param name="txt1">First Text public class</param>
            /// <param name="txt">Second Text public class</param>
            /// <param name="vect1">Return Vector For The First Text public class</param>
            /// <param name="vect2">Return Vector For The Second Text public class</param> 
            public static void WalkWith(Graphics.TextWriter txt1, Graphics.TextWriter txt2, ref Vector2 vect1, ref Vector2 vect2)
            {
                Vector2 velocity = Velocity(txt1, txt2);
                vect1 = (-velocity);
                vect2 = (velocity);
            }
            /// <summary>
            /// A Half Bouncing Effect
            /// </summary>
            /// <param name="txt1">First Text public class</param>
            /// <param name="txt2">Second Text public class</param>
            /// <param name="vect1">Return Vector For The First Text public class</param>
            /// <param name="vect2">Return Vector For The Second Text public class</param>
            public static void HalfBounce(Graphics.TextWriter txt1, Graphics.TextWriter txt2, ref Vector2 vect1, ref Vector2 vect2)
            {
                Vector2 velocity = Velocity(txt1, txt2);
                vect1 = new Vector2(0, 0);
                vect2 = (-velocity);
            }
            /// <summary>
            /// Get Randomize Velocity Effects
            /// </summary>
            /// <param name="txt1">First Text public class</param>
            /// <param name="txt">Second Text public class</param>
            /// <param name="vect1">Return Vector For The First Text public class</param>
            /// <param name="vect2">Return Vector For The Second Text public class</param> 
            public static void RandomVelocityEffects(Graphics.TextWriter txt1, Graphics.TextWriter txt2, ref Vector2 vect1, ref Vector2 vect2)
            {
                Vector2 velocity = Velocity(txt1, txt2);
                Random rnd = new Random(DateTime.Now.Millisecond);
                vect1 = (velocity * rnd.Next(-1, 1));
                vect2 = (velocity * rnd.Next(-1, 1));
            }
             #endregion
            #region Main Function txt
            /// <summary>
            /// Calculate a Velocity Vector
            /// </summary>
            /// <param name="img1">First Text public class</param>
            /// <param name="img2">Second Text public class</param>
            /// <returns></returns>
            public static Vector2 Velocity(Graphics.TextWriter txt1, Graphics.TextWriter txt2)
            {
                Vector2 center1 = new Vector2((txt1.Position.X + txt1.Text.Length) / 2, (txt1.Position.Y + 1) / 2);
                Vector2 center2 = new Vector2((txt2.Position.X + txt2.Text.Length) / 2, (txt2.Position.Y +1) / 2);
                Vector2 velocity = center1 - center2;
                velocity.Normalize();
                return velocity;
            }
            #endregion

            #region Effects rec
            /// <summary>
            /// A Bouncing Effect
            /// </summary>
            /// <param name="rec1">First Rectangle</param>
            /// <param name="rec2">Second Rectangle</param>
            /// <param name="vect1">Return Vector For The First Rectangle</param>
            /// <param name="vect2">Return Vector For The Second Rectangle</param>
            public static void Bounce(Rectangle rec1, Rectangle rec2, ref Vector2 vect1, ref Vector2 vect2)
            {
                Vector2 velocity = Velocity(rec1, rec2);
                vect1 = (velocity);
                vect2 = (-velocity);
            }
            /// <summary>
            /// A Attract Effect
            /// </summary>
            /// <param name="rec1">First Rectangle</param>
            /// <param name="rec2">Second Rectangle</param>
            /// <param name="vect1">Return Vector For The First Rectangle</param>
            /// <param name="vect2">Return Vector For The Second Rectangle</param>
            public static void Attract(Rectangle rec1, Rectangle rec2, ref Vector2 vect1, ref Vector2 vect2)
            {
                Vector2 velocity = Velocity(rec1, rec2);
                vect1 = (-velocity);
                vect2 = (velocity);
            }
            /// <summary>
            /// An Absorbation Effect
            /// </summary>
            /// <param name="rec1">First Rectangle</param>
            /// <param name="rec2">Second Rectangle</param>
            /// <param name="vect1">Return Vector For The First Rectangle</param>
            /// <param name="vect2">Return Vector For The Second Rectangle</param>
            public static void Absorb(Rectangle rec1, Rectangle rec2, ref Vector2 vect1, ref Vector2 vect2)
            {
                Vector2 velocity = Velocity(rec1, rec2);
                vect1 = (velocity);
                vect2 = (velocity);
            }
            /// <summary>
            /// A WalkWith Effect
            /// </summary>
            /// <param name="rec1">First Rectangle</param>
            /// <param name="rec2">Second Rectangle</param>
            /// <param name="vect1">Return Vector For The First Rectangle</param>
            /// <param name="vect2">Return Vector For The Second Rectangle</param> 
            public static void WalkWith(Rectangle rec1, Rectangle rec2, ref Vector2 vect1, ref Vector2 vect2)
            {
                Vector2 velocity = Velocity(rec1, rec2);
                vect1 = (-velocity);
                vect2 = (velocity);
            }
            /// <summary>
            /// A Half Bouncing Effect
            /// </summary>
            /// <param name="rec1">First Rectangle</param>
            /// <param name="rec2">Second Rectangle</param>
            /// <param name="vect1">Return Vector For The First Rectangle</param>
            /// <param name="vect2">Return Vector For The Second Rectangle</param> 
            public static void HalfBounce(Rectangle rec1, Rectangle rec2, ref Vector2 vect1, ref Vector2 vect2)
            {
                Vector2 velocity = Velocity(rec1, rec2);
                vect1 = new Vector2(0, 0);
                vect2 = (-velocity);
            }
            /// <summary>
            /// Get Randomize Velocity Effects
            /// </summary>
            /// <param name="rec1">First Rectangle</param>
            /// <param name="rec2">Second Rectangle</param>
            /// <param name="vect1">Return Vector For The First Rectangle</param>
            /// <param name="vect2">Return Vector For The Second Rectangle</param> 
            public static void RandomVelocityEffects(Rectangle rec1, Rectangle rec2, ref Vector2 vect1, ref Vector2 vect2)
            {
                Vector2 velocity = Velocity(rec1, rec2);
                Random rnd = new Random(DateTime.Now.Millisecond);
                vect1 = (velocity * rnd.Next(-1, 1));
                vect2 = (velocity * rnd.Next(-1, 1));
            }
            #endregion
            #region Main Function rec
            /// <summary>
            /// Calculate a Velocity Vector
            /// </summary>
            /// <param name="rec1">First Rectangle</param>
            /// <param name="rec2">Second Rectangle</param>
            /// <returns></returns>
            public static Vector2 Velocity(Rectangle rec1, Rectangle rec2)
            {
                Vector2 center1 = new Vector2((rec1.X + rec1.Width) / 2, (rec1.Y + rec1.Height) / 2);
                Vector2 center2 = new Vector2((rec2.X + rec2.Width) / 2, (rec2.Y + rec2.Height) / 2);
                Vector2 velocity = center1 - center2;
                velocity.Normalize();
                return velocity;
            }
            #endregion
        }
    }
}
