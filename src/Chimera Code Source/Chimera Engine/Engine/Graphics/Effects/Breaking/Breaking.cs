#region Using Statement
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace Chimera.Graphics.Effects.Breaking
{
    /// <summary>
    /// This Class Allow You Using Breaking Effect
    /// </summary>
    public class Breaking : Chimera.Helpers.Interface.IUpdateable, Helpers.Interface.IDrawable, Helpers.Interface.IDrawables
 
    {
        #region Fields
        private Image firstbg;
        private Image[,] img;
        private Vector2 size;
        private Vector2[,] vect;
        private float speed;
        private float rotspeed;
        
        private GraphicsDeviceManager graphics;
        
        private bool enable;
        private int w,h;
        private Vector2 pos;
        #endregion
        #region Properties
        /// <summary>
        /// Get Or Set The Effect Speed
        /// </summary>
        public float Speed
        {
            get {return speed ;}
            set { speed = value ;}
        }
        /// <summary>
        /// Get Or Set The Effect Speed Rotation
        /// </summary>
        public float SpeedRotation
        {
            get { return rotspeed; }
            set { rotspeed = value; }
        }
        /// <summary>
        /// Get Or Set Size Of Each Image In The Collection Effect
        /// </summary>
        public Vector2 Size
        {
            get {return size ;}
            set {size= value ;}
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
        /// Get Or Set The Image On What The Effect Will Be Applied
        /// </summary>
        public Image Image
        {
            get {return firstbg ;}
            set {firstbg=value ;}
        }
        /// <summary>
        /// Enable Or Disable The Effect
        /// </summary>
        public bool Enable
        {
            get {return enable ;}
            set { enable = value; }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="graphics">XNA GraphicsDeviceManager</param>
        public Breaking(GraphicsDeviceManager graphics)
        {this.graphics = graphics;}
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="graphics">XNA GraphicsDeviceManager</param>
        /// <param name="Image">The Image On What The Effect Will Be Applied</param>
        public Breaking(GraphicsDeviceManager graphics,Image Image)
        {
            this.graphics = graphics;
            this.firstbg = Image;
            this.pos = Image.Position;
        }
        #endregion
        #region Init
        /// <summary>
        /// Initiaze The Effect
        /// </summary>
        /// <param name="size">Set Size Of Each Image In The Collection Effect</param>
        public void Initialize(Vector2 size)
        {
            w = (int)(firstbg.Size.X / size.X);
            speed = 1;
            h = (int)(firstbg.Size.Y / size.Y);
            img = new Image[w, h];
            vect = new Vector2[w, h];
            this.size = size;
            
            InitRandomDest(new Vector2(-10, -10), new Vector2(10, 10));    
            
            this.Reset();
           
        }
        /// <summary>
        /// Initialize Randomize Destionation Of Image Each Image
        /// </summary>
        /// <param name="start">The Start Vector</param>
        /// <param name="end">The Final Vector</param>
        public void InitRandomDest(Vector2 start , Vector2 end)
        {
            if ((start.X != end.X) || (start.Y != end.Y))
            {

                Random rand = new Random(DateTime.Now.Millisecond);
                int x = 0;
                int y = 0;
                for (int i = 0; i < w; i++)
                    for (int j = 0; j < h; j++)
                    {
                        if ((start.X != 0) && (end.X != 0))
                        {
                            do
                            {
                                x = rand.Next((int)start.X, (int)end.X);
                            } while (x == 0);
                        }
                        else
                            x = rand.Next((int)start.X, (int)end.X);
                        if ((start.Y != 0) && (end.Y != 0))
                        {
                            do
                            {
                                y = rand.Next((int)start.Y, (int)end.Y);
                            } while (y == 0);
                        }
                        else
                            y = rand.Next((int)start.Y, (int)end.Y);

                        vect[i, j] = new Vector2(x, y);
                    }
            }
            else
            {
                for (int i = 0; i < w; i++)
                    for (int j = 0; j < h; j++)
                        vect[i, j] = end;
            }
        }
        /// <summary>
        /// Initialize Randomize Rotation
        /// </summary>
        /// <param name="origin">Rotation Origin</param>
        public void InitRandomRotation(Enumeration.EImageOrigin origin)
        {
            
                Random rand = new Random(DateTime.Now.Millisecond);
                
                for (int i = 0; i < w; i++)
                    for (int j = 0; j < h; j++)
                    {
                           img[i, j].SetOrigin(origin);
                           img[i, j].Rotation = rand.Next(0,360);
                    }
            
            
        }
        /// <summary>
        /// Initalize Randomize Rotatio
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
                bool allover = true;
                for (int i = 0; i < w; i++)
                {
                    for (int j = 0; j < h; j++)
                    {
                        img[i, j].Rotation += rotspeed;
                        img[i, j].Position += (vect[i, j]*speed);
                        if ((img[i, j].Position.X  <= graphics.GraphicsDevice.Viewport.Width) &&
                            (img[i, j].Position.Y <= graphics.GraphicsDevice.Viewport.Height) &&
                            (img[i, j].Position.X + img[i,j].Size.X >= graphics.GraphicsDevice.Viewport.X) &&
                            (img[i, j].Position.Y + img[i, j].Size.Y >= graphics.GraphicsDevice.Viewport.Y))
                            allover = false;
                    }
                }
                if (allover == true)
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
        /// Optimize StrechDraw Method
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
