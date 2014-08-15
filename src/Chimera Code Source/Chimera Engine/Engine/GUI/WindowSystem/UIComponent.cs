#region File Description
//-----------------------------------------------------------------------------
// File:      UIComponent.cs
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
using Microsoft.Xna.Framework.Graphics;
using Chimera.GUI.InputEventSystem;
#endregion

namespace Chimera.GUI.WindowSystem
{
    #region Delegates
    /// <summary>
    /// When a control is moved.
    /// </summary>
    /// <param name="sender">Moved control.</param>
    public delegate void MoveHandler(UIComponent sender);
    /// <summary>
    /// When a control is resized.
    /// </summary>
    /// <param name="sender">Resized control.</param>
    public delegate void ResizeHandler(UIComponent sender);
    /// <summary>
    /// When the mouse enters a control's location rectangle.
    /// </summary>
    /// <param name="args">Mouse event arguments.</param>
    public delegate void MouseOverHandler(MouseEventArgs args);
    /// <summary>
    /// When the mouse leaves a control's location rectangle.
    /// </summary>
    /// <param name="args">Mouse event arguments.</param>
    public delegate void MouseOutHandler(MouseEventArgs args);
    /// <summary>
    /// When a control requires a redraw.
    /// </summary>
    /// <param name="sender">Control requiring redraw.</param>
    public delegate void RequiresRedrawHandler(UIComponent sender);
    /// <summary>
    /// When a control has been clicked (mouse pressed then released inside the
    /// area of a control).
    /// </summary>
    /// <param name="sender">Clicked control.</param>
    public delegate void ClickHandler(UIComponent sender);
    /// <summary>
    /// When a control receives focus.
    /// </summary>
    public delegate void GetFocusHandler();
    /// <summary>
    /// When a control no longer has focus.
    /// </summary>
    public delegate void LoseFocusHandler();
    /// <summary>
    /// When a control such as a window is closed.
    /// </summary>
    /// <param name="sender">Closed control.</param>
    public delegate void CloseHandler(UIComponent sender);
    #endregion

    /// <summary>
    /// This is the base class for all GUI controls. Handles all the shared
    /// events.
    /// </summary>
    public partial class UIComponent : DrawableGameComponent
    {
        #region Fields
        private static int instanceCount = 0;

        private GUIManager guiManager;
        private IInputEventsService inputEvents;
        private Rectangle location;
        private Point absolutePosition;
        private List<UIComponent> controls;
        private UIComponent parent;
        private int minWidth;
        private int minHeight;
        private float zOrder;
        private bool canHaveFocus;
        private bool isRedrawRequired;
        private bool isInitialized;
        private bool isAnimating;
        private bool isMouseOver;
        private bool isPressed;
        #endregion

        #region Events
        public event MouseDownHandler MouseDown;
        public event MouseUpHandler MouseUp;
        public event MouseMoveHandler MouseMove;
        public event KeyDownHandler KeyDown;
        public event KeyUpHandler KeyUp;
        public event ClickHandler Click;
        public event MoveHandler Move;
        public event ResizeHandler Resize;
        public event MouseOverHandler MouseOver;
        public event MouseOutHandler MouseOut;
        public event RequiresRedrawHandler RequiresRedraw;
        public event GetFocusHandler GetFocus;
        public event LoseFocusHandler LoseFocus;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the GUIManager object associated with this control.
        /// </summary>
        protected GUIManager GUIManager
        {
            get { return this.guiManager; }
        }

        /// <summary>
        /// Gets the IInputEventService object used for event handling.
        /// </summary>
        protected IInputEventsService InputEvents
        {
            get { return this.inputEvents; }
        }

        /// <summary>
        /// Gets the control location.
        /// </summary>
        protected Rectangle Location
        {
            get { return this.location; }
        }

