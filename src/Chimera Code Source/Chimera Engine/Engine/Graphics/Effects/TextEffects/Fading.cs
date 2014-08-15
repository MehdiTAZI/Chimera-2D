using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Chimera.Graphics.Effects.TextEffects
{
    /// <summary>
    /// This Class Allow You Using The Fading Effect On Text Compenent
    /// </summary>
    public class Fading : Chimera.Helpers.Interface.IUpdateable
    {
        /// <summary>
        /// The Default Constructor
        /// </summary>
        public Fading()
        {
            fad = new Chimera.Graphics.Effects.Fading();
        }
        #region Fields
        private Graphics.Effects.Fading fad;
        private Graphics.TextWriter txt;
        #endregion
        #region Properieties


        public byte Value
        {
            get { return this.fad.Value;}
            set
            {
                this.fad.Value= value ;
                if (this.fad.Value == 0) this.fad.Enable = false;
            }
        }
        public byte FirstValue
        {
            get { return this.fad.StartValue;}
            set { this.fad.StartValue = value;}
        }
        public byte LastValue
        {
            get { return this.fad.EndValue; }
            set { this.fad.EndValue = value; }
        }

        public bool Enable
        {
            get { return this.fad.Enable; }
            set { this.fad.Enable= value; }
        }
        #endregion
        #region Functions
        /// <summary>
        /// Initialize The Effect
        /// </summary>
        /// <param name="txt">The Text on what the effect will be applied</param>
        public void Initialize(TextWriter txt)
        {
            this.txt = txt;
            fad.Initialize(this.txt);
        }
        /// <summary>
        /// Update The Effect
        /// </summary>
        public void Update()
        {
            fad.Update();
        }
        /// <summary>
        /// Set The Default Parameter
        /// </summary>
        public void SetEffectParameter()
        {
            fad.SetEffectParameter();
        }
        /// <summary>
        /// Set The Default Parameter
        /// </summary>
        /// <param name="first_value">The Initial Value</param>
        /// <param name="last_value">The Final Value</param>
        public void SetEffectParameter(byte first_value, byte last_value)
        {
            fad.SetEffectParameter(first_value,last_value);
        }
        /// <summary>
        /// Reset The Effect
        /// </summary>
        public void Reset()
        {
            fad.Reset();
        }
        #endregion
    }

}
