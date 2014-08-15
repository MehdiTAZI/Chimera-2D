#region Info/Author
//----------------------------------------------------------------------------
// Author:    Tazi Mehdi
// Source:    Chimera 2D GAMES ENGINE
// Info:      Simple XNA Sound public class
//-----------------------------------------------------------------------------
#endregion
#region Using Statement
using Microsoft.Xna.Framework.Audio;
#endregion
namespace Chimera.Audio.SimpleEngine
{
    /// <summary>
    /// This Class Allow You To Create A Simple Sound From XNB Files 
    /// </summary>
    public class Sound
    {        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="theCueName">Cue Name</param>
        /// <param name="theSoundBank">Sound Bank</param>
        public Sound(string theCueName, SoundBank theSoundBank)
        {
            Initialize(theCueName, theSoundBank);
        }
        #region Initialization
        /// <summary>
        /// Initialize The Sound
        /// </summary>
        /// <param name="theCueName">The Cue Name</param>
        /// <param name="theSoundBank">The Sound Bank</param>
        /// 
        public void Initialize(string theCueName, SoundBank theSoundBank)
        {
            CueName = theCueName;
            mCue = theSoundBank.GetCue(theCueName);
        }
        #endregion
        #region CueName
        private string mCueName = string.Empty;
        /// <summary>
        /// Get Or Set The CueName
        /// </summary>
        public string CueName
        {
            get
            {
                return mCueName;
            }
            set
            {
                mCueName = value;
            }
        }
        #endregion
        #region Cue
        private Cue mCue;
        /// <summary>
        /// Get Or Set The Cue Reference
        /// </summary>
        public Cue MyCue
        {
            get
            {
                return mCue;
            }
            set
            {
                mCue = value;
            }
        }
        #endregion

    }
}
