#region File Description
//-----------------------------------------------------------------------------
// File:      ComboBox.cs
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
    /// <summary>
    /// A graphical combobox control.
    /// </summary>
    /// 
    /// <remarks>
    /// Contains a textbox and a listbox, as well as an Icon object, faking an
    /// ImageButton. The reason is that they share most of the same code, and
    /// without any mouse event handling, it's easier to predict state changes
    /// for when list box is open (has focus).
    /// </remarks>
    public class ComboBox : UIComponent
    {
        #region Default Properties
        private static int defaultWidth = 200;
        private static int defaultHeight = 20;
        private static Rectangle defaultButtonSkin = new Rectangle(138, 5, 20, 20);
        private static Rectangle defaultButtonHoverSkin = new Rectangle(159, 5, 20, 20);
        private static Rectangle defaultButtonPressedSkin = new Rectangle(180, 5, 20, 20);

        /// <summary>
        /// Sets the default control width.
        /// </summary>
        /// <value>Must be greater than 0.</value>
        public static int DefaultWidth
        {
            set
            {
                Debug.Assert(value > 0);
                defaultWidth = value;
            }
        }

        /// <summary>
        /// Sets the default control height.
        /// </summary>
        /// <value>Must be greater than 0.</value>
        public static int DefaultHeight
        {
            set
            {
                Debug.Assert(value > 0);
                defaultHeight = value;
            }
        }

        /// <summary>
        /// Sets the default skin of the ComboBox button.
        /// </summary>
        public static Rectangle DefaultButtonSkin
        {
            set { defaultButtonSkin = value; }
        }

        /// <summary>
        /// Sets the default hover skin of the ComboBox button.
        /// </summary>
        public static Rectangle DefaultButtonHoverSkin
        {
            set { defaultButtonHoverSkin = value; }
        }

        /// <summary>
        /// Sets the default pressed skin of the ComboBox button.
        /// </summary>
        public static Rectangle DefaultButtonPressedSkin
        {
            set { defaultButtonPressedSkin = value; }
        }
        #endregion

        #region Fields
        private TextBox textBox;
        private Icon button;
        private ListBox listBox;
        private bool isListBoxOpen;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the current text.
        /// </summary>
        public string SelectedText
        {
            get { return this.textBox.Text; }
        }

        /// <summary>
        /// Get/Set the selected index.
        /// </summary>
        public int SelectedIndex
        {
            get { return listBox.SelectedIndex; }
            set
            {
                this.listBox.SelectedIndex = value;
                if (this.listBox.SelectedIndex == -1)
                    this.textBox.Text = "";
            }
        }

        /// <summary>
        /// Get/Set whether the text can be modified.
        /// </summary>
        public bool IsEditable
        {
            get { return textBox.IsEditable; }
            set { this.textBox.IsEditable = value; }
        }

        /// <summary>
        /// Sets the skin of the ComboBox skin.
        /// </summary>
        public Rectangle ButtonSkin
        {
            set { this.button.SetSkinLocation(0, value); }
        }

        /// <summary>
        /// Sets the hover skin of the ComboBox skin.
        /// </summary>
        public Rectangle ButtonHoverSkin
        {
            set { this.button.SetSkinLocation(1, value); }
        }

        /// <summary>
        /// Sets the pressed skin of the ComboBox skin.
        /// </summary>
        public Rectangle ButtonPressedSkin
        {
            set { this.button.SetSkinLocation(2, value); }
        }
        #endregion

        #region Events
        public event SelectionChangedHandler SelectionChanged;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="game">The currently running Game object.</param>
        /// <param name="guiManager">GUIManager that this control is part of.</param>
        public ComboBox(Game game, GUIManager guiManager)
            : base(game, guiManager)
        {
            this.isListBoxOpen = false;

            #region Create Child Controls
            this.textBox = new TextBox(game, guiManager);
            this.button = new Icon(game, guiManager);
            this.listBox = new ListBox(game, guiManager);
            #endregion

            #region Add Child Controls
            Add(this.textBox);
            Add(this.button);
            #endregion

            #region Set Properties
            this.listBox.ResizeToFit = true;
            #endregion

            #region Set Default Properties
            this.Width = defaultWidth;
            this.Height = defaultHeight;
            ButtonSkin = defaultButtonSkin;
            ButtonHoverSkin = defaultButtonHoverSkin;
            ButtonPressedSkin = defaultButtonPressedSkin;
            #endregion

            #region Event Handlers
            this.button.MouseOver += new MouseOverHandler(OnButtonMouseOver);
            this.button.MouseOut += new MouseOutHandler(OnButtonMouseOut);
            this.button.MouseDown += new MouseDownHandler(OnButtonMouseDown);
            this.button.LoseFocus += new LoseFocusHandler(OnButtonLoseFocus);
            this.listBox.SelectedChanged += new SelectionChangedHandler(OnSelectionChanged);
            this.listBox.LoseFocus += new LoseFocusHandler(OnListBoxLoseFocus);
            #endregion
        }
        #endregion

        /// <summary>
        /// Performs necessary cleanup operations when control is removed from
        /// a parent, or when it is no longer needed.
        /// </summary>
        /// <remarks>
        /// This method is necessary to ensure references to object are
        /// removed, allowing the object to be cleared by the garbage
        /// collector.
        /// 
        /// As such, all event handlers to outside objects such as
        /// Chimera.GUI.InputEventSystem should be removed. All controls that have been
        /// added will automatically be taken care of, but any UIComponent
        /// controls that have not been added, should have their CleanUp()
        /// methods called from this method.
        /// </remarks>
        public override void CleanUp()
        {
            this.listBox.CleanUp();

            base.CleanUp();
        }

        /// <summary>
        /// Adds a string as an entry in the listbox.
        /// </summary>
        /// <param name="text">Text of the new entry.</param>
        public void AddEntry(string text)
        {
            // Add entry to child listbox
            this.listBox.AddEntry(text);
        }

        /// <summary>
        /// Clears all entries from listbox.
        /// </summary>
        public void Clear()
        {
            this.listBox.Clear();
        }

        /// <summary>
        /// Closes listbox.
        /// </summary>
        protected void CloseListBox()
        {
            if (this.isListBoxOpen)
            {
                GUIManager.Remove(this.listBox);

                // Check if mouse is over button
                if (this.button.CheckCoordinates(InputEvents.GetMouseX(), InputEvents.GetMouseY()))
                    this.button.CurrentSkinState = SkinState.Hover;
                else
                    this.button.CurrentSkinState = SkinState.Normal;

                this.isListBoxOpen = false;
            }
        }

        #region Event Handlers
        /// <summary>
        /// Close listbox and update textbox, and invoke SelectionChanged
        /// event.
        /// </summary>
        /// <param name="sender">Selected control.</param>
        protected void OnSelectionChanged(UIComponent sender)
        {
            // Update text
            string text = this.listBox.GetSelectedText();
            if (text != null)
                this.textBox.Text = text;

            CloseListBox();

            if (SelectionChanged != null)
                SelectionChanged.Invoke(this);
        }

        protected void OnButtonMouseOver(MouseEventArgs args)
        {
            if (!this.isListBoxOpen)
                this.button.CurrentSkinState = SkinState.Hover;
        }

        protected void OnButtonMouseOut(MouseEventArgs args)
        {
            if (!this.isListBoxOpen)
                this.button.CurrentSkinState = SkinState.Normal;
        }

        /// <summary>
        /// Open listbox when button is pressed.
        /// </summary>
        /// <param name="args">Mouse event arguments.</param>
        protected void OnButtonMouseDown(MouseEventArgs args)
        {
            if (args.Button == MouseButtons.Left)
            {
                if (!this.isListBoxOpen && this.listBox.Count > 0)
                {
                    this.listBox.X = AbsolutePosition.X;
                    this.listBox.Y = AbsolutePosition.Y + Height - 1;
                    this.listBox.Width = Width;

                    GUIManager.Add(this.listBox);

                    this.button.CurrentSkinState = SkinState.Pressed;

                    this.isListBoxOpen = true;
                }
                else
                    CloseListBox();
            }
        }

        /// <summary>
        /// Close listbox when it loses focus.
        /// </summary>
        protected void OnListBoxLoseFocus()
        {
            CloseListBox();
        }

        /// <summary>
        /// Close listbox when it loses focus.
        /// </summary>
        protected void OnButtonLoseFocus()
        {
            // Only close if not listbox
            if (GUIManager.GetFocus() != this.listBox)
                CloseListBox();
        }

        /// <summary>
        /// Resize child controls.
        /// </summary>
        /// <param name="sender">Resizing control.</param>
        protected override void OnResize(UIComponent sender)
        {
            base.OnResize(sender);

            this.button.Width = Height;
            this.button.Height = Height;
            this.button.X = Width - this.button.Width;
            this.textBox.Width = Width - this.button.Width;
            this.textBox.Height = Height;
        }
        #endregion
    }
}