#region Using Statement
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace Chimera.Graphics.Effects
{
    /// <summary>
    /// This Class Allow You Using The Fading Effect
    /// </summary>
    public class Fading : Chimera.Helpers.Interface.IUpdateable
    {
        /// <summary>
        /// The Default Constructor
        /// </summary>
        public Fading()
        {
            this.ival = 255;
            this.eval = 0;
            this.value = 1;
        }
        #region Fields
        private byte ival,eval;
        private byte value;
        private bool enable;
        private Graphics.Image img;
        private Graphics.TextWriter txt;
        #endregion
        #region Properties
        /// <summary>
        /// Get Or Set The Fading Value
        /// </summary>
        public byte Value
        {
            get { return this.value;}
            set
            {
                this.value = value ;
                if (this.value == 0) this.enable = false;
            }
        }
        /// <summary>
        /// Get Or Set The Initial Value Effect
        /// </summary>
        public byte StartValue
        {
            get { return this.ival;}
            set { this.ival = value;}
        }
        /// <summary>
        /// Get Or Set The Final Value Effect
        /// </summary>
        public byte EndValue
        {
            get { return this.eval; }
            set {this.eval = value;}
        }
        /// <summary>
        /// Enable Or Disable The Effect
        /// </summary>
        public bool Enable
        {
            get { return this.enable; }
            set { this.enable = value; }
        }
        #endregion
        #region Intialization
        /// <summary>
        /// Initialize The Effect
        /// </summary>
        /// <param name="img">The image on what the effect will be applied</param>
        public void Initialize(Graphics.Image img)
        {
            this.img = img;
            this.txt = null;
            this.Reset();
          
        }
        /// <summary>
        /// Initialize The Effect
        /// </summary>
        /// <param name="txt">The Text on what the effect will be applied</param>
        public void Initialize(Graphics.TextWriter txt)
        {
            this.img = null;
            this.txt = txt;
            this.Reset();
        }
        #endregion
        #region Functions
        /// <summary>
        /// Set The Default Parameter
        /// </summary>
        /// <param name="first_value">The Initial Value</param>
        /// <param name="last_value">The Final Value</param>
        public  void SetEffectParameter(byte first_value,byte last_value)
        {
            this.ival = first_value;
            this.eval = last_value;
            this.SetEffectParameter();
        }
        /// <summary>
        /// Set The Effect Parameter
        /// </summary>
        public void SetEffectParameter()
        {
            if (img != null) this.img.Color =new Color(img.Color.R, img.Color.G, img.Color.B, ival);
            else if (txt != null) this.txt.Color =new Color(txt.Color.R, txt.Color.G, txt.Color.B, ival);
        }
        /// <summary>
        /// Reset The Effect
        /// </summary>
        public void Reset()
        {
          this.enable = true;
          this.SetEffectParameter();
        }

        /// <summary>
        /// Update The Effect
        /// </summary>
        public void Update()
        {  
            if (enable)
            {
                    if (value != 0)
                    {

                        if (img != null)
                        {
                            if (ival < eval)
                                img.Color = new Color(img.Color.R, img.Color.G, img.Color.B, (byte)(img.Color.A + value));
                            else
                                img.Color = new Color(img.Color.R, img.Color.G, img.Color.B, (byte)(img.Color.A - value));
                            if (eval ==img.Color.A) this.enable = false;
                        }
                        else if (txt != null)
                        {
                            if (ival < eval)
                                txt.Color = new Color(txt.Color.R, txt.Color.G, txt.Color.B, (byte)(txt.Color.A + value));
                            else
                                txt.Color = new Color(txt.Color.R, txt.Color.G, txt.Color.B, (byte)(txt.Color.A - value));
                            if (eval == txt.Color.A) this.enable = false;
                        }
                    }
            }
        }
        #endregion

    }
}
