#region Info/Author
//----------------------------------------------------------------------------
// Author:    Tazi Mehdi
// Source:    Chimera 2D GAMES ENGINE
// Info:      Mouse public class
//-----------------------------------------------------------------------------
#region Original Author
/**
 * This code was written by Derek Nedelman and released at www.gameprojects.com
 * 
 * Derek Nedelman and GameProjects.com assume no responsibility for any harm caused 
 * by using this code.
 * 
 * This code is in the public domain. That said, it would be nice if you left 
 * the previous comments if you decide to use it for anything. 
 */
#endregion
#endregion
#region Using Statement
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
#endregion
namespace Chimera.Input
{
    #if !XBOX
    /// <summary>
    /// This Class Allow You To Manages the changing state of The Mouse.
    /// Be sure to call Update() every frame before accessing the data.
    /// </summary>
    public static class Mouse 
    {
        #region Graphics
        /// <summary>
        /// Mouse Graphics (Cursor,Size...)
        /// </summary>
        public static class Graphic
        {
            /// <summary>
            /// Initialize The Mouse Size
            /// </summary>
            /// <param name="Size">Mouse Image Size</param>
            public static void Initialize(Vector2 Size)
            {
                Graphics.Mouse.Initialize(Size);
            }
            /// <summary>
            /// Load Mouse Graphics Contents
            /// </summary>
            /// <param name="spriteBatch"></param>
            /// <param name="texture"></param>
            public static void LoadGraphicsContents(SpriteBatch spriteBatch, Texture2D texture)
            {
                Graphics.Mouse.LoadGraphicsContents(spriteBatch, texture);
            }
            /// <summary>
            /// Update The Mouse Picture And Position
            /// </summary>
            public static void Update()
            {
                Graphics.Mouse.Update();
            }
            /// <summary>
            /// Draw The Mouse Image
            /// </summary>          
            public static void Draw()
            {
                Graphics.Mouse.Draw();
            }
            /// <summary>
            /// Set The Mouse Image
            /// </summary>
            /// <param name="img">Image To Bet Set</param>
            public static void SetImage(Graphics.Image img)
            {
                Graphics.Mouse.SetImage(img);
            }
        }
        #endregion
        #region Properties
        /// <summary>
        /// Get The MouseState
        /// </summary>
      
        public static MouseState State
        {
            get { return Microsoft.Xna.Framework.Input.Mouse.GetState() ; }
        }
      
        /// <summary>
        /// Get Or Set The Mouse Position
        /// </summary>
        public static Vector2 Position
        {
            get {return new Vector2(State.X,State.Y) ;}
            set { Microsoft.Xna.Framework.Input.Mouse.SetPosition((int)value.X, (int)value.Y); }
        }
       
        #endregion
        #region Proprities
        /// <summary>
        /// Get The X Mouse Position
        /// </summary>
        public static int X { get { return x; } }
        /// <summary>
        /// Get The Y Mouse Position
        /// </summary>
        public static int Y { get { return y; } }
        /// <summary>
        /// Get The Relative X Mouse Position
        /// </summary>
        public static int RelativeX { get { return x - previousX; } }
        /// <summary>
        /// Get The Relative Y Mouse Position
        /// </summary>
        public static int RelativeY { get { return y - previousY; } }
        /// <summary>
        /// Return true If The Mouse Moved
        /// </summary>
        public static bool Moved { get { return x != previousX || y != previousY; } }

        public static int Wheel { get { return wheel; } }
        public static int RelativeWheel { get { return wheel - previousWheel; } }
        #endregion
        #region Properties
        //Indicate whether the specified buttons are being held down
        public static  bool HoldingLeftButton { get { return IsHolding(ButtonFlags.LeftButton); } }
        public static bool HoldingMiddleButton { get { return IsHolding(ButtonFlags.MiddleButton); } }
        public static bool HoldingRightButton { get { return IsHolding(ButtonFlags.RightButton); } }
        public static bool HoldingXButton1 { get { return IsHolding(ButtonFlags.XButton1); } }
        public static bool HoldingXButton2 { get { return IsHolding(ButtonFlags.XButton2); } }
        public static bool HoldingAnyButton { get { return (previousButtons & buttons) != 0; } }

        //Indicate whether the specified buttons became pressed between the last and current frames
        public static bool PressedLeftButton { get { return WasPressed(ButtonFlags.LeftButton); } }
        public static bool PressedMiddleButton { get { return WasPressed(ButtonFlags.MiddleButton); } }
        public static bool PressedRightButton { get { return WasPressed(ButtonFlags.RightButton); } }
        public static bool PressedXButton1 { get { return WasPressed(ButtonFlags.XButton1); } }
        public static bool PressedXButton2 { get { return WasPressed(ButtonFlags.XButton2); } }
        public static bool PressedAnyButton { get { return pressedButtons != 0; } }

