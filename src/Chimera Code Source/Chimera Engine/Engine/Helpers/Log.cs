#region Info/Author
//----------------------------------------------------------------------------
// Author:    Tazi Mehdi
// Source:    Chimera 2D GAMES ENGINE
// Info:      Log : UseFull For Debug
//-----------------------------------------------------------------------------
#endregion
#region Using Statement
using System.Collections.Generic;
using Chimera;
using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
#endregion
namespace Chimera.Helpers
{
    /// <summary>
    /// This Class Allow You To Use A Logger ( Important For Debuging )
    /// </summary>
    public static class Logging
    {
        
        private static string filename="Chimera_LogFile.txt";
        /// <summary>
        /// Get Or Set The File Name
        /// </summary>
        public static string FileName
        {
            get {return filename ;}
            set {filename = value ;}
        }

        /// <summary>
        /// Write And Append Into The Log File
        /// </summary>
        /// <param name="err">Error To Write</param>
        public static void Write(string err)
        {
            Write(err, true);
        }
        /// <summary>
        /// Write Into The Log
        /// </summary>
        /// <param name="err">Error Message</param>
        /// <param name="append">If append is set to true the Error Message Will Be Add In The End Of The File</param>
        public static void Write(string err,bool append)
        {
            if (filename == "")
                return;
            #if !XBOX
            StackFrame CallStack = new StackFrame(1, true);
            #endif
            System.IO.StreamWriter file = new System.IO.StreamWriter(filename, append);
            file.WriteLine("___________________________");
#if !XBOX
            file.WriteLine("Time : " + DateTime.Now);
            file.WriteLine("File : " + CallStack.GetFileName());
            file.WriteLine("Line : " + CallStack.GetFileLineNumber());
#endif
            file.WriteLine("Error :  "+err);

            file.Flush();
            file.Close();
        }

    }
}
