#region File Description
//-----------------------------------------------------------------------------
// File:      Skin.cs
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
using System.Text;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace Chimera.GUI.WindowSystem
{
    /// <summary>
    /// Represents a skin property.
    /// </summary>
    public class UIComponentSkin
    {
        /// <summary>
        /// The type of control to skin.
        /// </summary>
        public Type ComponentType;
        /// <summary>
        /// The list of default properties that will be set.
        /// </summary>
        public List<PropertyInfo> Properties = new List<PropertyInfo>();
        /// <summary>
        /// The values to set the default properties.
        /// </summary>
        public List<string> Values = new List<string>();
    }

    /// <summary>
    /// An imported XML skin file. Allows the default skin properties of all
    /// Window System controls to be modified using XML markup.
    /// </summary>
    public class Skin
    {
        /// <summary>
        /// List of skin properties to be set.
        /// </summary>
        public List<UIComponentSkin> ComponentSkins = new List<UIComponentSkin>();

        /// <summary>
        /// Applies this skin to the specified control instance.
        /// </summary>
        /// <param name="control">Control to skin.</param>
        internal void Apply(UIComponent control)
        {
            object target = control;
            object value;

            // Go through each control skin, and apply it's settings
            foreach (UIComponentSkin componentSkin in this.ComponentSkins)
            {
                // Check if skin is for this control
                if (componentSkin.ComponentType == control.GetType() ||
                    control.GetType().IsSubclassOf(componentSkin.ComponentType)
                    )
                {
                    for (int i = 0; i < componentSkin.Properties.Count; i++)
                    {
                        Debug.Assert(componentSkin.Properties[i] != null, "An invalid skin property has been used.");

                        value = ParseValue(componentSkin.Properties[i], componentSkin.Values[i]);

                        Debug.Assert(value != null, "Conversion could not be performed on property \"" +
                            componentSkin.ComponentType.Name +
                            "." +
                            componentSkin.Properties[i].Name +
                            "\": " +
                            componentSkin.Values[i] +
                            " to " +
                            componentSkin.Properties[i].PropertyType.Name
                            );

                        // Apply property
                        componentSkin.Properties[i].SetValue(target, value, null);
                    }
                }
            }
        }

        /// <summary>
        /// Sets this skin as the current skin for the specified GUIManager. If
        /// tryDefaults is true, then skin will also be applied to all control
        /// default properties. This only affects the skins of subsequently
        /// created controls, existing controls will keep their current
        /// properties.
        /// </summary>
        /// <param name="guiManager">GUIManager to skin.</param>
        /// <param name="tryDefaults">Should control defaults be set?</param>
        internal void Apply(GUIManager guiManager, bool tryDefaults)
        {
            object target;
            PropertyInfo property;
            object value;

            // Go through each control skin, and apply it's default settings
            foreach (UIComponentSkin componentSkin in this.ComponentSkins)
            {
                for (int i = 0; i < componentSkin.Properties.Count; i++)
                {
                    target = null;
                    property = componentSkin.Properties[i];
                    value = null;

                    if (tryDefaults && !property.Name.Contains("Default"))
                        property = componentSkin.ComponentType.GetProperty("Default" + property.Name);
                    else if (componentSkin.ComponentType == typeof(GUIManager))
                        target = guiManager;
                    else if (componentSkin.ComponentType == typeof(MouseCursor))
                        target = guiManager.MouseCursor;
                    else
                        property = null;

                    if (property != null)
                    {
                        value = ParseValue(componentSkin.Properties[i], componentSkin.Values[i]);

                        Debug.Assert(value != null, "Conversion could not be performed on property \"" +
                            componentSkin.ComponentType.Name +
                            "." +
                            componentSkin.Properties[i].Name +
                            "\": " +
                            componentSkin.Values[i] +
                            " to " +
                            componentSkin.Properties[i].PropertyType.Name
                            );

                        // Apply property
                        property.SetValue(target, value, null);
                    }
                }
            }
        }

        /// <summary>
        /// Parses a property value into the correct type of object.
        /// </summary>
        /// <param name="property">Property used to obtain type required.</param>
        /// <param name="value">String value to parse.</param>
        /// <returns></returns>
        private object ParseValue(PropertyInfo property, string value)
        {
            object result = null;

            // If the property is already a string, then use it straight away
            if (property.PropertyType == typeof(string))
                result = value;
            else if (property.PropertyType == typeof(Rectangle) ||
                property.PropertyType == typeof(Color)
                )
            {
                string[] split;
                char[] separator = { ',' };
                int[] values = new int[4];

                // Split numbers into individual strings
                split = value.Split(separator);

                Debug.Assert(split.Length == 4);

                // Convert to integers
                for (int j = 0; j < 4; j++)
                    values[j] = int.Parse(split[j]);

                // Set value to the correct type
                if (property.PropertyType == typeof(Rectangle))
                    result = new Rectangle(values[0], values[1], values[2], values[3]);
                else
                    result = new Color((byte)values[0], (byte)values[1], (byte)values[2], (byte)values[3]);
            }
            else if (property.PropertyType == typeof(Point))
            {
                string[] split;
                char[] separator = { ',' };
                int x;
                int y;

                // Split numbers into individual strings
                split = value.Split(separator);

                Debug.Assert(split.Length == 2);

                // Convert to integers
                x = int.Parse(split[0]);
                y = int.Parse(split[1]);

                result = new Point(x, y);
            }
            else
            {
                try
                {
                    #if !XBOX
                    // Try to use a standard conversion
                    result = Convert.ChangeType(value, property.PropertyType);
                    #endif
                }
                catch
                {
                    result = null;
                }
            }

            return result;
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class SkinAttribute : Attribute
    {
    }

    /// <summary>
    /// Takes the skin input, and converts it to a format that can be used to
    /// set the default skins at runtime.
    /// </summary>
    class SkinReader : ContentTypeReader<Skin>
    {
        /// <summary>
        /// Takes the skin input, and converts it to a format that can be used to
        /// set the default skins at runtime.
        /// </summary>
        protected override Skin Read(ContentReader input, Skin existingInstance)
        {
            Skin instance = new Skin();

            // Check how many components we have defined in this screen
            int maxComponents = input.ReadInt32();

            for (int i = 0; i < maxComponents; i++)
            {
                UIComponentSkin componentSkin = new UIComponentSkin();

                // Get the object type
                string type = input.ReadString();

                // Retrive the properties of the object
                IDictionary<string, string> properties = RetrieveProperties(input);

                // Use reflection to get the UIComponent type, and add it to skin
                componentSkin.ComponentType = Type.GetType(type);

                PropertyInfo property;
                object[] attributes;

                // Find all properties and values
                foreach (string propertyName in properties.Keys)
                {
                    property = componentSkin.ComponentType.GetProperty(propertyName);
                    Debug.Assert(property != null, "An invalid property has been used: " +
                        componentSkin.ComponentType.ToString() +
                        "." + propertyName);

                    // Check if property is a skin attribute
                    attributes = property.GetCustomAttributes(typeof(SkinAttribute), true);

                    if (attributes.Length > 0)
                    {
                        // Add the property name, and the string representation on it's value
                        componentSkin.Properties.Add(property);
                        componentSkin.Values.Add(properties[propertyName]);
                    }
                }

                instance.ComponentSkins.Add(componentSkin);
            }

            return instance;
        }

        /// <summary>
        /// Reads each property and value, adding them to a list to be used
        /// later to actually set skin properties.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private IDictionary<string, string> RetrieveProperties(ContentReader input)
        {
            IDictionary<string, string> properties = new Dictionary<string, string>();

            int propertiesCount = input.ReadInt32();

            for (int i = 0; i < propertiesCount; i++)
            {
                string key = input.ReadString();
                string value = input.ReadString();

                properties[key] = value;
            }

            return properties;
        }

    }
}