#region File Description
//-----------------------------------------------------------------------------
// File:      TextButton.cs
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
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Chimera.GUI.InputEventSystem;
#endregion

namespace Chimera.GUI.WindowSystem
{
    /// <summary>
    /// A push button comprised of a graphical Bar component, with a text label
    /// on top.
    /// </summary>
    public class TextButton : UIComponent
    {
        #region Default Properties
        private static int defaultWidth = 65;
        private static int defaultHeight = 25;
        private static int defaultEdgeSize = 12;
        private static string defaultFont = "Content/Fonts/DefaultFont";
        private static Rectangle defaultSkin = new Rectangle(1, 142, 25, 25);
        private static Rectangle defaultHoverSkin = new Rectangle(27, 142, 25, 25);
        private static Rectangle defaultPressedSkin = new Rectangle(52, 142, 25, 25);

        /// <summary>
        /// Sets the default button width.
        /// </summary>
        /// <value>Must be greater than 0.</value>
        public static int DefaultWidth
        {
            set
            {
                Debug.Assert(value > 0);
                defaultWidth = value;
            }
        }

        /// <summary>
        /// Sets the default button height.
        /// </summary>
        /// <value>Must be greater than 0.</value>
        public static int DefaultHeight
        {
            set
            {
                Debug.Assert(value > 0);
                defaultHeight = value;
            }
        }

        /// <summary>
        /// Sets the default size of left and right sides of button.
        /// </summary>
        /// <value>Must be greater than 0.</value>
        public static int DefaultEdgeSize
        {
            set
            {
                Debug.Assert(value > 0);
                defaultEdgeSize = value;
            }
        }

        /// <summary>
        /// Sets the default button font.
        /// </summary>
        /// <value>Must not be null.</value>
        public static string DefaultFont
        {
            set
            {
                Debug.Assert(value != null);
                defaultFont = value;
            }
        }

        /// <summary>
        /// Sets the default control skin.
        /// </summary>
        public static Rectangle DefaultSkin
        {
            set { defaultSkin = value; }
        }

        /// <summary>
        /// Sets the default control hover skin.
        /// </summary>
        public static Rectangle DefaultHoverSkin
        {
            set { defaultHoverSkin = value; }
        }

        /// <summary>
        /// Sets the default control pressed skin.
        /// </summary>
        public static Rectangle DefaultPressedSkin
        {
            set { defaultPressedSkin = value; }
        }
        #endregion

        #region Fields
        private Bar buttonBar;
        private Label label;
        #endregion

        #region Properties
        /// <summary>
        /// Sets the control skin.
        /// </summary>
        public Rectangle Skin
        {
            set { this.buttonBar.SetSkinLocation(0, value); }
        }

        /// <summary>
        /// Sets the control hover skin.
        /// </summary>
        public Rectangle HoverSkin
        {
            set { this.buttonBar.SetSkinLocation(1, value); }
        }

        /// <summary>
        /// Sets the control pressed skin.
        /// </summary>
        public Rectangle PressedSkin
        {
            set { this.buttonBar.SetSkinLocation(2, value); }
        }

        /// <summary>
        /// Get/Set size of left and right parts of the button.
        /// </summary>
        /// <value>Must be at least 0.</value>
        public int EdgeSize
        {
            get { return this.buttonBar.EdgeSize; }
            set { this.buttonBar.EdgeSize = value; }
        }

        /// <summary>
        /// Get/Set the button text.
        /// </summary>
        /// <value>Must not be null.</value>
        public string Text
        {
            get { return this.label.Text; }
            set
            {
                this.label.Text = value;
                RefreshLabelPosition();
            }
        }

        /// <summary>
        /// The font used to draw button text.
        /// </summary>
        /// <value>Must not be a valid path.</value>
        public string Font
        {
            set
            {
                label.Font = value;
                RefreshLabelPosition();
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="game">The currently running Game object.</param>
        /// <param name="guiManager">GUIManager that this control is part of.</param>
        public TextButton(Game game, GUIManager guiManager)
            : base(game, guiManager)
        {
            #region Create Child Controls
            this.buttonBar = new Bar(game, guiManager);
            this.label = new Label(game, guiManager);
            #endregion

            #region Add Child Controls
            Add(this.buttonBar);
            Add(this.label);
            #endregion

            #region Set Properties
            this.buttonBar.IsVertical = false;
            MinWidth = defaultHeight;
            MinHeight = defaultHeight;
            #endregion

            #region Set Default Properties
            Width = defaultWidth;
            Height = defaultHeight;
            EdgeSize = defaultEdgeSize;
            Skin = defaultSkin;
            HoverSkin = defaultHoverSkin;
            PressedSkin = defaultPressedSkin;
            #endregion
        }
        #endregion

        /// <summary>
        /// Loads the default font.
        /// </summary>
        protected override void LoadContent()
        {
            Font = defaultFont;

            base.LoadContent();
        }

        /// <summary>
        /// Sets the label sizes, and centres it on the button.
        /// </summary>
        private void RefreshLabelPosition()
        {
            this.label.Width = this.label.TextWidth;
            this.label.Height = this.label.TextHeight;
            // Centre
            this.label.X = (this.Width - this.label.Width) / 2;
            this.label.Y = (this.Height - this.label.Height) / 2;
        }

        #region Event Handlers
        /// <summary>
        /// Updates button skin to reflect current mouse state.
        /// </summary>
        /// <param name="args">Mouse event arguments.</param>
        protected override void OnMouseOver(MouseEventArgs args)
        {
            base.OnMouseOver(args);

            if (IsPressed)
                this.buttonBar.CurrentSkinState = SkinState.Pressed;
            else
                this.buttonBar.CurrentSkinState = SkinState.Hover;
        }

        /// <summary>
        /// Updates button skin to reflect current mouse state.
        /// </summary>
        /// <param name="args">Mouse event arguments.</param>
        protected override void OnMouseOut(MouseEventArgs args)
        {
            base.OnMouseOut(args);
            this.buttonBar.CurrentSkinState = SkinState.Normal;
        }

        /// <summary>
        /// Updates button skin to reflect current mouse state.
        /// </summary>
        /// <param name="args">Mouse event arguments.</param>
        protected override void OnMouseDown(MouseEventArgs args)
        {
            base.OnMouseDown(args);
            if (args.Button == MouseButtons.Left)
                this.buttonBar.CurrentSkinState = SkinState.Pressed;
        }

        /// <summary>
        /// Updates button skin to reflect current mouse state.
        /// </summary>
        /// <param name="args">Mouse event arguments.</param>
        protected override void OnMouseUp(MouseEventArgs args)
        {
            base.OnMouseUp(args);
            if (args.Button == MouseButtons.Left)
            {
                if (CheckCoordinates(args.Position.X, args.Position.Y))
                    this.buttonBar.CurrentSkinState = SkinState.Hover;
                else
                    this.buttonBar.CurrentSkinState = SkinState.Normal;
            }
        }

        /// <summary>
        /// Refresh child controls when control is resized.
        /// </summary>
        /// <param name="sender">Resized control.</param>
        protected override void OnResize(UIComponent sender)
        {
            base.OnResize(sender);

            if (this.buttonBar != null)
            {
                this.buttonBar.Width = Width;
                this.buttonBar.Height = Height;
            }

            if (this.label != null)
                RefreshLabelPosition();
        }
        #endregion
    }
}