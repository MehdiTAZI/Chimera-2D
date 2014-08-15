#region Info/Author
//----------------------------------------------------------------------------
// Author:    Tazi Mehdi
// Source:    Chimera 2D GAMES ENGINE
// Info:      Audio Engine ( Using Fmod Library )
//-----------------------------------------------------------------------------
#endregion
#if !XBOX
#region Using Statement
using System;
using Chimera.Audio.FMOD;
#endregion

namespace Chimera.Audio
{
    /// <summary>
    /// Music Type
    /// </summary>

    public enum SoundType
    {
        /// <summary>
        /// For Short Music
        /// </summary>
        Sound,
        /// <summary>
        /// For Long Music
        /// </summary>
        Stream
    }
    /// <summary>
    /// This Class Allow You To Manage Audio
    /// </summary>
    public class AudioFile
    {
        #region Fields

        private FMOD.System system = null;

        private Sound music = null;

        private string currentPath;
        private Channel Channel = null;

        /// <summary>
        /// This delegate is using for handling end music event
        /// </summary>
        /// <param name="sender">Sender Object</param>
        /// <param name="e">Args</param>
        public delegate void EndMusicEventHandler(object sender, EventArgs e);
        /// <summary>
        /// This delegate is using for handling the error event
        /// </summary>
        /// <param name="result"></param>
        public delegate void ErrorEventHandler(RESULT result);

        private CHANNEL_CALLBACK channelCallback;

        /// <summary>
        ///This event is called when the music stop
        /// </summary>
        public event EndMusicEventHandler EndMusic;
        /// <summary>
        /// This Event is called When Error Occured
        /// </summary>
        public event ErrorEventHandler EngineError;

        #endregion
        #region Properties

        /// <summary>
        /// Return a FMOD System reference
        /// </summary>
        public FMOD.System System
        {
            get { return system; }
        }
        /// <summary>
        /// Get Or Set The Current Path
        /// </summary>
        public string CurrentPath
        {
            get { return currentPath; }
            set { currentPath = value; }
        }
        /// <summary>
        /// Get The Current Reference To Fmod Sound
        /// </summary>
        public Sound Music
        {
            get { return music; }
        }
        #endregion
        #region Init-Update-Release
        /// <summary>
        /// Initialize The Audio Engine
        /// </summary>
        /// <param name="NbChannel">Set Number Of Maximum Channels To Use</param>
        public void Initialize()
        {
            RESULT result;

            result = Factory.System_Create(ref system);
            if (EngineError != null) EngineError(result);

            uint version = 0;

            result = system.getVersion(ref version);
            if (EngineError != null) EngineError(result);

            if (version < VERSION.number)
            {
                throw new ApplicationException("Error! You are using an old version of FMOD " + version.ToString("X") + ". This program requires " + VERSION.number.ToString("X") + ".");
            }

            result = system.init(1, INITFLAG.NORMAL, (IntPtr)null);
            if (EngineError != null) EngineError(result);
            channelCallback = new CHANNEL_CALLBACK(OnEndMusic);

        }
        /// <summary>
        /// Update The System
        /// </summary>
        public void Update()
        {
            RESULT result = system.Update();
            if (EngineError != null) EngineError(result);
        }
        /// <summary>
        /// Exit And Close The Audio Engine
        /// </summary>
        public void Dispose()
        {
            RESULT result = RESULT.OK;
            if (music != null)
            {
                result = music.release();
                if (EngineError != null) EngineError(result);
            }
            if (system != null)
                result = system.release();
            if (EngineError != null) EngineError(result);
        }

        #endregion
        #region MainFunctions

