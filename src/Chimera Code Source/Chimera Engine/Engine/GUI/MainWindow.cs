#region Info/Author
//----------------------------------------------------------------------------
// Author:    Tazi Mehdi
// Source:    Chimera 2D GAMES ENGINE
// Info:      Game Window Properties
//-----------------------------------------------------------------------------
#endregion
#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
#endregion

namespace Chimera.GUI
{
   /// <summary>
   /// This Class Offer The To Control The Game Window
   /// </summary>
   public static class MainWindow
    {
       private static GameWindow win;
       /// <summary>
       ///Initialize / Apply The Window
       /// </summary>
       /// <param name="window">XNA GameWindow Reference</param>
       public static void ApplyGameWindow(Microsoft.Xna.Framework.GameWindow window)
        {
            win = window;
        }
        /// <summary>
        ///Initialize / Apply The Game
        /// </summary>
        /// <param name="window">XNA Game Reference</param>
        /// <param name="game">XNA Game Reference</param>
       public static void ApplyGameWindow(Microsoft.Xna.Framework.Game game)
       {
           win = game.Window; 
       }
       /// <summary>
       /// Get Or Set The Game Window Properties
       /// </summary>
       public static GameWindow Properties
       {
           get { return win ;}
           set { win = value;}
       }
    }
}
