#region File Description
//-----------------------------------------------------------------------------
// File:      PopUpMenu.cs
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
using Chimera.GUI.InputEventSystem;
#endregion

namespace Chimera.GUI.WindowSystem
{
    /// <summary>
    /// A graphical popup menu used by menu bars, and as context menus.
    /// </summary>
    public class PopUpMenu : Box
    {
        #region Default Properties
        private static int defaultHMargin = 2;
        private static int defaultVMargin = 2;
        private static Rectangle defaultSkin = new Rectangle(84, 41, 25, 25);

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
        /// Sets the default skin.
        /// </summary>
        public static Rectangle DefaultSkin
        {
            set { defaultSkin = value; }
        }
        #endregion

        #region Fields
        private int hMargin;
        private int vMargin;
        private List<MenuItem> menuItems;
        private bool isPopUpShown;
        private MenuItem selectedMenuItem;
        #endregion

        #region Properties
        /// <summary>
        /// Get/Set the horizontal padding.
        /// </summary>
        /// <value>Must be at least 0.</value>
        public int HMargin
        {
            get { return this.hMargin; }
            set
            {
                Debug.Assert(value >= 0);
                this.hMargin = value;
            }
        }

        /// <summary>
        /// Get/Set the vertical padding.
        /// </summary>
        /// <value>Must be at least 0.</value>
        public int VMargin
        {
            get { return this.vMargin; }
            set
            {
                Debug.Assert(value >= 0);
                this.vMargin = value;
            }
        }

        /// <summary>
        /// Sets the control skin.
        /// </summary>
        public Rectangle Skin
        {
            set { SetSkinLocation(0, value); }
        }
        #endregion

        #region Events
        public CloseHandler Close;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="game">The currently running Game object.</param>
        /// <param name="guiManager">GUIManager that this control is part of.</param>
        public PopUpMenu(Game game, GUIManager guiManager)
            : base(game, guiManager)
        {
            this.menuItems = new List<MenuItem>();
            this.isPopUpShown = false;

            #region Set Properties
            CanHaveFocus = true;
            #endregion

            #region Set Default Properties
            HMargin = defaultHMargin;
            VMargin = defaultVMargin;
            Skin = defaultSkin;
            #endregion

            #region Event Handlers
            Close += new CloseHandler(OnClose);
            #endregion
        }
        #endregion

        /// <summary>
        /// Overridden to prevent any control except MenuItem from being added.
        /// </summary>
        /// <param name="control">Control to add.</param>
        public override void Add(UIComponent control)
        {
            Debug.Assert(false);
        }

        /// <summary>
        /// Overloaded to prevent any control except MenuItem from being added.
        /// </summary>
        /// <param name="control">MenuItem to add.</param>
        public void Add(MenuItem control)
        {
            // Add event handlers
            control.PopUpOpen += new PopUpOpenHandler(OnPopUpOpened);
            control.PopUpClose += new PopUpClosedHandler(OnPopUpClosed);
            control.CloseAll += new CloseAllHandler(OnCloseAll);

            // Add Control
            this.menuItems.Add(control);
            base.Add(control);
        }

        /// <summary>
        /// Refreshes popup size, and the locations of child menu items.
        /// </summary>
        public void Populate()
        {
            if (!this.isPopUpShown)
            {
                // Resize control
                int width = 0;
                int height = this.vMargin;

                foreach (MenuItem item in this.menuItems)
                {
                    // Ensure highlight status is reset
                    item.RemoveHighlight();
                    item.CanClose = false;

                    item.X = this.hMargin;
                    item.Y = height;

                    if (item.Width > width)
                        width = item.Width;
                    height += item.Height;
                }

                // Fix widths to maximum
                foreach (MenuItem item in this.menuItems)
                    item.Width = width;

                width += this.hMargin * 2;
                height += this.vMargin;

                Width = width;
                Height = height;

                this.isPopUpShown = true;
            }
        }

        /// <summary>
        /// Closes this popup menu, and child menu.
        /// </summary>
        internal void ClosePopUp()
        {
            Close.Invoke(this);

            if (this.selectedMenuItem != null)
                this.selectedMenuItem.ClosePopUp();

            this.isPopUpShown = false;
        }

        /// <summary>
        /// Checks if the supplied location is within the menu structure (this
        /// control or it's children).
        /// </summary>
        /// <param name="x">X-position.</param>
        /// <param name="y">Y-position.</param>
        /// <returns>true if position is inside menu strucure, otherwise false.</returns>
        internal bool CheckMenuCoordinates(int x, int y)
        {
            bool result = false;

            if (CheckCoordinates(x, y)) // This control
                result = true;
            else
            {
                // Child menu items
                foreach (MenuItem item in this.menuItems)
                {
                    if (item.CheckMenuCoordinates(x, y))
                    {
                        result = true;
                        break;
                    }
                }
            }

            return result;
        }

        #region Event Handlers
        /// <summary>
        /// Close when all menus are being closed.
        /// </summary>
        protected void OnCloseAll()
        {
            ClosePopUp();
        }

        /// <summary>
        /// Child menu item has opened it's popup.
        /// </summary>
        /// <param name="sender">Menu item opening it's popup.</param>
        protected void OnPopUpOpened(object sender)
        {
            this.selectedMenuItem = (MenuItem)sender;
        }

        /// <summary>
        /// Child menu item has closed it's popup.
        /// </summary>
        /// <param name="sender">Menu item closing it's popup.</param>
        protected void OnPopUpClosed(object sender)
        {
            this.selectedMenuItem = null;
        }

        /// <summary>
        /// Check if mouse button was released on top of a menu item.
        /// </summary>
        /// <param name="args">Mouse event arguments.</param>
        protected override void MouseUpIntercept(MouseEventArgs args)
        {
            if (this.isPopUpShown && CheckCoordinates(args.Position.X, args.Position.Y))
            {
                // Check if a child menu item should be clicked
                foreach (MenuItem item in this.menuItems)
                {
                    if (item.CheckCoordinates(args.Position.X, args.Position.Y))
                    {
                        item.InvokeClick();
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// If any menu is currently open, automatically open popups when the
        /// mouse hovers over the menu item.
        /// </summary>
        /// <param name="args">Mouse event arguments.</param>
        protected override void MouseMoveIntercept(MouseEventArgs args)
        {
            if (this.isPopUpShown && CheckCoordinates(args.Position.X, args.Position.Y))
            {
                foreach (MenuItem item in menuItems)
                {
                    if (item.CheckCoordinates(args.Position.X, args.Position.Y))
                    {
                        if (item != this.selectedMenuItem)
                        {
                            if (this.selectedMenuItem != null)
                                this.selectedMenuItem.ClosePopUp();

                            item.SelectByMove();
                        }
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// When popup is closed, remove it from the GUIManager.
        /// </summary>
        /// <param name="sender">Sender.</param>
        protected void OnClose(object sender)
        {
            GUIManager.Remove(this);
        }
        #endregion
    }
}