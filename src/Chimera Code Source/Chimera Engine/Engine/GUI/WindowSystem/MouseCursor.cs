#region File Description
//-----------------------------------------------------------------------------
// File:      MouseCursor.cs
// Namespace: Chimera.GUI.WindowSystem
// Author:    Aaron MacDougall
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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Chimera.GUI.InputEventSystem;
#endregion

namespace Chimera.GUI.WindowSystem
{
    /// <summary>
    /// Possible mouse cursors.
    /// </summary>
    public enum MouseState
    {
        /// <summary>
        /// Normal cursor.
        /// </summary>
        Normal,
        /// <summary>
        /// Moving cursor.
        /// </summary>
        Moving,
        /// <summary>
        /// Top/bottom resizing cursor.
        /// </summary>
        ResizingNS,
        /// <summary>
        /// Left/right resizing cursor.
        /// </summary>
        ResizingWE,
        /// <summary>
        /// Top left/bottom right resizing cursor.
        /// </summary>
        ResizingNWSE,
        /// <summary>
        /// Top right/bottom left resizing cursor.
        /// </summary>
        ResizingNESW
    }

    /// <summary>
    /// Maintains the mouse state, and draws the mouse cursor.
    /// </summary>
    internal class MouseCursor : DrawableGameComponent
    {
        #region Default Settings
        private static Rectangle defaultNormalLocation = new Rectangle(114, 67, 12, 21);
        private static Rectangle defaultMovingLocation = new Rectangle(127, 67, 21, 21);
        private static Rectangle defaultResizingNSLocation = new Rectangle(149, 67, 9, 21);
        private static Rectangle defaultResizingWELocation = new Rectangle(159, 67, 21, 9);
        private static Rectangle defaultResizingNWSELocation = new Rectangle(181, 67, 15, 15);
        private static Rectangle defaultResizingNESWLocation = new Rectangle(197, 67, 15, 15);
        private static Point defaultNormalOffset = new Point(0, 0);
        private static Point defaultMovingOffset = new Point(-10, -10);
        private static Point defaultResizingNSOffset = new Point(-4, -10);
        private static Point defaultResizingWEOffset = new Point(-10, -4);
        private static Point defaultResizingNWSEOffset = new Point(-7, -7);
        private static Point defaultResizingNESWOffset = new Point(-7, -7);
        #endregion

        #region Members
        private GUIManager guiManager;
        private IInputEventsService inputEvents;
        private MouseState mouseState;
        private Rectangle source;
        private Point offset;
        private Rectangle normalLocation;
        private Rectangle movingLocation;
        private Rectangle resizingNSLocation;
        private Rectangle resizingWELocation;
        private Rectangle resizingNWSELocation;
        private Rectangle resizingNESWLocation;
        private Point normalOffset;
        private Point movingOffset;
        private Point resizingNSOffset;
        private Point resizingWEOffset;
        private Point resizingNWSEOffset;
        private Point resizingNESWOffset;
        #endregion

        #region Properties
        /// <summary>
        /// Sets the mouse cursor skin location.
        /// </summary>
        public Rectangle NormalLocation
        {
            set
            {
                this.normalLocation = value;
                SetMouseState(mouseState);
            }
        }

        /// <summary>
        /// Sets the mouse cursor skin location.
        /// </summary>
        public Rectangle MovingLocation
        {
            set
            {
                this.movingLocation = value;
                SetMouseState(mouseState);
            }
        }

        /// <summary>
        /// Sets the mouse cursor skin location.
        /// </summary>
        public Rectangle ResizingNSLocation
        {
            set
            {
                this.resizingNSLocation = value;
                SetMouseState(mouseState);
            }
        }

        /// <summary>
        /// Sets the mouse cursor skin location.
        /// </summary>
        public Rectangle ResizingWELocation
        {
            set
            {
                this.resizingWELocation = value;
                SetMouseState(mouseState);
            }
        }

        /// <summary>
        /// Sets the mouse cursor skin location.
        /// </summary>
        public Rectangle ResizingNWSELocation
        {
            set
            {
                this.resizingNWSELocation = value;
                SetMouseState(mouseState);
            }
        }

        /// <summary>
        /// Sets the mouse cursor skin location.
        /// </summary>
        public Rectangle ResizingNESWLocation
        {
            set
            {
                this.resizingNESWLocation = value;
                SetMouseState(mouseState);
            }
        }

        /// <summary>
        /// Sets the offset of the pointer in the skin.
        /// </summary>
        public Point NormalOffset
        {
            set
            {
                this.normalOffset = value;
                SetMouseState(mouseState);
            }
        }

        /// <summary>
        /// Sets the offset of the pointer in the skin.
        /// </summary>
        public Point MovingOffset
        {
            set
            {
                this.movingOffset = value;
                SetMouseState(mouseState);
            }
        }