        /// <summary>
        /// Get/Set the x-position of this control in relation to the parent.
        /// </summary>
        public int X
        {
            get { return this.location.X; }
            set
            {
                this.location.X = value;

                if (Move != null)
                    Move.Invoke(this);
            }
        }

        /// <summary>
        /// Get/Set the y-position of this control in relation to the parent.
        /// </summary>
        public int Y
        {
            get { return this.location.Y; }
            set
            {
                this.location.Y = value;

                if (Move != null)
                    Move.Invoke(this);
            }
        }

        /// <summary>
        /// Get/Set the control width.
        /// </summary>
        public int Width
        {
            get { return this.location.Width; }
            set
            {
                if (value < this.minWidth)
                    value = this.minWidth;

                this.location.Width = value;

                if (Resize != null)
                    Resize.Invoke(this);
            }
        }

        /// <summary>
        /// Get/Set the control height.
        /// </summary>
        public int Height
        {
            get { return this.location.Height; }
            set
            {
                if (value < this.minHeight)
                    value = this.minHeight;

                this.location.Height = value;

                if (Resize != null)
                    Resize.Invoke(this);
            }
        }

        /// <summary>
        /// Gets the location on the screen of the control.
        /// </summary>
        public Point AbsolutePosition
        {
            get { return this.absolutePosition; }
        }

        /// <summary>
        /// Gets the list of child controls.
        /// </summary>
        internal List<UIComponent> Controls
        {
            get { return this.controls; }
        }

        /// <summary>
        /// Get/Set the direct parent.
        /// </summary>
        internal protected virtual UIComponent Parent
        {
            get { return this.parent; }
            set
            {
                // Remove old event handlers
                if (this.parent != null)
                {
                    this.parent.Move -= OnParentMoved;
                    this.parent.Resize -= OnParentResized;
                }

                this.parent = value;

                // Add new event handlers
                if (this.parent != null)
                {
                    this.parent.Move += new MoveHandler(OnParentMoved);
                    this.parent.Resize += new ResizeHandler(OnParentResized);
                }

                // Update absolute position
                Refresh();
            }
        }

        /// <summary>
        /// Get/Set the minimum control width.
        /// </summary>
        /// <remarks>
        /// Control must always have a size of at least 1, otherwise
        /// calculations can become unpredictable, and DrawableUIComponent
        /// objects cannot create render targets.
        /// </remarks>
        /// <value>Must be at least 1.</value>
        public int MinWidth
        {
            get { return this.minWidth; }
            set
            {
                Debug.Assert(value > 0);
                this.minWidth = value;

                // If width is less than minimum width, resize
                if (Width < this.minWidth)
                    Width = this.minWidth;
            }
        }

        /// <summary>
        /// Get/Set the minimum control height.
        /// </summary>
        /// <remarks>
        /// Control must always have a size of at least 1, otherwise
        /// calculations can become unpredictable, and DrawableUIComponent
        /// objects cannot create render targets.
        /// </remarks>
        /// <value>Must be at least 1.</value>
        public int MinHeight
        {
            get { return this.minHeight; }
            set
            {
                Debug.Assert(value > 0);
                this.minHeight = value;

                // If height is less than minimum height, resize
                if (Height < this.minHeight)
                    Height = this.minHeight;
            }
        }

        /// <summary>
        /// Get/Set the focusing z-order.
        /// </summary>
        public float ZOrder
        {
            get { return this.zOrder; }
            set
            {
                Debug.Assert(value >= 0.0f && value <= 1.0f);
                this.zOrder = value;
            }
        }

        /// <summary>
        /// Get/Set whether this control receive focus.
        /// </summary>
        public bool CanHaveFocus
        {
            get { return this.canHaveFocus; }
            set { this.canHaveFocus = value; }
        }

        /// <summary>
        /// Get/Set whether control needs to be redrawn.
        /// </summary>
        protected bool IsRedrawRequired
        {
            set { this.isRedrawRequired = value; }
            get { return this.isRedrawRequired; }
        }

