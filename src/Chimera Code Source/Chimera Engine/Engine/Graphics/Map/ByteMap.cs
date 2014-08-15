#region Info/Author
//----------------------------------------------------------------------------
// Author:    Tazi Mehdi
// Source:    Chimera 2D GAMES ENGINE
// Info:      Simple Byte Map
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
    /// This Class Allow You Creating A Simple Map Based On Byte Array
    /// </summary>
    public class ByteMap : Helpers.Interface.IDrawable 
    {
           #region Fields
        private Texture2D[] GameMap;
        private byte[,] Map1;
        private Color col;
        private int Map1X = 0;int Map1Y = 0;
        private int MapOffsetX = 0;int MapOffsetY = 0;
        private int MapTileDisplayWidth = 20;int MapTileDisplayHeight = 20;
        private int MapTilesWidth = 64;int MapTilesHeight = 64;
        private SpriteBatch spriteBatch;
        private GraphicsDevice graphics;
        #endregion
        #region Properties
        /// <summary>
        /// Get Or Set Where The Map Will Started In The Map Byte
        /// </summary>
        public Vector2 MapStart
        {
            get {return new Vector2(Map1X, Map1Y); }
            set {
                Map1X=(int)value.X;
                Map1Y=(int)value.Y;
            }
        }
        
        // OpenFromFile b1=width,b2=height =>for generateur de map
        // charger une struct [byte:type ,taille x,tailley]
        
        
         /// <summary>
         /// Get Or Set The Map Array Byte
         /// </summary>
       public byte[,] Map
        {
            get {return Map1 ;}
            set { Map1 = value; }
        }
        /// <summary>
        /// Get Or Set Map Titles Displayed Size
        /// </summary>
       public Vector2 MapTitlesDisplaySize 
        {
            get {return new Vector2(MapTileDisplayWidth,MapTileDisplayHeight);}
            set 
            {
                if ((value.X > 0) && (value.Y > 0))
                {
                    MapTileDisplayWidth = (int)value.X;
                    MapTileDisplayHeight = (int)value.Y;
                }
            }
        }
        /// <summary>
        /// Get Or Set Map Titles Size
        /// </summary>
        public Vector2 MapTitlesSize
        {
            get {return new Vector2(MapTilesWidth,MapTilesHeight);}
            set {
                    if ((value.X > 0) && (value.Y > 0))
                    {
                        MapTilesWidth=(int)value.X;
                        MapTilesHeight = (int)value.Y;
                    }
                }
        }
        /// <summary>
        /// Get Or Set Where The Map Will Start In The Screen
        /// </summary>
       public Vector2 MapOffset
        {
            get { return new Vector2(MapOffsetX, MapOffsetY); }
            set
            {
                MapOffsetX = (int)value.X;
                MapOffsetY = (int)value.Y;
            }
        }
        /// <summary>
        /// Get Or Set Map Color
        /// </summary>
       public Color Color
        {
            get {return col ;}
            set {col = value ;}
        }
        #endregion
        #region Main Methods And Others
        /// <summary>
        /// Matche texture with index byte
        /// </summary>
        /// <param name="texture">Texture To Match With The Index</param>
        /// <param name="Index">Index To Matche With The Texture</param>
       public void SetTextureMap(Texture2D texture, byte Index)
        {GameMap[Index] = texture;}
        /// <summary>
        /// Initialize The Map
        /// </summary>
        /// <param name="spriteBatch">XNA SpriteBatch Reference</param>
       public void Initialize(GraphicsDevice graphics,SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
            this.col = Color.White;
            this.graphics = graphics;
        }
        /// <summary>
        /// Set Maximal Texture
        /// </summary>
        /// <param name="MaxTexture">Maximal Number Of Texture To Use</param>
       public void SetMaxTexture(byte MaxTexture)
        {
            GameMap = new Texture2D[MaxTexture];
        }
        /// <summary>
        /// Set MapSize
        /// </summary>
        /// <param name="MapHeight">Map Hieght Value</param>
        /// <param name="MapWidth">Map Width Value</param>
       public void SetMapSize(byte MapHeight, byte MapWidth)
        {
            Map1 = new byte[MapHeight, MapWidth];
            MapTileDisplayWidth = MapWidth;
            MapTileDisplayHeight = MapHeight;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="MaxTexture">Maximal Number Of Texture To Be Used</param>
        /// <param name="MapHeight">Map Height Value</param>
        /// <param name="MapWidth">Map Width Value</param>
       public ByteMap(byte MaxTexture,int MapHeight, int MapWidth)
        {
             GameMap = new Texture2D[MaxTexture];
             Map1 = new byte[MapHeight, MapWidth];
             MapTileDisplayWidth = MapWidth;
             MapTileDisplayHeight = MapHeight;
        }
        /// <summary>
        /// Draw The Map
        /// </summary>
       public void Draw()
        {
            // Draw the map
            for (int y = 0; y < MapTileDisplayHeight; y++)
            {
                for (int x = 0; x < MapTileDisplayWidth; x++)
                {
                    spriteBatch.Draw(GameMap[Map1[y + Map1Y, x + Map1X]], new Rectangle((x * MapTilesWidth) + MapOffsetX, y * MapTilesHeight + MapOffsetY, MapTilesWidth, MapTilesHeight), col);
                }
            }
        }
        /// <summary>
        /// Optimize Draw Method
        /// </summary>
        public void OptimizeDraw()
        {
            
            for (int y = 0; y < MapTileDisplayHeight; y++)
            {
                for (int x = 0; x < MapTileDisplayWidth; x++)
                {
                    int x1=(x * MapTilesWidth) + MapOffsetX;
                    int y1= y * MapTilesHeight + MapOffsetY;
                    if( (x1 >= graphics.Viewport.X) && (y1>=graphics.Viewport.Y) && (x1<=graphics.Viewport.Width) &&(y1<=graphics.Viewport.Height)  )
                    spriteBatch.Draw(GameMap[Map1[y + Map1Y, x + Map1X]], new Rectangle(x1,y1 , MapTilesWidth, MapTilesHeight), col);
                }
            }
        }
        #endregion
    }
}