        #region Play Functions
        /// <summary>
        /// Play the Current Sound
        /// </summary>
        /// <param name="Type">Type Of Sound</param>
        public void Play(SoundType Type)
        {
            Play(currentPath, Type);
        }
        /// <summary>
        /// Play The Sound
        /// </summary>
        /// <param name="path">Sound Path</param>
        /// <param name="Type">Type Of Sound</param>
        public void Play(string path, SoundType Type)
        {
            Play(path, false, Type);
        }
        /// <summary>
        /// Play The Sound
        /// </summary>
        /// <param name="path">Sound Path</param>
        /// <param name="paused">The Sound Stat</param>
        /// <param name="Type">Type Of Sound</param>
        public void Play(string path, bool paused, SoundType Type)
        {
            bool isPlaying = false;
            RESULT result = RESULT.OK;

            if (Channel != null)
            {
                //si la musique existe
                result = Channel.isPlaying(ref isPlaying);
            }
            else
            {
                isPlaying = false;
            }

            if (EngineError != null) EngineError(result);

            if ((currentPath == path) && isPlaying)//si la musique du chemin courant est entrain detre joué
            {
                Stop();
                Play(Type);
            }
            else if (currentPath == path)//sinon la musique du chemin courant nest pas entrain detre joué
            {
                result = system.playSound(CHANNELINDEX.FREE, music, false, ref Channel);
                if (EngineError != null) EngineError(result);
                if (Channel != null)
                    result = Channel.setCallback(FMOD.CHANNEL_CALLBACKTYPE.END, channelCallback, 0);
                if (EngineError != null) EngineError(result);
            }
            else
            {

                //si cest une nouvelle musique
                if (Channel != null)
                {
                    Channel.stop();
                    Channel = null;
                }
                if (music != null)
                {
                    result = music.release();
                    music = null;
                    if (EngineError != null) EngineError(result);
                }

                if (Type == SoundType.Sound)
                    result = system.createSound(path, MODE.SOFTWARE | MODE.CREATECOMPRESSEDSAMPLE | MODE.LOOP_OFF, ref music);
                else
                    result = system.createStream(path, MODE.SOFTWARE | MODE.CREATECOMPRESSEDSAMPLE | MODE.LOOP_OFF, ref music);

                if (EngineError != null) EngineError(result);

                result = system.playSound(CHANNELINDEX.FREE, music, paused, ref Channel);
                if (EngineError != null) EngineError(result);
                if (Channel != null)
                    result = Channel.setCallback(FMOD.CHANNEL_CALLBACKTYPE.END, channelCallback, 0);
                if (EngineError != null) EngineError(result);
                currentPath = path;
                Update();

            }
        }
        #endregion
        #region PlaySound
        /// <summary>
        /// Play the current Sound
        /// </summary>
        public void PlaySound()
        {
            Play(SoundType.Sound);
        }
        /// <summary>
        /// Play The Sound
        /// </summary>
        /// <param name="path">Sound Path</param>
        public void PlaySound(string path)
        {
            Play(path, SoundType.Sound);
        }
        /// <summary>
        /// Play The Sound
        /// </summary>
        /// <param name="path">Sound Path</param>
        /// <param name="paused">The Sound State</param>
        public void PlaySound(string path, bool paused)
        {
            Play(path, paused, SoundType.Sound);
        }
        #endregion
        #region PlayStream
        /// <summary>
        /// Play the Current Stream
        /// </summary>
        public void PlayStream()
        {
            Play(SoundType.Stream);
        }
        /// <summary>
        /// Play The Stream
        /// </summary>
        /// <param name="path">Stream Path</param>
        public void PlayStream(string path)
        {
            Play(path, SoundType.Stream);
        }
        /// <summary>
        /// Play The Stream
        /// </summary>
        /// <param name="path">Stream Path</param>
        /// <param name="paused">The Stream State</param>
        public void PlayStream(string path, bool paused)
        {
            Play(path, paused, SoundType.Stream);
        }
        #endregion

        #region Stop Functions
        /// <summary>
        /// Stop The Current Sound
        /// </summary>
        public void Stop()
        {
            if (Channel != null)
            {
                RESULT result = Channel.stop();
                Channel = null;
                if (EngineError != null) EngineError(result);
            }
        }
        #endregion
        #region Pause Functions
        /// <summary>
        /// Return The Pause State
        /// </summary>
        /// <returns>Pause State</returns>
        public bool GetPaused()
        {
            bool pause = false;
            if (Channel != null)
            {
                RESULT result = Channel.getPaused(ref pause);
                if (EngineError != null) EngineError(result);
            }
            return pause;
        }
        /// <summary>
        /// Set Pause State
        /// </summary>
        /// <param name="stat"></param>
        public void SetPaused(bool stat)
        {
            if (Channel != null)
            {
                RESULT result = Channel.setPaused(stat);
                if (EngineError != null) EngineError(result);
            }
        }

        /// <summary>
        /// Set Pause On If The Sound Is Playing Or Set it Off
        /// </summary>
        public void SetPaused()
        {
            bool paused = false;
            if (Channel != null)
            {
                RESULT result = Channel.getPaused(ref paused);
                if (EngineError != null) EngineError(result);
                result = Channel.setPaused(!paused);
                if (EngineError != null) EngineError(result);
            }
        }

        #endregion
        #region Volume Functions
        /// <summary>
        ///Switch Of Mute Mode
        /// </summary>
        public void SetMute()
        {
            bool mute = false;
            if (Channel != null)
            {
                RESULT result = Channel.getMute(ref mute);
                if (EngineError != null) EngineError(result);
                result = Channel.setMute(!mute);
                if (EngineError != null) EngineError(result);
            }
        }
        /// <summary>
        /// Set On/Off The Mute Mode
        /// </summary>
        /// <param name="value"></param>
        public void SetMute(bool value)
        {
            if (Channel != null)
            {
                RESULT result = Channel.setMute(value);
                if (EngineError != null) EngineError(result);
            }
        }

        /// <summary>
        /// Return a value that indicate if the Sound Is Mute
        /// </summary>
        /// <returns></returns>
        public bool GetMute()
        {
            bool mute = false;
            if (Channel != null)
            {
                RESULT result = Channel.getMute(ref mute);
                if (EngineError != null) EngineError(result);
            }
            return mute;
        }

