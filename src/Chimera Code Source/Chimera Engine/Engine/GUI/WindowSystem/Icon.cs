#region File Description
//-----------------------------------------------------------------------------
// File:      Icon.cs
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
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace Chimera.GUI.WindowSystem
{
    /// <summary>
    /// A static image from the GUI texture. Can be scaled to fit the control
    /// size.
    /// </summary>
    public class Icon : SkinnedComponent
    {
        #region Fields
        private bool scale;
        #endregion

        #region Properties
        /// <summary>
        /// Get/Set whether the image be scaled to control size.
        /// </summary>
        public bool Scale
        {
            get { return this.scale; }
            set
            {
                this.scale = value;
                RefreshSkins();
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="game">The currently running Game object.</param>
        /// <param name="guiManager">GUIManager that this control is part of.</param>
        public Icon(Game game, GUIManager guiManager)
            : base(game, guiManager)
        {
            this.scale = false;
        }
        #endregion

        /// <summary>
        /// Resizes control to fit icon skin, as long as icon isn't scaling.
        /// </summary>
        public void ResizeToFit()
        {
            if (!this.scale)
            {
                int currentSkin = CurrentSkin;

                if (currentSkin != -1)
                {
                    Rectangle source = GetSkinLocation(currentSkin);

                    if (source.Width > 0 && source.Height > 0)
                    {
                        Width = source.Width;
                        Height = source.Height;
                    }
                }
            }
        }

        /// <summary>
        /// Update skin sizes.
        /// </summary>
        protected override void RefreshSkins()
        {
            // Get the current list of skins
            Dictionary<int, ComponentSkin> skins = Skins;

            foreach (KeyValuePair<int, ComponentSkin> skin in skins)
            {
                GUIRect rect = new GUIRect();
                rect.Source = GetSkinLocation(skin.Key);

                if (this.scale)
                    rect.Destination = new Rectangle(0, 0, Width, Height);
                else
                    rect.Destination = new Rectangle(0, 0, rect.Source.Width, rect.Source.Height);

                skin.Value.Rects.Clear();
                skin.Value.Rects.Add(rect);
            }

            Redraw();
        }
    }
}