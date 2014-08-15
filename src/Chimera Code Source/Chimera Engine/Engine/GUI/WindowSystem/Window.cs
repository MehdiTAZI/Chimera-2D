#region File Description
//-----------------------------------------------------------------------------
// File:      Window.cs
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
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Chimera.GUI.InputEventSystem;
#endregion

namespace Chimera.GUI.WindowSystem
{
    /// <summary>
    /// Represents a graphical window, with a title bar, a close button, and
    /// movable and resizable areas. Most aspects can be removed, and the
    /// movable area can cover the whole window, like WinAmp or iTunes.
    /// </summary>
    public class Window : UIComponent
    {
        #region Default Properties
        private static bool defaultHasCloseButton = true;
        private static bool defaultFullWindowMovableArea = true;
        private static int defaultTitleBarHeight = 24;
        private static int defaultButtonSize = 20;
        private static int defaultMargin = 5;
        private static string defaultTitleFont = "Content/Fonts/DefaultHeading";
        private static Rectangle defaultSkin = new Rectangle(15, 1, 15, 15);
        private static Rectangle defaultTitleBarSkin = new Rectangle(1, 1, 13, 25);
        private static Rectangle defaultCloseButtonSkin = new Rectangle(1, 168, 20, 20);
        private static Rectangle defaultCloseButtonHoverSkin = new Rectangle(22, 168, 20, 20);
        private static Rectangle defaultCloseButtonPressedSkin = new Rectangle(43, 168, 20, 20);

        /// <summary>
        /// Sets whether windows have close buttons by default.
        /// </summary>
        [SkinAttribute]
        public static bool DefaultHasCloseButton
        {
            set { defaultHasCloseButton = value; }
        }

        /// <summary>
        /// Sets whether the movable area covers the whole window by default.
        /// </summary>
        [SkinAttribute]
        public static bool DefaultHasFullWindowMovableArea
        {
            set { defaultFullWindowMovableArea = value; }
        }

        /// <summary>
        /// Sets the default title bar height.
        /// </summary>
        /// <value>Must be greater than 0.</value>
        [SkinAttribute]
        public static int DefaultTitleBarHeight
        {
            set
            {
                Debug.Assert(value > 0);
                defaultTitleBarHeight = value;
            }
        }

        /// <summary>
        /// Sets the default close button width and height.
        /// </summary>
        /// <value>Must be greater than 0.</value>
        [SkinAttribute]
        public static int DefaultButtonSize
        {
            set
            {
                Debug.Assert(value > 0);
                defaultButtonSize = value;
            }
        }

        /// <summary>
        /// Sets the default distance from the edge to display child controls.
        /// </summary>
        /// <value>Must be at least 0.</value>
        [SkinAttribute]
        public static int DefaultMargin
        {
            set
            {
                Debug.Assert(value >= 0);
                defaultMargin = value;
            }
        }

        /// <summary>
        /// Sets the default title textfont.
        /// </summary>
        /// <value>Must be a non-empty string.</value>
        [SkinAttribute]
        public static string DefaultTitleFont
        {
            set
            {
                Debug.Assert(value != null);
                Debug.Assert(value.Length > 0);
                defaultTitleFont = value;
            }
        }

        /// <summary>
        /// Sets the default window background skin.
        /// </summary>
        [SkinAttribute]
        public static Rectangle DefaultSkin
        {
            set { defaultSkin = value; }
        }

        /// <summary>
        /// Sets the default title bar skin.
        /// </summary>
        [SkinAttribute]
        public static Rectangle DefaultTitleBarSkin
        {
            set { defaultTitleBarSkin = value; }
        }

        /// <summary>
        /// Sets the default close button skin.
        /// </summary>
        [SkinAttribute]
        public static Rectangle DefaultCloseButtonSkin
        {
            set { defaultCloseButtonSkin = value; }
        }

        /// <summary>
        /// Sets the default hover close button skin.
        /// </summary>
        [SkinAttribute]
        public static Rectangle DefaultCloseButtonHoverSkin
        {
            set { defaultCloseButtonHoverSkin = value; }
        }

        /// <summary>
        /// Sets the default pressed close button skin.
        /// </summary>
        [SkinAttribute]
        public static Rectangle DefaultCloseButtonPressedSkin
        {
            set { defaultCloseButtonPressedSkin = value; }
        }
        #endregion

