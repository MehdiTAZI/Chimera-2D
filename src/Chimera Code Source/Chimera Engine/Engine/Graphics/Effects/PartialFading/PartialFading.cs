#region Using Statement
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace Chimera.Graphics.Effects.PartialFading
{
    /// <summary>
    /// This Class Allow You Using Partial Fading Effect
    /// </summary>
    public class PartialFading : Chimera.Helpers.Interface.IUpdateable, Helpers.Interface.IDrawable, Helpers.Interface.IDrawables 
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Image">The Image on what the effect will be applied</param>
        public PartialFading(Image Image)
        {
            this.firstbg = Image;
        }
        #region Fields
        private Image firstbg;
        private Image[,] img;
        private Vector2 size;
        private float[,] vect;
        private float speed;
        private float rotspeed;
        
        private float value;
        private bool enable;
        private Vector2 pos;
        private int w, h;
        #endregion

        #region Properties
        /// <summary>
        /// Get Or Set Rotation Speed
        /// </summary>
        public float SpeedRotation
        {
            get { return rotspeed; }
            set { rotspeed = value; }
        }
        /// <summary>
        /// Get Or Set The Effect Value
        /// </summary>
        public float Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
        /// <summary>
        /// Get Or Set The Effect Position
        /// </summary>
        public Vector2 Position
        {
            get { return pos; }
            set { pos = value; }
        }
        /// <summary>
        /// Get Or Set The Effect Speed
        /// </summary>
        public float Speed
        {
            get { return speed*100; }
            set { speed = value/100; }
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
        /// Get Or Set The Image On What The Effect Will Be Applied
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
  
        #region Init
        
        private void initvect()
        {
            for (int i = 0; i < w; i++)
                for (int j = 0; j < h; j++)
                    vect[i, j] = (value)*(i+1)*(j+1);
        }
        /// <summary>
        /// Initialize Rotation With Randomize Value
        /// </summary>
        /// <param name="origin">The Origin Of Rotation</param>
        public void InitRandomRotation(Enumeration.EImageOrigin origin)
        {

            Random rand = new Random(DateTime.Now.Millisecond);

            for (int i = 0; i < w; i++)
                for (int j = 0; j < h; j++)
                {
                    img[i, j].SetOrigin(origin);
                    img[i, j].Rotation = rand.Next(0, 360);
                }
        }
        /// <summary>
        /// Initialize Rotation With Randomize Value
        /// </summary>
        public void InitRandomRotation()
        {
            InitRandomRotation(Enumeration.EImageOrigin.Center);
        }
        /// <summary>
        /// Initialize The Effect
        /// </summary>
        /// <param name="size">Set Size Of Each Image In The Collection Effect</param>
        public void Initialize(Vector2 size)
        {
            w = (int)(firstbg.Size.X / size.X);
            speed = 0.01f;
            value = 0.3f;
            h = (int)(firstbg.Size.Y / size.Y);
            img = new Image[w, h];
            vect = new float[w, h];
            this.size = size;
            this.initvect();
            
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
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    img[i, j] = new Image();
                    img[i, j].LoadGraphicsContent(this.firstbg.SpriteBatch, firstbg.Texture);
                    img[i, j].Position = new Vector2((i * size.X) + pos.X, (j * size.Y) + pos.Y);
                    img[i, j].Initialize(new Vector2(size.X, size.Y));
                    img[i, j].SetSourceImage(j + 1, i + 1);
                    //img[i, j].Color = new Color(firstbg.Color.R, firstbg.Color.G, firstbg.Color.B, 255);
                }
            }
        }
        /// <summary>
        /// Update The Effect
        /// </summary>
        public void Update()
        {
            if (enable)
            {
                int allover = 0;
                for (int i = 0; i < w; i++)
                {
                    for (int j = 0; j < h; j++)
                    {

                        if (img[i, j].Color.A > 0.05)
                        {
                            img[i, j].Rotation += rotspeed;
                            img[i, j].Color = new Color(img[i, j].Color.R, img[i, j].Color.G, img[i, j].Color.B, (byte)(img[i, j].Color.A - speed - (vect[i, j])));
                        }
                        else allover += 1;
                    }
                }
                if (allover == w*h)
                    enable = false;

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
                        img[i, j].Draw();
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
                        img[i, j].DefaultDraw();
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
                        img[i, j].StretchDraw();
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
                        Helpers.DrawOptimizer.DrawElement(img[i, j]);
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
                        Helpers.DrawOptimizer.StretchDrawElement(img[i, j]);
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
                        Helpers.DrawOptimizer.DefaultDrawElement(img[i, j]);
            }
        }
        
        #endregion
    }
}
