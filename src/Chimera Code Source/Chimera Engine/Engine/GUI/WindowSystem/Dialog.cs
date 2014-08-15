#region File Description
//-----------------------------------------------------------------------------
// File:      Dialog.cs
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
    /// Result of a dialog or message box.
    /// </summary>
    public enum DialogResult
    {
        OK,
        Cancel,
        Yes,
        No
    }

    /// <summary>
    /// A dialog box is a window that should return a value, defined by the
    /// DialogResult enumeration.
    /// </summary>
    public class Dialog : Window
    {
        #region Fields
        private DialogResult result;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the result of the dialog box. Will return DialogResult.Cancel
        /// if called before dialog is closed.
        /// </summary>
        public DialogResult DialogResult
        {
            get { return this.result; }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="game">The currently running Game object.</param>
        /// <param name="guiManager">GUIManager that this control is part of.</param>
        public Dialog(Game game, GUIManager guiManager)
            : base(game, guiManager)
        {
            // The default result is cancel, because that is the result of the
            // user pressing the window close button.
            this.result = DialogResult.Cancel;
        }
        #endregion

        /// <summary>
        /// Used to set the dialog result before dialog closes.
        /// </summary>
        /// <param name="result">Dialog result.</param>
        protected void SetDialogResult(DialogResult result)
        {
            this.result = result;
        }
    }
}