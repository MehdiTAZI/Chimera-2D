#region Info/Author
//----------------------------------------------------------------------------
// Author:    Tazi Mehdi
// Source:    Chimera 2D GAMES ENGINE
// Info:      TextureManager For Free Distribution Without Content Pipeline
#endregion
//-----------------------------------------------------------------------------
#region Using Statement
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion
namespace Chimera.Helpers
{
#if !XBOX
    /// <summary>
    /// This Class Allow You To Manage Your Textures
    /// </summary>
    public static class TextureManager
    {
        #region Fields
        private struct DicoTexture
        {
            public Texture2D texture;
            public string FullPath;
        }
        private static Dictionary<string, DicoTexture> dico = new Dictionary<string, DicoTexture>();
        #endregion
        #region Main Functions
        private static bool IsTextureExist(DicoTexture texture)
        {
            foreach (DicoTexture txt in dico.Values)
            {
                if (txt.FullPath == texture.FullPath)
                    return true;
            }
            return false;
        }
        /// <summary>
        /// Add A Texture To The TextureManager
        /// </summary>
        /// <param name="AssetName">The AssetName To Add</param>
        /// <param name="FullPath">The Path Of Texture To Matche With The AssetName</param>
        /// <param name="graphicsDevice">XNA GraphicsDevice Reference</param>
        public static void Add(string AssetName,string FullPath,GraphicsDevice graphicsDevice)
        {
            bool textureerreur = false;
            bool errload = true;
            try
            {  
                DicoTexture texture = new  DicoTexture();
                    texture.texture = Texture2D.FromFile(graphicsDevice,FullPath);
                    errload = false;
                    texture.FullPath = FullPath;

                    if (!IsTextureExist(texture))
                        dico.Add(AssetName, texture);
                    else
                    {
                        textureerreur = true;
                        throw new System.Exception();
                    }
            }
            catch
            {
                if (textureerreur)
                    throw new Exception("Error This Texture Allready Exist");
                else
                {
                    if (errload)
                        throw new Exception("Error This Asset filename Doesn't Exist");
                    else
                        throw new Exception("Error This Asset Allready Exist");
                }
            }

        }
        #if !XBOX
        /// <summary>
        /// Add A Texture To The Texture Manager
        /// </summary>
        /// <param name="AssetName">The AssetName To Add</param>
        /// <param name="FullPath">The Path Of Texture To Matche With The AssetName</param>
        /// <param name="TCP">Texture Parameters</param>
        /// <param name="graphicsDevice">XNA GraphicsDevice Referenec</param>
        public static void Add(string AssetName, string FullPath,TextureCreationParameters TCP ,GraphicsDevice graphicsDevice)
        {
            bool textureerreur = false;
            bool errload = true;
            try
            {
                DicoTexture texture = new DicoTexture();
                texture.texture = Texture2D.FromFile(graphicsDevice, FullPath,TCP);
                errload = false;
                texture.FullPath = FullPath;

                if (!IsTextureExist(texture))
                    dico.Add(AssetName, texture);
                else
                {
                    textureerreur = true;
                    throw new System.Exception();
                }
            }
            catch
            {
                if (textureerreur)
                    throw new Exception("Error This Texture Allready Exist");
                else
                { 
                    if(errload)
                        throw new Exception("Error This Asset filename Doesn't Exist");
                    else
                        throw new Exception("Error This Asset Allready Exist");
                }
            }
        }
        #endif
        /// <summary>
       /// Remove A Texture From The Texture Manager
       /// </summary>
       /// <param name="AssetName">The AssetName To Remove</param>
        public static void Remove(string AssetName)
        {
            try
            {
                dico.Remove(AssetName);
            }
            catch
            {
                throw new Exception("Error AssetName Doesn't Exist");
            }

        }
        /// <summary>
       /// Edit A Texture From The Texture Manager
       /// </summary>
       /// <param name="AssetName">AssetName To Edits</param>
       /// <param name="FullPath">The New TexturePath</param>
       /// <param name="graphicsDevice">XNA GraphicsDevice Reference</param>
        public static void Edit(string AssetName, string FullPath,GraphicsDevice graphicsDevice)
        {
            bool textureerreur = false;
            bool errload = true;
            try
            {

                DicoTexture texture = new DicoTexture();
                texture.texture = Texture2D.FromFile(graphicsDevice, FullPath);
                errload = false;
                texture.FullPath = FullPath;

                if (!IsTextureExist(texture))
                   dico[AssetName]= texture;
               else
               {
                   textureerreur = true;
                   throw new System.Exception();
               }
           }
           catch
           {
               if (textureerreur)
                   throw new Exception("Error This Texture Allready Exist");
               else
               {
                   if (errload)
                       throw new Exception("Error The filename Doesn't Exist");
                   else
                       throw new Exception("Error AssetName Doesn't Exist");
               }
           }
        }
        #if !XBOX
        /// <summary>
        /// Edit A Texture From The Texture Manager
        /// </summary>
        /// <param name="AssetName">AssetName To Edits</param>
        /// <param name="FullPath">The New TexturePath</param>
        /// <param name="TCP">Texture Parameters</param>
        /// <param name="graphicsDevice">XNA GraphicsDevice Reference</param>
        public static void Edit(string AssetName, string FullPath,TextureCreationParameters TCP, GraphicsDevice graphicsDevice)
        {
            bool textureerreur = false;
            bool errload = true;
            try
            {
                DicoTexture texture = new DicoTexture();
                texture.texture = Texture2D.FromFile(graphicsDevice, FullPath,TCP);
                errload = false;
                texture.FullPath = FullPath;

                if (!IsTextureExist(texture))
                    dico[AssetName] = texture;
                else
                {
                    textureerreur = true;
                    throw new System.Exception();
                }
            }
            catch
            {
                if (textureerreur)
                    throw new Exception("Error This Texture Allready Exist");
                else
                {
                    if(errload)
                        throw new Exception("Error The filename Doesn't Exist");
                    else    
                        throw new Exception("Error AssetName Doesn't Exist");
                }
            }
        }
        #endif
        /// <summary>
        /// Remove ALL The Textures Of The TextureManager
        /// </summary>
        public static void Clear()
        {
            dico.Clear();
        }

        /// <summary>
        /// Get Number Of Textures
        /// </summary>
        /// <returns>Return Number Of Textures</returns>
        public static int Count()
        {
            return dico.Count;
        }
        /// <summary>
        /// Get Length Of The Textures In The TextureManager
        /// </summary>
        /// <returns>Return Number Of Textures</returns>
        public static int GetLength()
        {
            return dico.Count;
        }

        /// <summary>
        /// Get A Texture Data From AN AssetName
        /// </summary>
        /// <param name="AssetName">Return The Texture2D Matches With The AssetName</param>
        public static Texture2D GetTexture(string AssetName)
        {
            Texture2D text=null;
            try
            {
                text = dico[AssetName].texture ;
            }
            catch
            {
                throw new Exception("Error AssetName Doesn't Exist");
            }
            return text;
        }
        /// <summary>
        /// Get Full Path Of Texture Referenced By His AssetName
        /// </summary>
        /// <param name="AssetName">The AssetName</param>
        /// <returns>Return Full Path Of Texture Referenced By His AssetName</returns>
        public static string GetFullPath(string AssetName)
        {
            string text = null;
            try
            {
                text = dico[AssetName].FullPath;
            }
            catch
            {
                throw new Exception("Error AssetName Doesn't Exist");
            }
            return text;
        }

        #endregion
    }
#endif
}
