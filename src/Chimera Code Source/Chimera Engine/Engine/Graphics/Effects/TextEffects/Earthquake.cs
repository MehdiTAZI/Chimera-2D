using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Chimera.Graphics.Effects.TextEffects
{
    /// <summary>
    /// This Class Allow Using The EarthQuake Effect On A Text
    /// </summary>
    public class EarthQuake : Chimera.Helpers.Interface.IUpdateable
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public EarthQuake()
        {
            eff = new Graphics.Effects.EarthQuake.Object();
        }
        #region Fields
        private TextWriter txt;
        private Graphics.Effects.EarthQuake.Object eff;
        #endregion
        #region Properties
        /// <summary>
        /// Set Or Get Maximal Power Of The EarthQuake Effect
        /// </summary>
        public Vector2 Max
        {
            get { return this.eff.Max; }
            set { this.eff.Max = value; }
        }
        /// <summary>
        /// Set Or Get Minimal Power Of The EarthQuake Effect
        /// </summary>
        public Vector2 Min
        {
            get { return this.eff.Min; }
            set { this.eff.Min = value; }
        }
        /// <summary>
        /// Enable Or Disable The Effect
        /// </summary>
        public bool Enable
        {
            get { return this.eff.Enable; }
            set { this.eff.Enable = value; }
        }

        #endregion
        #region Functions
        /// <summary>
        /// Initialize The Effect 
        /// </summary>
        /// <param name="txt">Text On What The Effect Will Be Applied</param>
        public void Intialize(TextWriter txt)
        {
            this.txt = txt;
            eff.Initialize(this.txt);
        }
        /// <summary>
        /// Update The Effect
        /// </summary>
        public void Update()
        {
            eff.Update( );
        }
        /// <summary>
        /// Set Effect Parameters
        /// </summary>
        /// <param name="min">Minimal Power EarthQuake Value</param>
        /// <param name="max">Maximal Power EarthQuake Value</param>
        public void SetEffectParameters(Vector2 min,Vector2 max)
        {
            eff.SetEffectParameters(min, Max);
        }
        /// <summary>
        /// Reset The Effect
        /// </summary>
        public void Reset()
        {
            eff.Reset();
        }
        #endregion
    }
}
