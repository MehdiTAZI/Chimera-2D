#region File Description
//-----------------------------------------------------------------------------
// File:      GUIManager.cs
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
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Chimera.GUI.InputEventSystem;
#endregion

namespace Chimera.GUI.WindowSystem
{
    /// <summary>
    /// This is a game component that implements IUpdateable. It manages GUI
    /// components, in particular passing on events and handling focus.
    /// </summary>
    public partial class GUIManager : DrawableGameComponent
    {
        #region Default Properties
        private static string defaultSkinTexture = "Content/Textures/DefaultStyle";
        #endregion

        #region Fields
        private static ContentManager contentManager;
        private IInputEventsService inputEvents;
        private List<UIComponent> controls;
        private UIComponent focusedControl;
        private MouseCursor mouseCursor;
        private UIComponent modalControl;
        private Texture2D skinTexture;
        private SpriteBatch spriteBatch;
        #endregion

        #region Properties
        /// <summary>
        /// Get/Set the content manager used by the GUI system.
        /// </summary>
        /// <value>Must not be null.</value>
        public static ContentManager ContentManager
        {
            get { return contentManager; }
            set
            {
                Debug.Assert(value != null);
                contentManager = value;
            }
        }

        /// <summary>
        /// Gets the texture containing the graphics of all GUI components.
        /// </summary>
        internal Texture2D SkinTexture
        {
            get { return this.skinTexture; }
        }

        /// <summary>
        /// Gets the mouse cursor.
        /// </summary>
        internal MouseCursor MouseCursor
        {
            get { return this.mouseCursor; }
        }

        /// <summary>
        /// Sets the current texture file name and loads it.
        /// </summary>
        /// <value>Must be a valid path.</value>
        public string SkinTextureFileName
        {
            set
            {
                Debug.Assert(value != null && value.Length > 0);
                this.skinTexture = contentManager.Load<Texture2D>(value);
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="game">The currently running Game object.</param>
        public GUIManager(Game game)
            : base(game)
        {
            this.controls = new List<UIComponent>();
            this.focusedControl = null;
            this.modalControl = null;

            // Ensure this and mouse are always drawn on top, mouse is MaxValue
            this.DrawOrder = int.MaxValue - 1;

            // Get input event system, and register event handlers
            this.inputEvents = (IInputEventsService)this.Game.Services.GetService(typeof(IInputEventsService));
            if (this.inputEvents != null)
            {
                this.inputEvents.RequestingFocus += new MouseDownHandler(RequestingFocus);
                this.inputEvents.MouseMove += new MouseMoveHandler(CheckMouseStatus);
            }

            // Create graphical mouse cursor
            this.mouseCursor = new MouseCursor(game, this);
            this.mouseCursor.Initialize();
        }
        #endregion

        /// <summary>
        /// Create SpriteBatch object and load default graphics.
        /// </summary>
        /// <param name="loadAllContent">Which type of content to load.</param>
        protected override void LoadContent()
        {
            if (contentManager == null)
                contentManager = new ContentManager(Game.Services);

            SkinTextureFileName = defaultSkinTexture;

            this.spriteBatch = new SpriteBatch(GraphicsDevice);

            base.LoadContent();
        }

        /// <summary>
        /// Unload your graphics content.  If unloadAllContent is true, you should
        /// unload content from both ResourceManagementMode pools.  Otherwise, just
        /// unload ResourceManagementMode.Manual content.  Manual content will get
        /// Disposed by the GraphicsDevice during a Reset.
        /// </summary>
        protected override void UnloadContent()
        {
            contentManager.Unload();
        }

        /// <summary>
        /// Update all child components.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            foreach (UIComponent control in this.controls)
                control.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// Adds a top-level control if it hasn't already been added. Also
        /// initializes control.
        /// </summary>
        /// <param name="control">Control to add.</param>
        public void Add(UIComponent control)
        {
            if (!this.controls.Contains(control))
            {
                control.Parent = null;
                control.Initialize();
                this.controls.Add(control);
            }
        }

        /// <summary>
        /// Removes a top-level control.
        /// </summary>
        /// <param name="control">Control to remove.</param>
        public void Remove(UIComponent control)
        {
            if (this.controls.Remove(control))
                control.CleanUp();
        }

        /// <summary>
        /// Bring the control to the front. Currently only works with top-level
        /// controls.
        /// </summary>
        internal void BringToTop(UIComponent control)
        {
            if (this.controls.Remove(control))
                this.controls.Add(control);
        }

        /// <summary>
        /// Accessor retrieves currently focused control.
        /// </summary>
        /// <returns>The currently focused control.</returns>
        public UIComponent GetFocus()
        {
            return this.focusedControl;
        }

        /// <summary>
        /// Sets the currently focused control. Also tells old and new controls
        /// about the change in focus.
        /// </summary>
        /// <param name="control">Control receiving focus.</param>
        public void SetFocus(UIComponent control)
        {
            if (control != this.focusedControl)
            {
                // Checl that control can have focus
                if (control != null && !control.CanHaveFocus)
                    return;

                // Update mFocusedControl here in case a control calls
                // GetFocus() in OnLoseFocus().
                UIComponent oldFocus = this.focusedControl;
                this.focusedControl = control;

                // Take focus from the last control with focus
                if (oldFocus != null)
                    oldFocus.TakeFocus();

                // Give control to new control
                if (this.focusedControl != null)
                    this.focusedControl.GiveFocus();
            }
        }

        /// <summary>
        /// Sets the current mousr cursor to be drawn.
        /// </summary>
        /// <param name="state">New mouse state.</param>
        public void SetMouseCursor(MouseState state)
        {
            this.mouseCursor.SetMouseState(state);
        }

        /// <summary>
        /// Retrieves the current modal control. If null is returned, then no
        /// control has modal control.
        /// </summary>
        /// <returns>Modal control, or null if there is none.</returns>
        public UIComponent GetModal()
        {
            return this.modalControl;
        }

        /// <summary>
        /// Sets the modal control. Modal only allows modal control or it's
        /// children to receive focus. If null is passed, GUI is no longer in
        /// modal mode.
        /// </summary>
        /// <param name="modalControl">Control to set to modal.</param>
        public void SetModal(UIComponent modalControl)
        {
            this.modalControl = modalControl;
        }

        /// <summary>
        /// This applies a skin to this gui. If applyDefaults is true, then the
        /// defaults for subsequent controls are also changed.
        /// </summary>
        /// <param name="skin">The skin to apply.</param>
        /// <param name="applyDefaults">Should control defaults be modified?</param>
        /// <param name="applyCurrent">Should existing controls have their properties modified?</param>
        public void ApplySkin(Skin skin, bool applyCurrent, bool applyDefaults)
        {
            // Apply to GUI and possibly control defaults
            skin.Apply(this, applyDefaults);

            // Apply to every control already part of the GUI
            if (applyCurrent)
            {
                foreach (UIComponent control in this.controls)
                    control.ApplySkin(skin);
            }
        }

        /// <summary>
        /// This is called when the GUI should draw itself. Tells all controls
        /// to draw themselves, and then their children. Also draws the mouse
        /// cursor.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            if (this.skinTexture != null)
            {
                // At this stage the scissor rectangle is the whole screen
                Rectangle parentScissor = new Rectangle(
                    0,
                    0,
                    GraphicsDevice.Viewport.Width,
                    GraphicsDevice.Viewport.Height
                    );

                this.spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None);

                foreach (UIComponent control in this.controls)
                    control.Draw(this.spriteBatch, parentScissor);

                // Draw mouse cursor last
                this.mouseCursor.DrawCursor(gameTime, this.spriteBatch);

                this.spriteBatch.End();
            }

            base.Draw(gameTime);
        }

