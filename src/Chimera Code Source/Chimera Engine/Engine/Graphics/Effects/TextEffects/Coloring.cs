using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Chimera.Graphics.Effects.TextEffects
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
            col = new Chimera.Graphics.Effects.Coloring();   
        }
        #region Fields
        private Graphics.Effects.Coloring col;
        private Graphics.TextWriter txt;
        #endregion
        #region Properties
        /// <summary>
        ///The Value With what will increase the color  
        /// X = Red , Y = Green , Z = Blue , W = Alpha
        /// </summary>
        public Vector4 Value
        {
            get { return this.col.Value; }
            set
            {
                this.col.Value = value;
                if (this.col.Value == new Vector4(0, 0, 0, 0)) this.col.Enable = false;
            }
        }
        /// <summary>
        /// The Start Color
        /// </summary>
        public Color StartValue
        {
            get { return this.col.StartValue; }
            set { this.col.StartValue= value; }
        }
        /// <summary>
        /// The Final Color
        /// </summary>
        public Color EndValue
        {
            get { return this.col.EndValue; }
            set { this.col.EndValue= value; }
        }
        /// <summary>
        /// Enable Or Disable The Effect
        /// </summary>
        public bool Enable
        {
            get { return this.col.Enable; }
            set { this.col.Enable = value; }
        }
        #endregion
        #region Functions
        /// <summary>
        /// Initialize The Effect With a text
        /// </summary>
        /// <param name="txt">Text To Be Used In The Effect</param>
        public void Initialize(TextWriter txt)
        {
            this.txt = txt;
            col.Initialize(this.txt);
        }
        /// <summary>
        /// Update The Effect
        /// </summary>
        public void Update()
        {
            col.Update();
        }
        /// <summary>
        /// Reset The Effect
        /// </summary>
        public void Reset()
        {
            col.Reset();
        }
        /// <summary>
        /// Set The Effect Parameter
        /// </summary>
        public void SetEffectParameter()
        {
            col.SetEffectParameter();
        }
        /// <summary>
        /// Set The Effect Parameter
        /// </summary>
        /// <param name="first_value">Initial Color</param>
        /// <param name="last_value">Final Color</param>
        public void SetEffectParameter(Color first_value, Color last_value)
        {
            col.SetEffectParameter(first_value, last_value);
        }
        #endregion
    }
}
