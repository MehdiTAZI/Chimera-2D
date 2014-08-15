#region Using Statement
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace Chimera.Graphics.Effects.Eraser
{
    /// <summary>
    /// This Class Allow You Using The Eraser Effect
    /// </summary>
    public class Eraser : Chimera.Helpers.Interface.IUpdateable, Helpers.Interface.IDrawable, Helpers.Interface.IDrawables
    {
       /// <summary>
       /// The Default Constructor
       /// </summary>
        /// <param name="Image">The image on what the effect will be applied</param>
        public Eraser(Image Image)
        {
            this.firstbg = Image;
        }
        #region Fields
        private Image firstbg;
        private Image[,] img;
        private Vector2 size;

        private float speed;
        
        private float value;
        private bool enable;
        private Vector2 pos;
        private int w, h;
        #endregion
        #region Properties
       /// <summary>
       /// Get Or Set The Effect Value
       /// </summary>
        public float Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
       /// <summary>
       /// Get Or Set The Position
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
            get { return speed * 100; }
            set { speed = value / 100; }
        }
       /// <summary>
        /// Get Or Set The image Size on what the effect will be applied 
       /// </summary>
        public Vector2 Size
        {
            get { return size; }
            set { size = value; }
        }
       /// <summary>
        /// Set Or Get The image on what the effect will be applied
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
       /// <summary>
       /// Initalize The Effect
       /// </summary>
        /// <param name="size">Size of the image on what the effect will be applied</param>
        public void Initialize(Vector2 size)
        {
            w = (int)(firstbg.Size.X / size.X);
            speed = 0.01f;
            value = 0.3f;
            h = (int)(firstbg.Size.Y / size.Y);
            img = new Image[w, h];

            this.size = size;

            this.Reset();

        }
        #endregion
        #region Main Function
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
                        if ( (byte)(img[i, j].Color.A - speed) <  (byte)(img[i, j].Color.A) )
                        {
                            if (img[i, j].Color.A > 0.01) img[i, j].Color = new Color(img[i, j].Color.R, img[i, j].Color.G, img[i, j].Color.B, (byte)(img[i, j].Color.A - speed));
                            else allover += 1;
                        }
                        else allover += 1;
                    }
                }
                if (allover == w * h)
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
       /// Draw With Stretching The Effect
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
       /// Optimize Draw Method For The Effect
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
       /// Optimize StretchDraw Method For The Effect
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
       /// Optimize DefaultDraw Method Of The Effect
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
                    img[i, j].Color = new Color(firstbg.Color.R, firstbg.Color.G, firstbg.Color.B, (byte)(255 - (Helpers.BasicFunctions.PerCent((i + 1) * (j + 1), w + h) + 150)));
                    img[i, j].Initialize(new Vector2(size.X, size.Y));
                    img[i, j].SetSourceImage(j + 1, i + 1);

                }
            }
        }
        #endregion
    }
}
