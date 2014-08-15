#region File Description
//-----------------------------------------------------------------------------
// File:      DisplaySettingsDialog.cs
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
    /// A dialog box that allows the user to set the display resolution, and
    /// choose between fullscreen and windowed modes.
    /// </summary>
    public class DisplaySettingsDialog : Dialog
    {
        #region Fields
        private const int LargeSeperation = 10;
        private const int SmallSeperation = 5;
        private CheckBox fullscreenCheckBox;
        private Label resolutionLabel;
        private ComboBox resolutionCombo;
        private TextButton OKButton;
        private TextButton cancelButton;
        private TextButton applyButton;
        private GraphicsDeviceManager graphics;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="game">The currently running Game object.</param>
        /// <param name="guiManager">GUIManager that this control is part of.</param>
        /// <param name="graphics">Graphics device manager to change settings.</param>
        public DisplaySettingsDialog(Game game, GUIManager guiManager, GraphicsDeviceManager graphics)
            : base(game, guiManager)
        {
            #region Create Child Controls
            this.fullscreenCheckBox = new CheckBox(game, guiManager);
            this.resolutionLabel = new Label(game, guiManager);
            this.resolutionCombo = new ComboBox(game, guiManager);
            this.OKButton = new TextButton(game, guiManager);
            this.cancelButton = new TextButton(game, guiManager);
            this.applyButton = new TextButton(game, guiManager);
            #endregion

            #region Add Child Controls
            Add(this.fullscreenCheckBox);
            Add(this.resolutionLabel);
            Add(this.resolutionCombo);
            Add(this.OKButton);
            Add(this.cancelButton);
            Add(this.applyButton);
            #endregion

            this.graphics = graphics;

            // Set checkbox to current value
            if (graphics.IsFullScreen)
                this.fullscreenCheckBox.IsChecked = true;

            // Populate combobox
            this.resolutionCombo.AddEntry("640x480");
            this.resolutionCombo.AddEntry("800x600");
            this.resolutionCombo.AddEntry("1024x768");
            this.resolutionCombo.AddEntry("1280x800");
            this.resolutionCombo.AddEntry("1280x960");
            this.resolutionCombo.AddEntry("1280x1024");

            // Set combobox to current value
            if (graphics.PreferredBackBufferWidth == 640 &&
                graphics.PreferredBackBufferHeight == 480)
                this.resolutionCombo.SelectedIndex = 0;
            else if (graphics.PreferredBackBufferWidth == 800 &&
                graphics.PreferredBackBufferHeight == 600)
                this.resolutionCombo.SelectedIndex = 1;
            else if (graphics.PreferredBackBufferWidth == 1024 &&
                graphics.PreferredBackBufferHeight == 768)
                this.resolutionCombo.SelectedIndex = 2;
            else if (graphics.PreferredBackBufferWidth == 1280 &&
                graphics.PreferredBackBufferHeight == 800)
                this.resolutionCombo.SelectedIndex = 3;
            else if (graphics.PreferredBackBufferWidth == 1280 &&
                graphics.PreferredBackBufferHeight == 960)
                this.resolutionCombo.SelectedIndex = 4;
            else if (graphics.PreferredBackBufferWidth == 1280 &&
                graphics.PreferredBackBufferHeight == 1024)
                this.resolutionCombo.SelectedIndex = 5;

            // Child settings
            TitleText = "Display Settings";
            this.fullscreenCheckBox.Text = "Fullscreen";
            this.resolutionLabel.Text = "Select desired display resolution";
            this.resolutionLabel.Width = this.resolutionLabel.TextWidth;
            this.resolutionCombo.IsEditable = false;
            this.OKButton.Text = "OK";
            this.cancelButton.Text = "Cancel";
            this.applyButton.Text = "Apply";
            Resizable = false;

            // Layout
            this.fullscreenCheckBox.X = LargeSeperation;
            this.fullscreenCheckBox.Y = LargeSeperation;
            this.resolutionLabel.X = LargeSeperation;
            this.resolutionLabel.Y = this.fullscreenCheckBox.Y + this.fullscreenCheckBox.Height + LargeSeperation;
            this.resolutionCombo.X = LargeSeperation;
            this.resolutionCombo.Y = this.resolutionLabel.Y + this.resolutionLabel.Height + SmallSeperation;
            this.OKButton.Y = this.resolutionCombo.Y + this.resolutionCombo.Height + (LargeSeperation * 2);
            this.cancelButton.Y = this.OKButton.Y;
            this.applyButton.Y = this.OKButton.Y;

            // Check if buttons require more space
            int buttonsWidth = this.OKButton.Width + this.cancelButton.Width + this.applyButton.Width + (2 * SmallSeperation);

            if (buttonsWidth > this.resolutionCombo.Width)
            {
                this.resolutionCombo.Width = buttonsWidth;
                ClientWidth = buttonsWidth + (2 * LargeSeperation);
            }
            else
                ClientWidth = this.resolutionCombo.Width + (2 * LargeSeperation);

            this.ClientHeight = this.applyButton.Y + this.applyButton.Height + LargeSeperation;

            // Align buttons to the right of the window
            this.OKButton.X = ClientWidth - this.OKButton.Width - LargeSeperation;
            this.cancelButton.X = OKButton.X - this.cancelButton.Width - SmallSeperation;
            this.applyButton.X = cancelButton.X - this.cancelButton.Width - SmallSeperation;

            #region Event Handlers
            this.OKButton.Click += new ClickHandler(OnOK);
            this.cancelButton.Click += new ClickHandler(OnCancel);
            this.applyButton.Click += new ClickHandler(OnApply);
            #endregion
        }
        #endregion

        /// <summary>
        /// Applies settings.
        /// </summary>
        protected void Apply()
        {
            #region Resolution
            int newWidth = -1;
            int newHeight = -1;

            if (this.resolutionCombo.SelectedIndex == 0)
            {
                newWidth = 640;
                newHeight = 480;
            }
            else if (this.resolutionCombo.SelectedIndex == 1)
            {
                newWidth = 800;
                newHeight = 600;
            }
            else if (this.resolutionCombo.SelectedIndex == 2)
            {
                newWidth = 1024;
                newHeight = 768;
            }
            else if (this.resolutionCombo.SelectedIndex == 3)
            {
                newWidth = 1280;
                newHeight = 800;
            }
            else if (this.resolutionCombo.SelectedIndex == 4)
            {
                newWidth = 1280;
                newHeight = 960;
            }
            else if (this.resolutionCombo.SelectedIndex == 5)
            {
                newWidth = 1280;
                newHeight = 1024;
            }

            if (newWidth != -1 && newHeight != -1)
            {
                if (this.graphics.PreferredBackBufferWidth != newWidth)
                {
                    if (this.graphics.PreferredBackBufferHeight != newHeight)
                    {
                        this.graphics.PreferredBackBufferWidth = newWidth;
                        this.graphics.PreferredBackBufferHeight = newHeight;
                        this.graphics.ApplyChanges();
                    }
                }
            }
            #endregion

            #region Windowed Mode
            if (this.fullscreenCheckBox.IsChecked && !this.graphics.IsFullScreen)
                this.graphics.ToggleFullScreen();
            else if (!this.fullscreenCheckBox.IsChecked && this.graphics.IsFullScreen)
                this.graphics.ToggleFullScreen();
            #endregion
        }

        #region Event Handlers
        /// <summary>
        /// When the user clicks on the OK button, settings are applied, and
        /// dialog is closed.
        /// </summary>
        /// <param name="sender"></param>
        protected void OnOK(UIComponent sender)
        {
            Apply();
            SetDialogResult(DialogResult.OK);
            CloseWindow();
        }

        /// <summary>
        /// When the user clicks on the Cancel button, dialog is closed without
        /// applying changes.
        /// </summary>
        /// <param name="sender"></param>
        protected void OnCancel(UIComponent sender)
        {
            CloseWindow();
        }

        /// <summary>
        /// Apply changes without closing the dialog.
        /// </summary>
        /// <param name="sender"></param>
        protected void OnApply(UIComponent sender)
        {
            Apply();
        }
        #endregion
    }
}