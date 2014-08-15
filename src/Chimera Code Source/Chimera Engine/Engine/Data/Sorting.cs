#region Info/Author
//----------------------------------------------------------------------------
// Author:    Tazi Mehdi
// Source:    Chimera 2D GAMES ENGINE
// Info:      ________________________
//-----------------------------------------------------------------------------
#endregion
using System;

namespace Chimera.Data
{   
    /// <summary>
    /// Data Sorting public class
    /// </summary>
    /// <typeparam name="T">Data Type</typeparam>
    public static class Sorting<T>
    {
        /// <summary>
        /// Sort Data
        /// </summary>
        /// <param name="Data">Data TO Sort</param>
        /// <returns>Return Sorted Array</returns>

        public static T[] Sort(T[] Data)
        {
            Array.Sort(Data);
            return Data;
                     
        } 
    }
}
