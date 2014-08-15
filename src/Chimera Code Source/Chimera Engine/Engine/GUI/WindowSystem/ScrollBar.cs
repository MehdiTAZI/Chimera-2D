#region File Description
//-----------------------------------------------------------------------------
// File:      ScrollBar.cs
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
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Chimera.GUI.InputEventSystem;
#endregion

namespace Chimera.GUI.WindowSystem
{
    #region Delegates
    /// <summary>
    /// When scrollbar's position changes.
    /// </summary>
    /// <param name="position">New scrollbar position.</param>
    public delegate void ScrollHandler(int position);
    #endregion

    /// <summary>
    /// A graphical vertical scrollbar. To use just set the maximum value, and
    /// register for the OnScroll event.
    /// </summary>
    /// <remarks>
    /// Even though only vertical scrollbars are currently implemented, it
    /// shouldn't be difficult to add horizontal functionality at some point in
    /// the future.
    /// </remarks>
    public class ScrollBar : Icon
    {
        #region Default Properties
        private static int defaultButtonSize = 17;
        private static Rectangle defaultBackgroundSkin = new Rectangle(66, 5, 17, 17);
        private static Rectangle defaultTopButtonSkin = new Rectangle(84, 23, 17, 17);
        private static Rectangle defaultTopButtonHoverSkin = new Rectangle(102, 23, 17, 17);
        private static Rectangle defaultTopButtonPressedSkin = new Rectangle(120, 23, 17, 17);
        private static Rectangle defaultBottomButtonSkin = new Rectangle(84, 5, 17, 17);
        private static Rectangle defaultBottomButtonHoverSkin = new Rectangle(102, 5, 17, 17);
        private static Rectangle defaultBottomButtonPressedSkin = new Rectangle(120, 5, 17, 17);
        private static Rectangle defaultThumbSkin = new Rectangle(66, 23, 17, 10);
        private static Rectangle defaultThumbHoverSkin = new Rectangle(66, 34, 17, 10);
        private static Rectangle defaultThumbPressedSkin = new Rectangle(66, 45, 17, 10);

        /// <summary>
        /// Sets the default button width and height.
        /// </summary>
        /// <value>Must be greater than 0.</value>
        public static int DefaultButtonSize
        {
            set
            {
                Debug.Assert(value > 0);
                defaultButtonSize = value;
            }
        }

        /// <summary>
        /// Sets the background skin.
        /// </summary>
        public static Rectangle DefaultBackgroundSkin
        {
            set { defaultBackgroundSkin = value; }
        }

        /// <summary>
        /// Sets the top button skin.
        /// </summary>
        public static Rectangle DefaultTopButtonSkin
        {
            set { defaultTopButtonSkin = value; }
        }

        /// <summary>
        /// Sets the top button hover skin.
        /// </summary>
        public static Rectangle DefaultTopButtonHoverSkin
        {
            set { defaultTopButtonHoverSkin = value; }
        }

        /// <summary>
        /// Sets the top button pressed skin.
        /// </summary>
        public static Rectangle DefaultTopButtonPressedSkin
        {
            set { defaultTopButtonPressedSkin = value; }
        }

        /// <summary>
        /// Sets the bottom button skin.
        /// </summary>
        public static Rectangle DefaultBottomButtonSkin
        {
            set { defaultBottomButtonSkin = value; }
        }

        /// <summary>
        /// Sets the bottom button hover skin.
        /// </summary>
        public static Rectangle DefaultBottomButtonHoverSkin
        {
            set { defaultBottomButtonHoverSkin = value; }
        }

        /// <summary>
        /// Sets the bottom button pressed skin.
        /// </summary>
        public static Rectangle DefaultBottomButtonPressedSkin
        {
            set { defaultBottomButtonPressedSkin = value; }
        }

        /// <summary>
        /// Sets the thumb skin.
        /// </summary>
        public static Rectangle DefaultThumbSkin
        {
            set { defaultThumbSkin = value; }
        }

        /// <summary>
        /// Sets the thumb hover skin.
        /// </summary>
        public static Rectangle DefaultThumbHoverSkin
        {
            set { defaultThumbHoverSkin = value; }
        }

