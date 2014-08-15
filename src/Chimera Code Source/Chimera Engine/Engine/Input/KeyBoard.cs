#region Info/Author
//----------------------------------------------------------------------------
// Author:    Tazi Mehdi
// Source:    Chimera 2D GAMES ENGINE
// Info:      KeyBorad Manager
//-----------------------------------------------------------------------------
#endregion
#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
#endregion
namespace Chimera.Input
{
    /// <summary>
    /// This Class Allow You To Manages the changing state of The Keyboard.
    /// Be sure to call Update() every frame before accessing the data.
    /// </summary>
    public static class KeyBoard 
    {
        #region Fields
        /// <summary>
        /// The current keyboard state
        /// </summary>
        private static KeyboardState currentstate;
        /// <summary>
        /// The previous frame's keyboard state
        /// </summary>
        private static KeyboardState previousState;
        #endregion
        #region Main Key Functions
        /// <summary>
        /// Check If The Specified key is being pressed right now
        /// </summary>
        /// <param name="key">The key to check</param>
        /// <returns>Returns true if the key is being pressed</returns>
        public static bool IsPressingKey(Keys key)
        {
            return currentstate.IsKeyDown(key);
        }
        /// <summary>
        /// Check If The Specified key is being held down
        /// </summary>
        /// <param name="key">The key to check</param>
        /// <returns>Returns true if the key is being held</returns>     
        public static bool IsHoldingKey(Keys key)
        {
            return
                previousState != null &&
                previousState.IsKeyDown(key) &&
                currentstate.IsKeyDown(key);
        }
        /// <summary>
        /// Check If The Specified Key Became Pressed Between The Current And Mast Frames
        /// </summary>
        /// <param name="key">The key to check</param>
        /// <returns>Returns true if the key became pressed</returns>
        public static bool IsKeyPressed(Keys key)
        {
            return
                previousState != null &&
                previousState.IsKeyUp(key) &&
                currentstate.IsKeyDown(key);
        }
        /// <summary>
        /// Check If The Specified Key Became Released Between The Current And Mast Frames
        /// </summary>
        /// <param name="key">The key to check</param>
        /// <returns>Returns true if the key became released</returns>
        public static bool IsKeyReleased(Keys key)
        {
            return
                previousState != null &&
                previousState.IsKeyDown(key) &&
                currentstate.IsKeyUp(key);
        }
        /// <summary>
        /// Updates The internal State. This Should Be Called Once Per Game Frame
        /// </summary>
        public static void Update()
        {
            previousState = currentstate;

            currentstate = Keyboard.GetState();
        }
        #endregion
        #region Main Keys Functions
        /// <summary>
        /// Return True If All The Keys Are Pressed
        /// </summary>
        /// <param name="keys">The keys to check</param>
        /// <returns>Returns true if the keys became pressed</returns>
        public static bool IsKeysPressed(Keys[] keys)
        {
            int keysok = 0;
           
            foreach (Keys key in keys)
            {
                    if (IsKeyPressed(key))
                        keysok++;
            }

            if (keysok == keys.GetLength(0))
                return true;

            return false;
        }
        /// <summary>
        /// Return True If All The Keys Are Released
        /// </summary>
        /// <param name="keys">The keys to check</param>
        /// <returns>Returns true if the keys became released</returns>
        public static bool IsKeysReleased(Keys[] keys)
        {
            int keysok = 0;

            foreach (Keys key in keys)
            {
                if (IsKeyReleased(key))
                    keysok++;
            }

            if (keysok == keys.GetLength(0))
                return true;

            return false;
        }
        /// <summary>
        /// Return True If All The Keys Are Pressing
        /// </summary>
        /// <param name="keys">The keys to check</param>
        /// <returns>Returns true if the keys is being pressed</returns>
        public static bool IsPressingKeys(Keys[] keys)
        {
            int keysok = 0;

            foreach (Keys key in keys)
            {
                if (IsPressingKey(key))
                    keysok++;
            }

            if (keysok == keys.GetLength(0))
                return true;

            return false;
        }
        /// <summary>
        /// Return True If All The Keys Are Holding
        /// </summary>
        /// <param name="keys">The keys to check</param>
        /// <returns>Returns true if the keys is being held</returns>
        public static bool IsHoldingKeys(Keys[] keys)
        {
            int keysok = 0;

            foreach (Keys key in keys)
            {
                if (IsHoldingKey(key))
                    keysok++;
            }

            if (keysok == keys.GetLength(0))
                return true;

            return false;
        }
        #endregion
        #region Main All Keys
        /// <summary>
        /// Get All Pressing Keys
        /// </summary>
        /// <returns>Return All Pressing Keys</returns>
        public static Keys[] GetAllPressingKeys()
        {
            return currentstate.GetPressedKeys();
        }
        /// <summary>
        /// Get All Hollding Keys
        /// </summary>
        /// <returns>Return All Holding Keys</returns>
        public static Keys[] GetAllHoldingKeys()
        {
            List<Keys> returned = new List<Keys>();
            Keys[] cpk = currentstate.GetPressedKeys();
           
            foreach (Keys Ckey in cpk)
                    if (IsHoldingKey(Ckey))
                        returned.Add(Ckey);
            
            return returned.ToArray();
        }
        /// <summary>
        /// Get All Pressed Keys
        /// </summary>
        /// <returns>Return All Pressing Keys</returns>
        public static Keys[] GetAllKeysPressed()
        {
            List<Keys> returned = new List<Keys>();
            Keys[] cpk = currentstate.GetPressedKeys();

            foreach (Keys Ckey in cpk)
              if(IsKeyPressed(Ckey))
                        returned.Add(Ckey);

            return returned.ToArray();
        }
        /// <summary>
        /// Get All Released Keys
        /// </summary>
        /// <returns>Return All Pressing Keys</returns>
        public static Keys[] GetAllKeysReleased()
        {
            List<Keys> returned = new List<Keys>();
            Keys[] cpk = previousState.GetPressedKeys();

            foreach (Keys Ckey in cpk)
                if (IsKeyReleased(Ckey))
                    returned.Add(Ckey);

            return returned.ToArray();
        }
        #endregion
        #region Main Functions
        
