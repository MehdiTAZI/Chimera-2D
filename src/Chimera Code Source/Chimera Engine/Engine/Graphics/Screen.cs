using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace Chimera.Graphics
{
    /// <summary>
    /// This Class Offer You Some Functions That Communicate With The Game Screen
    /// </summary>
    public static class Screen
    {
       
        private  static Color color = Color.White;
        /// <summary>
        /// The Default Drawing Color Screen
        /// </summary>
        public static Color Color
        {
            get {return color ;}
            set {color = value ;}
        }
        private static GraphicsDevice gd;
        
        /// <summary>
        /// Set And Initialize The public class
        /// </summary>
        /// <param name="GraphicsDevice">XNA GraphicsDevice Reference</param>
        public  static void Set(GraphicsDevice GraphicsDevice)
        {
            gd = GraphicsDevice;
        }
        /// <summary>
        /// Get ScreenShoot Of Screen
        /// </summary>
        /// <returns>Return The ScreenShoot Texture</returns>
        public  static Texture2D GetScreenTexture()
        {
            ResolveTexture2D text;
            text = new ResolveTexture2D(gd, gd.PresentationParameters.BackBufferWidth, gd.PresentationParameters.BackBufferHeight, 1, gd.PresentationParameters.BackBufferFormat);
            gd.ResolveBackBuffer(text);
            return text;
        }
        /// <summary>
        /// Get ScreenShoot Of A Part Of The Screen
        /// </summary>
        /// <param name="source"></param>
        /// <returns>Return The ScreenShoot Texture</returns>
        public  static Texture2D GetScreenTexture (Rectangle source)
        {
            ResolveTexture2D text;
            text = new ResolveTexture2D(gd,source.Width, source.Height, 1, gd.PresentationParameters.BackBufferFormat);
            gd.ResolveBackBuffer(text);
            return text;
        }
        /// <summary>
        /// Clear The Screen With a Color
        /// </summary>
        /// <param name="Color">Screen Filling Color</param>
        public  static void ClearScreen(Color Color)
        {
            gd.Clear(Color);
        }

        /// <summary>
        /// Draw The Current Screen
        /// </summary>
        /// <param name="sprite">XNA SPRITEBACH REFERENCE</param>
        public static void DrawScreen(SpriteBatch sprite)
        {
            sprite.Draw(GetScreenTexture(), new Vector2(0, 0), Color);
        }
        /// <summary>
        /// Draw The Current Screen in a specific Position
        /// </summary>
        ///<param name="pos">Position Where The Screen Will Be Drawn</param>
        /// <param name="sprite">XNA SPRITEBACH REFERENCE</param>
        public static void DrawScreen(Vector2 pos,SpriteBatch sprite)
        {
            sprite.Draw(GetScreenTexture(), pos, Color);
        }
        /// <summary>
        /// Draw The The Current Screen Into A Specific Area
        /// </summary>
        /// <param name="dest">Destination Area</param>
        /// <param name="sprite">XNA SPRITEBATCH REFERENCE</param>
        public  static void DrawScreen(Rectangle dest,SpriteBatch sprite)
        {
            sprite.Draw(GetScreenTexture(), dest, Color);
        }
        /// <summary>
        /// Draw The An Area Of The Current Screen Into An Other Specific Area
        /// </summary>
        /// <param name="scr">Source Area</param>
        /// <param name="dest">The Destination Area</param>
        /// <param name="sprite">XNA SPRITEBATCH REFERENCE</param>
        public static void DrawScreen(Rectangle scr,Rectangle dest,SpriteBatch sprite)
        {
            sprite.Draw(GetScreenTexture(scr), dest, Color);
        }
    }
}
