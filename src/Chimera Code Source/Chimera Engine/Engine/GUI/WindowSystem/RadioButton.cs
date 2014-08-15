#region File Description
//-----------------------------------------------------------------------------
// File:      RadioButton.cs
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
#endregion

namespace Chimera.GUI.WindowSystem
{
    /// <summary>
    /// RadioButton is exactly the same as the CheckBox control, except for
    /// different default graphics.
    /// </summary>
    public class RadioButton : CheckBox
    {
        #region Default Properties
        private static Rectangle defaultSkin = new Rectangle(1, 59, 15, 15);
        private static Rectangle defaultHoverSkin = new Rectangle(17, 59, 15, 15);
        private static Rectangle defaultPressedSkin = new Rectangle(33, 59, 15, 15);
        private static Rectangle defaultCheckedSkin = new Rectangle(1, 75, 15, 15);
        private static Rectangle defaultCheckedHoverSkin = new Rectangle(17, 75, 15, 15);
        private static Rectangle defaultCheckedPressedSkin = new Rectangle(33, 75, 15, 15);

        /// <summary>
        /// Sets the default skin.
        /// </summary>
        new public static Rectangle DefaultSkin
        {
            set { defaultSkin = value; }
        }

        /// <summary>
        /// Sets the default hover skin.
        /// </summary>
        new public static Rectangle DefaultHoverSkin
        {
            set { defaultHoverSkin = value; }
        }

        /// <summary>
        /// Sets the default pressed skin.
        /// </summary>
        new public static Rectangle DefaultPressedSkin
        {
            set { defaultPressedSkin = value; }
        }

        /// <summary>
        /// Sets the default checked skin.
        /// </summary>
        new public static Rectangle DefaultCheckedSkin
        {
            set { defaultCheckedSkin = value; }
        }

        /// <summary>
        /// Sets the default checked hover skin.
        /// </summary>
        new public static Rectangle DefaultCheckedHoverSkin
        {
            set { defaultCheckedHoverSkin = value; }
        }

        /// <summary>
        /// Sets the default checked pressed skin.
        /// </summary>
        new public static Rectangle DefaultCheckedPressedSkin
        {
            set { defaultCheckedPressedSkin = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="game">The currently running Game object.</param>
        /// <param name="guiManager">GUIManager that this control is part of.</param>
        public RadioButton(Game game, GUIManager guiManager)
            : base(game, guiManager)
        {
            #region Set Default Properties
            Skin = defaultSkin;
            HoverSkin = defaultHoverSkin;
            PressedSkin = defaultPressedSkin;
            CheckedSkin = defaultCheckedSkin;
            CheckedHoverSkin = defaultCheckedHoverSkin;
            CheckedPressedSkin = defaultCheckedPressedSkin;
            #endregion
        }
        #endregion
    }
}