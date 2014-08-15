#region File Description
//-----------------------------------------------------------------------------
// File:      Chimera.GUI.InputEventSystem.cs
// Namespace: Chimera.GUI.InputEventSystem
// Author:    Aaron MacDougall
// Info:      Based on article by John Sedlak.
//-----------------------------------------------------------------------------
#endregion

#region License
//-----------------------------------------------------------------------------
// Copyright (c) 2007, Aaron MacDougall
// All rights reserved.
//
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
//
// * Redistributions of source code must retain the above copyright notice,
//   this list of conditions and the following disclaimer.
//
// * Redistributions in binary form must reproduce the above copyright notice,
//   this list of conditions and the following disclaimer in the documentation
//   and/or other materials provided with the distribution.
//
// * Neither the name of Aaron MacDougall nor the names of its contributors may
//   be used to endorse or promote products derived from this software without
//   specific prior written permission.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
// ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE
// LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR
// CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
// SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS
// INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN
// CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
// ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
// POSSIBILITY OF SUCH DAMAGE.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
#endregion

namespace Chimera.GUI.InputEventSystem
{
    #region Delegates
    public delegate void KeyDownHandler(KeyEventArgs args);
    public delegate void KeyUpHandler(KeyEventArgs args);
    public delegate void MouseDownHandler(MouseEventArgs args);
    public delegate void MouseUpHandler(MouseEventArgs args);
    public delegate void MouseMoveHandler(MouseEventArgs args);
    #endregion

    /// <summary>
    /// An enumeration of the mouse buttons the system handles.
    /// </summary>
    public enum MouseButtons
    {
        None,
        Left,
        Right
    }

    /// <summary>
    /// Interface for other classes to access for input events.
    /// </summary>
    public interface IInputEventsService
    {
        #region Events
        event KeyDownHandler KeyDown;
        event KeyUpHandler KeyUp;
        event MouseDownHandler MouseDown;
        event MouseUpHandler MouseUp;
        event MouseDownHandler RequestingFocus;
        event MouseMoveHandler MouseMove;
        #endregion

        /// <summary>
        /// Retrieves the current mouse x-position.
        /// </summary>
        /// <returns>Current mouse x-position.</returns>
        int GetMouseX();

        /// <summary>
        /// Retrieves the current mouse y-position.
        /// </summary>
        /// <returns>Current mouse y-position.</returns>
        int GetMouseY();
    }

    #region Event Argument Classes
    public class KeyEventArgs
    {
        public Keys Key;
        public bool Control = false;
        public bool Shift = false;
        public bool Alt = false;
    }

    public class MouseEventArgs
    {
#if !XBOX
        public MouseState State;
#endif        
        public MouseButtons Button;
        public Point Position;
    }
    #endregion

    /// <summary>
    /// Checks for input each update, and fires events for other classes to use
    /// for triggering events.
    /// </summary>
    public partial class InputEvents : GameComponent, IInputEventsService
    {
        /// <summary>
        /// Used to track each key status.
        /// </summary>
        protected class InputKey
        {
            public Keys Key;
            public bool Pressed;
            public int Countdown;
        }

        #region Fields
        private const int RepeatDelay = 500;
        private const int RepeatRate = 50;
        private List<InputKey> keys;
        
#if !XBOX
        private MouseState currentMouseState;
#endif
        #endregion

        #region Events
        public event KeyDownHandler KeyDown;
        public event KeyUpHandler KeyUp;
        public event MouseDownHandler MouseDown;
        public event MouseUpHandler MouseUp;
        public event MouseDownHandler RequestingFocus;
        public event MouseMoveHandler MouseMove;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="game">The currently running Game object.</param>
        public InputEvents(Game game)
            : base(game)
        {
            this.keys = new List<InputKey>();
            #if !XBOX
            this.currentMouseState = new MouseState();
            #endif
            game.Services.AddService(typeof(IInputEventsService), this);
        }
        #endregion

        /// <summary>
        /// Sets up key tracking data.
        /// </summary>
        public override void Initialize()
        {
            #if !XBOX
            // Loop through the Keys enumeration
            foreach (string key in Enum.GetNames(typeof(Keys)))
            {
                bool found = false;

                // Search for the key in our list
                foreach (InputKey compareKey in keys)
                {
                    if (compareKey.Key == (Keys)Enum.Parse(typeof(Keys), key))
                        found = true;
                }

                // If it wasn't found, we need to add it
                if (!found)
                {
                    // Create the key instance and set the values
                    InputKey newKey = new InputKey();
                    newKey.Key = (Keys)Enum.Parse(typeof(Keys), key);
                    newKey.Pressed = false;
                    newKey.Countdown = RepeatDelay;

                    // Add the key.
                    this.keys.Add(newKey);
                }
            }
            #endif
            base.Initialize();
        }

        /// <summary>
        /// Goes through each key checking it's state compared to the last
        /// frame, triggering events if necessary. Updates the current mouse
        /// state and triggers events if necessary.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            #region Update Keyboard
            // Create the Arguments class and fill in with the modifier data
            KeyEventArgs keyEvent = new KeyEventArgs();

            foreach (Keys key in Keyboard.GetState().GetPressedKeys())
            {
                if (key == Keys.LeftAlt || key == Keys.RightAlt)
                    keyEvent.Alt = true;
                else if (key == Keys.LeftShift || key == Keys.RightShift)
                    keyEvent.Shift = true;
                else if (key == Keys.LeftControl || key == Keys.RightControl)
                    keyEvent.Control = true;
            }

