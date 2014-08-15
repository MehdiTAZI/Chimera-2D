#region Info/Author
//----------------------------------------------------------------------------
// Author:    Tazi Mehdi
// Source:    Chimera 2D GAMES ENGINE
// Info:      GameDPad 
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

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Chimera.Input
{
    /// <summary>
    /// This Class Allow You To Manages the changing state of a gamePad for a particular player index.
    /// Be sure to call Update() every frame before accessing the data.
    /// </summary>
    public static class GamePad 
    {
        #region MainFunction
        /// <summary>
        /// Initializes the GamePad with the specified player index.
        /// </summary>
        /// <param name="player">
        /// The player index to which this gamePad mapds.
        /// </param>
        public static void Initialize(PlayerIndex _playerIndex)
        {
            playerIndex = _playerIndex;
        }

        public static void Update()
        {
            GamePadState state = Microsoft.Xna.Framework.Input.GamePad.GetState(playerIndex);

            //Track insertion and removals
            bool wasPreviouslyConnected = isConnected;
            isConnected = state.IsConnected;
            wasRemoved = wasPreviouslyConnected && !isConnected;
            wasInserted = !wasPreviouslyConnected && isConnected;

            if (wasInserted)
                capabilities = Microsoft.Xna.Framework.Input.GamePad.GetCapabilities(playerIndex);

            //Update everything else
            if (isConnected)
            {
                previousButtons = buttons;

                //Get button, DPad, Thumbstick and trigger presses
                buttons = GetButtons(state);
                leftThumbstick = state.ThumbSticks.Left;
                rightThumbstick = state.ThumbSticks.Right;
                leftTrigger = state.Triggers.Left;
                rightTrigger = state.Triggers.Right;

                //Get the buttons that have been pressed and released since the last call
                pressedButtons = (previousButtons ^ buttons) & buttons;
                releasedButtons = (previousButtons & ~buttons) & ~buttons;
            }
            else
            {
                buttons = 0;
                leftThumbstick.X = leftThumbstick.Y = 0;
                rightThumbstick.X = rightThumbstick.Y = 0;
                leftTrigger = 0;
                rightTrigger = 0;

                pressedButtons = 0;
                releasedButtons = 0;
                previousButtons = 0;
            }
        }

        /// <summary>
        /// Sets the gamePad vibration parameters.
        /// </summary>
        public static void SetVibration(float leftMotor, float rightMotor)
        {
            Microsoft.Xna.Framework.Input.GamePad.SetVibration(playerIndex, leftMotor, rightMotor);
        }

        /// <summary>
        /// Determines if the specified button is pressed in this Update
        /// </summary>
        /// <param name="button">Flag of the button to check.</param>
        /// <returns>Returns true if the button is currently pressed, false otherwise.</returns>
        public static bool IsPressed(Buttons button)
        {
            return (buttons & button) == button;
        }

        /// <summary>
        /// Determines if the specified button is being held down
        /// </summary>
        /// <param name="button">Flag of the button to check.</param>
        /// <returns>Returns true if the button is being held down, false otherwise.</returns>
        public static bool IsHolding(Buttons button)
        {
            return (previousButtons & buttons & button) == button;
        }

        /// <summary>
        /// Determines if the specified button became pressed between the last 
        /// Update and the current Update
        /// </summary>
        /// <param name="button">Flag of the button to check.</param>
        /// <returns>Returns true if the button became pressed, false otherwise.</returns>
        public static bool WasPressed(Buttons button)
        {
            return (pressedButtons & button) == button;
        }
        /// <summary>
        /// Determines if the specified button became released between the last 
        /// Update and the current Update
        /// </summary>
        /// <param name="button">Flag of the button to check.</param>
        /// <returns>Returns true if the button became released, false otherwise.</returns>
        public static bool WasReleased(Buttons button)
        {
            return (releasedButtons & button) == button;
        }
        private static Buttons GetButtons(GamePadState state)
        {
            return
                ButtonsToFlags(state.Buttons) |
                DPadToFlags(state.DPad) |
                TriggersToFlags(state.Triggers) |
                ThumbsticksToFlags(state.ThumbSticks);
        }
        private static Buttons ButtonsToFlags(GamePadButtons buttons)
        {
            Buttons flags = 0;

            if (buttons.A == ButtonState.Pressed) flags |= Buttons.A;
            if (buttons.B == ButtonState.Pressed) flags |= Buttons.B;
            if (buttons.X == ButtonState.Pressed) flags |= Buttons.X;
            if (buttons.Y == ButtonState.Pressed) flags |= Buttons.Y;
            if (buttons.Back == ButtonState.Pressed) flags |= Buttons.Back;
            if (buttons.Start == ButtonState.Pressed) flags |= Buttons.Start;
            if (buttons.LeftShoulder == ButtonState.Pressed) flags |= Buttons.LeftShoulder;
            if (buttons.LeftStick == ButtonState.Pressed) flags |= Buttons.LeftStick;
            if (buttons.RightShoulder == ButtonState.Pressed) flags |= Buttons.RightShoulder;
            if (buttons.RightStick == ButtonState.Pressed) flags |= Buttons.RightStick;

            return flags;
        }
        private static Buttons DPadToFlags(GamePadDPad dDPad)
        {
            Buttons flags = 0;

            if (dDPad.Up == ButtonState.Pressed) flags |= Buttons.DPadUp;
            if (dDPad.Right == ButtonState.Pressed) flags |= Buttons.DPadRight;
            if (dDPad.Down == ButtonState.Pressed) flags |= Buttons.DPadDown;
            if (dDPad.Left == ButtonState.Pressed) flags |= Buttons.DPadLeft;

            return flags;
        }
        private static Buttons TriggersToFlags(GamePadTriggers triggers)
        {
            Buttons flags = 0;

            if (triggers.Left > 0) flags |= Buttons.LeftTrigger;
            if (triggers.Right > 0) flags |= Buttons.RightTrigger;

            return flags;
        }
        private static Buttons ThumbsticksToFlags(GamePadThumbSticks Thumbsticks)
        {
            Buttons flags = 0;

            if (Thumbsticks.Left.Y > 0) flags |= Buttons.LeftThumbstickUp;
            if (Thumbsticks.Left.X > 0) flags |= Buttons.LeftThumbstickRight;
            if (Thumbsticks.Left.Y < 0) flags |= Buttons.LeftThumbstickDown;
            if (Thumbsticks.Left.X < 0) flags |= Buttons.LeftThumbstickLeft;

            if (Thumbsticks.Right.Y > 0) flags |= Buttons.RightThumbstickUp;
            if (Thumbsticks.Right.X > 0) flags |= Buttons.RightThumbstickRight;
            if (Thumbsticks.Right.Y < 0) flags |= Buttons.RightThumbstickDown;
            if (Thumbsticks.Right.X < 0) flags |= Buttons.RightThumbstickLeft;

            return flags;
        }
#endregion
        #region Properties
        public static PlayerIndex PlayerIndex { get { return playerIndex; } }

        public static GamePadCapabilities Capabilities { get { return capabilities; } }

        public static Vector2 LeftThumbstick { get { return leftThumbstick; } }
        public static Vector2 RightThumbstick { get { return rightThumbstick; } }

        public static float LeftTrigger { get { return leftTrigger; } }
        public static float RightTrigger { get { return rightTrigger; } }

        //Indicate whether the specified buttons are being pressed right now
        #region Pressing
        public static bool PressingA { get { return IsPressed(Buttons.A); } }
        public static bool PressingB { get { return IsPressed(Buttons.B); } }
        public static bool PressingX { get { return IsPressed(Buttons.X); } }
        public static bool PressingY { get { return IsPressed(Buttons.Y); } }
        public static bool PressingBack { get { return IsPressed(Buttons.Back); } }
        public static bool PressingStart { get { return IsPressed(Buttons.Start); } }
        public static bool PressingLeftShoulder { get { return IsPressed(Buttons.LeftShoulder); } }
        public static bool PressingLeftThumbstick { get { return IsPressed(Buttons.LeftStick); } }
        public static bool PressingRightShoulder { get { return IsPressed(Buttons.RightShoulder); } }
        public static bool PressingRightThumbstick { get { return IsPressed(Buttons.RightStick); } }

        public static bool PressingDPadUp { get { return IsPressed(Buttons.DPadUp); } }
        public static bool PressingDPadRight { get { return IsPressed(Buttons.DPadRight); } }
        public static bool PressingDPadDown { get { return IsPressed(Buttons.DPadDown); } }
        public static bool PressingDPadLeft { get { return IsPressed(Buttons.DPadLeft); } }

        public static bool PressingLeftTrigger { get { return IsPressed(Buttons.LeftTrigger); } }
        public static bool PressingRightTrigger { get { return IsPressed(Buttons.RightTrigger); } }

        public static bool PressingLeftThumbstickUp { get { return IsPressed(Buttons.LeftThumbstickUp); } }
        public static bool PressingLeftThumbstickRight { get { return IsPressed(Buttons.LeftThumbstickRight); } }
        public static bool PressingLeftThumbstickDown { get { return IsPressed(Buttons.LeftThumbstickDown); } }
        public static bool PressingLeftThumbstickLeft { get { return IsPressed(Buttons.LeftThumbstickLeft); } }

        public static bool PressingRightThumbstickUp { get { return IsPressed(Buttons.RightThumbstickUp); } }
        public static bool PressingRightThumbstickRight { get { return IsPressed(Buttons.RightThumbstickRight); } }
        public static bool PressingRightThumbstickDown { get { return IsPressed(Buttons.RightThumbstickDown); } }
        public static bool PressingRightThumbstickLeft { get { return IsPressed(Buttons.RightThumbstickLeft); } }
        #endregion
        //Indicate whether the specified buttons are being held down
        #region Holding
        public static bool HoldingA { get { return IsHolding(Buttons.A); } }
        public static bool HoldingB { get { return IsHolding(Buttons.B); } }
        public static bool HoldingX { get { return IsHolding(Buttons.X); } }
        public static bool HoldingY { get { return IsHolding(Buttons.Y); } }
        public static bool HoldingBack { get { return IsHolding(Buttons.Back); } }
        public static bool HoldingStart { get { return IsHolding(Buttons.Start); } }
        public static bool HoldingLeftShoulder { get { return IsHolding(Buttons.LeftShoulder); } }
        public static bool HoldingLeftThumbstick { get { return IsHolding(Buttons.LeftStick); } }
        public static bool HoldingRightShoulder { get { return IsHolding(Buttons.RightShoulder); } }
        public static bool HoldingRightThumbstick { get { return IsHolding(Buttons.RightStick); } }

        public static bool HoldingDPadUp { get { return IsHolding(Buttons.DPadUp); } }
        public static bool HoldingDPadRight { get { return IsHolding(Buttons.DPadRight); } }
        public static bool HoldingDPadDown { get { return IsHolding(Buttons.DPadDown); } }
        public static bool HoldingDPadLeft { get { return IsHolding(Buttons.DPadLeft); } }

        public static bool HoldingLeftTrigger { get { return IsHolding(Buttons.LeftTrigger); } }
        public static bool HoldingRightTrigger { get { return IsHolding(Buttons.RightTrigger); } }

        public static bool HoldingLeftThumbstickUp { get { return IsHolding(Buttons.LeftThumbstickUp); } }
        public static bool HoldingLeftThumbstickRight { get { return IsHolding(Buttons.LeftThumbstickRight); } }
        public static bool HoldingLeftThumbstickDown { get { return IsHolding(Buttons.LeftThumbstickDown); } }
        public static bool HoldingLeftThumbstickLeft { get { return IsHolding(Buttons.LeftThumbstickLeft); } }

        public static bool HoldingRightThumbstickUp { get { return IsHolding(Buttons.RightThumbstickUp); } }
        public static bool HoldingRightThumbstickRight { get { return IsHolding(Buttons.RightThumbstickRight); } }
        public static bool HoldingRightThumbstickDown { get { return IsHolding(Buttons.RightThumbstickDown); } }
        public static bool HoldingRightThumbstickLeft { get { return IsHolding(Buttons.RightThumbstickLeft); } }
        #endregion
        //Indicate whether the specified buttons became pressed between the last and current frames
        #region Pressed
        public static bool PressedA { get { return WasPressed(Buttons.A); } }
        public static bool PressedB { get { return WasPressed(Buttons.B); } }
        public static bool PressedX { get { return WasPressed(Buttons.X); } }
        public static bool PressedY { get { return WasPressed(Buttons.Y); } }
        public static bool PressedBack { get { return WasPressed(Buttons.Back); } }
        public static bool PressedStart { get { return WasPressed(Buttons.Start); } }
        public static bool PressedLeftShoulder { get { return WasPressed(Buttons.LeftShoulder); } }
        public static bool PressedLeftThumbstick { get { return WasPressed(Buttons.LeftStick); } }
        public static bool PressedRightShoulder { get { return WasPressed(Buttons.RightShoulder); } }
        public static bool PressedRightThumbstick { get { return WasPressed(Buttons.RightStick); } }

        public static bool PressedDPadUp { get { return WasPressed(Buttons.DPadUp); } }
        public static bool PressedDPadRight { get { return WasPressed(Buttons.DPadRight); } }
        public static bool PressedDPadDown { get { return WasPressed(Buttons.DPadDown); } }
        public static bool PressedDPadLeft { get { return WasPressed(Buttons.DPadLeft); } }

        public static bool PressedLeftTrigger { get { return WasPressed(Buttons.LeftTrigger); } }
        public static bool PressedRightTrigger { get { return WasPressed(Buttons.RightTrigger); } }

        public static bool PressedLeftThumbstickUp { get { return WasPressed(Buttons.LeftThumbstickUp); } }
        public static bool PressedLeftThumbstickRight { get { return WasPressed(Buttons.LeftThumbstickRight); } }
        public static bool PressedLeftThumbstickDown { get { return WasPressed(Buttons.LeftThumbstickDown); } }
        public static bool PressedLeftThumbstickLeft { get { return WasPressed(Buttons.LeftThumbstickLeft); } }

        public static bool PressedRightThumbstickUp { get { return WasPressed(Buttons.RightThumbstickUp); } }
        public static bool PressedRightThumbstickRight { get { return WasPressed(Buttons.RightThumbstickRight); } }
        public static bool PressedRightThumbstickDown { get { return WasPressed(Buttons.RightThumbstickDown); } }
        public static bool PressedRightThumbstickLeft { get { return WasPressed(Buttons.RightThumbstickLeft); } }
        #endregion
        //Indicate whether the specified buttons became released between the last and current frames
        #region Released
        public static bool ReleasedA { get { return WasReleased(Buttons.A); } }
        public static bool ReleasedB { get { return WasReleased(Buttons.B); } }
        public static bool ReleasedX { get { return WasReleased(Buttons.X); } }
        public static bool ReleasedY { get { return WasReleased(Buttons.Y); } }
        public static bool ReleasedBack { get { return WasReleased(Buttons.Back); } }
        public static bool ReleasedStart { get { return WasReleased(Buttons.Start); } }
        public static bool ReleasedLeftShoulder { get { return WasReleased(Buttons.LeftShoulder); } }
        public static bool ReleasedLeftThumbstick { get { return WasReleased(Buttons.LeftStick); } }
        public static bool ReleasedRightShoulder { get { return WasReleased(Buttons.RightShoulder); } }
        public static bool ReleasedRightThumbstick { get { return WasReleased(Buttons.RightStick); } }

        public static bool ReleasedDPadUp { get { return WasReleased(Buttons.DPadUp); } }
        public static bool ReleasedDPadRight { get { return WasReleased(Buttons.DPadRight); } }
        public static bool ReleasedDPadDown { get { return WasReleased(Buttons.DPadDown); } }
        public static bool ReleasedDPadLeft { get { return WasReleased(Buttons.DPadLeft); } }

        public static bool ReleasedLeftTrigger { get { return WasReleased(Buttons.LeftTrigger); } }
        public static bool ReleasedRightTrigger { get { return WasReleased(Buttons.RightTrigger); } }

        public static bool ReleasedLeftThumbstickUp { get { return WasReleased(Buttons.LeftThumbstickUp); } }
        public static bool ReleasedLeftThumbstickRight { get { return WasReleased(Buttons.LeftThumbstickRight); } }
        public static bool ReleasedLeftThumbstickDown { get { return WasReleased(Buttons.LeftThumbstickDown); } }
        public static bool ReleasedLeftThumbstickLeft { get { return WasReleased(Buttons.LeftThumbstickLeft); } }

        public static bool ReleasedRightThumbstickUp { get { return WasReleased(Buttons.RightThumbstickUp); } }
        public static bool ReleasedRightThumbstickRight { get { return WasReleased(Buttons.RightThumbstickRight); } }
        public static bool ReleasedRightThumbstickDown { get { return WasReleased(Buttons.RightThumbstickDown); } }
        public static bool ReleasedRightThumbstickLeft { get { return WasReleased(Buttons.RightThumbstickLeft); } }
        #endregion
        /// <summary>
        /// Indicates whether the gamePad is currently connected
        /// </summary>
        public static bool IsConnected { get { return isConnected; } }

        /// <summary>
        /// Indicates whether the gamePad became connected during the last Update
        /// </summary>
        public static bool WasInserted { get { return wasInserted; } }

        /// <summary>
        /// Indicates whether the gamePad became disconnected during the last Update
        /// </summary>
        public static bool WasRemoved { get { return wasRemoved; } }

        /// <summary>
        /// Updates the internal state. This should be called once per game frame
        /// </summary>
        #endregion
        #region Fields
        /// <summary>
        /// The player index
        /// </summary>
        private static PlayerIndex playerIndex;

        /// <summary>
        /// </summary>
        private static GamePadCapabilities capabilities;

        /// <summary>
        /// The current state of the buttons, Thumbsticks, and triggers
        /// </summary>
        private static Buttons buttons;
        private static Vector2 leftThumbstick;
        private static Vector2 rightThumbstick;
        private static float leftTrigger;
        private static float rightTrigger;

        /// <summary>
        /// The buttons that were not pressed in the previous Update but are 
        /// pressed in this Update
        /// </summary>
        private static Buttons pressedButtons;

        /// <summary>
        /// The buttons that were pressed in the previous Update but are not
        /// pressed in this Update
        /// </summary>
        private static Buttons releasedButtons;

        /// <summary>
        /// The previous state of the buttons and triggers
        /// </summary>
        private static Buttons previousButtons;

        /// <summary>
        /// Indicates whether the controller is currently connected
        /// </summary>
        private static bool isConnected;

        /// <summary>
        /// Indicates whether the controller was inserted this Update
        /// </summary>
        private static bool wasInserted;

        /// <summary>
        /// Indicates whether the controller was removed this Update
        /// </summary>
        private static bool wasRemoved;
        #endregion
    }
}
