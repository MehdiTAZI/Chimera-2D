#region Info/Author
//----------------------------------------------------------------------------
// Author:    Tazi Mehdi
// Source:    Chimera 2D GAMES ENGINE
// Info:      Main public class Engine
//-----------------------------------------------------------------------------
#endregion
using Microsoft.Xna.Framework;
namespace Chimera
{
    /// <summary>
    /// The Main Engine public class
    /// </summary>
    public static class Engine
    {
        /// <summary>
        /// Set The Current Games Options To The Engine
        /// </summary>
        /// <param name="game"></param>
        /// <param name="graphics"></param>
        public static void Set(Game game, GraphicsDeviceManager graphics)
        {
            Helpers.GameOptions.Aplly(game, graphics);
            Helpers.GraphicsDeviceCapabilities.ApplyGraphicsDevice(game.GraphicsDevice);
            #if !XBOX
            Helpers.ScreenShoot.GraphicsDevice = game.GraphicsDevice;
            #endif
            Graphics.Screen.Set(graphics.GraphicsDevice);
        }
        /// <summary>
        /// Initialize The Engine
        /// </summary>
        /// <param name="game"></param>
        /// <param name="graphics"></param>
        public static void Initialize(Game game, GraphicsDeviceManager graphics)
        {
            Set(game, graphics);
        }
    }
}