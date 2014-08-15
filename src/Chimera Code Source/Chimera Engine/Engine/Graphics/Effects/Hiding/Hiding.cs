#region Using Statement
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace Chimera.Graphics.Effects.Hiding
{
    /// <summary>
    /// This Class Allow You Using Hiding Effect
    /// </summary>
    public class Hiding : Helpers.Interface.IDrawable, Helpers.Interface.IDrawables
    {
        #region Fields
        private Image firstbg;
        private Image[,] img;
        private Vector2 size;
        private bool[,] hide;
        private int time;
        private float etime;
        
        private GraphicsDeviceManager graphics;
        private Enumeration.EDirection dir;
        private bool enable;
        private int w, h,ii,jj;
        private Vector2 pos;
        #endregion
        #region Properties

        /// <summary>
        /// Effect Time Interval betwen each animation : represented per milisecond
        /// </summary>
        public int Time
        {
            get { return time; }
            set { time = value; }
        }
        /// <summary>
        /// Get Or Set Size Of Each Image In The Collection Effect
        /// </summary>
        public Vector2 Size
        {
            get { return size; }
            set { size = value; }
        }

        /// <summary>
        /// Effect Position
        /// </summary>
        public Vector2 Position
        {
            get { return pos; }
            set { pos = value; }
        }

        /// <summary>
        /// Image On What the effect will be applied
        /// </summary>
        public Image Image
        {
            get { return firstbg; }
            set { firstbg = value; }
        }


        /// <summary>
        /// Enable Or Disable The Effect
        /// </summary>
        public bool Enable
        {
            get { return enable; }
            set { enable = value; }
        }
        #endregion
        #region Constructors
        /// <summary>
        /// A Constructor
        /// </summary>
        /// <param name="graphics">XNA GraphicsDeviceManager Reference</param>
        public Hiding(GraphicsDeviceManager graphics)
        { this.graphics = graphics; }
        /// <summary>
        /// A Constructor
        /// </summary>
        /// <param name="graphics">Xna GraphicsDeviceManager Reference</param>
        /// <param name="Image">Image on what the effect will be applied</param>
        public Hiding(GraphicsDeviceManager graphics, Image Image)
        {
            this.graphics = graphics;
            this.firstbg = Image;
            this.pos = Image.Position;
        }
        #endregion
        #region Init
        /// <summary>
        /// Initialize The Effect
        /// </summary>
        /// <param name="size">Set Size Of Each Image In The Collection Effect</param>
        /// <param name="direction">Effect Direction</param>
        public void Initialize(Vector2 size,Enumeration.EDirection direction)
        {
            this.dir = direction;
            w = (int)(firstbg.Size.X / size.X);
            time = 10;
            h = (int)(firstbg.Size.Y / size.Y);
            img = new Image[w, h];
            hide = new bool[w, h];
            this.size = size;
   
            
            this.Reset();

        }
        #endregion
        #region Main Functions
        /// <summary>
        /// Reset The Effect
        /// </summary>
        public void Reset()
        {
            enable = true;
            if (dir == Chimera.Graphics.Enumeration.EDirection.Next)
            {
                ii = 0;
                jj = 0;
            }
            else
            {
                ii = w - 1;
                jj = h - 1;
            }
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    hide[i, j] = true;
                    img[i, j] = new Image();
                    img[i, j].LoadGraphicsContent(this.firstbg.SpriteBatch, firstbg.Texture);
                    img[i, j].Position = new Vector2((i * size.X) + pos.X, (j * size.Y) + pos.Y);
                    img[i, j].Initialize(new Vector2(size.X, size.Y));
                    img[i, j].SetSourceImage(j + 1, i + 1);

                }
            }
        }
        /// <summary>
        /// Update The Effect
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        public void Update(GameTime gameTime)
        {
            if (enable)
            {
                        etime += (float)gameTime.ElapsedRealTime.Milliseconds;
                        if (time <= etime)
                        {
                            etime = 0;
                            hide[ii, jj] = false;
                            if (dir == Chimera.Graphics.Enumeration.EDirection.Next)
                            {
                                if (ii < w - 1) ii++;
                                else if (ii == w - 1)
                                {
                                    ii = 0;
                                    jj++;
                                }
                            }
                            else 
                            {
                                if (ii > 0) ii--;
                                else if (ii == 0)
                                {
                                    ii = w - 1;
                                    jj--;
                                }
                            }

                        }

                        if (dir == Chimera.Graphics.Enumeration.EDirection.Next)
                        {
                            if ((ii == w - 1) && (jj == h - 1))
                                enable = false;
                        }
                        else
                        {
                            if ((ii == 0) && (jj == 0))
                                enable = false;
                        }
            }
        }
         
        /// <summary>
        /// Draw The Effect
        /// </summary>
        public void Draw()
        {
            if (enable)
            {
                for (int i = 0; i < w; i++)
                    for (int j = 0; j < h; j++)
                      if(hide[i,j]==true)  img[i, j].Draw();
            }
        }
        /// <summary>
        /// Optimize Draw Method
        /// </summary>
        public void OptimizedDraw()
        {
            if (enable)
            {
                for (int i = 0; i < w; i++)
                    for (int j = 0; j < h; j++)
                        if (hide[i, j] == true) Helpers.DrawOptimizer.DrawElement (img[i, j]);
            }
        }
        /// <summary>
        /// Draw Without Stretch The Effect
        /// </summary>
        public void DefaultDraw()
        {
            if (enable)
            {
                for (int i = 0; i < w; i++)
                    for (int j = 0; j < h; j++)
                        if (hide[i, j] == true) img[i, j].DefaultDraw();
            }
        }
        /// <summary>
        /// Optimize DefaultDraw Method
        /// </summary>
        public void OptimizedDefaultDraw()
        {
            if (enable)
            {
                for (int i = 0; i < w; i++)
                    for (int j = 0; j < h; j++)
                        if (hide[i, j] == true) Helpers.DrawOptimizer.DefaultDrawElement(img[i, j]);
            }
        }


        /// <summary>
        /// Draw With Stretch The Effect
        /// </summary>
        public void StretchDraw()
        {
            if (enable)
            {
                for (int i = 0; i < w; i++)
                    for (int j = 0; j < h; j++)
                        if (hide[i, j] == true) img[i, j].StretchDraw();
            }
        }
        /// <summary>
        /// Optimize StretchDraw Method
        /// </summary>
        public void OptimizedStretchDraw()
        {
            if (enable)
            {
                for (int i = 0; i < w; i++)
                    for (int j = 0; j < h; j++)
                        if (hide[i, j] == true) Helpers.DrawOptimizer.StretchDrawElement(img[i, j]);
            }
        }

        #endregion
    }
}
