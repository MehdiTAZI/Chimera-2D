#region File Description
//-----------------------------------------------------------------------------
// File:      SkinImporter.cs
// Namespace: Chimera.GUI.WindowSystemSkinPipeline
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
using System.Xml;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
#endregion

namespace Chimera.GUI.WindowSystemSkinPipeline
{
    /// <summary>
    /// Complete imported skin information.
    /// </summary>
    class SkinInfo
    {
        /// <summary>
        /// List of individual control skin definitions.
        /// </summary>
        public List<ComponentSkinInfo> UIComponentSkins = new List<ComponentSkinInfo>();
    }

    /// <summary>
    /// Combines the control type, as well as properties and their values.
    /// </summary>
    class ComponentSkinInfo
    {
        /// <summary>
        /// Type of UIComponent.
        /// </summary>
        public string TypeName;
        /// <summary>
        /// Properties and their values.
        /// </summary>
        public IDictionary<string, string> Properties = new Dictionary<string, string>();
    }

    /// <summary>
    /// Imports the XML skin definition, and converts it to a format that can
    /// be used by the Window System.
    /// </summary>
    [ContentImporter(".skin", DefaultProcessor = "SkinProcessor")]
    class SkinImporter : ContentImporter<SkinInfo>
    {
        public override SkinInfo Import(string filename, ContentImporterContext context)
        {
            SkinInfo skin = new SkinInfo();

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.Load(filename);

            XmlElement skinElement = xmlDoc.SelectSingleNode("Skin") as XmlElement;
            foreach (XmlNode child in skinElement.ChildNodes)
            {
                XmlElement childElement = child as XmlElement;

                if (childElement != null)
                {
                    ComponentSkinInfo componentSkin = new ComponentSkinInfo();

                    componentSkin.TypeName = childElement.LocalName;

                    foreach (XmlAttribute attribute in childElement.Attributes)
                        componentSkin.Properties[attribute.Name] = attribute.Value;

                    skin.UIComponentSkins.Add(componentSkin);
                }
            }

            return skin;
        }
    }

    /// <summary>
    /// Writes the skin info into a format that can be used in game by the
    /// Window System.
    /// </summary>
#if !XBOX    
    [ContentTypeWriter]
    class SkinWriter : ContentTypeWriter<SkinInfo> 

    {
        private void WriteString(ContentWriter output, string data)
        {
            output.Write(data);
        }

        protected override void Write(ContentWriter output, SkinInfo value)
        {
            output.Write(value.UIComponentSkins.Count);

            foreach (ComponentSkinInfo componentSkin in value.UIComponentSkins)
            {
                WriteString(output, componentSkin.TypeName);

                output.Write(componentSkin.Properties.Keys.Count);

                foreach (string key in componentSkin.Properties.Keys)
                {
                    WriteString(output, key);
                    WriteString(output, componentSkin.Properties[key]);
                }
            }
        }

        public override string GetRuntimeType(TargetPlatform targetPlatform)
        {
            return "Chimera.GUI.WindowSystem.Skin, Chimera.GUI.WindowSystem, Version=1.0.0.0, Culture=neutral";
        }

        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return "Chimera.GUI.WindowSystem.SkinReader, Chimera.GUI.WindowSystem, Version=1.0.0.0, Culture=neutral";
        }


        
    }
#endif
}