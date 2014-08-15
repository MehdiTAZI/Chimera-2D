#region File Description
//-----------------------------------------------------------------------------
// File:      MovableArea.cs
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
using Chimera.GUI.InputEventSystem;
#endregion

namespace Chimera.GUI.WindowSystem
{
    #region Delegates
    /// <summary>
    /// When control begins moving.
    /// </summary>
    /// <param name="sender">Moving control.</param>
    public delegate void StartMovingHandler(UIComponent sender);
    /// <summary>
    /// When a control finishes moving.
    /// </summary>
    /// <param name="sender">Moving control.</param>
    public delegate void EndMovingHandler(UIComponent sender);
    #endregion

    /// <summary>
    /// A non-graphical control that moves it's parent control when dragged
    /// with the mouse.
    /// </summary>
    public class MovableArea : UIComponent
    {
        #region Fields
        private bool isDragging;
        private Point lastLocation;
        #endregion

        #region Events
        public event StartMovingHandler StartMoving;
        public event EndMovingHandler EndMoving;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="game">The currently running Game object.</param>
        /// <param name="guiManager">GUIManager that this control is part of.</param>
        public MovableArea(Game game, GUIManager guiManager)
            : base(game, guiManager)
        {
            this.isDragging = false;
            this.lastLocation = Point.Zero;
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// Starts dragging, and invokes the StartMoving event.
        /// </summary>
        /// <param name="args">Mouse event arguments.</param>
        protected override void OnMouseDown(MouseEventArgs args)
        {
            if (args.Button == MouseButtons.Left)
            {
                this.isDragging = true;
                this.lastLocation = args.Position;

                if (StartMoving != null)
                    StartMoving.Invoke(this);
            }
        }

        /// <summary>
        /// Finishes dragging, and invokes the EndMoving event.
        /// </summary>
        /// <param name="args">Mouse event arguments.</param>
        protected override void OnMouseUp(MouseEventArgs args)
        {
            if (args.Button == MouseButtons.Left)
            {
                this.isDragging = false;

                if (EndMoving != null)
                    EndMoving.Invoke(this);
            }
        }

        /// <summary>
        /// Updates parent position if the mouse is being dragged.
        /// </summary>
        /// <param name="args">Mouse event arguments.</param>
        protected override void OnMouseMove(MouseEventArgs args)
        {
            if (this.isDragging)
            {
                int xDiff = args.Position.X - lastLocation.X;
                int yDiff = args.Position.Y - lastLocation.Y;

                if (Parent != null)
                {
                    // By moving parent, all children will automatically update
                    // their position, including this control
                    Parent.X += xDiff;
                    Parent.Y += yDiff;
                }

                this.lastLocation = args.Position;
            }
        }
        #endregion
    }
}