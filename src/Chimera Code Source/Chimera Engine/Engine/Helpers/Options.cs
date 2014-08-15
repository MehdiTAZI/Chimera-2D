#region Info/Author
//----------------------------------------------------------------------------
// Author:    Tazi Mehdi
// Source:    Chimera 2D GAMES ENGINE
// Info:      __________Game Options______________
//-----------------------------------------------------------------------------
#endregion
#region Using Statement

using Chimera;
using Microsoft.Xna.Framework;
#endregion

namespace Chimera.Helpers
{
    /// <summary>
    /// This Class Allow You To Set Or Get The Game Options
    /// </summary>
    public static class GameOptions
    {
        private static Game _game;
        private static  GraphicsDeviceManager _graphics;
        /// <summary>
        /// Initialize / Apply The Games Options
        /// </summary>
        /// <param name="game">Reference TO XNA Game public class</param>
        /// <param name="graphics">Reference TO XNA GraphicsDeviceManager public class</param>
        public static void Aplly(Microsoft.Xna.Framework.Game game,GraphicsDeviceManager graphics)
        {
             _game =game;
             GUI.MainWindow.ApplyGameWindow(game);
            _graphics = graphics;   
        
            
        }
        /// <summary>
        /// Get Or Set Game Window
        /// </summary>
        public static GameWindow Window
        {
            get { return GUI.MainWindow.Properties; }
            set { GUI.MainWindow.Properties = value; }
        }
        /// <summary>
        /// Get The Graphics Device Parameters
        /// </summary>
        public static Microsoft.Xna.Framework.Graphics.GraphicsDevice Parameters
        {
            get {return _game.GraphicsDevice ;}            
        }
        /// <summary>
        /// Get Or Set The Window Properties
        /// </summary>
        public static GraphicsDeviceManager Properties
        {
            get {return _graphics ;}
            set {_graphics=value ;}
        }
        /// <summary>
        /// Set The Mouse Cursor Visible Or Not
        /// </summary>
        public static bool IsMouseVisible
        {
            get{return _game.IsMouseVisible;}
            set{_game.IsMouseVisible = value;}
        }
        /// <summary>
        /// Fixe Or Not The Number Of Frame Per Seconde
        /// </summary>
        /// <param name="state">If state is set to true , The FPS Will Be Fixed</param>
        public static void SetFixedFPS(bool state)
        {
            
                _game.IsFixedTimeStep = state;
                _graphics.SynchronizeWithVerticalRetrace = state;
                _graphics.ApplyChanges();
        }
    }
}
