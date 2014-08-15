#region Info/Author
//----------------------------------------------------------------------------
// Author:    Tazi Mehdi
// Source:    Chimera 2D GAMES ENGINE
// Info:      An Simple Audio Engine ( Using XNA Sound Engine )
//-----------------------------------------------------------------------------
#endregion
#region Using Statement
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
#endregion

namespace Chimera.Audio.SimpleEngine
{
    /// <summary>
    /// This Class Allow You To Manage The Sound public class That Use The XACT System
    /// </summary>
    public static class AudioManager
    {
        #region Fields
        private static AudioEngine mAudioEngine;
        private static WaveBank mWaveBank;
        private static SoundBank mSoundBank;
        private static Dictionary<string, Sound> mSounds = new Dictionary<string, Sound>();
        #endregion

        #region Properties
        /// <summary>
        /// Set Or Get Dictionary Of Sounds
        /// </summary>
        public static Dictionary<string, Sound> Sounds
        {
            get { return mSounds; }
            set { mSounds = value;}
        }
        /// <summary>
        ///Get  Sound Bank Reference
        /// </summary>
        public static SoundBank MySoundBank
        {
            get { return mSoundBank;}
        }
        #endregion

        #region Functions

        /// <summary>
        /// Initialize The Engine
        /// </summary>
        /// <param name="AudioEngine">Audio Engine</param>
        /// <param name="WaveBank">The Wav Bank</param>
        /// <param name="SoundBank">The Sound Bank</param>
        public static void Initialize(string AudioEngine, string WaveBank, string SoundBank)
        {
            mAudioEngine = new AudioEngine(AudioEngine);
            mWaveBank = new WaveBank(mAudioEngine, WaveBank);
            mSoundBank = new SoundBank(mAudioEngine, SoundBank);
        }
        #region Play

        /// <summary>
        /// Play All The Dictionary Sounds
        /// </summary>
        public static void Play()
        {
            mAudioEngine.Update();
            foreach (KeyValuePair<string, Sound> aSound in mSounds)
                if (!aSound.Value.MyCue.IsPaused) MySoundBank.GetCue(aSound.Value.CueName).Play();
        }
        /// <summary>
        /// Play A Specific Sound
        /// </summary>
        /// <param name="theCueName">The Cue Name To Play</param>
        public static void Play(string theCueName)
        {
            mAudioEngine.Update();
            mSounds[theCueName].MyCue = MySoundBank.GetCue(mSounds[theCueName].CueName);
            if ((!mSounds[theCueName].MyCue.IsPaused)) mSounds[theCueName].MyCue.Play();
        }

        /// <summary>
        /// Force Dictionary Sounds To Be Played
        /// </summary>
        public static void ForcePlay()
        {
            mAudioEngine.Update();
            foreach (KeyValuePair<string, Sound> aSound in mSounds)
                MySoundBank.GetCue(aSound.Value.CueName).Play();
        }
        /// <summary>
        /// Force A Specific Sound To Be Played
        /// </summary>
        /// <param name="theCueName"></param>
        public static void ForcePlay(string theCueName)
        {
            mAudioEngine.Update();
            mSounds[theCueName].MyCue = MySoundBank.GetCue(mSounds[theCueName].CueName);
            mSounds[theCueName].MyCue.Play();
        }
        #endregion

        #endregion

        
    }
}
