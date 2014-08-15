#region File Description
//-----------------------------------------------------------------------------
// File:      ResizableArea.cs
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
    /// <summary>
    /// Possible resizable area types.
    /// </summary>
    public enum ResizeAreas
    {
        TopLeft,
        Top,
        TopRight,
        Left,
        Right,
        BottomLeft,
        Bottom,
        BottomRight
    }

    #region Delegates
    /// <summary>
    /// When a control begins resizing.
    /// </summary>
    /// <param name="sender">Resizing control.</param>
    public delegate void StartResizingHandler(UIComponent sender);
    /// <summary>
    /// When a control finishes resizing.
    /// </summary>
    /// <param name="sender">Resizing control.</param>
    public delegate void EndResizingHandler(UIComponent sender);
    #endregion

    /// <summary>
    /// A non-graphical control that resizes it's parent control when dragged
    /// with the mouse.
    /// </summary>
    public class ResizableArea : UIComponent
    {
        #region Members
        private bool dragging;
        private Point lastLocation;
        private ResizeAreas resizeArea;
        #endregion

        #region Properties
        /// <summary>
        /// Get/Set the type of resizable area.
        /// </summary>
        public ResizeAreas ResizeArea
        {
            get { return resizeArea; }
            set { resizeArea = value; }
        }
        #endregion

        #region Events
        public event StartResizingHandler   StartResizing;
        public event EndResizingHandler     EndResizing;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="game">The currently running Game object.</param>
        /// <param name="guiManager">GUIManager that this control is part of.
        /// </param>
        public ResizableArea(Game game, GUIManager guiManager)
            : base(game, guiManager)
        {
            this.dragging = false;
            this.lastLocation = Point.Zero;
            this.ResizeArea = ResizeAreas.BottomRight;
        }
        #endregion

        /// <summary>
        /// Change the mouse cursor to resizing cursor.
        /// </summary>
        private void ShowResizeCursor()
        {
            // Set new mouse cursor
            switch (this.resizeArea)
            {
                case ResizeAreas.Top:
                case ResizeAreas.Bottom:
                    GUIManager.SetMouseCursor(MouseState.ResizingNS);
                    break;
                case ResizeAreas.Left:
                case ResizeAreas.Right:
                    GUIManager.SetMouseCursor(MouseState.ResizingWE);
                    break;
                case ResizeAreas.TopLeft:
                case ResizeAreas.BottomRight:
                    GUIManager.SetMouseCursor(MouseState.ResizingNWSE);
                    break;
                case ResizeAreas.TopRight:
                case ResizeAreas.BottomLeft:
                    GUIManager.SetMouseCursor(MouseState.ResizingNESW);
                    break;
            }
        }

        #region Event Handlers
        /// <summary>
        /// Change to resize cursor.
        /// </summary>
        /// <param name="args">Mouse event arguments.</param>
        protected override void OnMouseOver(MouseEventArgs args)
        {
            base.OnMouseOver(args);
            ShowResizeCursor();
        }

        /// <summary>
        /// Put cursor back to normal.
        /// </summary>
        /// <param name="args">Mouse event arguments.</param>
        protected override void OnMouseOut(MouseEventArgs args)
        {
            base.OnMouseOut(args);

            // Set back mouse cursor
            GUIManager.SetMouseCursor(MouseState.Normal);
        }

        /// <summary>
        /// Starts dragging, and invokes the StartResizing event.
        /// </summary>
        /// <param name="args">Mouse event arguments.</param>
        protected override void OnMouseDown(MouseEventArgs args)
        {
            if (args.Button == MouseButtons.Left)
            {
                this.dragging = true;
                this.lastLocation = args.Position;

                if (StartResizing != null)
                    StartResizing.Invoke(this);
            }
        }

        /// <summary>
        /// Finishes dragging, and invokes the EndResizing event.
        /// </summary>
        /// <param name="args">Mouse event arguments.</param>
        protected override void OnMouseUp(MouseEventArgs args)
        {
            if (args.Button == MouseButtons.Left)
            {
                this.dragging = false;

                if (EndResizing != null)
                    EndResizing.Invoke(this);
            }
        }

        /// <summary>
        /// Updates parent size if the mouse is being dragged.
        /// </summary>
        /// <param name="args">Mouse event arguments.</param>
        protected override void OnMouseMove(MouseEventArgs args)
        {
            if (this.dragging)
            {
                // By moving parent, all children will automatically update
                // their position, including this control
                if (Parent != null)
                {
                    // Determine which areas are being resized
                    bool top = false;
                    bool bottom = false;
                    bool left = false;
                    bool right = false;

                    if (this.resizeArea == ResizeAreas.Top ||
                        this.resizeArea == ResizeAreas.TopLeft ||
                        this.resizeArea == ResizeAreas.TopRight
                        )
                        top = true;
                    else if (this.resizeArea == ResizeAreas.Bottom ||
                        this.resizeArea == ResizeAreas.BottomLeft ||
                        this.resizeArea == ResizeAreas.BottomRight
                        )
                        bottom = true;

                    if (this.resizeArea == ResizeAreas.Left ||
                        this.resizeArea == ResizeAreas.BottomLeft ||
                        this.resizeArea == ResizeAreas.TopLeft
                        )
                        left = true;
                    else if (this.resizeArea == ResizeAreas.Right ||
                        this.resizeArea == ResizeAreas.BottomRight ||
                        this.resizeArea == ResizeAreas.TopRight
                        )
                        right = true;

                    // Get the position from before resize
                    int beforeX = Parent.Width;
                    int beforeY = Parent.Height;

                    // Do the resizing, and update last mouse position base on
                    // how much the control was actually resized by. Takes into
                    // account minimum sizes.
                    if (left)
                    {
                        Parent.Width -= args.Position.X - lastLocation.X;
                        Parent.X += beforeX - Parent.Width;
                        this.lastLocation.X += beforeX - Parent.Width;
                    }
                    else if (right)
                    {
                        Parent.Width += args.Position.X - lastLocation.X;
                        this.lastLocation.X += Parent.Width - beforeX;
                    }

                    if (top)
                    {
                        Parent.Height -= args.Position.Y - lastLocation.Y;
                        Parent.Y += beforeY - Parent.Height;
                        this.lastLocation.Y += beforeY - Parent.Height;
                    }
                    else if (bottom)
                    {
                        Parent.Height += args.Position.Y - lastLocation.Y;
                        this.lastLocation.Y += Parent.Height - beforeY;
                    }
                }
            }
        }
        #endregion
    }
}