        #region Fields
        private Box box;
        private UIComponent viewPort;
        private Bar titleBar;
        private MovableArea movableArea;
        private MovableArea backgroundMovableArea;
        private Label label;
        private ImageButton closeButton;
        private ResizableArea[] resizableAreas;
        private bool hasCloseButton;
        private bool fullWindowMovableArea;
        private int margin;
        private bool isResizable;
        private float transparency;
        #endregion

        #region Properties
        /// <summary>
        /// Get/Set whether window can be resized by the user.
        /// </summary>
        [SkinAttribute]
        public bool Resizable
        {
            get { return this.isResizable; }
            set
            {
                this.isResizable = value;

                // Simply change focus settings of resizable area, preventing
                // them from receiving focus when window is not resizable.
                if (this.isResizable)
                {
                    for (int i = 0; i < 8; i++)
                        this.resizableAreas[i].CanHaveFocus = true;
                }
                else
                {
                    for (int i = 0; i < 8; i++)
                        this.resizableAreas[i].CanHaveFocus = false;
                }
            }
        }

        /// <summary>
        /// Sets the width of the window client area.
        /// </summary>
        public int ClientWidth
        {
            get { return this.viewPort.Width; }
            set { Width = value + (this.margin * 2); }
        }

        /// <summary>
        /// Sets the height of the window client area.
        /// </summary>
        public int ClientHeight
        {
            get { return this.viewPort.Height; }
            set { Height = value + TitleBarHeight + this.margin; }
        }

        /// <summary>
        /// Get/Set whether the window has a close button.
        /// </summary>
        [SkinAttribute]
        public bool HasCloseButton
        {
            get { return this.hasCloseButton; }
            set
            {
                if (this.hasCloseButton && !value)
                    base.Remove(this.closeButton);
                else if (!this.hasCloseButton && value)
                    base.Add(this.closeButton);

                this.hasCloseButton = value;
            }
        }

        /// <summary>
        /// Get/Set whether the movable area covers the whole window.
        /// </summary>
        [SkinAttribute]
        public bool HasFullWindowMovableArea
        {
            get { return this.fullWindowMovableArea; }
            set
            {
                this.fullWindowMovableArea = value;

                if (this.fullWindowMovableArea)
                {
                    this.viewPort.Add(this.backgroundMovableArea);
                    // Set parent to this so window is moved instead of
                    // viewport.
                    this.backgroundMovableArea.Parent = this;
                }
                else
                    this.viewPort.Remove(this.backgroundMovableArea);
            }
        }

        /// <summary>
        /// Get/Set the title bar height.
        /// </summary>
        /// <value>Must be greater than 0.</value>
        [SkinAttribute]
        public int TitleBarHeight
        {
            get { return this.titleBar.Height; }
            set
            {
                Debug.Assert(value > 0);

                this.titleBar.Height = value;
                this.movableArea.Height = value;

                MinHeight = value;

                this.viewPort.Y = value;
                this.backgroundMovableArea.Y = this.viewPort.Y;

                // Ensure client size remains the same
                ClientHeight = ClientHeight;
            }
        }

        /// <summary>
        /// Get/Set the close button width and height.
        /// </summary>
        /// <value>Must be at least 0.</value>
        [SkinAttribute]
        public int ButtonSize
        {
            get { return this.closeButton.Width; }
            set
            {
                Debug.Assert(value > 0);

                this.closeButton.Width = value;
                this.closeButton.Height = value;
                this.closeButton.Y = (TitleBarHeight - value) / 2;
                this.closeButton.X = Width - value - this.closeButton.Y;
            }
        }

        /// <summary>
        /// Get/Set the padding between the window edge and controls.
        /// </summary>
        /// <value>Must be at least 0.</value>
        [SkinAttribute]
        public int Margin
        {
            get { return margin; }
            set
            {
                Debug.Assert(value >= 0);
                
                this.margin = value;

                this.box.CornerSize = value;

                // Align title bar text with the margin
                this.label.X = value;

                this.viewPort.X = value;
                this.backgroundMovableArea.X = value;
                this.viewPort.Width = Width - (value * 2);
                this.viewPort.Height = Height - TitleBarHeight - value;

                RefreshResizableAreas();

                // Save client height, because resetting width will cause a
                // resize.
                int clientHeight = ClientHeight;
                // Ensure client size remains the same
                ClientWidth = ClientWidth;
                ClientHeight = clientHeight;
            }
        }

