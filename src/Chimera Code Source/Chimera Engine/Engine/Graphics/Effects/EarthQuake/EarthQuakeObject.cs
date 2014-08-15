#region Using Statement
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace Chimera.Graphics.Effects.EarthQuake
{
    /// <summary>
    /// This Class Allow Using The EarthQuake Effect On An Object
    /// </summary>
    public class Object : Chimera.Helpers.Interface.IUpdateable
    {
        #region Fields
        private Vector2 min, max;
        private bool enable;
        private Graphics.Image img;
        private Graphics.TextWriter txt;
        #endregion

        #region Properties
        
        /// <summary>
        /// Set Or Get Maximal Power Of The EarthQuake Effect
        /// </summary>
        public Vector2 Max
        {
            get {return this.max ;}
            set {this.max=value ;}
        }
        /// <summary>
        /// Set Or Get Minimal Power Of The EarthQuake Effect
        /// </summary>
        public Vector2 Min
        {
            get { return this.min; }
            set { this.min = value; }
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

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public Object()
        {

        }
        #endregion

        #region Main Function
        /// <summary>
        /// Initialize The Effect 
        /// </summary>
        /// <param name="img">Image On What The Effect Will Be Applied</param>
        public void Initialize(Graphics.Image img)
        {
            this.img = img;
            this.txt = null;
            this.Reset();
        }
        /// <summary>
        /// Initialize The Effect 
        /// </summary>
        /// <param name="txt">Text On What The Effect Will Be Applied</param>
        public void Initialize(Graphics.TextWriter txt)
        {
            this.img = null;
            this.txt = txt;
            this.Reset();
        }
        /// <summary>
        /// Set Effect Parameters
        /// </summary>
        /// <param name="min">Minimal Power EarthQuake Value</param>
        /// <param name="max">Maximal Power EarthQuake Value</param>
        public void SetEffectParameters(Vector2 min,Vector2 max)
        {
            this.min = min;
            this.max = max;
        }

    /// <summary>
    /// Reset The Effect
    /// </summary>
        public void Reset()
        {
            this.enable = true;
        }
        /// <summary>
        /// Update The Effect
        /// </summary>
        public void Update()
        {
            if (this.enable)
            {
                if(img!=null)
                {
                    //code pour image
                    img.Position = new Vector2(Helpers.Randomize.GenerateInteger((int)min.X, (int)max.X),Helpers.Randomize.GenerateInteger((int)min.Y, (int)max.Y));  
                }
                else if (txt != null)
                {
                    //code pour TextWriter
                    txt.Position = new Vector2(Helpers.Randomize.GenerateInteger((int)min.X, (int)max.X),Helpers.Randomize.GenerateInteger((int)min.Y, (int)max.Y));  
                }
            }
        }

        #endregion       

    }
}
