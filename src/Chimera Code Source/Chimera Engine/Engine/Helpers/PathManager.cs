#region Info/Author
//----------------------------------------------------------------------------
// Author:    Tazi Mehdi
// Source:    Chimera 2D GAMES ENGINE
// Info:      PathManager
#endregion
//-----------------------------------------------------------------------------
#region Using Statement
using System.Collections.Generic;
using System;
#endregion
namespace Chimera.Helpers
{
    /// <summary>
    ///This Class Allow You To Manage Paths
    /// </summary>
    public static class PathManager
    {
        #region Fields
        private static Dictionary<string, string> dico = new Dictionary<string, string>();
        #endregion
        #region Main Functions
        /// <summary>
        /// Check If The Path Allready Exist In The PathManagers
        /// </summary>
        /// <param name="Path">The Path To Search</param>
        /// <returns>Return Tue If The Path Allready Exist</returns>
        public static bool IsPathExist(string Path)
        {
            foreach (string txt in dico.Values)
            {
                if (txt == Path)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Add Path To The Manager
        /// </summary>
        /// <param name="Key">The Key To Add</param>
        /// <param name="Path">The Path To Matche With The Key</param>
        public static void Add(string Key, string Path)
        {
            bool patherr = false;
            try
            {


                if (!IsPathExist(Path))
                    dico.Add(Key, Path);
                else
                {
                    patherr = true;
                    throw new System.Exception();
                }
            }
            catch
            {
                if (patherr)
                    throw new Exception("Error This Path Allready Exist");
                else
                    throw new Exception("Error This Key Allready Exist");
            }

        }
        /// <summary>
        /// Remove Path By Key From The Manager
        /// </summary>
        /// <param name="Key">The Key To Remove</param>
        public static void Remove(string Key)
        {
            try
            {
                dico.Remove(Key);
            }
            catch
            {
                throw new Exception("Error Key Doesn't Exist");
            }

        }
        /// <summary>
        /// Edit A Path From The Manager
        /// </summary>
        /// <param name="Key">The Key To Edit</param>
        /// <param name="Path">The New Path To Matche With The Key</param>
        public static void Edit(string Key, string Path)
        {
            bool PathEER = false;
            try
            {
               
               

                if (!IsPathExist(Path))
                    dico[Key] = Path;
                else
                {
                    PathEER = true;
                    throw new System.Exception();
                }
            }
            catch
            {
                if (PathEER)
                    throw new Exception("Error This Path Allready Exist");
                else
                    throw new Exception("Error Key Doesn't Exist");
            }
        }
        /// <summary>
        /// Remove All Key And Path From The Manager
        /// </summary>
        public static void Clear()
        {
            dico.Clear();
        }
        /// <summary>
        /// Elements Count
        /// </summary>
        /// <returns>Return The Number Of Elements</returns>
        public static int Count()
        {
            return dico.Count;
        }
        /// <summary>
        /// Elements Length : Count The Number Of Elements
        /// </summary>
        /// <returns>Return The Number Of Elements</returns>
        public static int GetLength()
        {
            return dico.Count;
        }
        /// <summary>
        ///Get The Path Matches With The Key 
        /// </summary>
        /// <param name="Key">The Key</param>
        /// <returns>Return The Path Matches With The Key</returns>
        public static string GetPath(string Key)
        {
            string path;
            try
            {
                path = dico[Key];
            }
            catch
            {
                throw new Exception("Error Key Doesn't Exist");
            }
            return path;
        }

        /// <summary>
        /// Change The Exention Of File In Path
        /// </summary>
        /// <param name="path">Path To Be Changed</param>
        /// <param name="extension">The New Extension</param>
        /// <returns>Return The New Changed Path</returns>
        public static string ChangeExtension(string path,string extension)
        {
           return  System.IO.Path.ChangeExtension(path, extension);
        }
        /// <summary>
        /// Get A FileName With Extension From Path
        /// </summary>
        /// <param name="path">Path</param>
        /// <returns>Return FileName With Extension From The Path In Parameter</returns>
        public static string GetFileName(string path)
        {
            return System.IO.Path.GetFileName(path);
        }
        /// <summary>
        /// Get A FileName Without Extension From Path
        /// </summary>
        /// <param name="Path">Path</param>
        /// <returns>Return FileName Without Extension From The Path In Parameter</returns>
        public static string GetFileNameWithoutExension(string Path)
        {
           return System.IO.Path.GetFileNameWithoutExtension(Path);
        }
        /*Static donc impossible de creer des indexeurs : creer par la suite une public class singleton
         * public static string this[string Key]
        {
            get {return GetPath(Key) ;}
            set {
                if (IsPathExist(value))
                    Edit(Key, value);
                else
                    Add(Key, value);
                }
        }*/
        #endregion
    }

}