        /// <summary>
        /// Get/Set the title text.
        /// </summary>
        /// <value>Must not be null.</value>
        public string TitleText
        {
            get { return this.label.Text; }
            set { this.label.Text = value; }
        }

        /// <summary>
        /// Sets the font of the title text.
        /// </summary>
        /// <value>Must be a valid path.</value>
        [SkinAttribute]
        public string TitleFont
        {
            set
            {
                this.label.Font = value;
                // Centre title vertically
                this.label.Height = this.label.TextHeight;
                this.label.Y = (TitleBarHeight - this.label.Height) / 2;
            }
        }

        /// <summary>
        /// Gets the background skin.
        /// </summary>
        [SkinAttribute]
        public Rectangle Skin
        {
            set { this.box.SetSkinLocation(0, value); }
        }

        /// <summary>
        /// Gets the title bar skin.
        /// </summary>
        [SkinAttribute]
        public Rectangle TitleBarSkin
        {
            set { this.titleBar.SetSkinLocation(0, value); }
        }

        /// <summary>
        /// Sets the close button skin.
        /// </summary>
        [SkinAttribute]
        public Rectangle CloseButtonSkin
        {
            set { this.closeButton.SetSkinLocation(0, value); }
        }

        /// <summary>
        /// Sets the hover close button skin.
        /// </summary>
        [SkinAttribute]
        public Rectangle CloseButtonHoverSkin
        {
            set { this.closeButton.SetSkinLocation(1, value); }
        }

        /// <summary>
        /// Sets the pressed close button skin.
        /// </summary>
        [SkinAttribute]
        public Rectangle CloseButtonPressedSkin
        {
            set { this.closeButton.SetSkinLocation(2, value); }
        }
        #endregion

        #region Events
        public event CloseHandler Close;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="game">The currently running Game object.</param>
        /// <param name="guiManager">GUIManager that this control is part of.</param>
        public Window(Game game, GUIManager guiManager)
            : base(game, guiManager)
        {
            this.isResizable = true;
            this.transparency = -1;

            #region Create Child Controls
            this.box = new Box(game, guiManager);
            this.viewPort = new UIComponent(game, guiManager);
            this.titleBar = new Bar(game, guiManager);
            this.movableArea = new MovableArea(game, guiManager);
            this.backgroundMovableArea = new MovableArea(game, guiManager);
            this.label = new Label(game, guiManager);
            this.closeButton = new ImageButton(game, guiManager);
            #endregion

            #region Add Child Controls
            base.Add(this.box);
            base.Add(this.viewPort);
            base.Add(this.titleBar);
            base.Add(this.label);
            base.Add(this.movableArea);
            #endregion

            #region Add Resizable Areas
            this.resizableAreas = new ResizableArea[8];

            for (int i = 0; i < 8; i++)
            {
                this.resizableAreas[i] = new ResizableArea(game, guiManager);
                this.resizableAreas[i].ZOrder = 0.3f;
                this.resizableAreas[i].StartResizing += new StartResizingHandler(OnStartAnimating);
                this.resizableAreas[i].EndResizing += new EndResizingHandler(OnEndAnimating);
                base.Add(this.resizableAreas[i]);
            }

            this.resizableAreas[0].ResizeArea = ResizeAreas.TopLeft;
            this.resizableAreas[1].ResizeArea = ResizeAreas.Top;
            this.resizableAreas[2].ResizeArea = ResizeAreas.TopRight;
            this.resizableAreas[3].ResizeArea = ResizeAreas.Left;
            this.resizableAreas[4].ResizeArea = ResizeAreas.Right;
            this.resizableAreas[5].ResizeArea = ResizeAreas.BottomLeft;
            this.resizableAreas[6].ResizeArea = ResizeAreas.Bottom;
            this.resizableAreas[7].ResizeArea = ResizeAreas.BottomRight;
            #endregion

            #region Set Non-Default Properties
            this.movableArea.ZOrder = 0.1f;
            this.closeButton.ZOrder = 0.4f;
            this.viewPort.ZOrder = 0.2f;
            this.viewPort.CanHaveFocus = true;
            MinWidth = 150;
            #endregion

            #region Set Default Properties
            Margin = defaultMargin;
            HasCloseButton = defaultHasCloseButton;
            HasFullWindowMovableArea = defaultFullWindowMovableArea;
            TitleBarHeight = defaultTitleBarHeight;
            Width = MinWidth;
            Height = MinHeight;
            ButtonSize = defaultButtonSize;
            Skin = defaultSkin;
            TitleBarSkin = defaultTitleBarSkin;
            CloseButtonSkin = defaultCloseButtonSkin;
            CloseButtonHoverSkin = defaultCloseButtonHoverSkin;
            CloseButtonPressedSkin = defaultCloseButtonPressedSkin;
            #endregion

            #region Event Handlers
            this.closeButton.Click += new ClickHandler(OnClose);
            this.movableArea.StartMoving += new StartMovingHandler(OnStartAnimating);
            this.movableArea.EndMoving += new EndMovingHandler(OnEndAnimating);
            this.backgroundMovableArea.StartMoving += new StartMovingHandler(OnStartAnimating);
            this.backgroundMovableArea.EndMoving += new EndMovingHandler(OnEndAnimating);
            #endregion
        }
        #endregion

