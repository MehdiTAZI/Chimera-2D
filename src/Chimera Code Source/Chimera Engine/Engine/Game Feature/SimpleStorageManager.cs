#region Info/Author
//----------------------------------------------------------------------------
// Author:    Tazi Mehdi
// Source:    Chimera 2D GAMES ENGINE
// Info:      Save Load Engine
//-----------------------------------------------------------------------------
#endregion
#region Using Statement 
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework;
using System.IO;
using System;
using System.Xml;
using System.Xml.Serialization;
#endregion
namespace Chimera.Game_Feature.Storage
{
  
    #region Manager
    /// <summary>
    /// This class Allow You To Save And Load Data
    /// </summary>
    /// <typeparam name="SaveLoadclass">Data class</typeparam>
    public static class SimpleStorageManager<SaveLoadclass>
    {
        /// <summary>
        /// Save The Data
        /// </summary>
        /// <param name="save">class To Save</param>
        /// <param name="directory">Directory</param>
        /// <param name="file">File Name</param>
        public static void Save(SaveLoadclass save, string directory, string file)
        {
            //MehdiModification
            IAsyncResult result = Microsoft.Xna.Framework.GamerServices.Guide.BeginShowStorageDeviceSelector(null, null);
            StorageDevice device = Microsoft.Xna.Framework.GamerServices.Guide.EndShowStorageDeviceSelector(result) ;// StorageDevice.ShowStorageDeviceGuide();
            StorageContainer container = device.OpenContainer(directory);
            string filename = Path.Combine(container.Path, file);
            FileStream stream = File.Open(filename, FileMode.OpenOrCreate);
            XmlSerializer serializer = new XmlSerializer(typeof(SaveLoadclass));
            serializer.Serialize(stream, save);
            stream.Close();
            container.Dispose();
        }
        /// <summary>
        /// Load Data
        /// </summary>
        /// <param name="load">Return The Loaded Data</param>
        /// <param name="directory">Directory</param>
        /// <param name="file">File Name</param>
        public static void Load(ref SaveLoadclass load,string directory, string file)
        {
            //BaseSaveLoad ret = new BaseSaveLoad();

            IAsyncResult result = Microsoft.Xna.Framework.GamerServices.Guide.BeginShowStorageDeviceSelector(null, null);
            StorageDevice device = Microsoft.Xna.Framework.GamerServices.Guide.EndShowStorageDeviceSelector(result);// StorageDevice.ShowStorageDeviceGuide();
            StorageContainer container = device.OpenContainer(directory);
            string filename = Path.Combine(container.Path, file);
            if (!File.Exists(filename))
                return;
            FileStream stream = File.Open(filename, FileMode.OpenOrCreate,
                FileAccess.Read);
            XmlSerializer serializer = new XmlSerializer(typeof(SaveLoadclass));
            load = (SaveLoadclass)serializer.Deserialize(stream);
            stream.Close();
            container.Dispose();
        }
        /// <summary>
        /// Get Path Function
        /// </summary>
        /// <param name="directory">Directory</param>
        /// <param name="file">File Name</param>
        /// <returns></returns>
        public static string GetPath(string directory, string file)
        {
            IAsyncResult result = Microsoft.Xna.Framework.GamerServices.Guide.BeginShowStorageDeviceSelector(null, null);
            StorageDevice device = Microsoft.Xna.Framework.GamerServices.Guide.EndShowStorageDeviceSelector(result);// StorageDevice.ShowStorageDeviceGuide();
            StorageContainer container = device.OpenContainer(directory);
            string filename = Path.Combine(container.Path, file);
            container.Dispose();
            return filename;
        }
    }
    #endregion

}
