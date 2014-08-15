#region File Description
//-----------------------------------------------------------------------------
// File:      ImageButton.cs
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
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Chimera.GUI.InputEventSystem;
#endregion

namespace Chimera.GUI.WindowSystem
{
    /// <summary>
    /// A button comprised of seperate images for each state.
    /// </summary>
    public class ImageButton : Icon
    {
        #region Fields
        protected bool isChecked = false;
        #endregion

        #region Properties
        /// <summary>
        /// Get/Set whether the button is checked.
        /// </summary>
        public bool IsChecked
        {
            get { return isChecked; }
            set
            {
                this.isChecked = value;

                CurrentSkinState = SkinState.Normal;

                if (isChecked)
                    CurrentSkinState = SkinState.Checked;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="game">The currently running Game object.</param>
        /// <param name="guiManager">GUIManager that this control is part of.</param>
        public ImageButton(Game game, GUIManager guiManager)
            : base(game, guiManager)
        {
            Scale = true;
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// Updates button skin to reflect current mouse state.
        /// </summary>
        /// <param name="args">Mouse event arguments.</param>
        protected override void OnMouseOver(MouseEventArgs args)
        {
            base.OnMouseOver(args);

            if (IsPressed)
            {
                CurrentSkinState = SkinState.Pressed;

                if (this.isChecked)
                    CurrentSkinState = SkinState.CheckedPressed;
            }
            else
            {
                CurrentSkinState = SkinState.Hover;

                if (this.isChecked)
                    CurrentSkinState = SkinState.CheckedHover;
            }
        }

        /// <summary>
        /// Updates button skin to reflect current mouse state.
        /// </summary>
        /// <param name="args">Mouse event arguments.</param>
        protected override void OnMouseOut(MouseEventArgs args)
        {
            base.OnMouseOut(args);

            CurrentSkinState = SkinState.Normal;

            if (this.isChecked)
                CurrentSkinState = SkinState.Checked;
        }

        /// <summary>
        /// Updates button skin to reflect current mouse state.
        /// </summary>
        /// <param name="args">Mouse event arguments.</param>
        protected override void OnMouseDown(MouseEventArgs args)
        {
            base.OnMouseDown(args);

            if (args.Button == MouseButtons.Left)
            {
                CurrentSkinState = SkinState.Pressed;

                if (this.isChecked)
                    CurrentSkinState = SkinState.CheckedPressed;
            }
        }

        /// <summary>
        /// Updates button skin to reflect current mouse state.
        /// </summary>
        /// <param name="args">Mouse event arguments.</param>
        protected override void OnMouseUp(MouseEventArgs args)
        {
            if (args.Button == MouseButtons.Left)
            {
                // Check that the mouse was depressed inside the button
                if (CheckCoordinates(args.Position.X, args.Position.Y))
                {
                    this.isChecked = !this.isChecked;

                    CurrentSkinState = SkinState.Hover;

                    if (this.isChecked)
                        CurrentSkinState = SkinState.CheckedHover;
                }
                else
                {
                    CurrentSkinState = SkinState.Normal;

                    if (this.isChecked)
                        CurrentSkinState = SkinState.Checked;
                }
            }

            base.OnMouseUp(args);
        }
        #endregion
    }
}