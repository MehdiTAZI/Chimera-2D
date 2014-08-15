#region Info/Author
//----------------------------------------------------------------------------
// Author:    Tazi Mehdi
// Source:    Chimera 2D GAMES ENGINE
// Info:      Scene Manager Engine
//-----------------------------------------------------------------------------
#endregion
#region Using Statement
using System.Collections.Generic;
using System;
#endregion
namespace Chimera.Game_Feature.SceneManager
{
    #region DefaultSceneManager
    /// <summary>
    ///Default Scene Manager Function
    /// </summary>
    public delegate void SceneFunction();
     /// <summary>
     /// SceneManager Item
     /// </summary>
    public class Scene
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public Scene()
        {
            Draw = null;
            Update = null;
        }
        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="draw">Draw Function</param>
        /// <param name="update">Update Function</param>
        public Scene(SceneFunction draw, SceneFunction update)
        {
            Draw = draw;
            Update = update;
        }
        /// <summary>
        /// Scene draw item
        /// </summary>
        public SceneFunction Draw;
        /// <summary>
        /// Scene update item
        /// </summary>
        public SceneFunction Update;

    }   
    /// <summary>
    /// 
    /// This Class Offer You The Possibility To Manage Your Scene.
    /// </summary>
    public static class SceneManager
    {
        #region Fields 
        private static Dictionary<string, Scene> dico = new Dictionary<string,Scene>();
        private static string current_scene;
        #endregion
        #region Properties
        /// <summary>
        /// Get Of Set The Current Scene Name
        /// </summary>
        public static string Current
        {
            get { return current_scene; }
            set { current_scene = value; }
        }
        #endregion
        #region Main Functions
        /// <summary>
        /// Add Scene To The Manager
        /// </summary>
        /// <param name="SceneName">Scene To Add</param>
        /// <param name="SceneFunctions">The Scenes Functions Matches With The Scene Name</param>
        public static void Add(string SceneName,Scene SceneFunctions)
        {
          try
          {
              dico.Add(SceneName, SceneFunctions);
          }
          catch
          {
              throw new Exception("Error This SceneName Allready Exist");
          }
       
        }
        /// <summary>
        /// Remove Scene By Name From The Manager
        /// </summary>
        /// <param name="SceneName">Scene Name To Remove</param>
        public static void Remove(string SceneName)
        {
            try
            {
                dico.Remove(SceneName);
            }
            catch
            {
                throw new Exception("Error SceneName Doesn't Exist");
            }

        }
        /// <summary>
        /// Edit The Scene Function From The Manager
        /// </summary>
        /// <param name="SceneName">Scene Name To Edit</param>
        /// <param name="SceneFunctions">The New Scenes Functions</param>
        public static void Edit(string SceneName,Scene SceneFunctions)
        {
            try
            {
                dico[SceneName] = SceneFunctions;
            }
            catch
            {
                throw new Exception("Error SceneName Doesn't Exist");
            }
        }
        /// <summary>
        /// Remove ALL Scenes From The Manager
        /// </summary>
        public static void Clear()
        {
            dico.Clear();
            current_scene = "";
        }

        /// <summary>
        /// Count The Nombre Of Scene
        /// </summary>
        /// <returns>Return The Number Of Scene</returns>
        public static int  Count()
        {
            return dico.Count;
        }

        /// <summary>
        /// Draw The Scene Matches With The Scene Name
        /// </summary>
        /// <param name="SceneName">The SceneName To Be Drawn</param>
        public static void Draw(string SceneName)
        {
            try
            {
                dico[SceneName].Draw() ;
            }
            catch
            {
                throw new Exception("Error SceneName Doesn't Exist");
            }
        }
        /// <summary>
        /// Update The Scene Matches With The Scene Name
        /// </summary>
        /// <param name="SceneName">The SceneName To Be Updated</param>
        public static void Update(string SceneName)
        {
            try
            {
                dico[SceneName].Update();
            }
            catch
            {
                throw new Exception("Error SceneName Doesn't Exist");
            }
        }
        /// <summary>
        /// Draw The Current Scene
        /// </summary>
        public static void Draw()
        {
            try
            {
                dico[current_scene].Draw();
            }
            catch
            {
                throw new Exception("Error SceneName Doesn't Exist");
            }
        }
        /// <summary>
        /// Update The Current Scene
        /// </summary>
        public static void Update()
        {
            try
            {
                dico[current_scene].Update();
            }
            catch
            {
                throw new Exception("Error SceneName Doesn't Exist");
            }
        }
        #endregion
    }
    #endregion
}
