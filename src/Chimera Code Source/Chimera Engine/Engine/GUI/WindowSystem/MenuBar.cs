#region File Description
//-----------------------------------------------------------------------------
// File:      MenuBar.cs
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
    /// Graphical bar for dropdown menus. Automatically stretches to the width
    /// of the parent control.
    /// </summary>
    public class MenuBar : Bar
    {
        #region Default Properties
        private static int defaultHMargin = 5;
        private static int defaultVMargin = 2;
        private static Rectangle defaultSkin = new Rectangle(78, 67, 13, 15);

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

        /// <summary>
        /// Get/Set the parent control.
        /// </summary>
        /// <remarks>
        /// This version changes the control width to fill the parent.
        /// </remarks>
        internal protected override UIComponent Parent
        {
            get { return base.Parent; }
            set
            {
                base.Parent = value;

                if (Parent == null)
                    Width = Game.Window.ClientBounds.Width;
                else
                    Width = Parent.Width;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="game">The currently running Game object.</param>
        /// <param name="guiManager">GUIManager that this control is part of.</param>
        public MenuBar(Game game, GUIManager guiManager)
            : base(game, guiManager)
        {
            this.menuItems = new List<MenuItem>();
            this.isPopUpShown = false;

            #region Set Properties
            IsVertical = true;
            CanHaveFocus = true;
            ZOrder = 1.0f;
            #endregion

            #region Set Default Properties
            HMargin = defaultHMargin;
            VMargin = defaultVMargin;
            Skin = defaultSkin;
            #endregion
        }
        #endregion

        /// <summary>
        /// Sets up an event handler to track when window is resized.
        /// </summary>
        public override void Initialize()
        {
            if (!IsInitialized)
            {
                // Allows menu bar to fill screen width, when not attached to a
                // parent control.
                Game.Window.ClientSizeChanged += new EventHandler(OnClientSizeChanged);
            }

            base.Initialize();
        }

        /// <summary>
        /// Removes event handlers.
        /// </summary>
        public override void CleanUp()
        {
            if (!IsInitialized)
                Game.Window.ClientSizeChanged -= OnClientSizeChanged;

            base.CleanUp();
        }

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
            control.Resize += new ResizeHandler(OnMenuItemResize);

            // Add control
            this.menuItems.Add(control);
            base.Add(control);

            // Refresh menu item positions
            RefreshMenuItems();
        }

        /// <summary>
        /// Overloaded to refresh control positions after menu items are
        /// removed.
        /// </summary>
        /// <param name="control">Control to remove.</param>
        /// <returns>TRUE if control exists, otherwise FALSE.</returns>
        public override bool Remove(UIComponent control)
        {
            bool result = false;

            if (base.Remove(control))
            {
                this.menuItems.Remove((MenuItem)control);
                RefreshMenuItems();
                result = true;
            }

            return result;
        }

        /// <summary>
        /// After the list of menu items have changed, this method repositions
        /// controls, and alters the menu bar height if necessary.
        /// </summary>
        private void RefreshMenuItems()
        {
            int xPosition = this.hMargin;
            int yPosition = this.vMargin;
            int maxHeight = 0;

            foreach (MenuItem menuItem in this.menuItems)
            {
                menuItem.X = xPosition;
                menuItem.Y = yPosition;

                // Check if menubar height needs to be changed
                if (menuItem.Height > maxHeight)
                    maxHeight = menuItem.Height;

                xPosition += menuItem.Width;
            }

            if ((maxHeight + (2 * this.vMargin)) != Height)
                Height = maxHeight + (2 * this.vMargin);

            Redraw();
        }

        /// <summary>
        /// Checks that the mouse position is inside the menu structure. If it
        /// isn't, then the menu is closed.
        /// </summary>
        /// <param name="args">Mouse event arguments.</param>
        protected void CheckMenuMouseStatus(MouseEventArgs args)
        {
            if (this.isPopUpShown)
            {
                bool onMenu = false;

                foreach (MenuItem item in this.menuItems)
                {
                    if (item.CheckMenuCoordinates(args.Position.X, args.Position.Y))
                    {
                        onMenu = true;
                        break;
                    }
                }

                // Check if menu should be closed
                if (!onMenu && this.selectedMenuItem != null)
                    this.selectedMenuItem.ClosePopUp();
            }
        }

        #region Event Handlers
        /// <summary>
        /// Closes popup when all menus are being closed.
        /// </summary>
        protected void OnCloseAll()
        {
            if (this.selectedMenuItem != null)
                this.selectedMenuItem.ClosePopUp();
            this.isPopUpShown = false;
        }

        /// <summary>
        /// Popup menu is opened.
        /// </summary>
        /// <param name="sender">Menu item closing it's popup.</param>
        protected void OnPopUpOpened(object sender)
        {
            this.isPopUpShown = true;
            this.selectedMenuItem = (MenuItem)sender;
        }

        /// <summary>
        /// Popup menu is closed.
        /// </summary>
        /// <param name="sender">Menu item closing it's popup.</param>
        protected void OnPopUpClosed(object sender)
        {
            this.isPopUpShown = false;
        }

        /// <summary>
        /// Close menu if mouse is pressed outside the menu structure.
        /// </summary>
        /// <param name="args">Mouse event arguments.</param>
        protected override void MouseDownIntercept(MouseEventArgs args)
        {
            CheckMenuMouseStatus(args);
        }

        /// <summary>
        /// Close menu if mouse is released outside the menu structure.
        /// </summary>
        /// <param name="args">Mouse event arguments.</param>
        protected override void MouseUpIntercept(MouseEventArgs args)
        {
            CheckMenuMouseStatus(args);
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
                foreach (MenuItem item in this.menuItems)
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
        /// Fill the screen width for top-level menu bars when the screen size
        /// changes.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="args">Event arguments.</param>
        protected void OnClientSizeChanged(object sender, EventArgs args)
        {
            if (Parent == null)
                Width = Game.Window.ClientBounds.Width;
        }

        /// <summary>
        /// Refresh menu items positions when any of them change size.
        /// </summary>
        /// <param name="sender">Resized control.</param>
        protected void OnMenuItemResize(UIComponent sender)
        {
            RefreshMenuItems();
        }

        /// <summary>
        /// When the parent control is resized, fill the entire width.
        /// </summary>
        /// <param name="sender">Resized control.</param>
        protected override void OnParentResized(UIComponent sender)
        {
            base.OnParentResized(sender);
            Width = sender.Width;
        }
        #endregion
    }
}