        #region Event Handlers
        /// <summary>
        /// Event handler called when requesting focus. Checks if a new control
        /// should receive focus. If so it brings it's top-level window to the
        /// top and calls SetFocus() with the focused control.
        /// </summary>
        /// <param name="args">Mouse event arguments.</param>
        private void RequestingFocus(MouseEventArgs args)
        {
            UIComponent result = null;
            UIComponent parentControl = null;

            // Asks each top-level control if it or it's children should
            // receive focus.
            foreach (UIComponent control in this.controls)
            {
                UIComponent child = control.CheckFocus(args.Position.X, args.Position.Y);

                if (child != null)
                {
                    // Child has focus, so store the shild and parent control
                    result = child;
                    parentControl = control;
                }
            }

            // When modal, only allow modal control, it's children, and null through
            if (this.modalControl != null && result != null)
            {
                if (!this.modalControl.IsChild(result))
                    return;
            }

            if (parentControl != null)
                parentControl.BringToTop();

            SetFocus(result);

            // Ensure a newly active control gets the correct mouse status
            CheckMouseStatus(args);
        }

        /// <summary>
        /// Event handler called when the mouse is moved. Asks each control to
        /// check it's mouse status (whether the mouse is over or not). Control
        /// invokes the MouseOut event, while this method invokes the MouseOver
        /// event.
        /// </summary>
        /// <param name="args">Mouse event arguments.</param>
        private void CheckMouseStatus(MouseEventArgs args)
        {
            UIComponent result = null;

            // Ask each control to checks it's mouse status
            foreach (UIComponent control in this.controls)
            {
                // Early out if any control is animating (moving or resizing)
                if (control.IsAnimating)
                    return;

                UIComponent temp = control.CheckMouseStatus(args);
                // When modal, only allow modal control, it's children, and null through
                if (temp != null && (this.modalControl == null || this.modalControl.IsChild(temp)))
                {
                    // MouseOut last result
                    if (result != null && result.IsMouseOver)
                        result.InvokeMouseOut(args);

                    result = temp;
                }
            }

            if (result != null)
            {
                // Indirectly invoke MouseOver event
                if (!result.IsMouseOver)
                    result.InvokeMouseOver(args);
            }
        }
        #endregion

