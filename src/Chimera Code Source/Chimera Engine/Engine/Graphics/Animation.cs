#region Info/Author
//----------------------------------------------------------------------------
// Author:    Tazi Mehdi
// Source:    Chimera 2D GAMES ENGINE
// Info:      Animation public class
//-----------------------------------------------------------------------------
#endregion
#region Using Statement
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion
 namespace Chimera.Graphics
{
     /// <summary>
     /// This Class Offer You The Possibility to create a image
     /// </summary>
    public class Animation : Chimera.Graphics.Image, Helpers.Interface.IDrawable
    {
        #region Fields(islooped,isstarte,current_index,first_index,last_index,framespersecond)
        
        bool islooped, enable;
        Vector2 first_index,last_index;
        float currentfrm;
        float framespersecond;
        private List<Rectangle> frames;
        
        #endregion
        #region Properties(IsLooped,Enable,CurrentIndex,StartIndex,EndIndex,FramePerSeconde)

        /// <summary>
        /// Enable Or Disable Looped Mode
        /// </summary>
        public bool IsLooped
        {
            get { return islooped;}
            set { islooped=value;}
        }

        /// <summary>
        /// Enable Or Disble The Animation
        /// </summary>
        public bool Enable
        {
            get { return enable; }
            set { enable = value; }
        }
        /// <summary>
        /// The Start Index Where The Animation Begin
        /// </summary>
        public Vector2 StartIndex
        {
            get { return first_index; }
            set { first_index = value; }
        }
        /// <summary>
        /// The End Index Where The Animation Stop
        /// </summary>
        public Vector2 EndIndex
        {
            get { return last_index; }
            set { last_index = value; }
        }

        /// <summary>
        /// Number Frames Per Second
        /// </summary>
        public float FramesPerSecond
        {
            get { return framespersecond; }
            set { framespersecond= value; }
        }

        #endregion
        #region Main Methods ( Initialize,Update,Constructor)
        /// <summary>
        /// The Default Constructor
        /// </summary>
        public Animation()
        {   
            this.frames = new List<Rectangle>();
        }
        /// <summary>
        /// Initialize The Animation
        /// </summary>
        /// <param name="size">Image Size</param>
        public override void Initialize(Vector2 size)
        {
            base.Initialize(size);
            this.framespersecond = 2f;
            this.islooped = true;
            this.enable = true;
            this.first_index = new Vector2(1, 1);
            this.last_index = new Vector2(this.totalRows, this.totalColumns);
            Generate();
        }         

        /// <summary>
        /// Update The Animation
        /// </summary>
        
        public virtual void Update(GameTime gameTime)
        {
            if (this.enable)
            {
                this.currentfrm += this.framespersecond * (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (this.islooped)
                {
                    this.currentfrm %= this.frames.Count;
                }
                else if (this.currentfrm > this.frames.Count)
                {
                    this.currentfrm = this.frames.Count -1;
                    this.enable = false;
                }
                this.imgsource = frames[(int)this.currentfrm];
            }
        }

        #endregion
        #region Additional Functions(Generate)
        /// <summary>
        /// Generate The Animation
        /// </summary>
        public void Generate()
        {
            this.frames.Clear();

            int indj = (int)this.first_index.Y;
            int indi = (int)this.first_index.X;

            while((indi < this.last_index.X) || (indi==this.last_index.X) &&(indj<=this.last_index.Y+1) )
            {
                this.frames.Add(CalculatImgSource(indi, indj));
                 indj +=1;
                 if (indj > this.totalColumns)
                 {
                     indi += 1;
                     indj = 1;
                 }
            }       
        }
        #endregion
    }
}
