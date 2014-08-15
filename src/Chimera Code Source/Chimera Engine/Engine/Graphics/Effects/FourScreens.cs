#region Using Statement
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion
namespace Chimera.Graphics.Effects
{
    /// <summary>
    /// This Class Allow You Using The Four Screens Effects
    /// </summary>
    public class FourScreens : Chimera.Helpers.Interface.IUpdateable, Helpers.Interface.IDrawable, Helpers.Interface.IDrawables
    {
        #region Fields
        private Image firstbg;
        private Image[,] img;
        private float[,] vect;
        private float speed;
        private float rotspeed;
        
        private GraphicsDeviceManager graphics;
        private bool enable;
        private Vector2 pos;
        #endregion
        #region Properties

        /// <summary>
        /// Get Or Set The Speed Effect
        /// </summary>
        public float Speed
        {
            get { return (speed * 100); }
            set { speed = value / 100; }
        }
        /// <summary>
        /// Get Or Set The Speed Rotation
        /// </summary>
        public float SpeedRotation
        {
            get { return rotspeed; }
            set { rotspeed = value; }
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
        #region Constructors
        /// <summary>
        /// A Constructor
        /// </summary>
        /// <param name="graphics">XNA GraphicsDeviceManager Manager</param>
        public FourScreens(GraphicsDeviceManager graphics)
        {
            this.graphics = graphics;
            img = new Image[2, 2];
              for (int i = 0; i < 2; i++)
                for (int j = 0; j < 2; j++)
                    img[i, j] = new Image();
        }
        /// <summary>
        /// A Constructor
        /// </summary>
        /// <param name="graphics">XNA GraphicsDeviceManager Reference</param>
        /// <param name="Image">The image on what the effect will be applied</param>
        public FourScreens(GraphicsDeviceManager graphics, Image Image)
        {
            this.graphics = graphics;
            img = new Image[2, 2];
            for (int i = 0; i < 2; i++)
                for (int j = 0; j < 2; j++)
                    img[i, j] = new Image();
            this.firstbg = Image;
            this.pos = Image.Position;
            
        }
        #endregion
        #region Init
        /// <summary>
        /// Initialize The Effect
        /// </summary>
        /// <param name="size">The Size Of Each Screen</param>
        /// <param name="Type">Effect Type</param>
        /// <param name="Image">The image on what the effect will be appliedt</param>
        public void Initialize(Vector2 size, Enumeration.EZOOM Type,Image Image)
        {
            speed = 0.008f;
            
            vect = new float[2, 2];
            InitRandomScale(1, 10);
            
            firstbg = Image;
            
            for (int i = 0; i < 2; i++)
                for (int j = 0; j < 2; j++)
                    img[i, j].LoadGraphicsContent(this.Image.SpriteBatch, firstbg.Texture);

            this.Reset();
        }
        /// <summary>
        /// Initialize Scale With Randomize Values
        /// </summary>
        /// <param name="min">Minimal Value</param>
        /// <param name="max">Maximal Value</param>
        public void InitRandomScale(float min, float max)
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < 2; i++)
                for (int j = 0; j < 2; j++)
                {
                    int x = rand.Next((int)min, (int)max);
                    float y = 0.01f * (float)rand.Next(0, 100);
                    vect[i, j] = x * y;
                }
        }
        /// <summary>
        /// Initialize Rotation With Randomize Value
        /// </summary>
        /// <param name="origin">The Origin Of Image</param>
        public void InitRandomRotation(Enumeration.EImageOrigin origin)
        {
            Random rand = new Random(DateTime.Now.Millisecond);

            for (int i = 0; i < 2; i++)
                for (int j = 0; j < 2; j++)
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
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    img[i, j].Rotation = 0;
                    img[i, j].Scale = new Vector2(1, 1);
                    this.vect[i, j] = 0;
                    img[i, j].Initialize(new Vector2((firstbg.Size.X / 2), (firstbg.Size.Y / 2)));
                    img[i, j].Position = new Vector2((i * (firstbg.Size.X / 2)) + pos.X, (j * (firstbg.Size.Y / 2)) + pos.Y);
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
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        img[i, j].Rotation += rotspeed;

                        img[i, j].Scale -= new Vector2((speed));
                        if (img[i, j].Scale.X <= vect[i, j]) allover++;

                    }
                }
                if (allover == 4)
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
                for (int i = 0; i < 2; i++)
                    for (int j = 0; j < 2; j++)
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
                for (int i = 0; i < 2; i++)
                    for (int j = 0; j < 2; j++)
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
                for (int i = 0; i < 2; i++)
                    for (int j = 0; j < 2; j++)
                        img[i, j].StretchDraw();
            }
        }
        /// <summary>
        /// Optimize Drawing The Effect
        /// </summary>
        public void OptimizedDraw()
        {
            if (enable)
            {
                for (int i = 0; i < 2; i++)
                    for (int j = 0; j < 2; j++)
                        Helpers.DrawOptimizer.DrawElement(img[i, j]);
            }
        }
        /// <summary>
        /// Optimize Drawing The Effect With Stretch
        /// </summary>
        public void OptimizedStretchDraw()
        {
            if (enable)
            {
                for (int i = 0; i < 2; i++)
                    for (int j = 0; j < 2; j++)
                        Helpers.DrawOptimizer.StretchDrawElement(img[i, j]);
            }
        }
        /// <summary>
        /// Optimize Drawing The Effect Without Stretch
        /// </summary>
        public void OptimizedDefaultDraw()
        {
            if (enable)
            {
                for (int i = 0; i < 2; i++)
                    for (int j = 0; j < 2; j++)
                        Helpers.DrawOptimizer.DefaultDrawElement(img[i, j]);
            }
        }
        #endregion
    }
}

