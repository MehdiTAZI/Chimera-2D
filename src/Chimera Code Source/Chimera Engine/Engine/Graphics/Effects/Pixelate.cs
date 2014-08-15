#region Using Statement
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace Chimera.Graphics.Effects
{
    /// <summary>
    /// This Class Allow You Using The Pixelisation Effects
    /// </summary>
    public class Pixelate : Chimera.Helpers.Interface.IUpdateable, Helpers.Interface.IDrawable, Helpers.Interface.IDrawables
    {
        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="graphics">XNA GraphicsDeviceManager Reference</param>
        public Pixelate(GraphicsDeviceManager graphics)
        { this.graphics = graphics; }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="graphics">XNA GraphicsDeviceManager Reference</param>
        /// <param name="Image">The Image On What The Effect Will Be Applied</param>
        public Pixelate(GraphicsDeviceManager graphics, Image Image)
        {
            Initialize(graphics, Image, Chimera.Graphics.Enumeration.EZOOM.IN);
        }
        #endregion
        #region Fields
        private Image firstbg;
        private Image[,] img;
        private Enumeration.EZOOM type;
        private Vector2 size;
        private float[,] vect;
        private float speed;
        private float rotspeed;
        private GraphicsDeviceManager graphics;
        private bool enable;
        private float maxi;
        private int w, h;
        private Vector2 pos;
        #endregion
        #region Properties
        /// <summary>
        /// Get Or Set The Type Of Pixelisation Effect
        /// </summary>
        public Enumeration.EZOOM Type
        {
            get {return type ;}
            set {type=value ;}
        }
        /// <summary>
        /// Get Or Set The Speed Effect
        /// </summary>
        public float Speed
        {
            get { return (speed*100); }
            set { speed = value/100; }
        }
        /// <summary>
        /// Get Or Set The Speed Rotation Effect
        /// </summary>
        public float SpeedRotation
        {
            get { return rotspeed; }
            set { rotspeed = value; }
        }
        /// <summary>
        /// Set Or Get The Size
        /// </summary>
        public Vector2 Size
        {
            get { return size; }
            set { size = value; }
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
        /// The Image To Pixelate
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
        /// Initialize The Effect
        /// </summary>
        /// <param name="graphics">XNA GraphicsDeviceManager Reference</param>
        /// <param name="Image">The Image On What The Effect Will Be Applied</param>
        /// <param name="Type">Effect Type</param>
        public void Initialize(GraphicsDeviceManager graphics, Image Image, Enumeration.EZOOM Type)
        {
            this.graphics = graphics;
            this.firstbg = Image;
            this.pos = Image.Position;
            this.type = Type; 
            this.Reset();
        }
        /// <summary>
        /// Initialize The Effect
        /// </summary>
        /// <param name="size">The Image Size</param>
        /// <param name="Type">Effect Type</param>
        public void SetParameter(Vector2 size,Enumeration.EZOOM Type)
        {
            this.type = Type;
            w = (int)(firstbg.Size.X / size.X);
            speed = 0.05f;
            h = (int)(firstbg.Size.Y / size.Y);
            img = new Image[w, h];
            vect = new float[w, h];
            this.size = size;
            InitRandomScale(1,10);
        }
        /// <summary>
        /// Initialize Randomize Scal
        /// </summary>
        /// <param name="min">Minimal Value</param>
        /// <param name="max">Maximal Value</param>
        public void InitRandomScale(float min, float max)
        {
            this.maxi=max;
                Random rand = new Random(DateTime.Now.Millisecond);
                for ( int i=0;i<w;i++)
                    for (int j =0;j<h;j++)
                        {
                        int x = rand.Next((int)min, (int)max);
                        float y = 0.01f *(float)rand.Next(0, 100);
                        vect[i, j] = x * y;
                        }        
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

                    if (type == Chimera.Graphics.Enumeration.EZOOM.IN)
                        img[i, j].Scale = (new Vector2(1f));
                    else
                        img[i, j].Scale = (new Vector2(maxi));

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
        public void Update()
        {
            if (enable)
            {
                int allover = 0;
                for (int i = 0; i < w; i++)
                {
                    for (int j = 0; j < h; j++)
                    {
                        img[i, j].Rotation += rotspeed;
                        if (type == Chimera.Graphics.Enumeration.EZOOM.IN)
                        {
                            img[i, j].Scale += new Vector2((speed));
                            if (img[i, j].Scale.X >= vect[i, j]) allover++;
                        }
                        else
                        {
                            img[i, j].Scale -= new Vector2((speed));
                            if (img[i, j].Scale.X <= vect[i, j]) allover++;
                        }
                        
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
        /// Draw The Effect Without Stretch
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
        /// Draw The Effect With Stretch
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
        /// Optimize Effect Drawing
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
        /// Optimize The Drawing Effect Without Stretch
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
        /// Optimize The Drawing Effect With Stretch
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