        /// <summary>
        /// Tidy controls that might not have been added.
        /// </summary>
        /// <remarks>
        /// For some reason, the viewPort control needs to be cleaned up here,
        /// even though it is always added to Window. Will investigate in the
        /// future, but for now this allows Window controls to be garbage
        /// collected.
        /// </remarks>
        public override void CleanUp()
        {
            this.closeButton.CleanUp();
            this.viewPort.CleanUp();
            this.backgroundMovableArea.CleanUp();

            base.CleanUp();
        }

        /// <summary>
        /// Loads default font.
        /// </summary>
        protected override void LoadContent()
        {
            TitleFont = defaultTitleFont;

            base.LoadContent();
        }

        /// <summary>
        /// Add child controls to viewport, so user doesn't have to worry about
        /// title bars etc.
        /// </summary>
        /// <param name="control">Control to add.</param>
        public override void Add(UIComponent control)
        {
            // Add to viewport
            this.viewPort.Add(control);
        }

        /// <summary>
        /// Remove child controls from viewport, so user doesn't have to worry
        /// about title bars etc.
        /// </summary>
        /// <param name="control">Control to remove.</param>
        /// <returns>Removal result.</returns>
        public override bool Remove(UIComponent control)
        {
            return this.viewPort.Remove(control);
        }

        /// <summary>
        /// Centres the window on the screen.
        /// </summary>
        public void CenterWindow()
        {
            X = (Game.Window.ClientBounds.Width / 2) - (Width / 2);
            Y = (Game.Window.ClientBounds.Height / 2) - (Height / 2);
        }

        /// <summary>
        /// Shows window in modal mode, no other controls can receive focus
        /// while window is open.
        /// </summary>
        public void Show(bool modal)
        {
            GUIManager.Add(this);
            if (modal)
                GUIManager.SetModal(this);
        }

        /// <summary>
        /// Closes window.
        /// </summary>
        public void CloseWindow()
        {
            if (Parent == null)
                GUIManager.Remove(this);
            else
                GUIManager.Remove(this);

            if (Close != null)
                Close.Invoke(this);
        }

        /// <summary>
        /// Sets the positions and sizes of resizable areas.
        /// </summary>
        private void RefreshResizableAreas()
        {
            // Top left
            this.resizableAreas[0].X = 0;
            this.resizableAreas[0].Y = 0;
            this.resizableAreas[0].Width = this.margin;
            this.resizableAreas[0].Height = this.margin;

            // Top
            this.resizableAreas[1].X = this.margin;
            this.resizableAreas[1].Y = 0;
            this.resizableAreas[1].Width = Width - (2 * this.margin);
            this.resizableAreas[1].Height = this.margin;

            // Top right
            this.resizableAreas[2].X = Width - this.margin;
            this.resizableAreas[2].Y = 0;
            this.resizableAreas[2].Width = this.margin;
            this.resizableAreas[2].Height = this.margin;

            // Left
            this.resizableAreas[3].X = 0;
            this.resizableAreas[3].Y = this.margin;
            this.resizableAreas[3].Width = this.margin;
            this.resizableAreas[3].Height = Height - (2 * this.margin);

            // Right
            this.resizableAreas[4].X = Width - this.margin;
            this.resizableAreas[4].Y = this.margin;
            this.resizableAreas[4].Width = this.margin;
            this.resizableAreas[4].Height = Height - (2 * this.margin);

            // Bottom left
            this.resizableAreas[5].X = 0;
            this.resizableAreas[5].Y = Height - this.margin;
            this.resizableAreas[5].Width = this.margin;
            this.resizableAreas[5].Height = this.margin;

            // Bottom
            this.resizableAreas[6].X = this.margin;
            this.resizableAreas[6].Y = Height - this.margin;
            this.resizableAreas[6].Width = Width - (2 * this.margin);
            this.resizableAreas[6].Height = this.margin;

            // Bottom right
            this.resizableAreas[7].X = Width - this.margin;
            this.resizableAreas[7].Y = Height - this.margin;
            this.resizableAreas[7].Width = this.margin;
            this.resizableAreas[7].Height = this.margin;
        }

