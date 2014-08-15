#region Info/Author
//----------------------------------------------------------------------------
// Author:    Tazi Mehdi
// Source:    Chimera 2D GAMES ENGINE
// Info:      Sprite public class
//-----------------------------------------------------------------------------
#endregion
#region Using Statement
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace Chimera.Graphics
{
    /// <summary>
    /// This Offer You The Possibility To Use Animated Sprite
    /// </summary>
    /// <remarks>In The Image File Animations Are Represented By Rows And Frames By Columns</remarks>
    public class Sprite : Graphics.Image, Helpers.Interface.IDrawable
    {
        #region Fields(current_animation,current_frame)
        private float current_animation;
        private float current_frame;
        private float framespersecond;
        private float animationpersecond;
        private bool islooped;
        #endregion
        #region proprieties
        /// <summary>
        /// Get Or Set The Current Animation
        /// </summary>
        public float Current_Annimation
        {
            get { return current_animation;}
            set { current_animation=value;}
        }
        /// <summary>
        /// Enable Or Disable Loop Mode
        /// </summary>
        public bool IsLooped
        {
            get {return islooped ;}
            set {islooped=value ;}
        }
        /// <summary>
        /// Get Or Set Number Of Animation To Show Each Second
        /// </summary>
        public float AnimationsPerSecond
        {
            get { return animationpersecond; }
            set { animationpersecond = value; }
        }
        /// <summary>
        ///  Get Or Set Number Of Frame To Show Each Second
        /// </summary>
        public float FramesPerSecond
        {
            get { return framespersecond; }
            set { framespersecond = value; }
        }
        /// <summary>
        /// Get Or Set The Curent Frame 
        /// </summary>
        public float Current_Frame
        {
            get { return current_frame;}
            set { current_frame=value;}
        }
        #endregion
        #region Additional Functions
        /// <summary>
        /// Reset The Sprite
        /// </summary>
        public void Reset() 
        { 
            current_frame = 0;
            current_animation = 0; 
        }

        /// <summary>
        /// Set New Animation
        /// </summary>
        /// <param name="gameTime">XNA GameTime Reference</param>
        /// <param name="dir">Direction Enumeration</param>
        public void SetNewAnimation(GameTime gameTime, Enumeration.EDirection dir)
        {
            current_frame = 0;
            SetAnimation(gameTime, dir);
        }

        /// <summary>
        /// Set Frame
        /// </summary>   
        /// <param name="gameTime">XNA GameTime Reference</param>
        /// <param name="dir">Direction Enumeration</param>
        public void SetNewFrame(GameTime gameTime, Enumeration.EDirection dir)
        {
            current_animation = 0;
            SetAnimation(gameTime, dir);
        }

        /// <summary>
        /// Set Frame
        /// </summary>        
        /// <param name="gameTime">XNA GameTime Reference</param>
        /// <param name="dir">Direction Enumeration</param>
        public void SetFrame(GameTime gameTime, Enumeration.EDirection dir)
        {
            if (dir == Enumeration.EDirection.Next) this.current_frame += this.framespersecond * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (dir == Enumeration.EDirection.Previous) this.current_frame -= this.framespersecond * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (this.islooped)
            {
                if (dir == Enumeration.EDirection.Next) this.current_frame %= this.totalColumns + 1;
                else
                {
                    if (this.current_frame < 0) this.current_frame = this.totalColumns + 1;
                }
            }

            this.SetSourceImage((int)current_animation, (int)current_frame);

        }

        /// <summary>
        /// Set Animation
        /// </summary>
        /// <param name="gameTime">XNA GameTime Reference</param>
        /// <param name="dir">Direction Enumeration</param>
        public void SetAnimation(GameTime gameTime, Enumeration.EDirection dir)
        {
            if (dir == Enumeration.EDirection.Next) this.current_animation += this.animationpersecond * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (dir == Enumeration.EDirection.Previous) this.current_animation -= this.animationpersecond * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (this.islooped)
            {
                if (dir == Enumeration.EDirection.Next) this.current_animation %= this.totalRows + 1;
                else
                {
                    if (this.current_animation < 0) this.current_animation = this.totalRows + 1;
                }
            }

         this.SetSourceImage((int)current_animation,(int) current_frame);
        }


        /// <summary>
        /// Move To The Next Frame
        /// </summary>
        
        public void NextFrame(GameTime  gameTime)
        {
            SetFrame(gameTime, Enumeration.EDirection.Next);
        }
        /// <summary>
        /// Move To The Previous Frame
        /// </summary>
        
        public void PreviousFrame(GameTime gameTime)
        {
            SetFrame(gameTime, Enumeration.EDirection.Previous);
        }

        /// <summary>
        /// Move To The Next Animation
        /// </summary>
        
        public void NextAnimation(GameTime gameTime)
        {
            SetAnimation(gameTime, Enumeration.EDirection.Next);
        }
        /// <summary>
        /// Mofe TO ThE Previous Animation
        /// </summary>
        
        public void PreviousAnimation(GameTime gameTime)
        {
            SetAnimation(gameTime, Enumeration.EDirection.Previous);
        }
        /// <summary>
        /// Set The Sprite Image
        /// </summary>
        /// <param name="animation">The Animation To Select</param>
        /// <param name="frame">The Frame To Select</param>
        public void SetImage(int animation,int frame)
        {
            base.SetSourceImage(animation,frame);
        }

        #endregion
        #region Main Methods (Initialize)
        /// <summary>
        /// Initialize The Sprite
        /// </summary>
        /// <param name="size">Sprite Size</param>
        public override void Initialize(Vector2 size)
        {
            base.Initialize(size);
            this.framespersecond = this.totalColumns*2 ;
            this.animationpersecond = this.totalRows * 2;
            this.current_frame = 0;
            this.current_animation = 0;
            this.islooped = true;
            this.depth = 1;
        }

        #endregion
    }
}
