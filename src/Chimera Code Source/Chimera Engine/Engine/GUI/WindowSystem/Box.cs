#region File Description
//-----------------------------------------------------------------------------
// File:      Box.cs
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
#endregion

namespace Chimera.GUI.WindowSystem
{
    /// <summary>
    /// A graphical box drawn with four corner sprites, and five other sprites
    /// stretched to allow dynamic resizing.
    /// </summary>
    /// <remarks>
    /// Inherited from SkinnedComponent to allow arbitrary number of skin
    /// states. For example, the Scrollbar class uses a Bar to draw the thumb
    /// with three states, normal, hover, and pressed.
    /// </remarks>
    public class Box : SkinnedComponent
    {
        #region Default Properties
        protected static int defaultCornerSize = 5;

        /// <summary>
        /// The default width and height of corner sprites.
        /// </summary>
        /// <value>Must be greater than 0.</value>
        public static int DefaultCornerSize
        {
            set
            {
                Debug.Assert(value > 0);
                defaultCornerSize = value;
            }
        }
        #endregion

        #region Fields
        private int cornerSize;
        #endregion

        #region Properties
        /// <summary>
        /// Get/Set the width and height of corner sprites.
        /// </summary>
        /// <value>Must be greater than 0.</value>
        public int CornerSize
        {
            get { return cornerSize; }
            set
            {
                Debug.Assert(value > 0);

                this.cornerSize = value;
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
        public Box(Game game, GUIManager guiManager)
            : base(game, guiManager)
        {
            CanHaveFocus = false;

            #region Set Default Properties
            CornerSize = defaultCornerSize;
            #endregion
        }
        #endregion

        /// <summary>
        /// Update skin sizes.
        /// </summary>
        protected override void RefreshSkins()
        {
            // Get the current list of skins
            Dictionary<int, ComponentSkin> skins = this.Skins;

            foreach (KeyValuePair<int, ComponentSkin> skin in skins)
            {
                skin.Value.Rects = CreateBox(
                    GetSkinLocation(skin.Key),
                    new Rectangle(0, 0, Width, Height),
                    this.cornerSize
                );
            }

            Redraw();
        }

        /// <summary>
        /// Constructs a box drawn with four corner sprites, and five other
        /// sprites stretched to allow resizing. Note that there must a 1 pixel
        /// gap between edges and the middle sprite on the source texture.
        /// </summary>
        /// <param name="source">Location on source texture.</param>
        /// <param name="dimensions">Size of bar to construct.</param>
        /// <param name="edgeSize">The box corner size.</param>
        /// <param name="isVertical">Orientation of the bar.</param>
        /// <returns>List of skin locations of new bar.</returns>
        protected static List<GUIRect> CreateBox(Rectangle source, Rectangle dimensions, int cornerSize)
        {
            List<GUIRect> result = new List<GUIRect>();

            GUIRect[] sprites = new GUIRect[9];

            // Top Left
            sprites[0] = new GUIRect();
            sprites[0].Source = new Rectangle(source.X, source.Y, cornerSize, cornerSize);
            sprites[0].Destination = new Rectangle(dimensions.X, dimensions.Y, cornerSize, cornerSize);

            // Bottom Left
            sprites[1] = new GUIRect();
            sprites[1].Source = new Rectangle(source.X, source.Bottom - cornerSize, cornerSize, cornerSize);
            sprites[1].Destination = new Rectangle(dimensions.X, dimensions.Bottom - cornerSize, cornerSize, cornerSize);

            // Bottom Right
            sprites[2] = new GUIRect();
            sprites[2].Source = new Rectangle(source.Right - cornerSize, source.Bottom - cornerSize, cornerSize, cornerSize);
            sprites[2].Destination = new Rectangle(dimensions.Right - cornerSize, dimensions.Bottom - cornerSize, cornerSize, cornerSize);

            // Top Right
            sprites[3] = new GUIRect();
            sprites[3].Source = new Rectangle(source.Right - cornerSize, source.Y, cornerSize, cornerSize);
            sprites[3].Destination = new Rectangle(dimensions.Right - cornerSize, dimensions.Y, cornerSize, cornerSize);

            // Top
            sprites[4] = new GUIRect();
            sprites[4].Source = new Rectangle(source.X + cornerSize, source.Y, source.Width - (2 * cornerSize), cornerSize);
            sprites[4].Destination = new Rectangle(dimensions.X + cornerSize, dimensions.Y, dimensions.Width - (2 * cornerSize), cornerSize);

            // Bottom
            sprites[5] = new GUIRect();
            sprites[5].Source = new Rectangle(source.X + cornerSize, source.Bottom - cornerSize, source.Width - (2 * cornerSize), cornerSize);
            sprites[5].Destination = new Rectangle(dimensions.X + cornerSize, dimensions.Bottom - cornerSize, dimensions.Width - (2 * cornerSize), cornerSize);

            // Left
            sprites[6] = new GUIRect();
            sprites[6].Source = new Rectangle(source.X, source.Y + cornerSize, cornerSize, source.Height - (2 * cornerSize));
            sprites[6].Destination = new Rectangle(dimensions.X, dimensions.Y + cornerSize, cornerSize, dimensions.Height - (2 * cornerSize));

            // Right
            sprites[7] = new GUIRect();
            sprites[7].Source = new Rectangle(source.Right - cornerSize, source.Y + cornerSize, cornerSize, source.Height - (2 * cornerSize));
            sprites[7].Destination = new Rectangle(dimensions.Right - cornerSize, dimensions.Y + cornerSize, cornerSize, dimensions.Height - (2 * cornerSize));

            // Middle
            sprites[8] = new GUIRect();
            sprites[8].Source = new Rectangle(source.X + cornerSize, source.Y + cornerSize, source.Width - (2 * cornerSize), source.Height - (2 * cornerSize));
            sprites[8].Destination = new Rectangle(dimensions.X + cornerSize, dimensions.Y + cornerSize, dimensions.Width - (2 * cornerSize), dimensions.Height - (2 * cornerSize));

            // Copy over the results
            for (int i = 0; i < 9; i++)
                result.Add(sprites[i]);

            return result;
        }
    }
}