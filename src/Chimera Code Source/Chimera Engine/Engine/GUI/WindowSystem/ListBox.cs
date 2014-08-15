#region File Description
//-----------------------------------------------------------------------------
// File:      ListBox.cs
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
    #region Delegates
    /// <summary>
    /// When a listbox selection changes.
    /// </summary>
    /// <param name="sender">Selected control.</param>
    public delegate void SelectionChangedHandler(UIComponent sender);
    #endregion

    /// <summary>
    /// A graphical listbox control. Uses a scrollbar to allow more entries
    /// than can fit on control. Entries are actually children to a
    /// DrawableUIComponent, which acts as a viewport, only allowing some to
    /// be seen at one time. Scrollbar is only shown if required.
    /// </summary>
    public class ListBox : UIComponent
    {
        #region Default Properties
        private static int defaultWidth = 200;
        private static int defaultHeight = 150;
        private static int defaultHMargin = 5;
        private static int defaultVMargin = 2;
        private static string defaultFont = "Content/Fonts/DefaultFont";
        private static Rectangle defaultSkin = new Rectangle(84, 41, 25, 25);

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
        /// <value>Must be at least 0.</value>
        public static int DefaultHeight
        {
            set
            {
                Debug.Assert(value > 0);
                defaultHeight = value;
            }
        }

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
        /// Sets the default text font.
        /// </summary>
        /// <value>Must be a non-empty string.</value>
        public static string DefaultFont
        {
            set
            {
                Debug.Assert(value != null);
                Debug.Assert(value.Length > 0);
                defaultFont = value;
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
        private Box box;
        private UIComponent surface;
        private UIComponent viewPort;
        private ScrollBar scrollBar;
        private List<Label> entries;
        private SpriteFont font;
        private string fontFileName;
        private Label selectedLabel;
        private int selectedIndex;
        private int hMargin;
        private int vMargin;
        private bool resizeToFit;
        #endregion

        #region Properties
        /// <summary>
        /// Get/Set whether the listbox resize itself to fit all entries?
        /// </summary>
        public bool ResizeToFit
        {
            get { return this.resizeToFit; }
            set { this.resizeToFit = value; }
        }

        /// <summary>
        /// Gets the number of entries in the listbox.
        /// </summary>
        public int Count
        {
            get { return this.entries.Count; }
        }

        /// <summary>
        /// Get/Set the currently selected index.
        /// </summary>
        /// <value>Selection index, or -1 for no selection.</value>
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                if (value >= 0 && value < entries.Count)
                    Select(entries[value], value);
                else if (value == -1)
                    Select(null, -1);
            }
        }

        /// <summary>
        /// Sets the font to use for listbox entries.
        /// </summary>
        /// <value>Must not be a valid path.</value>
        public string Font
        {
            set
            {
                this.fontFileName = value;
                this.font = GUIManager.ContentManager.Load<SpriteFont>(value);
                this.scrollBar.ScrollStep = this. font.LineSpacing;
                RefreshEntries();
            }
        }

        /// <summary>
        /// Sets the horizontal padding.
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
        /// The vertical padding in the listbox.
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
        /// Sets the control skin.
        /// </summary>
        public Rectangle Skin
        {
            set { this.box.SetSkinLocation(0, value); }
        }
        #endregion

        #region Events
        public event SelectionChangedHandler SelectedChanged;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="game">The currently running Game object.</param>
        /// <param name="guiManager">GUIManager that this control is part of.</param>
        public ListBox(Game game, GUIManager guiManager)
            : base(game, guiManager)
        {
            this.entries = new List<Label>();
            this.selectedIndex = -1;
            this.resizeToFit = false;

            #region Create Child Controls
            this.box = new Box(game, guiManager);
            this.surface = new UIComponent(game, guiManager);
            this.viewPort = new UIComponent(game, guiManager);
            this.scrollBar = new ScrollBar(game, guiManager);
            #endregion

            #region Add Child Controls
            Add(this.box);
            this.viewPort.Add(this.surface);
            Add(this.viewPort);
            #endregion

            #region Set Properties
            this.surface.CanHaveFocus = false;
            this.viewPort.CanHaveFocus = false;
            //this.scrollBar.Y = 1;
            #endregion

            #region Set Default Properties
            Width = defaultWidth;
            Height = defaultHeight;
            HMargin = defaultHMargin;
            VMargin = defaultVMargin;
            Skin = defaultSkin;
            #endregion

            #region Event Handlers
            this.scrollBar.Scroll += new ScrollHandler(OnScroll);
            // Scrollbar doesn't need keyboard, so hand control over to this
            this.scrollBar.KeyDown += new KeyDownHandler(OnKeyDown);
            #endregion
        }
        #endregion

        /// <summary>
        /// Clean up scrollbar in case it hasn't been added.
        /// </summary>
        public override void CleanUp()
        {
            scrollBar.CleanUp();
            base.CleanUp();
        }

        /// <summary>
        /// Load default font.
        /// </summary>
        protected override void LoadContent()
        {
            Font = defaultFont;

            base.LoadContent();
        }

        /// <summary>
        /// Update controls to respect new margins.
        /// </summary>
        protected void RefreshMargins()
        {
            this.viewPort.X = this.hMargin;
            this.viewPort.Y = this.vMargin;
            this.viewPort.Width = Width - (this.hMargin * 2);
            this.viewPort.Height = Height - (this.vMargin * 2);
            this.surface.Width = this.viewPort.Width;
            this.scrollBar.Viewable = this.viewPort.Height;
        }

        /// <summary>
        /// Refresh control to take new entries into account.
        /// </summary>
        public void RefreshEntries()
        {
            int y = 0;
            foreach (Label label in this.entries)
            {
                label.Y = y;
                if (this.font != null)
                {
                    label.Font = this.fontFileName;
                    label.Height = this.font.LineSpacing;
                }
                else
                    label.Height = 15;
                y += label.Height;
                label.Width = this.surface.Width;
            }

            this.surface.Height = y;
            this.scrollBar.MaximumValue = this.surface.Height;

            if (this.resizeToFit)
            {
                int newHeight = this.surface.Height + (this.vMargin * 2);
                if (newHeight != Height)
                    Height = newHeight;
            }
            else if (y > this.viewPort.Height)
                Add(this.scrollBar);
            else
                Remove(this.scrollBar);

            this.surface.Redraw();
        }

        /// <summary>
        /// Adds a string as an entry in the listbox.
        /// </summary>
        /// <param name="text">Text of the new entry.</param>
        public void AddEntry(string text)
        {
            // Create a new label
            Label newEntry = new Label(Game, GUIManager);
            newEntry.Text = text;
            
            // Add new entry
            this.entries.Add(newEntry);
            this.surface.Add(newEntry);

            RefreshEntries();
        }

        /// <summary>
        /// Clears all entries from listbox.
        /// </summary>
        public void Clear()
        {
            foreach (Label entry in this.entries)
                this.surface.Remove(entry);
            this.entries.Clear();
            RefreshEntries();
        }

        /// <summary>
        /// Retrieves the text from the current selection.
        /// </summary>
        /// <returns>Text from current selection.</returns>
        public string GetSelectedText()
        {
            int index = SelectedIndex;

            if (index == -1)
                return null;
            else
                return this.entries[index].Text;
        }

        /// <summary>
        /// Selects a specified label. Changes the label to the selection
        /// colour, and scrolls to show entry if necessary Also invokes
        /// SelectionChanged event.
        /// </summary>
        /// <param name="label">Label to select.</param>
        /// <param name="index">Index of selected item.</param>
        protected void Select(Label label, int index)
        {
            if (index == -1)
            {
                if (this.selectedLabel != null)
                    this.selectedLabel.Color = Color.Black;

                this.selectedIndex = index;
            }
            else
            {
                if (label != this.selectedLabel)
                {
                    // Deselect current item
                    if (this.selectedLabel != null)
                        this.selectedLabel.Color = Color.Black;
                    // Select new item
                    this.selectedLabel = label;
                    this.selectedLabel.Color = Color.Blue;
                    this.selectedIndex = index;
                }

                if (SelectedChanged != null)
                    SelectedChanged.Invoke(this);

                // Automatically scroll to selected item
                if (-(this.selectedLabel.Y + this.selectedLabel.Height) <
                    (this.surface.Y - this.viewPort.Height)
                    ) // Scroll down
                {
                    this.surface.Y = (this.viewPort.Height -
                        (this.selectedLabel.Y + this.selectedLabel.Height));
                    this.scrollBar.Value = -this.surface.Y;
                    viewPort.Redraw();
                }
                else if (-this.selectedLabel.Y > this.surface.Y) // Scroll up
                {
                    this.surface.Y = -this.selectedLabel.Y;
                    this.scrollBar.Value = -this.surface.Y;
                    this.viewPort.Redraw();
                }
            }
        }

        /// <summary>
        /// Finds the index of the supplied label.
        /// </summary>
        /// <param name="label">Search key.</param>
        /// <returns>Index of entry, or -1 if it was not found.</returns>
        protected int FindIndex(Label label)
        {
            for (int i = 0; i < this.entries.Count; i++)
            {
                if (this.entries[i] == label)
                    return i;
            }

            return -1;
        }

        /// <summary>
        /// Used by mouse up and down to check if an entry should be selected.
        /// </summary>
        /// <param name="args">Mouse event arguments.</param>
        protected void CheckMouseSelect(MouseEventArgs args)
        {
            if (args.Button == MouseButtons.Left)
            {
                // Go through each label and check if it should be selected
                int index = -1;
                foreach (Label label in this.entries)
                {
                    index++;
                    if (label.CheckCoordinates(args.Position.X, args.Position.Y))
                    {
                        Select(label, index);
                        break;
                    }
                }
            }
        }

        #region Event Handlers
        /// <summary>
        /// Scrolls listbox view to the position supplied by the scrollbar.
        /// </summary>
        /// <param name="position">New scroll position.</param>
        protected void OnScroll(int position)
        {
            this.surface.Y = -position;
            this.viewPort.Redraw();
        }

        /// <summary>
        /// Up and down keys are used to move selection through the list.
        /// </summary>
        /// <param name="args">Key event arguments.</param>
        protected override void OnKeyDown(KeyEventArgs args)
        {
            base.OnKeyDown(args);

            if (this.selectedIndex != -1)
            {
                if (args.Key == Keys.Up)
                {
                    if (this.selectedIndex > 0)
                        Select(this.entries[this.selectedIndex - 1], this.selectedIndex - 1);
                }
                else if (args.Key == Keys.Down)
                {
                    if (this.selectedIndex < this.entries.Count - 1)
                        Select(this.entries[this.selectedIndex + 1], this.selectedIndex + 1);
                }
            }
        }

        /// <summary>
        /// Select the entry under the mouse if there is one. Used in regular
        /// list boxes.
        /// </summary>
        /// <param name="args">Mouse event arguments.</param>
        protected override void OnMouseDown(MouseEventArgs args)
        {
            base.OnMouseDown(args);

            if (!this.resizeToFit)
                CheckMouseSelect(args);
        }

        /// <summary>
        /// Select the entry under the mouse if there is one. Used for combo
        /// boxes.
        /// </summary>
        /// <param name="args">Mouse event arguments.</param>
        protected override void OnMouseUp(MouseEventArgs args)
        {
            base.OnMouseUp(args);

            if (this.resizeToFit)
                CheckMouseSelect(args);
        }

        /// <summary>
        /// Resize child controls.
        /// </summary>
        /// <param name="sender">Resizing control.</param>
        protected override void OnResize(UIComponent sender)
        {
            base.OnResize(sender);

            this.box.Width = Width;
            this.scrollBar.X = Width - this.scrollBar.Width;

            this.box.Height = Height;
            this.scrollBar.Height = Height;

            RefreshMargins();
            RefreshEntries();
        }
        #endregion
    }
}