        /// <summary>
        /// Get/Set whether this control has been initialised.
        /// </summary>
        internal protected bool IsInitialized
        {
            get { return this.isInitialized; }
            set { this.isInitialized = value; }
        }

        /// <summary>
        /// Get/Set whether the control is currently animating.
        /// </summary>
        internal protected bool IsAnimating
        {
            get { return this.isAnimating; }
            set { this.isAnimating = value; }
        }
        /// <summary>
        /// Gets whether the mouse is currently hovering over this control.
        /// </summary>
        internal protected bool IsMouseOver
        {
            get { return this.isMouseOver; }
        }

        /// <summary>
        /// Get/Set whether button is currently pressed.
        /// </summary>
        protected bool IsPressed
        {
            get { return this.isPressed; }
            set { this.isPressed = value; }
        }

        /// <summary>
        /// Get the number of textures allocated by all DrawableUIComponents.
        /// </summary>
        public static int InstanceCount
        {
            get { return instanceCount; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor sets up data and event handlers.
        /// </summary>
        /// <param name="game">The currently running Game object.</param>
        /// <param name="guiManager">GUIManager that this control is part of.</param>
        public UIComponent(Game game, GUIManager guiManager)
            : base(game)
        {
            this.guiManager = guiManager;
            this.inputEvents = null;
            this.absolutePosition = Point.Zero;
            this.controls = new List<UIComponent>();
            this.parent = null;

            // Minimum size of 1
            this.location = new Rectangle(0, 0, 1, 1);
            this.minWidth = 1;
            this.minHeight = 1;

            this.zOrder = 0.0f;
            this.canHaveFocus = true;
            this.isRedrawRequired = true;
            this.isInitialized = false;
            this.isAnimating = false;
            this.isMouseOver = false;
            this.isPressed = false;

            #region Event Handlers
            this.MouseDown += new MouseDownHandler(OnMouseDown);
            this.MouseUp += new MouseUpHandler(OnMouseUp);
            this.MouseMove += new MouseMoveHandler(OnMouseMove);
            this.MouseOver += new MouseOverHandler(OnMouseOver);
            this.MouseOut += new MouseOutHandler(OnMouseOut);
            this.KeyDown += new KeyDownHandler(OnKeyDown);
            this.KeyUp += new KeyUpHandler(OnKeyUp);
            this.Move += new MoveHandler(OnMove);
            this.Resize += new ResizeHandler(OnResize);
            this.RequiresRedraw += new RequiresRedrawHandler(OnRequiresRedraw);
            this.GetFocus += new GetFocusHandler(OnGetFocus);
            this.LoseFocus += new LoseFocusHandler(OnLoseFocus);
            #endregion

            instanceCount++;
        }

        /// <summary>
        /// Destructor.
        /// </summary>
        ~UIComponent()
        {
            instanceCount--;
        }
        #endregion

        /// <summary>
        /// Registers event handlers with the input events system, and
        /// refreshes the control position.
        /// </summary>
        public override void Initialize()
        {
            foreach (UIComponent control in this.controls)
                control.Initialize();

            if (!this.isInitialized)
            {
                // Get input event system, and register event handlers
                this.inputEvents = (IInputEventsService)Game.Services.GetService(typeof(IInputEventsService));

                if (this.inputEvents != null)
                {
                    this.inputEvents.KeyDown += new KeyDownHandler(KeyDownIntercept);
                    this.inputEvents.KeyUp += new KeyUpHandler(KeyUpIntercept);
                    this.inputEvents.MouseDown += new MouseDownHandler(MouseDownIntercept);
                    this.inputEvents.MouseUp += new MouseUpHandler(MouseUpIntercept);
                    this.inputEvents.MouseMove += new MouseMoveHandler(MouseMoveIntercept);
                }

                // Refreshing here allows controls to sort themselves out
                Refresh();
                
                base.Initialize();
                this.isInitialized = true;
            }
        }

        /// <summary>
        /// Tidies up event handlers for this control, as well as all children.
        /// </summary>
        public virtual void CleanUp()
        {
            foreach (UIComponent control in this.controls)
                control.CleanUp();

            if (this.isInitialized)
            {
                if (this.inputEvents != null)
                {
                    // Remove event handlers from input event system
                    this.inputEvents.KeyDown -= KeyDownIntercept;
                    this.inputEvents.KeyUp -= KeyUpIntercept;
                    this.inputEvents.MouseDown -= MouseDownIntercept;
                    this.inputEvents.MouseUp -= MouseUpIntercept;
                    this.inputEvents.MouseMove -= MouseMoveIntercept;
                }

                if (this.guiManager.GetFocus() == this)
                    this.guiManager.SetFocus(null);

                if (this.guiManager.GetModal() == this)
                    this.guiManager.SetModal(null);

                base.Dispose(true);

                this.isInitialized = false;
            }
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
        /// Bring the control to the front. Currently only works with top-level
        /// controls.
        /// </summary>
        public virtual void BringToTop()
        {
            this.guiManager.BringToTop(this);
        }

        /// <summary>
        /// Is the specified control contained in this part of the GUI tree.
        /// </summary>
        /// <param name="control">Control to search for.</param>
        /// <returns>TRUE if control was found, otherwise FALSE.</returns>
        internal protected bool IsChild(UIComponent key)
        {
            if (key == this)
                return true;
            else
            {
                foreach (UIComponent control in this.controls)
                {
                    if (control.IsChild(key))
                        return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Adds a control as a child, if it has not already been added. Also
        /// initializes control.
        /// </summary>
        /// <param name="control">Control to add.</param>
        public virtual void Add(UIComponent control)
        {
            if (!this.controls.Contains(control))
            {
                control.Parent = this;
                control.Initialize();
                this.controls.Add(control);
            }
        }

        /// <summary>
        /// Removes the child control.
        /// </summary>
        /// <param name="control">Control to remove.</param>
        /// <returns>TRUE if control existed, otherwise FALSE.</returns>
        public virtual bool Remove(UIComponent control)
        {
            bool result = false;

            if (this.controls.Remove(control))
            {
                control.CleanUp();
                result = true;
            }

            return result;
        }

        /// <summary>
        /// Checks a skin rectangle for valid dimensions.
        /// </summary>
        /// <param name="location">Skin rectangle to check.</param>
        /// <returns>true if rectangle is valid, otherwise false</returns>
        internal static bool CheckSkinLocation(Rectangle location)
        {
            bool result = false;

            if (
                location.X >= 0 &&
                location.Y >= 0 &&
                location.Width > 0 &&
                location.Height > 0
                )
                result = true;

            return result;
        }

        /// <summary>
        /// Invokes RequiresRedraw event.
        /// </summary>
        public void Redraw()
        {
            if (RequiresRedraw != null)
                RequiresRedraw.Invoke(this);
        }

        /// <summary>
        /// Refreshes control's position, as well as all children.
        /// </summary>
        protected void Refresh()
        {
            if (this.parent != null)
            {
                this.absolutePosition.X = this.parent.AbsolutePosition.X + X;
                this.absolutePosition.Y = this.parent.AbsolutePosition.Y + Y;
            }
            else
            {
                this.absolutePosition.X = X;
                this.absolutePosition.Y = Y;
            }

            // Refresh children
            foreach (UIComponent control in this.controls)
                control.Refresh();
        }

        public void ApplySkin(Skin skin)
        {
            skin.Apply(this);

            foreach (UIComponent control in this.controls)
                control.ApplySkin(skin);
        }

        /// <summary>
        /// Tells all children to draw themselves.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        internal virtual void DrawControl(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (UIComponent control in this.controls)
                control.DrawControl(gameTime, spriteBatch);
        }

        /// <summary>
        /// Performs culling and clipping to inside the parent control, before
        /// calling DrawControl() which should be overridden to actually draw
        /// the control, and then it draws its children.
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw with.</param>
        /// <param name="parentScissor">The scissor region of the parent control.</param>
        internal void Draw(SpriteBatch spriteBatch, Rectangle parentScissor)
        {
            // Create rectangle with the absolute dimensions of this control
            Rectangle thisScissor = location;
            thisScissor.X = absolutePosition.X;
            thisScissor.Y = absolutePosition.Y;

            // Cull this control if it isn't inside the parent
            bool result;
            parentScissor.Intersects(ref thisScissor, out result);

            if (result)
            {
                // Clip this control so it is inside the parent
                if (thisScissor.X < parentScissor.X)
                {
                    thisScissor.Width -= parentScissor.X - thisScissor.X;
                    thisScissor.X = parentScissor.X;
                }
                if (thisScissor.Right > parentScissor.Right)
                    thisScissor.Width -= thisScissor.Right - parentScissor.Right;

                if (thisScissor.Y < parentScissor.Y)
                {
                    thisScissor.Height -= parentScissor.Y - thisScissor.Y;
                    thisScissor.Y = parentScissor.Y;
                }
                if (thisScissor.Bottom > parentScissor.Bottom)
                    thisScissor.Height -= thisScissor.Bottom - parentScissor.Bottom;

                // Actually draw the control
                DrawControl(spriteBatch, thisScissor);

                // Draw children
                foreach (UIComponent control in this.controls)
                    control.Draw(spriteBatch, thisScissor);
            }
        }

        /// <summary>
        /// Override to create a graphical control that draws itself.
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw with.</param>
        /// <param name="parentScissor">The scissor region of the parent control.</param>
        protected virtual void DrawControl(SpriteBatch spriteBatch, Rectangle parentScissor)
        {
        }

        #region Focusing
        /// <summary>
        /// Checks children if they can have focus, then checks itself. Uses
        /// children's z-order to determine which one should have focus.
        /// </summary>
        /// <param name="x">Mouse x-position.</param>
        /// <param name="y">Mouse y-position.</param>
        /// <returns>The object taking focus, otherwise null.</returns>
        internal virtual UIComponent CheckFocus(int x, int y)
        {
            UIComponent result = null;

            // Check if control is entitled to focus
            if (!this.isAnimating)
            {
                // Check that the mouse position is inside this control
                if (CheckCoordinates(x, y))
                {
                    float zTemp = 0.0f;

                    // Go through each child asking them if they can have focus
                    foreach (UIComponent control in this.controls)
                    {
                        // Keep track of highest z-order
                        if (control.ZOrder >= zTemp)
                        {
                            UIComponent child = control.CheckFocus(x, y);
                            if (child != null)
                            {
                                // Set result and update highest z-order
                                result = child;
                                zTemp = control.ZOrder;
                            }
                        }
                    }

                    // If no child took focus, see if this control can take it
                    if (result == null && this.canHaveFocus)
                        result = this;
                }
            }

            return result;
        }

        /// <summary>
        /// Asks children for their mouse status, and invokes MouseOut event
        /// for this control if necessary.
        /// </summary>
        /// <param name="args">Mouse event arguments.</param>
        /// <returns>Control asking for MouseOver event, otherwise null.</returns>
        internal UIComponent CheckMouseStatus(MouseEventArgs args)
        {
            UIComponent result = null;

            // Check if control can receive MouseOver and MouseOut events
            if (!this.isAnimating)
            {
                // Check if MouseOut event should be invoked
                bool mouseOver = CheckCoordinates(args.Position.X, args.Position.Y);
                if (this.isMouseOver)
                {
                    if (!mouseOver)
                    {
                        this.isMouseOver = false;
                        MouseOut.Invoke(args);
                    }
                }

                if (mouseOver)
                {
                    float zTemp = 0.0f;

                    // Ask each child to check it's mouse status
                    foreach (UIComponent control in this.controls)
                    {
                        // Keep track of highest z-order
                        if (control.ZOrder >= zTemp)
                        {
                            UIComponent child = control.CheckMouseStatus(args);

                            if (child != null)
                            {
                                // MouseOut last result
                                if (result != null && result.IsMouseOver)
                                    result.InvokeMouseOut(args);

                                // Set result and update highest z-order
                                result = child;
                                zTemp = control.ZOrder;
                            }
                        }
                    }

                    // If no child requires the event, see if this control can
                    // take it.
                    if (result == null && this.canHaveFocus)
                        result = this;
                }
                else
                {
                    // Ensure each control can receive MouseOut event
                    foreach (UIComponent control in this.controls)
                        control.CheckMouseStatus(args);
                }
            }

            return result;
        }

        /// <summary>
        /// Invokes MouseOver event.
        /// </summary>
        /// <param name="args">Event arguments.</param>
        internal void InvokeMouseOver(MouseEventArgs args)
        {
            this.isMouseOver = true;
            MouseOver.Invoke(args);
        }

        /// <summary>
        /// Invokes MouseOut event.
        /// </summary>
        /// <param name="args">Event arguments.</param>
        internal void InvokeMouseOut(MouseEventArgs args)
        {
            this.isMouseOver = false;
            MouseOut.Invoke(args);
        }

        /// <summary>
        /// Invokes the GetFocus event.
        /// </summary>
        internal void GiveFocus()
        {
            if (GetFocus != null)
                GetFocus.Invoke();
        }

        /// <summary>
        /// Invokes the LoseFocus event.
        /// </summary>
        internal void TakeFocus()
        {
            if (LoseFocus != null)
                LoseFocus.Invoke();
        }

        /// <summary>
        /// Checks if the specified position is inside the control's absolute
        /// bounds.
        /// </summary>
        /// <param name="x">X-position.</param>
        /// <param name="y">Y-position.</param>
        /// <returns>true if inside, otherwise false.</returns>
        internal bool CheckCoordinates(int x, int y)
        {
            bool result = false;

            if (x >= this.absolutePosition.X && (x < (this.absolutePosition.X + this.location.Width)))
            {
                if (y >= this.absolutePosition.Y && (y < (this.absolutePosition.Y + this.location.Height)))
                    result = true;
            }

            return result;
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// Receives all KeyDown events, and checks if control is focused and
        /// should receive event.
        /// </summary>
        /// <param name="args">Key event arguments.</param>
        protected virtual void KeyDownIntercept(KeyEventArgs args)
        {
            if (this.guiManager.GetFocus() == this)
                KeyDown.Invoke(args);
        }

        /// <summary>
        /// Receives all KeyUp events, and checks if control is focused and
        /// should receive event.
        /// </summary>
        /// <param name="args">Key event arguments.</param>
        protected virtual void KeyUpIntercept(KeyEventArgs args)
        {
            if (this.guiManager.GetFocus() == this)
                KeyUp.Invoke(args);
        }

        /// <summary>
        /// Receives all MouseDown events, and checks if control is focused and
        /// should receive event.
        /// </summary>
        /// <param name="args">Mouse event arguments.</param>
        protected virtual void MouseDownIntercept(MouseEventArgs args)
        {
            // Check coordinates as well because modal mode messes up the
            // MouseDown event.
            if (this.guiManager.GetFocus() == this &&
                CheckCoordinates(args.Position.X, args.Position.Y)
                )
                MouseDown.Invoke(args);
        }

        /// <summary>
        /// Receives all MouseUp events, and checks if control is focused and
        /// should receive event.
        /// </summary>
        /// <param name="args">Mouse event arguments.</param>
        protected virtual void MouseUpIntercept(MouseEventArgs args)
        {
            if (this.guiManager.GetFocus() == this)
                MouseUp.Invoke(args);
        }

        /// <summary>
        /// Receives all MouseMove events, and checks if control is focused and
        /// should receive event.
        /// </summary>
        /// <param name="args">Mouse event arguments.</param>
        protected virtual void MouseMoveIntercept(MouseEventArgs args)
        {
            if (this.guiManager.GetFocus() == this)
                MouseMove.Invoke(args);
        }

        /// <summary>
        /// Does nothing, override to handle event.
        /// </summary>
        /// <param name="args">Key event arguments.</param>
        protected virtual void OnKeyDown(KeyEventArgs args)
        {
        }

        /// <summary>
        /// Does nothing, override to handle event.
        /// </summary>
        /// <param name="args">Key event arguments.</param>
        protected virtual void OnKeyUp(KeyEventArgs args)
        {
        }

        /// <summary>
        /// Used for click event. Override to handle event.
        /// </summary>
        /// <param name="args">Mouse event arguments.</param>
        protected virtual void OnMouseDown(MouseEventArgs args)
        {
            if (args.Button == MouseButtons.Left)
                this.isPressed = true;
        }

        /// <summary>
        /// Used for click event. Override to handle event.
        /// </summary>
        /// <param name="args">Mouse event arguments.</param>
        protected virtual void OnMouseUp(MouseEventArgs args)
        {
            if (this.isPressed)
            {
                if (args.Button == MouseButtons.Left)
                {
                    this.isPressed = false;

                    // Check if button was clicked
                    if (CheckCoordinates(args.Position.X, args.Position.Y))
                    {
                        if (Click != null)
                            Click.Invoke(this);
                    }
                }
            }
        }

        /// <summary>
        /// Does nothing, override to handle event.
        /// </summary>
        /// <param name="args">Mouse event arguments.</param>
        protected virtual void OnMouseMove(MouseEventArgs args)
        {
        }

        /// <summary>
        /// Does nothing, override to handle event.
        /// </summary>
        /// <param name="args">Mouse event arguments.</param>
        protected virtual void OnMouseOver(MouseEventArgs args)
        {
        }

        /// <summary>
        /// Does nothing, override to handle event.
        /// </summary>
        /// <param name="args">Mouse event arguments.</param>
        protected virtual void OnMouseOut(MouseEventArgs args)
        {
        }

        /// <summary>
        /// Simply refreshes, override to handle event.
        /// </summary>
        /// <param name="args">Moved control.</param>
        protected virtual void OnMove(UIComponent sender)
        {
            Refresh();
        }

        /// <summary>
        /// Simply refreshes, override to handle event.
        /// </summary>
        /// <param name="args">Resized control.</param>
        protected virtual void OnResize(UIComponent sender)
        {
            Refresh();
        }

        /// <summary>
        /// Called when a parent control is moved. Invokes Move event.
        /// </summary>
        /// <param name="sender">Moved control.</param>
        protected virtual void OnParentMoved(UIComponent sender)
        {
            Move.Invoke(this);
        }

        /// <summary>
        /// Called when a parent controls is resized.
        /// </summary>
        /// <param name="sender">Resized control.</param>
        protected virtual void OnParentResized(UIComponent sender)
        {
        }

        /// <summary>
        /// Called when control needs to redraw it's texture. Events are used
        /// to filter redraw message to all parents.
        /// </summary>
        /// <param name="sender">Control requiring redraw.</param>
        protected virtual void OnRequiresRedraw(UIComponent sender)
        {
            this.isRedrawRequired = true;
            // Tell parent it also needs to redraw
            if (this.parent != null)
                this.parent.OnRequiresRedraw(sender);
        }

        /// <summary>
        /// Does nothing, override to handle event.
        /// </summary>
        /// <param name="args">Event arguments.</param>
        protected virtual void OnGetFocus()
        {
        }

        /// <summary>
        /// Does nothing, override to handle event.
        /// </summary>
        /// <param name="args">Event arguments.</param>
        protected virtual void OnLoseFocus()
        {
        }
        #endregion
    }
}