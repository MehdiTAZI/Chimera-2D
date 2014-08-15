#region File Description
//-----------------------------------------------------------------------------
// File:      CheckBox.cs
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
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace Chimera.GUI.WindowSystem
{
    /// <summary>
    /// A graphical checkbox button, with a description label.
    /// </summary>
    public class CheckBox : UIComponent
    {
        #region Default Properties
        private static int defaultHeight = 15;
        private static int defaultHMargin = 5;
        private static string defaultFont = "Content/Fonts/DefaultFont";
        private static Rectangle defaultSkin = new Rectangle(1, 27, 15, 15);
        private static Rectangle defaultHoverSkin = new Rectangle(17, 27, 15, 15);
        private static Rectangle defaultPressedSkin = new Rectangle(33, 27, 15, 15);
        private static Rectangle defaultCheckedSkin = new Rectangle(1, 43, 15, 15);
        private static Rectangle defaultCheckedHoverSkin = new Rectangle(17, 43, 15, 15);
        private static Rectangle defaultCheckedPressedSkin = new Rectangle(33, 43, 15, 15);

        /// <summary>
        /// Sets the default control height, which is also the width and height
        /// of the button.
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
        /// Sets the default gap between the button and the label.
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
        /// Sets the default font for CheckBox labels.
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
        /// Sets the default skin.
        /// </summary>
        public static Rectangle DefaultSkin
        {
            set { defaultSkin = value; }
        }

        /// <summary>
        /// Sets the default hover skin.
        /// </summary>
        public static Rectangle DefaultHoverSkin
        {
            set { defaultHoverSkin = value; }
        }

        /// <summary>
        /// Sets the default pressed skin.
        /// </summary>
        public static Rectangle DefaultPressedSkin
        {
            set { defaultPressedSkin = value; }
        }

        /// <summary>
        /// Sets the default checked skin.
        /// </summary>
        public static Rectangle DefaultCheckedSkin
        {
            set { defaultCheckedSkin = value; }
        }

        /// <summary>
        /// Sets the default checked hover skin.
        /// </summary>
        public static Rectangle DefaultCheckedHoverSkin
        {
            set { defaultCheckedHoverSkin = value; }
        }

        /// <summary>
        /// Sets the default checked pressed skin.
        /// </summary>
        public static Rectangle DefaultCheckedPressedSkin
        {
            set { defaultCheckedPressedSkin = value; }
        }
        #endregion

        #region Fields
        private ImageButton button;
        private Label label;
        private int hMargin;
        #endregion

        #region Properties
        /// <summary>
        /// Sets the gap between the button and the label.
        /// </summary>
        /// <value>Must be at least 0.</value>
        public int HMargin
        {
            get { return hMargin; }
            set
            {
                Debug.Assert(value >= 0);

                hMargin = value;
                RefreshMargins();
            }
        }

        /// <summary>
        /// Sets the label font.
        /// </summary>
        /// <value>Must not be a valid path.</value>
        public string Font
        {
            set { label.Font = value; }
        }

        /// <summary>
        /// Get/Set the description text.
        /// </summary>
        /// <value>Must not be null.</value>
        public string Text
        {
            get { return label.Text; }
            set
            {
                label.Text = value;
                // Automatically set control width
                label.Width = label.TextWidth;
                this.Width = label.X + label.Width;
            }
        }

        /// <summary>
        /// Sets the skin.
        /// </summary>
        public Rectangle Skin
        {
            set { this.button.SetSkinLocation(0, value); }
        }

        /// <summary>
        /// Sets the hover skin.
        /// </summary>
        public Rectangle HoverSkin
        {
            set { this.button.SetSkinLocation(1, value); }
        }

        /// <summary>
        /// Sets the pressed skin.
        /// </summary>
        public Rectangle PressedSkin
        {
            set { this.button.SetSkinLocation(2, value); }
        }

        /// <summary>
        /// Sets the checked skin.
        /// </summary>
        public Rectangle CheckedSkin
        {
            set { this.button.SetSkinLocation(3, value); }
        }

        /// <summary>
        /// Sets the checked hover skin.
        /// </summary>
        public Rectangle CheckedHoverSkin
        {
            set { this.button.SetSkinLocation(4, value); }
        }

        /// <summary>
        /// Sets the checked pressed skin.
        /// </summary>
        public Rectangle CheckedPressedSkin
        {
            set { this.button.SetSkinLocation(5, value); }
        }

        /// <summary>
        /// Get/Set whether the button is currently checked.
        /// </summary>
        public bool IsChecked
        {
            get { return button.IsChecked; }
            set { button.IsChecked = value; }
        }

        /// <summary>
        /// Gets the checkbox button.
        /// </summary>
        protected ImageButton Button
        {
            get { return button; }
        }
        #endregion

        #region Events
        // Event is simply used to relay clicks on the button
        new public event ClickHandler Click;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="game">The currently running Game object.</param>
        /// <param name="guiManager">GUIManager that this control is part of.
        /// </param>
        public CheckBox(Game game, GUIManager guiManager)
            : base(game, guiManager)
        {
            #region Create Child Controls
            this.button = new ImageButton(game, guiManager);
            this.label = new Label(game, guiManager);
            #endregion

            #region Add Child Controls
            Add(this.button);
            Add(this.label);
            #endregion

            #region Set Properties
            CanHaveFocus = false;
            #endregion

            #region Set Default Properties
            Width = defaultHeight;
            Height = defaultHeight;
            HMargin = defaultHMargin;
            Skin = defaultSkin;
            HoverSkin = defaultHoverSkin;
            PressedSkin = defaultPressedSkin;
            CheckedSkin = defaultCheckedSkin;
            CheckedHoverSkin = defaultCheckedHoverSkin;
            CheckedPressedSkin = defaultCheckedPressedSkin;
            #endregion

            #region Event Handlers
            this.button.Click += new ClickHandler(OnClick);
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
        /// Update controls to respect new margins.
        /// </summary>
        protected void RefreshMargins()
        {
            // Update margin
            label.X = HMargin + button.Width;
            label.Width = Width - button.Width - HMargin;
        }

        #region Event Handlers
        /// <summary>
        /// Update locations of child control.
        /// </summary>
        /// <param name="sender"></param>
        protected override void OnResize(UIComponent sender)
        {
            base.OnResize(sender);

            // Update child sizes
            button.Width = Height;
            button.Height = Height;
            label.Height = Height;

            RefreshMargins();
        }

        /// <summary>
        /// This event handler is called when the button is clicked. It simply
        /// relays the event to the event handler of this control.
        /// </summary>
        /// <param name="sender">Clicked control.</param>
        public void OnClick(UIComponent sender)
        {
            if (Click != null)
                Click.Invoke(this);
        }
        #endregion
    }
}