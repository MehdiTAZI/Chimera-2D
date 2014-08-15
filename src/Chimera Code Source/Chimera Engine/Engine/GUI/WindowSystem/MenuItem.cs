#region File Description
//-----------------------------------------------------------------------------
// File:      MenuItem.cs
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
using Microsoft.Xna.Framework.Graphics;
using Chimera.GUI.InputEventSystem;
#endregion

namespace Chimera.GUI.WindowSystem
{
    #region Delegates
    /// <summary>
    /// When a popup menu is opened.
    /// </summary>
    /// <param name="sender">Menu item opening popup.</param>
    public delegate void PopUpOpenHandler(object sender);
    /// <summary>
    /// When a popup menu is closed.
    /// </summary>
    /// <param name="sender">Menu item closing popup.</param>
    public delegate void PopUpClosedHandler(object sender);
    /// <summary>
    /// When all menus should be closed.
    /// </summary>
    public delegate void CloseAllHandler();
    #endregion

    /// <summary>
    /// A graphical menu item, that can be clicked, or contain child menu
    /// items that are shown as a popup menu.
    /// </summary>
    public class MenuItem : UIComponent
    {
        #region Default Properties
        private static int defaultHMargin = 5;
        private static int defaultVMargin = 2;
        private static Rectangle defaultHighlightSkin = new Rectangle(61, 67, 15, 15);
        private static Rectangle defaultArrowSkin = new Rectangle(114, 43, 4, 7);

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
        /// Sets the default highlight skin.
        /// </summary>
        public static Rectangle DefaultHighlightSkin
        {
            set { defaultHighlightSkin = value; }
        }

        /// <summary>
        /// Sets the default arrow icon location.
        /// </summary>
        public static Rectangle DefaultArrowSkin
        {
            set { defaultArrowSkin = value; }
        }
        #endregion