        /// <summary>
        /// Get The Current Volume
        /// </summary>
        /// <returns></returns>
        public float GetVolume()
        {
            float vol = 1.0f;

            if (Channel != null)
            {
                RESULT result = Channel.getVolume(ref vol);
                if (EngineError != null) EngineError(result);
            }
            return vol;
        }
        /// <summary>
        /// Set The Volume
        /// </summary>
        /// <param name="Value">Volume Value</param>
        public void SetVolume(float Value)
        {
            Value = Math.Abs(Value);
            if (Channel != null)
            {
                RESULT result = Channel.setVolume(Value);
                if (EngineError != null) EngineError(result);
            }
        }
        #endregion
        #region Frequence Functions
        /// <summary>
        /// Get The Current Frequency
        /// </summary>
        /// <returns>The Default value is 91,66</returns>
        public float GetFrequency()
        {
            float frq = 0;
            if (Channel != null)
            {
                RESULT result = Channel.getFrequency(ref frq);
                if (EngineError != null) EngineError(result);
            }
            return ((frq * 100.0f) / 48000);
        }

        /// <summary>
        /// Set Frequency
        /// </summary>
        /// <param name="freq">Amount Of Frequency to set</param>
        public void SetFrequency(float freq)
        {
            if (Channel != null)
            {
                RESULT result = Channel.setFrequency(freq * 48000 / 100.0f);
                if (EngineError != null) EngineError(result);
            }
        }

        #endregion
        #region Position Functions

        /// <summary>
        /// Set Music Position 0-100
        /// </summary>
        /// <param name="pos">Percent</param>
        public void SetPosition(uint pos)
        {
            uint ln = 0;
            if (music != null) music.getLength(ref ln, TIMEUNIT.MS);
            pos = (ln * pos / 100);
            if (Channel != null)
            {
                RESULT result = Channel.setPosition(pos, TIMEUNIT.MS);
                if (EngineError != null) EngineError(result);
            }
        }
        /// <summary>
        /// Get The Current Music Position 0-100
        /// </summary>
        /// <returns></returns>
        public uint GetPosition()
        {
            uint ln = 0;
            uint ms = 0;
            if (music != null)
            {
                RESULT result = music.getLength(ref ln, TIMEUNIT.MS);
                if (EngineError != null) EngineError(result);
                ms = GetMsPosition();
                ms = (100 * ms / ln);
            }
            else
                ms = 0;
            return ms;
        }
        /// <summary>
        /// Set The Current Music Position ( Ms )
        /// </summary>
        /// <param name="ms"></param>
        public void SetMsPosition(uint ms)
        {
            if (Channel != null)
            {
                RESULT result = Channel.setPosition(ms, TIMEUNIT.MS);
                if (EngineError != null) EngineError(result);
            }
        }
        /// <summary>
        /// Return The Current Music Position (Ms)
        /// </summary>
        /// <returns></returns>
        public uint GetMsPosition()
        {
            uint ms = 0;
            if (Channel != null)
            {
                RESULT result = Channel.getPosition(ref ms, TIMEUNIT.MS);
                if (EngineError != null) EngineError(result);
            }
            return ms;
        }

        /// <summary>
        /// Return Music Length
        /// </summary>
        /// <returns></returns>
        public uint GetLength()
        {
            uint ln = 0;
            if (music != null) music.getLength(ref ln, TIMEUNIT.MS);
            return ln;
        }

        #endregion
        #endregion
        #region Event Functions

        private RESULT OnEndMusic(IntPtr channelraw, FMOD.CHANNEL_CALLBACKTYPE type, int command, uint commanddata1, uint commanddata2)
        {
            Channel = null;// en premier pour ne pas avoir erreur lors d'un MessageBox :) ; cas particulier
            if (EndMusic != null)
                EndMusic(currentPath, new EventArgs());

            return RESULT.OK;
        }
        #endregion
        #region Error Functions
        /// <summary>
        /// Throw A Fmod Error
        /// </summary>
        /// <param name="result">FMOD RESULT ENUMERATION</param>
        public void ShowErrorException(RESULT result)
        {
            if (result != RESULT.OK)
            {
                throw new ApplicationException("Sound Manager Error! " + result + " - " + Error.String(result));
            }
        }

        /// <summary>
        /// Return A Fmod Error
        /// </summary>
        /// <param name="result">FMOD RESULT ENUMERATION</param>
        /// <returns>The Error String</returns>
        public string ReturnErrorMessage(RESULT result)
        {
            string chaine = "";
            if (result != RESULT.OK)
            {
                chaine = "Sound Manager Error! " + result + " - " + Error.String(result);
            }
            return chaine;
        }

        #endregion
        #region Other Functions
        /// <summary>
        /// Get The Fmod Playing State
        /// </summary>
        /// <returns>Return true if the Sound Is Playing</returns>
        public bool IsPlaying()
        {
            bool ply = false;
            if (Channel != null)
            {
                RESULT result = Channel.isPlaying(ref ply);
                if (EngineError != null) EngineError(result);
            }
            return ply;
        }

        #endregion
    }
}
#endif
