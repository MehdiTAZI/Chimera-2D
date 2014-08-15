#region Info/Author
//----------------------------------------------------------------------------
// Author:    Tazi Mehdi
// Source:    Chimera 2D GAMES ENGINE
// Info:      Geometery
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
    /// This Class Offer You Some Geometery Functions
    /// </summary>
    public static class GeometryFunc
    {
        /// <summary>
        /// Do A Rotation
        /// </summary>
        /// <param name="vect">Vector To Be Rotated</param>
        /// <param name="Origin">The Origin Of The Rotation</param>
        /// <param name="rot">Degree Of The Rotation</param>
        /// <returns>Return The Rotated Vector</returns>
        public static Vector2 Rotation(Vector2 vect,Vector2 Origin, float rot)
        {
            float x, y;
            x = vect.X;
            y = vect.Y;
            /*Not Optimized Version
            vect.X = (float)(x * Math.Cos(rot) - y * Math.Sin(rot));
            vect.Y = (float)(x * Math.Sin(rot) + y * Math.Cos(rot));
            */
            //Optimized
            vect.X = (float)(Math.Cos(rot) * (x + y) - y * (Math.Sin(rot) + Math.Cos(rot)));
            vect.Y = (float)(x * (Math.Sin(rot) - Math.Cos(rot)) + Math.Cos(rot) * (x + y));
            return vect;
        }
        /// <summary>
        /// Do A Rotation
        /// </summary>
        /// <param name="vect">Vector To Be Rotated</param>
        /// <param name="rot">Degree Of The Rotation</param>
        /// <returns>Return The Rotated Vector</returns>
        public static Vector2 Rotation(Vector2 vect, float rot)
        {
            float x, y;
            x = vect.X;
            y = vect.Y;
            /*Not Optimized Version
            vect.X = (float)(x * Math.Cos(rot) - y * Math.Sin(rot));
            vect.Y = (float)(x * Math.Sin(rot) + y * Math.Cos(rot));
            */
            //Optimized
            vect.X= (float)( Math.Cos(rot)*(x+y) - y*( Math.Sin(rot)+Math.Cos(rot)));
            vect.Y = (float)(x * (Math.Sin(rot) - Math.Cos(rot)) + Math.Cos(rot) * (x + y));
            return vect;
        }
        /// <summary>
        /// Do A Translation
        /// </summary>
        /// <param name="vect">Vector To Be Translated</param>
        /// <param name="trans">Translation Value (X,Y)</param>
        /// <returns>Return The Translated Vector</returns>
        public static Vector2 Translate(Vector2 vect, Vector2 trans)
        {
            return new Vector2(vect.X + trans.X, vect.Y + trans.Y) ;
        }
        /// <summary>
        /// Reflect a Vector
        /// </summary>
        /// <param name="vect">Vector To Be Refelcted</param>
        /// <param name="normal">The Reflection Normal</param>
        /// <returns>Return The Refelcted Vector</returns>
        public static Vector2 Reflect(Vector2 vect,Vector2 normal)
        {
            return Vector2.Reflect(vect, normal);
        }
    }
}