        #region Fields
        private Label label;
        private Box highlightBox;
        private Icon arrow;
        private PopUpMenu popUpMenu;
        private int hMargin;
        private int vMargin;
        private int numMenuItems;
        private int numClicks;
        private bool isPopUpShown;
        private bool isHighlightShown;
        private bool isEnabled;
        private bool canClose;
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
                RefreshMargins();
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
                RefreshMargins();
            }
        }

        /// <summary>
        /// Get/Set the menu item text.
        /// </summary>
        /// <value>Must not be null.</value>
        public string Text
        {
            get { return label.Text; }
            set
            {
                Debug.Assert(value != null);
                this.label.Text = value;
                RefreshMargins();
            }
        }

        /// <summary>
        /// Sets the font used for menu text.
        /// </summary>
        /// <value>Must not be a valid path.</value>
        public string Font
        {
            set
            {
                Debug.Assert(value != null);
                this.label.Font = value;
                RefreshMargins();
            }
        }

        /// <summary>
        /// Get/Set whether the manu item is enabled.
        /// </summary>
        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set
            {
                this.isEnabled = value;
                // Change colour to grey if menu item is disabled
                if (this.isEnabled)
                    this.label.Color = Color.Black;
                else
                    this.label.Color = Color.Gray;
            }
        }

        /// <summary>
        /// Sets the highlight skin.
        /// </summary>
        public Rectangle HighlightSkin
        {
            set { this.highlightBox.SetSkinLocation(0, value); }
        }

        /// <summary>
        /// Sets the arrow icon location.
        /// </summary>
        /// <value>Must be a valid skin location.</value>
        public Rectangle ArrowSkin
        {
            set
            {
                Debug.Assert(CheckSkinLocation(value));
                this.arrow.SetSkinLocation(0, value);
                this.arrow.ResizeToFit();
            }
        }

        /// <summary>
        /// Get/Set whether the menu item in a popup menu.
        /// </summary>
        internal bool CanClose
        {
            get { return this.canClose; }
            set
            {
                this.canClose = value;
                RefreshMargins();
            }
        }
        #endregion

        #region Events
        new public event ClickHandler Click;
        internal event PopUpOpenHandler PopUpOpen;
        internal event PopUpClosedHandler PopUpClose;
        internal event CloseAllHandler CloseAll;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="game">The currently running Game object.</param>
        /// <param name="guiManager">GUIManager that this control is part of.</param>
        public MenuItem(Game game, GUIManager guiManager)
            : base(game, guiManager)
        {
            this.numMenuItems = 0;
            this.numClicks = 0;
            this.isPopUpShown = false;
            this.isHighlightShown = false;
            this.isEnabled = true;
            this.canClose = true;

            #region Create Child Controls
            this.label = new Label(game, guiManager);
            this.highlightBox = new Box(game, guiManager);
            this.popUpMenu = new PopUpMenu(game, guiManager);
            this.arrow = new Icon(game, guiManager);
            #endregion

            #region Add Child Controls
            base.Add(this.label);
            #endregion

            #region Set Default Properties
            HMargin = defaultHMargin;
            VMargin = defaultVMargin;
            HighlightSkin = defaultHighlightSkin;
            ArrowSkin = defaultArrowSkin;
            #endregion

            #region Event Handlers
            this.popUpMenu.Close += new CloseHandler(OnPopUpClosed);
            #endregion
        }
        #endregion

        /// <summary>
        /// Tells children to remove event handlers.
        /// </summary>
        public override void CleanUp()
        {
            if (IsInitialized)
            {
                this.highlightBox.CleanUp();
                this.arrow.CleanUp();
                this.popUpMenu.CleanUp();
            }

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
            // Add event handler
            control.CloseAll += new CloseAllHandler(OnCloseAll);

            // Add new menu item
            this.popUpMenu.Add(control);
            this.numMenuItems++;

            // Refreshes arrow settings
            CanClose = CanClose;
        }

        /// <summary>
        /// Refreshes child control positions and sizes.
        /// </summary>
        private void RefreshMargins()
        {
            if (!this.canClose && this.numMenuItems > 0) // Arrow displayed
            {
                base.Add(this.arrow);
                Width = this.label.TextWidth + (3 * this.hMargin) + this.arrow.Width;
            }
            else
            {
                base.Remove(this.arrow);
                Width = this.label.TextWidth + (2 * this.hMargin);
            }

            Height = this.label.TextHeight + (2 * this.vMargin);

            this.label.X = this.hMargin;
            this.label.Width = Width - (this.hMargin * 2);

            this.label.Y = this.vMargin;
            this.label.Height = Height - (this.vMargin * 2);
        }

        /// <summary>
        /// Highlights the menu item.
        /// </summary>
        protected void AddHighlight()
        {
            if (!this.isHighlightShown && this.isEnabled)
            {
                // Remove label and add it again after adding the highlight, to
                // ensure text is placed on top of the background.
                Remove(this.label);
                base.Add(this.highlightBox);
                base.Add(this.label);
                if (base.Remove(this.arrow))
                    base.Add(this.arrow);
                this.isHighlightShown = true;

                Redraw();
            }
        }

        /// <summary>
        /// Removes highlight from menu item.
        /// </summary>
        internal void RemoveHighlight()
        {
            this.isHighlightShown = false;
            base.Remove(this.highlightBox);
            Redraw();
        }

        /// <summary>
        /// Invokes the Click and CloseAll events, if menu item doesn't have a
        /// popup menu.
        /// </summary>
        internal void InvokeClick()
        {
            // Check that there are no children
            if (this.numMenuItems == 0 && this.isEnabled)
            {
                if (Click != null)
                    Click.Invoke(this);
                if (CloseAll != null)
                    CloseAll.Invoke();
            }
        }

        /// <summary>
        /// Shows popup if there is one, and sets the number of clicks to close
        /// to one.
        /// </summary>
        internal void SelectByMove()
        {
            if (!this.isPopUpShown)
            {
                Select();
                // Means that popup will close on first mouse up
                this.numClicks = 1;
            }
        }

        /// <summary>
        /// Shows popup if there is one, and sets the number of clicks to close
        /// to two.
        /// </summary>
        internal void Select()
        {
            if (!this.isPopUpShown && this.numMenuItems > 0 && this.isEnabled)
            {
                if (PopUpOpen != null)
                    PopUpOpen.Invoke(this);
                this.isPopUpShown = true;
                this.popUpMenu.Populate();

                if (Parent.GetType() == typeof(MenuBar)) // MenuBar parent
                {
                    this.popUpMenu.X = AbsolutePosition.X;
                    this.popUpMenu.Y = AbsolutePosition.Y + Height - 1;
                }
                else // PopUpMenu parent
                {
                    this.popUpMenu.X = Parent.AbsolutePosition.X + Parent.Width - 1;
                    this.popUpMenu.Y = AbsolutePosition.Y;
                }

                GUIManager.SetFocus(this.popUpMenu);
                GUIManager.Add(this.popUpMenu);

                AddHighlight();

                this.numClicks = 0;
            }
            else
                this.popUpMenu.BringToTop();
        }

        /// <summary>
        /// Closes popupmenu.
        /// </summary>
        internal void ClosePopUp()
        {
            if (isPopUpShown)
                this.popUpMenu.ClosePopUp();
        }

        /// <summary>
        /// Checks that the supplied position is inside the menu structure.
        /// </summary>
        /// <param name="x">X-position.</param>
        /// <param name="y">Y-position.</param>
        /// <returns>true if position is inside menu structure, otherwise false.</returns>
        internal bool CheckMenuCoordinates(int x, int y)
        {
            bool result = false;

            if (CheckCoordinates(x, y)) // This control
                result = true;
            else if (this.isPopUpShown && this.popUpMenu != null)
            {
                if (this.popUpMenu.CheckMenuCoordinates(x, y))
                    result = true;
            }

            return result;
        }

        #region Event Handlers
        /// <summary>
        /// Close when all menus are being closed.
        /// </summary>
        protected void OnCloseAll()
        {
            if (CloseAll != null)
                CloseAll.Invoke();
        }

        /// <summary>
        /// Child menu item has closed it's popup.
        /// </summary>
        /// <param name="sender">Menu item closing it's popup.</param>
        protected void OnPopUpClosed(object sender)
        {
            this.isPopUpShown = false;
            RemoveHighlight();

            if (PopUpClose != null)
                PopUpClose.Invoke(this);
        }

        /// <summary>
        /// Open popup menu if there is one.
        /// </summary>
        /// <param name="args"></param>
        protected override void OnMouseDown(MouseEventArgs args)
        {
            base.OnMouseDown(args);

            if (args.Button == MouseButtons.Left)
                Select();
        }

        /// <summary>
        /// Closes popup menu if parent is a menu bar, and user has clicked
        /// enough times.
        /// </summary>
        /// <param name="args">Mouse event arguments.</param>
        protected override void MouseUpIntercept(MouseEventArgs args)
        {
            if (args.Button == MouseButtons.Left)
            {
                // Check that the mouse was depressed inside the button
                if (CheckCoordinates(args.Position.X, args.Position.Y))
                {
                    if (this.isPopUpShown && this.canClose)
                    {
                        this.numClicks++;

                        if (this.numClicks > 1)
                        {
                            ClosePopUp();
                            AddHighlight();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Add highlight.
        /// </summary>
        /// <param name="args">Mouse event arguments.</param>
        protected override void OnMouseOver(MouseEventArgs args)
        {
            base.OnMouseOver(args);

            AddHighlight();
        }

        /// <summary>
        /// Remove highlight.
        /// </summary>
        /// <param name="args">Mouse event arguments.</param>
        protected override void OnMouseOut(MouseEventArgs args)
        {
            base.OnMouseOut(args);

            if (!this.isPopUpShown)
                RemoveHighlight();
        }

        /// <summary>
        /// Update highlight size.
        /// </summary>
        /// <param name="sender">Resized control</param>
        protected override void OnResize(UIComponent sender)
        {
            base.OnResize(sender);

            this.highlightBox.Width = Width;
            this.highlightBox.Height = Height;

            this.arrow.X = Width - this.arrow.Width - this.hMargin;
            this.arrow.Y = (Height - this.arrow.Height) / 2;
        }
        #endregion
    }
}