        #region Event Handlers
        /// <summary>
        /// Don't check mouse status during animation.
        /// </summary>
        /// <param name="sender">Animating control.</param>
        private void OnStartAnimating(UIComponent sender)
        {
            IsAnimating = true;
        }

        protected override void OnMove(UIComponent sender)
        {
            base.OnMove(sender);

            // If moving, change transparency
            if (IsAnimating && this.transparency == -1)
            {
                //this.transparency = Transparency;
                //Transparency *= defaultAnimationTransparency;
            }
        }

        /// <summary>
        /// Don't check mouse status during animation.
        /// </summary>
        /// <param name="sender">Animating control.</param>
        private void OnEndAnimating(UIComponent sender)
        {
            IsAnimating = false;
            // Reset transparency
            if (this.transparency != -1)
            {
                //Transparency = this.transparency;
                //this.transparency = -1;
            }
        }

        /// <summary>
        /// Closes window, starting animation if necessary. Also invokes the
        /// window Close event.
        /// </summary>
        /// <param name="sender">Control invoking close.</param>
        protected void OnClose(UIComponent sender)
        {
            CloseWindow();
        }

        /// <summary>
        /// Update child controls.
        /// </summary>
        /// <param name="sender">Resizing control.</param>
        protected override void OnResize(UIComponent sender)
        {
            base.OnResize(sender);

            // Width
            this.box.Width = Width;
            this.titleBar.Width = Width;
            this.movableArea.Width = Width;
            this.closeButton.X = Width - this.closeButton.Width - this.closeButton.Y;
            // Resize label
            this.label.Width = Width - this.closeButton.Width - this.closeButton.Y - (this.margin * 2);
            this.viewPort.Width = Width - (this.margin * 2);
            this.backgroundMovableArea.Width = this.viewPort.Width;

            // Height
            this.box.Height = Height;
            this.viewPort.Height = Height - TitleBarHeight - this.margin;
            this.backgroundMovableArea.Height = this.viewPort.Height;

            RefreshResizableAreas();

            // If resizing, change transparency
            if (IsAnimating && this.transparency == -1)
            {
                //this.transparency = Transparency;
                //Transparency *= defaultAnimationTransparency;
            }
        }

        /// <summary>
        /// Allows tabbing between child controls.
        /// </summary>
        /// <param name="args">Key event arguments.</param>
        protected override void KeyUpIntercept(KeyEventArgs args)
        {
            base.KeyUpIntercept(args);

            if (args.Key == Keys.Tab && IsChild(GUIManager.GetFocus()))
            {
                List<UIComponent> controls = this.viewPort.Controls;
                bool backwards = args.Shift;
                int index = 0;

                foreach (UIComponent control in controls)
                {
                    if (control.IsChild(GUIManager.GetFocus()))
                    {
                        UIComponent nextControl;
                        int nextIndex = index;

                        while (true)
                        {
                            // Loop round the list if necessary
                            if (backwards)
                            {
                                if (nextIndex > 0)
                                    nextIndex--;
                                else
                                    nextIndex = controls.Count - 1;
                            }
                            else
                            {
                                if ((nextIndex + 1) < controls.Count)
                                    nextIndex++;
                                else
                                    nextIndex = 0;
                            }

                            nextControl = controls[nextIndex];

                            // Set focus to next control
                            if (nextControl.CanHaveFocus && nextControl != this.backgroundMovableArea)
                            {
                                GUIManager.SetFocus(nextControl);
                                break;
                            }
                            else if (nextIndex == index) // Exit loop if coming back to the same control
                                break;
                        }

                        break;
                    }

                    index++;
                }
            }
        }
        #endregion
    }
}