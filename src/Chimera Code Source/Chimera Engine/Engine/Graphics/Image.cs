#region Info/Author
//----------------------------------------------------------------------------
// Author:    Tazi Mehdi
// Source:    Chimera 2D GAMES ENGINE
// Info:      Image public class : Basic public class 
//-----------------------------------------------------------------------------
#endregion
#region Using Statement
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace Chimera.Graphics
{
    /// <summary>
    /// This Class Allow You To Use An Image
    /// </summary>
    public class Image : Helpers.Interface.IDrawable, Helpers.Interface.IDrawables
    {
        /// <summary>
        /// The Default Constructor
        /// </summary>
        public  Image()
        {
          //SetOrigin(Chimera.Graphics.Enumeration.EImageOrigin.Center);
        }
        #region Fields(spriteBatch,texture,imgsource,origin,position,size,rotation,color,scale,effects,depth)

        private SpriteBatch spriteBatch;

        private Texture2D texture;//la texture contient une ou plusieur images
        protected Rectangle imgsource;//X,Y image a dessiner
        protected Vector2 origin;   //origine de l'image par defaut : haut agauche

        private Vector2 position;
        private Vector2 size;
        private float rotation;

        private Color color;
        private Vector2 scale;
        private Vector2 strevect = Vector2.Zero;
        private bool stretch = false;
        private SpriteEffects effects;
        protected float depth;

        protected int totalColumns;
        protected int totalRows;

        #endregion
        #region Properties(Position,Size,Rotation,Color,Scale)
        /// <summary>
        /// Get Or Set The Image Position
        /// </summary>
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        /// <summary>
        /// Get Reference The SpriteBatch
        /// </summary>
        public SpriteBatch SpriteBatch
        {
            get { return this.spriteBatch; }
        }
        /// <summary>
        /// Get Or Set Image Size
        /// </summary>
        public Vector2 Size
        {
            get { return size; }
            set {
                size = value;
                strevect = new Vector2(size.X / texture.Width, size.Y / texture.Height);
            }
        }
        /// <summary>
        /// Get Reference to Texture2D
        /// </summary>
        public Texture2D Texture
        {
            get { return this.texture;}
        }
        /// <summary>
        /// Get Or Set The Origin Of Image
        /// </summary>
        public Vector2 Origin
        {
            get { return  origin; }
            set { origin = value; }
        }
        /// <summary>
        /// Enable Or Disable The Stretch Mode
        /// </summary>
        public bool Stretch
        {
            get { return stretch;}
            set { stretch=value ;}
        }
        /// <summary>
        /// Get Or Set Rotation Degree
        /// </summary>
        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        /// <summary>
        /// Get Or Set The Image Depth
        /// </summary>
        public float Depth
        {
            get { return depth; }
            set { depth = value; }
        }

        /// <summary>
        /// Get Or Set Image Color
        /// </summary>
        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        /// <summary>
        /// Get Or Set Image Scale Value
        /// </summary>
        public Vector2 Scale
        {
            get { return scale;}
            set { scale = value;}
        }
        

        /// <summary>
        /// Get Totals Columns
        /// </summary>
        public int TotalColumns{get { return totalColumns; }}

        /// <summary>
        /// Get Total Rows
        /// </summary>
        public int TotalRows{get { return totalRows; }}
        /// <summary>
        /// Get Total Cels
        /// </summary>
        /// <returns></returns>
        public int GetLength(){ return totalColumns * totalRows;}

        #endregion
        #region Main Methods (LoadGraphicsContent,Initialize,Draw)
        /// <summary>
        /// Load Graphics Content
        /// </summary>
        /// <param name="spriteBatch">XNA Sprite Batch</param>
        /// <param name="texture">texture to use</param>
        public void LoadGraphicsContent(SpriteBatch spriteBatch, Texture2D texture)
        {
            this.spriteBatch = spriteBatch;
            this.texture = texture;
        }
        /// <summary>
        /// Initalize The Image
        /// </summary>
        /// <param name="size">Image Size</param>
        public virtual void Initialize(Vector2 size)
        {        
            this.color = Color.White;
            this.scale = Vector2.One;
            this.effects = SpriteEffects.None;
            this.depth = 0.5f;
            this.size = size;
            this.imgsource = CalculatImgSource(1,1);
            this.totalColumns = this.texture.Width / (int)this.size.X;
            this.totalRows = this.texture.Height / (int)this.size.Y;
            strevect = new Vector2(size.X / texture.Width, size.Y / texture.Height);
        }

        /// <summary>
        /// Draw Image Without Stretch Option
        /// </summary>
        
        public virtual void DefaultDraw()
        {
            this.spriteBatch.Draw(this.texture, this.position, this.imgsource, this.color, this.rotation, this.origin, this.scale, this.effects, this.depth);
        }
        /// <summary>
        /// Draw The Image
        /// </summary>
        
        public virtual void Draw()
        {
            if (this.stretch == false)
                DefaultDraw();
            else
                StretchDraw();
        }
        /// <summary>
        /// Draw Image With Stretch Option
        /// </summary>
        
        public virtual void StretchDraw()
        {   
            this.spriteBatch.Draw(this.texture, this.position, new Rectangle(0,0,texture.Width,texture.Height), this.color, this.rotation, this.origin, strevect, this.effects, this.depth);
        }        
       #endregion
        #region Additional Functions(CalculatImgSource,SetSourceImage,SetOrigin)
        /// <summary>
        /// Set The Source Image ( If It's Sprite Or Animation )
        /// </summary>
        /// <param name="row">The Row Or The Animation</param>
        /// <param name="column">The Column Or The Frame</param>
        public void SetSourceImage(int row,int column)
        {
            this.imgsource = CalculatImgSource(row,column);
        }
        /// <summary>
        /// Set The Image Origin
        /// </summary>
        /// <param name="_origin">The Image Origin</param>
        public void SetOrigin(Graphics.Enumeration.EImageOrigin _origin)
        {
            if (_origin == Graphics.Enumeration.EImageOrigin.Center)
            {
                origin = new Vector2(this.size.X/2, this.size.Y/2);
            }
            else if (_origin == Graphics.Enumeration.EImageOrigin.RightDownCorner)
            {
                origin = new Vector2(this.size.X,this.size.Y);
            }
            else
            {
                origin = new Vector2(0,0);
            }
        }

        /// <summary>
        /// Calculat The Image Source
        /// </summary>
        /// <param name="row">The Row In The Picture</param>
        /// <param name="column">The Column In The Picture</param>
        /// <returns></returns>
        protected Rectangle CalculatImgSource(int row,int column )
        {

            int frameWidth = (int)this.size.X;
            int frameHeight = (int)this.size.Y;
     

            this.totalColumns = this.texture.Width / frameWidth;
            this.totalRows = this.texture.Height / frameHeight;

            if (row > totalRows) row = totalRows;
            if (column > totalColumns) column = totalColumns;
            if (row < 1) row = 1;
            if (column < 1) column = 1;
            int overx = (column - 1) * frameWidth;
            int overy = (row - 1) * frameHeight;

            return new Rectangle(overx, overy, frameWidth, frameHeight);
        }
        #endregion
    }
}