        #region Utility Functions
        /// <summary>
        /// Converts KeyEventArgs to a string.
        /// </summary>
        /// <param name="args">Key event arguments.</param>
        /// <returns>string version of the arguments.</returns>
        public static string KeyToString(KeyEventArgs args)
        {
            switch (args.Key)
            {
                #region Alphabet
                case Keys.A:
                    return (args.Shift) ? "A" : "a";
                case Keys.B:
                    return (args.Shift) ? "B" : "b";
                case Keys.C:
                    return (args.Shift) ? "C" : "c";
                case Keys.D:
                    return (args.Shift) ? "D" : "d";
                case Keys.E:
                    return (args.Shift) ? "E" : "e";
                case Keys.F:
                    return (args.Shift) ? "F" : "f";
                case Keys.G:
                    return (args.Shift) ? "G" : "g";
                case Keys.H:
                    return (args.Shift) ? "H" : "h";
                case Keys.I:
                    return (args.Shift) ? "I" : "i";
                case Keys.J:
                    return (args.Shift) ? "J" : "j";
                case Keys.K:
                    return (args.Shift) ? "K" : "k";
                case Keys.L:
                    return (args.Shift) ? "L" : "l";
                case Keys.M:
                    return (args.Shift) ? "M" : "m";
                case Keys.N:
                    return (args.Shift) ? "N" : "n";
                case Keys.O:
                    return (args.Shift) ? "O" : "o";
                case Keys.P:
                    return (args.Shift) ? "P" : "p";
                case Keys.Q:
                    return (args.Shift) ? "Q" : "q";
                case Keys.R:
                    return (args.Shift) ? "R" : "r";
                case Keys.S:
                    return (args.Shift) ? "S" : "s";
                case Keys.T:
                    return (args.Shift) ? "T" : "t";
                case Keys.U:
                    return (args.Shift) ? "U" : "u";
                case Keys.V:
                    return (args.Shift) ? "V" : "v";
                case Keys.W:
                    return (args.Shift) ? "W" : "w";
                case Keys.X:
                    return (args.Shift) ? "X" : "x";
                case Keys.Y:
                    return (args.Shift) ? "Y" : "y";
                case Keys.Z:
                    return (args.Shift) ? "Z" : "z";
                #endregion

                #region Numbers
                case Keys.D0:
                    return (args.Shift) ? ")" : "0";
                case Keys.D1:
                    return (args.Shift) ? "!" : "1";
                case Keys.D2:
                    return (args.Shift) ? "@" : "2";
                case Keys.D3:
                    return (args.Shift) ? "#" : "3";
                case Keys.D4:
                    return (args.Shift) ? "$" : "4";
                case Keys.D5:
                    return (args.Shift) ? "%" : "5";
                case Keys.D6:
                    return (args.Shift) ? "^" : "6";
                case Keys.D7:
                    return (args.Shift) ? "&" : "7";
                case Keys.D8:
                    return (args.Shift) ? "*" : "8";
                case Keys.D9:
                    return (args.Shift) ? "(" : "9";
                #endregion

                #region Extra
                case Keys.OemPlus:
                    return (args.Shift) ? "+" : "=";
                case Keys.OemMinus:
                    return (args.Shift) ? "_" : "-";
                case Keys.OemOpenBrackets:
                    return (args.Shift) ? "{" : "[";
                case Keys.OemCloseBrackets:
                    return (args.Shift) ? "}" : "]";
                case Keys.OemQuestion:
                    return (args.Shift) ? "?" : "/";
                case Keys.OemPeriod:
                    return (args.Shift) ? ">" : ".";
                case Keys.OemComma:
                    return (args.Shift) ? "<" : ",";
                case Keys.OemPipe:
                    return (args.Shift) ? "|" : "\\";
                case Keys.Space:
                    return " ";
                case Keys.OemSemicolon:
                    return (args.Shift) ? ":" : ";";
                case Keys.OemQuotes:
                    return (args.Shift) ? "\"" : "'";
                case Keys.OemTilde:
                    return (args.Shift) ? "~" : "`";
                #endregion

                default:
                    return "";
            }
        }
        #endregion
    }
}