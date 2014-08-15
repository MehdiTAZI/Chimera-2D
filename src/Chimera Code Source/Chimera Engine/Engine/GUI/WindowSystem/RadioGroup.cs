#region File Description
//-----------------------------------------------------------------------------
// File:      RadioGroup.cs
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
using Microsoft.Xna.Framework;
#endregion

namespace Chimera.GUI.WindowSystem
{
    /// <summary>
    /// Contains multiple RadioButton controls, and handles the exclusively
    /// checked behavior. A radio button on it's own is simply a checkbox with
    /// a different default appearence.
    /// </summary>
    public class RadioGroup : UIComponent
    {
        #region Fields
        private bool firstButtonClicked;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="game">The currently running Game object.</param>
        /// <param name="guiManager">GUIManager that this control is part of.</param>
        public RadioGroup(Game game, GUIManager guiManager)
            : base(game, guiManager)
        {
            this.firstButtonClicked = false;
            CanHaveFocus = false;
        }
        #endregion

        /// <summary>
        /// Overridden to prevent any control except RadioButton from being
        /// added.
        /// </summary>
        /// <param name="control">Control to add.</param>
        public override void Add(UIComponent control)
        {
            Debug.Assert(false);
        }

        /// <summary>
        /// Overloaded to prevent any control except RadioButton from being
        /// added.
        /// </summary>
        /// <param name="control">RadioButton to add.</param>
        public void Add(RadioButton control)
        {
            // Set event handler and add control
            control.Click += new ClickHandler(OnClick);
            base.Add(control);
        }

        #region Event Handlers
        /// <summary>
        /// When a child is checked, uncheck all other children.
        /// </summary>
        /// <param name="sender">Clicked RadioButton</param>
        protected void OnClick(UIComponent sender)
        {
            RadioButton checkBox = (RadioButton)sender;

            // Uncheck every other checkbox
            foreach (RadioButton control in Controls)
            {
                if (control != sender)
                    control.IsChecked = false;
            }

            // Ensure that only one checkbox is checked at any one time.
            // Also ensure first click gets throught!
            if (this.firstButtonClicked && !checkBox.IsChecked)
                checkBox.IsChecked = true;
            else
                this.firstButtonClicked = true;
        }
        #endregion
    }
}