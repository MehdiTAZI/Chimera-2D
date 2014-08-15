#region File Description
//-----------------------------------------------------------------------------
// File:      TextBox.cs
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

#region Using Statement
using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Chimera.GUI.InputEventSystem;
#endregion

namespace Chimera.GUI.WindowSystem
{
    /// <summary>
    /// Graphical textbox control. Very simple, only writes characters and
    /// deletes with the backspace. May be updated in the future.
    /// </summary>
    public class TextBox : UIComponent
    {
        #region Default Properties
        private static int defaultWidth = 260;
        private static int defaultHeight = 25;
        private static int defaultHMargin = 5;
        private static int defaultVMargin = 2;
        private static string defaultFont = "Content/Fonts/DefaultFont";
        private static Rectangle defaultSkin = new Rectangle(84, 41, 25, 25);

        /// <summary>
        /// Sets the default control width.
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
        /// Sets the default control height.
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
        /// Sets the default horizontal padding.
        /// </summary>
        /// <value>Must be at least 0.</value>
        public static int DefaultHMargin
        {
            set
            {
                Debug.Assert(value >= 0);
                defaultHMargin = value;
            }
        }

        /// <summary>
        /// Sets the default vertical padding.
        /// </summary>
        /// <value>Must be at least 0.</value>
        public static int DefaultVMargin
        {
            set
            {
                Debug.Assert(value >= 0);
                defaultVMargin = value;
            }
        }

        /// <summary>
        /// Sets the default text font.
        /// </summary>
        /// <value>Must be a non-empty string.</value>
        public static string DefaultFont
        {
            set
            {
                Debug.Assert(value != null);
                Debug.Assert(value.Length > 0);
                defaultFont = value;
            }
        }

        /// <summary>
        /// Sets the default skin.
        /// </summary>
        public static Rectangle DefaultSkin
        {
            set { defaultSkin = value; }
        }
        #endregion

        #region Fields
        private Box box;
        private Label label;
        private double seconds;
        private int hMargin;
        private int vMargin;
        #endregion

        #region Properties
        /// <summary>
        /// Get/Set whether the user can edit text.
        /// </summary>
        public bool IsEditable
        {
            get { return CanHaveFocus; }
            set
            {
                // Easy way of preventing user from editing text. Simply don't
                // allow the control to receive focus.
                CanHaveFocus = value;
            }
        }

        /// <summary>
        /// Get/Set the horizontal padding between control edge and label.
        /// </summary>
        /// <value>Must be at least 0.</value>
        public int HMargin
        {
            get { return this.hMargin; }
            set
            {
                Debug.Assert(value >= 0);
                this.hMargin = value;
                RefreshMargins();
            }
        }

        /// <summary>
        /// Get/Set the vertical padding between control edge and label.
        /// </summary>
        /// <value>Must be at least 0.</value>
        public int VMargin
        {
            get { return this.vMargin; }
            set
            {
                Debug.Assert(value >= 0);
                this.vMargin = value;
                RefreshMargins();
            }
        }

        /// <summary>
        /// Sets the control skin.
        /// </summary>
        public Rectangle Skin
        {
            set { this.box.SetSkinLocation(0, value); }
        }

        /// <summary>
        /// Sets the text font.
        /// </summary>
        /// <value>Must not be a valid path.</value>
        public string Font
        {
            set
            {
                this.label.Font = value;
            }
        }

        /// <summary>
        /// Get/Set the control text.
        /// </summary>
        /// <value>Must not be null.</value>
        public string Text
        {
            get { return this.label.Text; }
            set { this.label.Text = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="game">The currently running Game object.</param>
        /// <param name="guiManager">GUIManager that this control is part of.
        /// </param>
        public TextBox(Game game, GUIManager guiManager)
            : base(game, guiManager)
        {
            this.seconds = 0.0;

            #region Create Child Controls
            this.box = new Box(game, guiManager);
            this.label = new Label(game, guiManager);
            #endregion

            #region Add Child Controls
            Add(this.box);
            Add(this.label);
            #endregion

            #region Set Default Properties
            Width = defaultWidth;
            Height = defaultHeight;
            HMargin = defaultHMargin;
            VMargin = defaultVMargin;
            Skin = defaultSkin;
            #endregion
        }
        #endregion

        /// <summary>
        /// Load default font.
        /// </summary>
        protected override void LoadContent()
        {
            Font = defaultFont;

            base.LoadContent();
        }

        /// <summary>
        /// Handles the timing for a flashing cursor.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // Update flashing cursor
            if (GUIManager.GetFocus() != this)
            {
                if (this.label.IsCursorShown)
                {
                    this.label.IsCursorShown = false;
                    this.seconds = 0;
                }
            }
            else
            {
                this.seconds += gameTime.ElapsedRealTime.TotalSeconds;

                if (this.seconds > 0.5)
                    this.label.IsCursorShown = false;
                else
                    this.label.IsCursorShown = true;

                if (this.seconds > 1)
                    this.seconds = 0;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Update controls to respect new margins.
        /// </summary>
        protected void RefreshMargins()
        {
            this.label.X = this.hMargin;
            this.label.Width = Width - (this.hMargin * 2);

            this.label.Y = this.vMargin;
            this.label.Height = Height - (this.vMargin * 2);   
        }

        #region Event Handlers
        /// <summary>
        /// Converts key events to text characters, and adds them to the text
        /// label. If backspace key is pressed, it deleted the last character
        /// from the label.
        /// </summary>
        /// <param name="args">Key event arguments.</param>
        protected override void OnKeyDown(KeyEventArgs args)
        {
            base.OnKeyDown(args);

            if (args.Key == Keys.Back)
            {
                if (this.label.Text.Length > 0)
                    this.label.Text = this.label.Text.Substring(
                    0,
                    this.label.Text.Length - 1
                    );
            }
            else if (!args.Alt && !args.Control)
                this.label.Text += GUIManager.KeyToString(args);
        }

        /// <summary>
        /// Update child controls.
        /// </summary>
        /// <param name="sender">Resized control.</param>
        protected override void OnResize(UIComponent sender)
        {
            base.OnResize(sender);

            this.box.Width = Width;
            this.box.Height = Height;

            RefreshMargins();
        }
        #endregion
    }
}