        /// <summary>
        /// Sets the thumb pressed skin.
        /// </summary>
        public static Rectangle DefaultThumbPressedSkin
        {
            set { defaultThumbPressedSkin = value; }
        }
        #endregion

        #region Fields
        // Should be the same as the keyboard delay and repeat rates
        private const int RepeatDelay = 500;
        private const int RepeatRate = 50;

        private ImageButton topButton;
        private ImageButton bottomButton;
        private Bar thumb;
        private int value;
        private int viewable;
        private int maximumValue;
        private int scrollStep;
        private int countdown;
        private bool firstRepeat;
        private bool isTopPressed;
        private bool isBottomPressed;
        private bool isTopOver;
        private bool isBottomOver;
        private bool draggingThumb;
        private Point lastLocation;
        #endregion

        #region Properties
        /// <summary>
        /// Sets the button size, and the width of the scrollbar.
        /// </summary>
        public int ButtonSize
        {
            get { return this.topButton.Width; }
            set
            {
                MinWidth = value;
                MinHeight = 2 * value;
                this.thumb.Y = value;
                Width = value;
            }
        }

        /// <summary>
        /// Get/Set the amount size of the viewport.
        /// </summary>
        /// <value>Must be at leat 0.</value>
        public int Viewable
        {
            get { return this.viewable; }
            set
            {
                Debug.Assert(value >= 0);
                this.viewable = value;
                CalculateThumbSize();

                if (this.viewable > this.maximumValue)
                    ScrollTo(0);
            }
        }

        /// <summary>
        /// Get/Set the amount to scroll each time a button is pressed.
        /// </summary>
        public int ScrollStep
        {
            get { return this.scrollStep; }
            set { this.scrollStep = value; }
        }

        /// <summary>
        /// Get/Set the current scroll value.
        /// </summary>
        public int Value
        {
            get { return value; }
            set { ScrollTo(value); }
        }

        /// <summary>
        /// Get/Set the maximum scroll value.
        /// </summary>
        /// <value>Must be at least 0.</value>
        public int MaximumValue
        {
            get { return this.maximumValue; }
            set
            {
                Debug.Assert(value >= 0);
                this.maximumValue = value;
                CalculateThumbSize();

                if (this.viewable > this.maximumValue)
                    ScrollTo(0);
            }
        }

        /// <summary>
        /// Sets the control background skin.
        /// </summary>
        public Rectangle BackgroundSkin
        {
            set { SetSkinLocation(0, value); }
        }

        /// <summary>
        /// Sets the top button skin.
        /// </summary>
        public Rectangle TopButtonSkin
        {
            set { this.topButton.SetSkinLocation(0, value); }
        }

        /// <summary>
        /// Sets the top button hover skin.
        /// </summary>
        public Rectangle TopButtonHoverSkin
        {
            set { this.topButton.SetSkinLocation(1, value); }
        }

        /// <summary>
        /// Sets the top button pressed skin.
        /// </summary>
        public Rectangle TopButtonPressedSkin
        {
            set { this.topButton.SetSkinLocation(2, value); }
        }

        /// <summary>
        /// Sets the bottom button skin.
        /// </summary>
        public Rectangle BottomButtonSkin
        {
            set { this.bottomButton.SetSkinLocation(0, value); }
        }

        /// <summary>
        /// Sets the bottom button hover skin.
        /// </summary>
        public Rectangle BottomButtonHoverSkin
        {
            set { this.bottomButton.SetSkinLocation(1, value); }
        }

        /// <summary>
        /// Sets the bottom button pressed skin.
        /// </summary>
        public Rectangle BottomButtonPressedSkin
        {
            set { this.bottomButton.SetSkinLocation(2, value); }
        }

        /// <summary>
        /// Sets the thumb skin.
        /// </summary>
        public Rectangle ThumbSkin
        {
            set { this.thumb.SetSkinLocation(0, value); }
        }

        /// <summary>
        /// Sets the thumb hover skin.
        /// </summary>
        public Rectangle ThumbHoverSkin
        {
            set { this.thumb.SetSkinLocation(1, value); }
        }

        /// <summary>
        /// Sets the thumb pressed skin.
        /// </summary>
        public Rectangle ThumbPressedSkin
        {
            set { this.thumb.SetSkinLocation(2, value); }
        }