        //Indicate whether the specified buttons became released between the last and current frames
        public static bool ReleasedLeftButton { get { return WasReleased(ButtonFlags.LeftButton); } }
        public static bool ReleasedMiddleButton { get { return WasReleased(ButtonFlags.MiddleButton); } }
        public static bool ReleasedRightButton { get { return WasReleased(ButtonFlags.RightButton); } }
        public static bool ReleasedXButton1 { get { return WasReleased(ButtonFlags.XButton1); } }
        public static bool ReleasedXButton2 { get { return WasReleased(ButtonFlags.XButton2); } }
        public static bool ReleasedAnyButton { get { return releasedButtons != 0; } }
    #endregion
        #region Methods
        /// <summary>
        /// Set Mouse Position
        /// </summary>
        /// <param name="x">X Position</param>
        /// <param name="y">Y Position</param>
        public static void SetMousePosition(int x, int y)
        {
            Microsoft.Xna.Framework.Input.Mouse.SetPosition(x, y);
        }
        /// <summary>
        /// Initialize The Mouse State
        /// </summary>
        public static void Initialize()
        {
            buttons = 0;
            x = y = wheel = 0;

            pressedButtons = releasedButtons = previousButtons = ButtonFlags.None;
            previousX = previousY = previousWheel = 0;

            Updated = false;
        }
        /// <summary>
        /// Updates the internal or/and Mouse Graphics state. This should be called once per game frame
        /// </summary>
        public static void Update(bool graphics)
        {
            #if !XBOX

            MouseState state = Microsoft.Xna.Framework.Input.Mouse.GetState();

            //Hold onto the previous mouse buttons and position
            previousButtons = buttons;
            previousX = x;
            previousY = y;
            previousWheel = wheel;

            //Get button presses, mouse position, and wheel value
            buttons = ButtonsToFlags(state);
            x = state.X;
            y = state.Y;
            wheel = state.ScrollWheelValue;

            if (!Updated)
            {
                //If the Update() has never been called before, the previous mouse coordinates
                //are set to the current coordinates to avoid an initial jump

                previousX = x;
                previousY = y;

                Updated = true;
            }

            //Get the buttons that have been pressed and released since the last call
            pressedButtons = (previousButtons ^ buttons) & buttons;
            releasedButtons = (previousButtons & ~buttons) & ~buttons;
            if (graphics)
                Graphic.Update();
            #endif
        }
        #endregion
        #region Functions
        /// <summary>
        /// Determines if the specified button is pressed in this Update
        /// </summary>
        /// <param name="button">Flag of the button to check.</param>
        /// <returns>Returns true if the button is currently pressed, false otherwise.</returns>
        public static bool IsPressed(ButtonFlags button)
        {
            return (buttons & button) == button;
        }

        /// <summary>
        /// Determines if the specified button is being held down
        /// </summary>
        /// <param name="button">Flag of the button to check.</param>
        /// <returns>Returns true if the button is being held down, false otherwise.</returns>
        public static bool IsHolding(ButtonFlags button)
        {
            return (previousButtons & buttons & button) == button;
        }

        /// <summary>
        /// Determines if the specified button became pressed between the last 
        /// Update and the current Update
        /// </summary>
        /// <param name="button">Flag of the button to check.</param>
        /// <returns>Returns true if the button became pressed, false otherwise.</returns>
        public static bool WasPressed(ButtonFlags button)
        {
            return (pressedButtons & button) == button;
        }

        /// <summary>
        /// Determines if the specified button became released between the last 
        /// Update and the current Update
        /// </summary>
        /// <param name="button">Flag of the button to check.</param>
        /// <returns>Returns true if the button became released, false otherwise.</returns>
        public static bool WasReleased(ButtonFlags button)
        {
            return (releasedButtons & button) == button;
        }
        #endregion

      
        private static ButtonFlags ButtonsToFlags(MouseState state)
        {
            ButtonFlags flags = 0;

            if (state.LeftButton == ButtonState.Pressed) flags |= ButtonFlags.LeftButton;
            if (state.MiddleButton == ButtonState.Pressed) flags |= ButtonFlags.MiddleButton;
            if (state.RightButton == ButtonState.Pressed) flags |= ButtonFlags.RightButton;
            if (state.XButton1 == ButtonState.Pressed) flags |= ButtonFlags.XButton1;
            if (state.XButton2 == ButtonState.Pressed) flags |= ButtonFlags.XButton2;
            
            return flags;
        }
      

        [Flags()]
        public enum ButtonFlags
        {
            None = 0,
            LeftButton = 0x1,
            MiddleButton = 0x2,
            RightButton = 0x4,
            XButton1 = 0x8,
            XButton2 = 0x10
        };
        #region Fields
        /// <summary>
        /// The current state of the buttons, position, and wheel
        /// </summary>
        private static ButtonFlags buttons;
        private static int x;
        private static int y;
        private static int wheel;
        
        /// <summary>
        /// The buttons that were not pressed in the previous Update but are 
        /// pressed in this Update
        /// </summary>
        private static ButtonFlags pressedButtons;

        /// <summary>
        /// The buttons that were pressed in the previous Update but are not
        /// pressed in this Update
        /// </summary>
        private static ButtonFlags releasedButtons;

        /// <summary>
        /// The previous state of the buttons and position
        /// </summary>
        private static ButtonFlags previousButtons;
        private static int previousX;
        private static int previousY;
        private static int previousWheel;

        /// <summary>
        /// Indicates whether Update() has ever been called before
        /// </summary>
        private static bool Updated;
        #endregion

    }
#endif
}