        /// <summary>
        /// Sets the offset of the pointer in the skin.
        /// </summary>
        public Point ResizingNSOffset
        {
            set
            {
                this.resizingNSOffset = value;
                SetMouseState(mouseState);
            }
        }

        /// <summary>
        /// Sets the offset of the pointer in the skin.
        /// </summary>
        public Point ResizingWEOffset
        {
            set
            {
                this.resizingWEOffset = value;
                SetMouseState(mouseState);
            }
        }

        /// <summary>
        /// Sets the offset of the pointer in the skin.
        /// </summary>
        public Point ResizingNWSEOffset
        {
            set
            {
                this.resizingNWSEOffset = value;
                SetMouseState(mouseState);
            }
        }

        /// <summary>
        /// Sets the offset of the pointer in the skin.
        /// </summary>
        public Point ResizingNESWOffset
        {
            set
            {
                this.resizingNESWOffset = value;
                SetMouseState(mouseState);
            }
        }
        #endregion

        /// <summary>
        /// Constructor adds game component, and sets up initial cursor.
        /// </summary>
        /// <param name="game">The currently running Game object.</param>
        /// <param name="guiManager">Associated GUIManager object.</param>
        public MouseCursor(Game game, GUIManager guiManager)
            : base(game)
        {
            this.guiManager = guiManager;
            // Get input event system
            this.inputEvents = (IInputEventsService)Game.Services.GetService(typeof(IInputEventsService));

            // Ensure mouse is ALWAYS drawn on top
            DrawOrder = int.MaxValue;

            #region Set Default Cursors
            SetSkinLocation(MouseState.Normal, defaultNormalLocation, defaultNormalOffset);
            SetSkinLocation(MouseState.Moving, defaultMovingLocation, defaultMovingOffset);
            SetSkinLocation(MouseState.ResizingNS, defaultResizingNSLocation, defaultResizingNSOffset);
            SetSkinLocation(MouseState.ResizingWE, defaultResizingWELocation, defaultResizingWEOffset);
            SetSkinLocation(MouseState.ResizingNWSE, defaultResizingNWSELocation, defaultResizingNWSEOffset);
            SetSkinLocation(MouseState.ResizingNESW, defaultResizingNESWLocation, defaultResizingNESWOffset);
            #endregion

            SetMouseState(MouseState.Normal);
        }

        /// <summary>
        /// Sets the skin location for a particular cursor.
        /// </summary>
        /// <param name="state">Mouse state.</param>
        private void SetSkinLocation(MouseState state, Rectangle location, Point offset)
        {
            if (UIComponent.CheckSkinLocation(location))
            {
                switch (state)
                {
                    case MouseState.Normal:
                        this.normalLocation = location;
                        this.normalOffset = offset;
                        break;
                    case MouseState.Moving:
                        this.movingLocation = location;
                        this.movingOffset = offset;
                        break;
                    case MouseState.ResizingNS:
                        this.resizingNSLocation = location;
                        this.resizingNSOffset = offset;
                        break;
                    case MouseState.ResizingWE:
                        this.resizingWELocation = location;
                        this.resizingWEOffset = offset;
                        break;
                    case MouseState.ResizingNWSE:
                        this.resizingNWSELocation = location;
                        this.resizingNWSEOffset = offset;
                        break;
                    case MouseState.ResizingNESW:
                        this.resizingNESWLocation = location;
                        this.resizingNESWOffset = offset;
                        break;
                }
            }
        }

        /// <summary>
        /// Changes the current mouse state, and therefore cursor as well.
        /// </summary>
        /// <param name="state">New mouse state.</param>
        public void SetMouseState(MouseState state)
        {
            this.mouseState = state;

            switch (mouseState)
            {
                case MouseState.Normal:
                    this.source = this.normalLocation;
                    this.offset = this.normalOffset;
                    break;
                case MouseState.Moving:
                    this.source = this.movingLocation;
                    this.offset = this.movingOffset;
                    break;
                case MouseState.ResizingNS:
                    this.source = this.resizingNSLocation;
                    this.offset = this.resizingNSOffset;
                    break;
                case MouseState.ResizingWE:
                    this.source = this.resizingWELocation;
                    this.offset = this.resizingWEOffset;
                    break;
                case MouseState.ResizingNWSE:
                    this.source = this.resizingNWSELocation;
                    this.offset = this.resizingNWSEOffset;
                    break;
                case MouseState.ResizingNESW:
                    this.source = this.resizingNESWLocation;
                    this.offset = this.resizingNESWOffset;
                    break;
            }
        }

        /// <summary>
        /// Draw the mouse cursor.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        internal void DrawCursor(GameTime gameTime, SpriteBatch sprite)
        {
            sprite.Draw(
                this.guiManager.SkinTexture,
                new Rectangle(
                    this.inputEvents.GetMouseX() + offset.X,
                    this.inputEvents.GetMouseY() + offset.Y,
                    this.source.Width,
                    this.source.Height
                    ),
                this.source,
                Color.White
                );

            base.Draw(gameTime);
        }
    }
}