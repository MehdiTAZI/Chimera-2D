#region Info/Author
//----------------------------------------------------------------------------
// Author:    Tazi Mehdi
// Source:    Chimera 2D GAMES ENGINE
// Info:      FPS COUNTER
//-----------------------------------------------------------------------------
#endregion
#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace Chimera.Helpers
{
    /// <summary>
    /// This Class offer you the posibility to calculat the game fps
    /// </summary>
    public static class FPSCounter
    {

        private static double lastSeconds;
        private static int curFPS;
        private static float counter;
        /// <summary>
        /// Get The Current Frame Per Second
        /// </summary>
        public static int CurrentFPS
        {
            get { return curFPS;}
        }
        /// <summary>
        /// To Call Every Drawing Frame 
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        public static void Draw(GameTime gameTime)
        {            
            counter++;
            if (gameTime.TotalGameTime.TotalSeconds - lastSeconds > 1)
            {
                curFPS = (int)counter;
                counter = 0;
                lastSeconds = gameTime.TotalGameTime.TotalSeconds;
            }
        }


    }
}