        /// <summary>
        /// Gets the range of scroll values.
        /// </summary>
        private float Range
        {
            get { return (float)(this.maximumValue - this.viewable); }
        }

        /// <summary>
        /// Gets the size of the thumb.
        /// </summary>
        private float ShaftHeight
        {
            get { return (float)(Height - (Width * 2)); }
        }
        #endregion

        #region Events
        public event ScrollHandler Scroll;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="game">The currently running Game object.</param>
        /// <param name="guiManager">GUIManager that this control is part of.</param>
        public ScrollBar(Game game, GUIManager guiManager)
            : base(game, guiManager)
        {
            this.value = 0;
            this.viewable = 1;
            this.maximumValue = 1;
            this.scrollStep = 1;
            this.countdown = 0;
            this.firstRepeat = true;
            this.isTopPressed = false;
            this.isBottomPressed = false;
            this.isTopOver = false;
            this.isBottomOver = false;
            this.draggingThumb = false;
            this.lastLocation = Point.Zero;

            #region Create Child Controls
            this.topButton = new ImageButton(game, guiManager);
            this.bottomButton = new ImageButton(game, guiManager);
            this.thumb = new Bar(game, guiManager);
            #endregion

            #region Add Child Controls
            // Thumb added first so buttons drawn on top when really small
            Add(this.thumb);
            Add(this.topButton);
            Add(this.bottomButton);
            #endregion

            #region Set Properties
            MinWidth = defaultButtonSize;
            MinHeight = 2 * defaultButtonSize;
            Scale = true;
            this.thumb.CanHaveFocus = true;
            this.thumb.IsVertical = true;
            this.thumb.Y = defaultButtonSize;
            #endregion

            #region Set Default Properties
            Width = defaultButtonSize;
            BackgroundSkin = defaultBackgroundSkin;
            TopButtonSkin = defaultTopButtonSkin;
            TopButtonHoverSkin = defaultTopButtonHoverSkin;
            TopButtonPressedSkin = defaultTopButtonPressedSkin;
            BottomButtonSkin = defaultBottomButtonSkin;
            BottomButtonHoverSkin = defaultBottomButtonHoverSkin;
            BottomButtonPressedSkin = defaultBottomButtonPressedSkin;
            ThumbSkin = defaultThumbSkin;
            ThumbHoverSkin = defaultThumbHoverSkin;
            ThumbPressedSkin = defaultThumbPressedSkin;
            #endregion

            #region Event Handlers
            this.topButton.MouseDown += new MouseDownHandler(OnTopButtonDown);
            this.topButton.MouseUp += new MouseUpHandler(OnButtonUp);
            this.topButton.MouseOut += new MouseOutHandler(OnTopButtonOut);
            this.topButton.MouseOver += new MouseOverHandler(OnTopButtonOver);
            this.bottomButton.MouseDown += new MouseDownHandler(OnBottomButtonDown);
            this.bottomButton.MouseUp += new MouseUpHandler(OnButtonUp);
            this.bottomButton.MouseOver += new MouseOverHandler(OnBottomButtonOver);
            this.bottomButton.MouseOut += new MouseOutHandler(OnBottomButtonOut);
            this.thumb.MouseOver += new MouseOverHandler(OnThumbMouseOver);
            this.thumb.MouseOut += new MouseOutHandler(OnThumbMouseOut);
            this.thumb.MouseDown += new MouseDownHandler(OnThumbDown);
            this.thumb.MouseUp += new MouseUpHandler(OnThumbUp);
            this.thumb.MouseMove += new MouseMoveHandler(OnThumbMove);
            #endregion
        }
        #endregion

        /// <summary>
        /// Handles the timing, and triggers scrolling when buttons are
        /// pressed.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // Trigger scroll if countdown is reached
            if (this.countdown <= 0)
            {
                if (this.isTopPressed && this.isTopOver)
                    ScrollTo(this.value - this.scrollStep);
                else if (this.isBottomPressed && this.isBottomOver)
                    ScrollTo(this.value + this.scrollStep);

                if (this.firstRepeat)
                {
                    this.countdown += RepeatDelay;
                    this.firstRepeat = false;
                }
                else
                    this.countdown = RepeatRate;
            }

            // Update timer
            if ((this.isTopPressed && this.isTopOver) ||
                (this.isBottomPressed && this.isBottomOver)
                )
                this.countdown -= gameTime.ElapsedRealTime.Milliseconds;
            else
                this.countdown = 0;

