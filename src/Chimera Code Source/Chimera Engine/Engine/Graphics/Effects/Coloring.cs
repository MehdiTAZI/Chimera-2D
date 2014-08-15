#region Using Statement
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace Chimera.Graphics.Effects
{
    /// <summary>
    /// This Class Allow You Do  An Color Effect On Compenent
    /// </summary>
    public class Coloring : Chimera.Helpers.Interface.IUpdateable
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Coloring()
        {
            this.ival = Color.Azure;
            this.eval = Color.LightBlue;
        }
        #region Fields
        private Color ival, eval;
        private Vector4 value;
        private bool enable;
        private Graphics.Image img;
        private Graphics.TextWriter txt;
        #endregion
        #region Properties
        /// <summary>
        ///The Value With what will increase the color  
        /// X = Red , Y = Green , Z = Blue , W = Alpha
        /// </summary>
        public Vector4 Value
        {
            get { return this.value; }
            set {
            
                this.value = value;
                if (this.value == new Vector4(0, 0, 0, 0)) this.enable = false;
            }
        }
        /// <summary>
        /// The Start Color
        /// </summary>
        public Color StartValue
        {
            get { return this.ival; }
            set { this.ival = value; }
        }
        /// <summary>
        /// The Final Color
        /// </summary>
        public Color EndValue
        {
            get { return this.eval; }
            set { this.eval = value; }
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
        #region Main Functions
        #region HelperFunctions
        private void eimg()
        {
            if (this.value.W != 0)
            {
                if (ival.A < eval.A)
                    img.Color = new Color(img.Color.R, img.Color.G, img.Color.B, (byte)(img.Color.A + value.W));
                else
                    img.Color = new Color(img.Color.R, img.Color.G, img.Color.B, (byte)(img.Color.A - value.W));
            }

            if (this.value.X != 0)
            {
                if (ival.R < eval.R)
                    img.Color = new Color((byte)(img.Color.R + value.X), img.Color.G, img.Color.B, img.Color.A);
                else
                    img.Color = new Color((byte)(img.Color.R - value.X), img.Color.G, img.Color.B, img.Color.A);
            }

            if (this.value.Y != 0)
            {
                if (ival.G < eval.G)
                    img.Color = new Color(img.Color.R, (byte)(img.Color.G + value.Y), img.Color.B, img.Color.A);
                else
                    img.Color = new Color(img.Color.R, (byte)(img.Color.G - value.Y), img.Color.B, img.Color.A);
            }
            if (this.value.Z != 0)
            {
                if (ival.B < eval.B)
                    img.Color = new Color(img.Color.R, img.Color.G, (byte)(img.Color.B + value.Z), img.Color.A);
                else
                    img.Color = new Color(img.Color.R, img.Color.G, (byte)(img.Color.B - value.Z), img.Color.A);
            }

            if (eval == img.Color) this.enable = false;
        }
        private void etxt()
        {
            if (this.value.W != 0)
            {
                if (ival.A < eval.A)
                    txt.Color = new Color(txt.Color.R, txt.Color.G, txt.Color.B, (byte)(txt.Color.A + value.W));
                else
                    txt.Color = new Color(txt.Color.R, txt.Color.G, txt.Color.B, (byte)(txt.Color.A - value.W));
            }

            if (this.value.X != 0)
            {
                if (ival.R < eval.R)
                    txt.Color = new Color((byte)(txt.Color.R + value.X), txt.Color.G, txt.Color.B, txt.Color.A);
                else
                    txt.Color = new Color((byte)(txt.Color.R - value.X), txt.Color.G, txt.Color.B, txt.Color.A);
            }

            if (this.value.Y != 0)
            {
                if (ival.G < eval.G)
                    txt.Color = new Color(txt.Color.R, (byte)(txt.Color.G + value.Y), txt.Color.B, txt.Color.A);
                else
                    txt.Color = new Color(txt.Color.R, (byte)(txt.Color.G - value.Y), txt.Color.B, txt.Color.A);
            }
            if (this.value.Z != 0)
            {
                if (ival.B < eval.B)
                    txt.Color = new Color(txt.Color.R, txt.Color.G, (byte)(txt.Color.B + value.Z), txt.Color.A);
                else
                    txt.Color = new Color(txt.Color.R, txt.Color.G, (byte)(txt.Color.B - value.Z), txt.Color.A);
            }

            if (eval == txt.Color) this.enable = false;
        }
        #endregion
        /// <summary>
        /// Initalize The Effect With An Image
        /// </summary>
        /// <param name="img">Image To Be Used In The Effect</param>
        public void Initialize(Graphics.Image img)
        {
            this.img = img;
            txt = null;
            this.Reset();
        }
        /// <summary>
        /// Initialize The Effect With a text
        /// </summary>
        /// <param name="txt">Text To Be Used In The Effect</param>
        public void Initialize(Graphics.TextWriter txt)
        {
            this.img = null;
            this.txt = txt;
            this.Reset();
        }
        /// <summary>
        /// Set The Effect Parameter
        /// </summary>
        /// <param name="first_value">Initial Color</param>
        /// <param name="last_value">Final Color</param>
        public void SetEffectParameter(Color first_value, Color last_value)
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
            if (img != null) this.img.Color = ival;
            else if(txt!=null) this.txt.Color=ival;
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
        ///Update The Effect
        /// </summary>
        public void Update()
        {
            if (enable)
            {
                if (img != null)
                    eimg();
                else if (txt != null)
                    etxt();
            }
        }
        #endregion

    }
}
