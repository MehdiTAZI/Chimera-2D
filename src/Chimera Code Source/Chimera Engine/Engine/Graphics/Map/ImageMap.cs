#region Info/Author
//----------------------------------------------------------------------------
// Author:    Tazi Mehdi
// Source:    Chimera 2D GAMES ENGINE
// Info:      Simple Image Map
//-----------------------------------------------------------------------------
#endregion
#region Using Statement
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace Chimera.Graphics.Map.Simple
{
    /// <summary>
    /// This Class Allow You Creating A Simple Map Based On Image Array
    /// </summary>
    public class ImageMap:Helpers.Interface.IDrawable 
    {
        #region Fields(Fields,position,size)
       
        private Graphics.Image[,] Fields;
        /// <summary>
        /// Get Or Set Map Fields
        /// </summary>
        public Graphics.Image[,] Map
        {
            get {return Fields ;}
            set {Fields = Map ;}
        }
        private Vector2 nbelem;
        private GraphicsDevice graphics;
        private Rectangle limite;
        private Rectangle screen;
        #endregion
        #region Properties(position,size)
        /// <summary>
        /// Get Or Set The Screen On What The Map Will Be Drawn
        /// </summary>
       public Rectangle Screen
        {
            get {return screen ;}
            set {screen = value ;}
        }
        /// <summary>
        /// Get Or Set GraphicsDevice Refernce
        /// </summary>
       public GraphicsDevice Graphics
        {
            get { return graphics; }
            set { graphics = value; }
        }
        #endregion
        #region Additional Function(InitializeScreen)
        /// <summary>
        /// Initialize And Calculte The Screen Where The Map Will Be Drawn
        /// </summary>
       public void InitializeScreen()
        {
            //Calcul l'ecran ou sera afficher les donné sert pour optimizer le rendu
            if (graphics.Viewport.X < screen.X)
                limite.X = screen.X;
            else limite.X = graphics.Viewport.X;

            if (graphics.Viewport.Y < screen.Y)
                limite.Y = screen.Y;
            else limite.Y = graphics.Viewport.Y;

            if (graphics.Viewport.Width < screen.Width)
                limite.Width = graphics.Viewport.Width;
            else limite.Width = screen.Width;

            if (graphics.Viewport.Height < screen.Height)
                limite.Height = graphics.Viewport.Height;
            else limite.Height = screen.Height;
        }
        #endregion
        #region Main Methods (Initialize,Draw,Constructors)
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="graphics">GraphicDevice Reference</param>
        /// <param name="Nbelem">Number Of Element In The Map</param>
        public ImageMap(GraphicsDevice graphics,Vector2 Nbelem)
        {
            Initialize(graphics, Nbelem);
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="graphics">GraphicDevice Reference</param>
        /// <param name="Nbelem">Number Of Element In The Map</param>
        /// <param name="screen">The Screen Where The Map Will Be Drawn</param>
        public ImageMap(GraphicsDevice graphics, Vector2 Nbelem,Rectangle screen)
        {
            Initialize(graphics, Nbelem, screen);
        }
        /// <summary>
        /// Initialize The Map
        /// </summary>
        /// <param name="graphics">GraphicDevice Reference</param>
        /// <param name="Nbelem">Number Of Element In The Map</param>
        public void Initialize(GraphicsDevice graphics, Vector2 Nbelem)
        {
            //Fields = new T[(int)Nbelem.X,(int) Nbelem.Y];
            Fields = new Graphics.Image[(int)Nbelem.X, (int)Nbelem.Y];

            if (!(Fields is Graphics.Image[,])) throw new Exception("Error Map Just Accept a Chimera.Graphics.Imagepublic Type");
            else
            {
                this.graphics = graphics;
                this.limite = new Rectangle();
                this.nbelem = Nbelem;
                this.screen = new Rectangle(0, 0, 0, 0);
            }
        }
        /// <summary>
        /// Initialize The Map
        /// </summary>
        /// <param name="graphics">GraphicDevice Reference</param>
        /// <param name="Nbelem">Number Of Element In The Map</param>
        /// <param name="screen">The Screen Where The Map Will Be Drawn</param>
        public void Initialize(GraphicsDevice graphics, Vector2 Nbelem, Rectangle screen)
        {
            //  Fields = new T[(int)Nbelem.X, (int)Nbelem.Y];
            Fields = new Graphics.Image[(int)Nbelem.X, (int)Nbelem.Y];
            if (!(Fields is Graphics.Image[,])) throw new Exception("Error Map Just Accept a Chimera.Graphics.Imagepublic Type");
            else
            {
                this.limite = new Rectangle();
                this.nbelem = Nbelem;
                this.screen = screen;
                this.graphics = graphics;
            }
        }
        /// <summary>
        /// Initialize The Screen
        /// </summary>
        /// <param name="screen">The Screen Where The Map Will Be Drawn</param>
       public void Initialize(Rectangle screen)
        {
            this.screen = screen;
        }
        /// <summary>
        /// Draw The Map
        /// </summary>
       public void Draw()
        {
            int i, j;
            i = 0; j = 0;
            Vector2 pos = new Vector2();
            Fields[0, 0].Position = new Vector2(screen.X, screen.Y) ;
            if ((Fields[0, 0].Position.X >= limite.X) && (Fields[0, 0].Position.X <= limite.Width) && (Fields[0, 0].Position.Y >= limite.Y) && (Fields[0, 0].Position.Y <= limite.Height))
                Fields[0, 0].Draw();

            for (j = 0; j < (int)nbelem.Y; j++)
            {
                if (!((i == 0) && (j == 0)))
                {
                    if (j == 0) j = 1;
                    if (i == nbelem.X) i = 0;
                    pos.Y = Fields[i, j - 1].Position.Y + Fields[i, j - 1].Size.Y;
                    pos.X = Fields[i, j - 1].Position.X;
                    Fields[i, j].Position = pos;
                }
                for (i=0;i<(int)nbelem.X;i++)
                {
                    if (!((i == 0) && (j == 0)))
                    {
                        if (i == 0) i = 1;
                        pos.X = Fields[i - 1, j].Position.X + Fields[i - 1, j].Size.X;
                        pos.Y = Fields[i - 1, j].Position.Y;
                        Fields[i, j].Position = pos;
                        //dessine que les faces visible (Optimization)
                        if ((Fields[i, j].Position.X >= limite.X) && (Fields[i, j].Position.X <= limite.Width) && (Fields[i, j].Position.Y >= limite.Y) && (Fields[i, j].Position.Y <= limite.Height))
                            Fields[i, j].Draw();              
                    }
                }
            }

        }

        #endregion
    }
}