            base.Update(gameTime);
        }

        /// <summary>
        /// Calculates the scroll thumb height, based on how much of the total
        /// scrollbar size can be fit in parent control.
        /// </summary>
        private void CalculateThumbSize()
        {
            int heightBefore = this.thumb.Height;

            try
            {
                // Calculate height
                float thumbHeight = ShaftHeight / ((float)this.maximumValue / this.viewable);
                this.thumb.Height = Convert.ToInt32(thumbHeight);
            }
            catch
            {
                this.thumb.Height = (int)ShaftHeight;
            }

            // Cap sizes to allowed values
            if (this.thumb.Height < 10)
                this.thumb.Height = 10;
            else if (this.thumb.Height > ShaftHeight)
                this.thumb.Height = (int)ShaftHeight;

            if (this.thumb.Height != heightBefore)
                Redraw();
        }

        /// <summary>
        /// Scrolls to the specified position, and invokes the Scroll event.
        /// </summary>
        /// <param name="position">Position to scroll to.</param>
        private void ScrollTo(int position)
        {
            // Cap position to allowed values
            if (position > Range)
                position = (int)Range;
            if (position < 0)
                position = 0;

            value = position;

            try
            {
                // Calculate pixel position
                float height = Height - (Width * 2) - this.thumb.Height;
                float percentage = (float)value / Range;
                this.thumb.Y = Width + Convert.ToInt32(height * percentage);
            }
            catch
            {
                this.thumb.Y = this.topButton.Y + this.topButton.Height;
            }

            if (Scroll != null)
                Scroll.Invoke(value);

            Redraw();
        }

        /// <summary>
        /// Converts mouse y-coordinates to a scroll position.
        /// </summary>
        /// <param name="mousePosition">Mouse y-position.</param>
        /// <returns>Scroll position.</returns>
        private int MouseToScrollPosition(int mousePosition)
        {
            float y = (float)mousePosition;
            float ratio = Range / (ShaftHeight - (float)this.thumb.Height);
            float position = ratio * y;

            try
            {
                return Convert.ToInt32(position);
            }
            catch
            {
                return 0;
            }
        }

        #region Event Handlers
        /// <summary>
        /// Update thumb skin.
        /// </summary>
        /// <param name="args">Mouse event arguments.</param>
        protected void OnThumbMouseOver(MouseEventArgs args)
        {
            if (!this.draggingThumb)
                this.thumb.CurrentSkinState = SkinState.Hover;
        }

        /// <summary>
        /// Update thumb skin.
        /// </summary>
        /// <param name="args">Mouse event arguments.</param>
        protected void OnThumbMouseOut(MouseEventArgs args)
        {
            if (!this.draggingThumb)
                this.thumb.CurrentSkinState = SkinState.Normal;
        }

        /// <summary>
        /// Starts dragging the thumb.
        /// </summary>
        /// <param name="args">Mouse event arguments.</param>
        protected void OnThumbDown(MouseEventArgs args)
        {
            if (args.Button == MouseButtons.Left)
            {
                this.draggingThumb = true;
                this.lastLocation = args.Position;
                this.thumb.CurrentSkinState = SkinState.Pressed;
            }
        }

        /// <summary>
        /// Finishes dragging the thumb.
        /// </summary>
        /// <param name="args">Mouse event arguments.</param>
        protected void OnThumbUp(MouseEventArgs args)
        {
            if (args.Button == MouseButtons.Left)
            {
                this.draggingThumb = false;

                // Check if hover state should be shown
                if (this.thumb.CheckCoordinates(args.Position.X, args.Position.Y))
                    this.thumb.CurrentSkinState = SkinState.Hover;
                else
                    this.thumb.CurrentSkinState = SkinState.Normal;

                // Get focus back from thumb
                GUIManager.SetFocus(this);
            }
        }

        /// <summary>
        /// Updates scroll position when the thumb is dragged.
        /// </summary>
        /// <param name="args">Mouse event arguments.</param>
        protected void OnThumbMove(MouseEventArgs args)
        {
            if (this.draggingThumb)
            {
                int yPosition = this.thumb.Y;
                int yDiff = args.Position.Y - this.lastLocation.Y;

                // Get the position from before move
                int beforeY = yPosition;

                // Actual resize
                yPosition += yDiff;

                ScrollTo(MouseToScrollPosition(yPosition - Width));

                this.lastLocation.Y += this.thumb.Y - beforeY;
            }
        }