            // Loop through our keys
            foreach (InputKey key in this.keys)
            {
                // If they are any of the modifier keys, skip them
                if (key.Key == Keys.LeftAlt || key.Key == Keys.RightAlt ||
                    key.Key == Keys.LeftShift || key.Key == Keys.RightShift ||
                    key.Key == Keys.LeftControl || key.Key == Keys.RightControl
                    )
                    continue;

                // Check if the key was pressed
                bool pressed = Keyboard.GetState().IsKeyDown(key.Key);

                // If it was, decrement the countdown for that key
                if (pressed)
                    key.Countdown -= gameTime.ElapsedRealTime.Milliseconds;

                if ((pressed) && (!key.Pressed)) // If it is pressed, but wasn't before...
                {
                    // Set some flags and invoke the KeyDown event
                    key.Pressed = true;
                    keyEvent.Key = key.Key;

                    if (KeyDown != null)
                        KeyDown.Invoke(keyEvent);
                }
                else if ((!pressed) && (key.Pressed)) // If it isn't pressed, but was before...
                {
                    // Set some flags, reset the countdown
                    key.Pressed = false;
                    key.Countdown = RepeatDelay;
                    keyEvent.Key = key.Key;

                    // Invoke the Key Up event
                    if (KeyUp != null)
                        KeyUp.Invoke(keyEvent);
                }

                // If the Key's Countdown has zeroed out, reset it, and make
                // sure that KeyDown fires again
                if (key.Countdown < 0)
                {
                    keyEvent.Key = key.Key;

                    if (KeyDown != null)
                        KeyDown.Invoke(keyEvent);

                    key.Countdown = RepeatRate;
                }
            }
            #endregion

            #region Update Mouse
            #if !XBOX
            MouseState mouseState = Mouse.GetState();
            
            // Check for mouse move event
            if ((mouseState.X != this.currentMouseState.X) || (mouseState.Y != this.currentMouseState.Y))
            {
                if (MouseMove != null)
                {
                    MouseEventArgs mouseEvent = new MouseEventArgs();
                    mouseEvent.State = mouseState;
                    mouseEvent.Button = MouseButtons.None;

                    // Cap mouse position to the window boundaries
                    mouseEvent.Position = new Point(mouseState.X, mouseState.Y);
                    if (mouseEvent.Position.X < 0)
                        mouseEvent.Position.X = 0;
                    if (mouseEvent.Position.Y < 0)
                        mouseEvent.Position.Y = 0;

                    Rectangle bounds = this.Game.Window.ClientBounds;

                    if (mouseEvent.Position.X > bounds.Width)
                        mouseEvent.Position.X = bounds.Width;
                    if (mouseEvent.Position.Y > bounds.Height)
                        mouseEvent.Position.Y = bounds.Height;


                    MouseMove.Invoke(mouseEvent);
                }
            }

            if (mouseState.LeftButton != currentMouseState.LeftButton)
            {
                if ((MouseUp != null) || (MouseDown != null))
                {
                    MouseEventArgs mouseEvent = new MouseEventArgs();
                    mouseEvent.State = mouseState;
                    mouseEvent.Position = new Point(mouseState.X, mouseState.Y);
                    mouseEvent.Button = MouseButtons.Left;

                    if (mouseState.LeftButton == ButtonState.Released)
                    {
                        if (MouseUp != null)
                            MouseUp.Invoke(mouseEvent);
                    }
                    else
                    {
                        if (MouseDown != null)
                        {
                            // Must request focus first, to prevent mousedown
                            // event from being swallowed up
                            if (RequestingFocus != null)
                                RequestingFocus.Invoke(mouseEvent);

                            MouseDown.Invoke(mouseEvent);
                        }
                    }
                }
            }

            if (mouseState.RightButton != currentMouseState.RightButton)
            {
                if ((MouseUp != null) || (MouseDown != null))
                {
                    MouseEventArgs mouseEvent = new MouseEventArgs();
                    mouseEvent.State = mouseState;
                    mouseEvent.Position = new Point(mouseState.X, mouseState.Y);
                    mouseEvent.Button = MouseButtons.Right;

                    if (mouseState.RightButton == ButtonState.Released)
                    {
                        if (MouseUp != null)
                            MouseUp.Invoke(mouseEvent);
                    }
                    else
                    {
                        if (MouseDown != null)
                            MouseDown.Invoke(mouseEvent);
                    }
                }
            }

            // Update mouse state
            this.currentMouseState = mouseState;
            #endif
            #endregion

            base.Update(gameTime);
        }

        #region IInputEventsService Implementation

        /// <summary>
        /// Retrieves the current mouse x-position.
        /// </summary>
        /// <returns>Current mouse x-position.</returns>
        
        public int GetMouseX()
        {
            #if !XBOX
            return currentMouseState.X;
            #else
            return 0;
            #endif
        }

        /// <summary>
        /// Retrieves the current mouse y-position.
        /// </summary>
        /// <returns>Current mouse y-position.</returns>
        public int GetMouseY()
        {
            #if !XBOX
            return currentMouseState.Y;
            #else
            return 0;
            #endif
        }

        #endregion
    }
}