        /// <summary>
        /// Indicate If Is Only The Keys In Parameter Are Pressed
        /// </summary>
        /// <param name="keys"></param>
        /// <returns>Return True If The Only Keys In Parameter Are Pressed</returns>
        public static bool IsOnlyThisKeysPressed(Keys[] keys)
        {
            Keys[] HK = GetAllKeysPressed();
            int nbkeys = HK.Length;
            int nbkeysok = 0;

            foreach (Keys pkey in keys)
                foreach (Keys phk in HK)
                    if (pkey == phk)
                        nbkeysok++;

            if (nbkeys == keys.GetLength(0) && nbkeysok == keys.GetLength(0))
                return true;

            return false;
        }
        /// <summary>
        /// Indicate If Is Only The Keys In Parameter Are Holding
        /// </summary>
        /// <param name="keys"></param>
        /// <returns>Return True If The Only Keys In Parameter Are Holding</returns>
        public static bool IsOnlyThisHoldingKeys(Keys[] keys)
        {
            Keys[] HK = GetAllHoldingKeys();
            int nbkeys = HK.Length;
            int nbkeysok=0;

            foreach (Keys pkey in keys)
                foreach (Keys phk in HK)
                    if (pkey == phk)
                        nbkeysok++;

            if (nbkeys == keys.GetLength(0) && nbkeysok == keys.GetLength(0))
                return true;

            return false;
        }
        /// <summary>
        /// Indicate If Is Only The Keys In Parameter Are Released
        /// </summary>
        /// <param name="keys"></param>
        /// <returns>Return True If The Only Keys In Parameter Are Released</returns>
        public static bool IsOnlyThisKeysReleased(Keys[] keys)
        {
            Keys[] HK = GetAllKeysReleased();
            int nbkeys = HK.Length;
            int nbkeysok = 0;

            foreach (Keys pkey in keys)
                foreach (Keys phk in HK)
                    if (pkey == phk)
                        nbkeysok++;

            if (nbkeys == keys.GetLength(0) && nbkeysok == keys.GetLength(0))
                return true;

            return false;
        }
        /// <summary>
        /// Indicate If Is Only The Keys In Parameter Are Pressing
        /// </summary>
        /// <param name="keys"></param>
        /// <returns>Return True If The Only Keys In Parameter Are Pressing</returns>
        public static bool IsOnlyThisPressingKeys(Keys[] keys)
        {
            Keys[] HK = GetAllPressingKeys();
            int nbkeys = HK.Length;
            int nbkeysok = 0;

            foreach (Keys pkey in keys)
                foreach (Keys phk in HK)
                    if (pkey == phk)
                        nbkeysok++;

            if (nbkeys == keys.GetLength(0) && nbkeysok == keys.GetLength(0))
                return true;

            return false;
        }
        
        #endregion
    }
}