        /// <summary>
        /// When the mouse is pressed inside the thumb trough, the thumb moves
        /// to that position. If above the thumb, the thumb top moves to that
        /// position, and the thumb bottom when clicking below.
        /// </summary>
        /// <param name="args">Mouse event arguments.</param>
        protected override void OnMouseDown(MouseEventArgs args)
        {
            base.OnMouseDown(args);

            if (args.Button == MouseButtons.Left)
            {
                // Convert mouse location to scrollbar location
                int yPosition = args.Position.Y - AbsolutePosition.Y;

                if (yPosition < this.thumb.Y) // Above the thumb
                    ScrollTo(MouseToScrollPosition(yPosition - Width));
                else // Below the thumb
                {
                    // +1 ensures the cursor is over the last pixel of the
                    // scrollbar, so user can instantly drag the scrollbar, as
                    // is the case with clicking above the thumb.
                    ScrollTo(MouseToScrollPosition(yPosition - Width - this.thumb.Height + 1));
                }
            }
        }

        /// <summary>
        /// Top button is pressed.
        /// </summary>
        /// <param name="args">Mouse event arguments.</param>
        protected void OnTopButtonDown(MouseEventArgs args)
        {
            if (args.Button == MouseButtons.Left)
            {
                this.isTopPressed = true;
                this.isTopOver = true;
                this.firstRepeat = true;
            }
        }

        /// <summary>
        /// Bottom button is pressed.
        /// </summary>
        /// <param name="args">Mouse event arguments.</param>
        protected void OnBottomButtonDown(MouseEventArgs args)
        {
            if (args.Button == MouseButtons.Left)
            {
                this.isBottomPressed = true;
                this.isBottomOver = true;
                this.firstRepeat = true;
            }
        }

        /// <summary>
        /// Either button is released.
        /// </summary>
        /// <param name="args">Mouse event arguments.</param>
        protected void OnButtonUp(MouseEventArgs args)
        {
            if (args.Button == MouseButtons.Left)
            {
                this.isTopPressed = false;
                this.isBottomPressed = false;
                this.isTopOver = false;
                this.isBottomOver = false;

                // Get focus back
                GUIManager.SetFocus(this);
            }
        }

        /// <summary>
        /// Mouse hovering over top button. Only scrolls when mouse is held
        /// over button.
        /// </summary>
        /// <param name="args">Mouse event arguments.</param>
        protected void OnTopButtonOver(MouseEventArgs args)
        {
            this.isTopOver = true;
        }

        /// <summary>
        /// Mouse hovering over bottom button. Only scrolls when mouse is held
        /// over button.
        /// </summary>
        /// <param name="args">Mouse event arguments.</param>
        protected void OnBottomButtonOver(MouseEventArgs args)
        {
            this.isBottomOver = true;
        }

        /// <summary>
        /// Mouse outside top button area. Only scrolls when mouse is held over
        /// button.
        /// </summary>
        /// <param name="args">Mouse event arguments.</param>
        protected void OnTopButtonOut(MouseEventArgs args)
        {
            this.isTopOver = false;
        }

        /// <summary>
        /// Mouse outside bottom button area. Only scrolls when mouse is held
        /// over button.
        /// </summary>
        /// <param name="args">Mouse event arguments.</param>
        protected void OnBottomButtonOut(MouseEventArgs args)
        {
            this.isBottomOver = false;
        }

        /// <summary>
        /// Update child controls.
        /// </summary>
        /// <param name="sender">Resized control.</param>
        protected override void OnResize(UIComponent sender)
        {
            base.OnResize(sender);

            // Widths
            this.topButton.Width = Width;
            this.topButton.Height = Width;
            this.bottomButton.Width = Width;
            this.bottomButton.Height = Width;
            this.thumb.Width = Width;
            this.bottomButton.Y = Height - this.bottomButton.Height;

            // Thumb size needs to be updated
            CalculateThumbSize();

            // Keep position up to date
            ScrollTo(this.value);
        }
        #endregion
    }
}