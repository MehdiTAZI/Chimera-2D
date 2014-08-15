#region Info/Author
//----------------------------------------------------------------------------
// Author:    Tazi Mehdi
// Source:    Chimera 2D GAMES ENGINE
// Info:      Simple Screen Map
//-----------------------------------------------------------------------------
#endregion

#region Using Statement
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace Chimera.Graphics.Map.Simple
{
    /// <summary>
    /// This Class Allow You Creating A Simple Map Based On Screen Rectangle
    /// </summary>
    public class ScreenedMap : Helpers.Interface.IDrawable
    {
    #region Fields(Fields,mapsize,Heritance)
       private  Graphics.Image[,] Fields;
        /// <summary>
        /// Get Or Set Map Fileds
        /// </summary>
        public Graphics.Image[,] Map
        {
            get {return Fields ;}
            set {Fields=value ;}
        }
        //Image de la map
        protected Vector2 mapsize;
        public Rectangle Screen;
       
        protected Vector2 size;

        #endregion

        #region Main Methods (Constructor,Initialize,Draw)
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mapsize">Map Size</param>
       public ScreenedMap(Vector2 mapsize)
        {
            //Initialize(new Vector2(16,12));        
            Initialize(mapsize);        
        }
       /// <summary>
       /// Initialize The Map
       /// </summary>
       /// <param name="mapsize">Map Size</param>
       public void Initialize(Vector2 mapsize)
        {
            this.mapsize = mapsize;//nombre d'image
            Fields = new Graphics.Image [(int)mapsize.X, (int)mapsize.Y];
            for (int i = 0; i < mapsize.X; i++)
                for (int j = 0; j < mapsize.Y; j++)
                    Fields[i, j] = new Graphics.Image();
            size.X = Screen.Width / mapsize.X;//taille d'une image
            size.Y = Screen.Height / mapsize.Y;
        }
        /// <summary>
        /// Draw All Element
        /// </summary>
       public void Draw()
        {
            for (int i = 1; i < mapsize.X; i++)
                for (int j = 1; j <= mapsize.Y; j++)
                    Fields[i, j].StretchDraw();
        }
        /// <summary>
        /// Optimize Draw Method
        /// </summary>
       public void OptimizeDraw()
        {
            for (int i = 1; i < mapsize.X; i++)
                for (int j = 1; j <= mapsize.Y; j++)
                    Helpers.DrawOptimizer.StretchDrawElement(Fields[i, j]);
        }
      

        #endregion
        #region Additional Function (AdjusteMap)
        /// <summary>
        /// Adjust The Map
        /// </summary>
       public void AdjusteMap()
        {

            Vector2 c = (Fields[0, 0].Size / size);
            Fields[0, 0].Size = size;
            Fields[0, 0].Scale *= (new Vector2(1, 1) / c);
            Fields[0, 0].Position = new Vector2(Screen.X, Screen.Y);

            for (int i = 1; i < mapsize.X; i++)
                for (int j = 1; j < mapsize.Y; j++)

                    if (size != Fields[i, j].Size)
                    {
                        Fields[i, j].Size = size;
                        c = Fields[i, j].Size / size;
                        Fields[i, j].Scale *= (new Vector2(1, 1) / c);
                        Fields[0, 0].Position = new Vector2(Screen.X + (size.X * i), Screen.Y + (size.Y * j));
                    }
        }
        #endregion

    }
   
}
