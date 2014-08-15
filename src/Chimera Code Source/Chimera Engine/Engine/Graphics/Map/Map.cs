#region Info/Author
//----------------------------------------------------------------------------
// Author:    Tazi Mehdi
// Source:    Chimera 2D GAMES ENGINE
// Info:      Completed Map
//-----------------------------------------------------------------------------
#endregion
#region Using Statement
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace Chimera.Graphics.Map
{
    /// <summary>
    /// This Class Allow You Create Map And Move Throut It
    /// </summary>
    public class Map : Helpers.Interface.IDrawable
    {
        private Rectangle scr;
        private SpriteBatch sprite;
        private Vector2 titlesize;
        private Vector2 currentpos;
        private Texture2D[] texture;
        private Color color;
        private byte[,] fields;

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="MaxTexture">Max Textures To Use</param>
        public Map(int MaxTexture)
        {
            texture = new Texture2D[MaxTexture];
            titlesize = new Vector2(64, 64);
            scr = new Rectangle(0,0,800, 600);
            color = Color.White;
        }
        /// <summary>
        /// Get Or Set The Current Position  of Map Camera 
        /// </summary>
        public Vector2 CurrentPosition
        {
            get { return currentpos; }
            set
            {
                if (value.X > 0 && value.Y > 0 && value.X < fields.GetLength(0) && value.Y < fields.GetLength(1))
                    currentpos = value;
            }
        }
        /// <summary>
        /// Get Or Set Map Fields
        /// </summary>
        public byte[,] Fields
        {
            get {return fields ;}
            set {fields = value ;}
        }
        /// <summary>
        /// Get Or Set Fields Color
        /// </summary>
        public Color Color
        {
            get {return color ;}
            set {color = value ;}
        }
        /// <summary>
        /// Get Or Set Titles Size
        /// </summary>
        public Vector2 TitlesSize
        {
            get {return titlesize ;}
            set {titlesize=value ;}
        }
        /// <summary>
        /// Get Or Set Map Screen
        /// </summary>
        public Rectangle Screen
        {
            get {return scr ;}
            set {scr = value ;}
        }
        /// <summary>
        /// Initialize The Map
        /// </summary>
        /// <param name="sprite">XNA SpriteBatch Reference</param>
        public void Initialize(SpriteBatch sprite)
        {
            this.sprite = sprite;
        }
        /// <summary>
        /// Matche The Texture With The Byte
        /// </summary>
        /// <param name="_Byte">Byte To Be Matched</param>
        /// <param name="_texture">Texture To Matche With The Byte</param>
        public void SetMapTexture(int _Byte,Texture2D _texture)
        {
            texture[_Byte] = _texture;
        }
        /// <summary>
        /// Move To The Position
        /// </summary>
        /// <param name="pos">Position Where The Map Camera Will Be Moved</param>
        public void MoveTo(Vector2 pos)
        {
            if (pos.X>0 && pos.Y>0 && pos.X<fields.GetLength(0) && pos.Y < fields.GetLength(1))
            currentpos = pos;
        }
        /// <summary>
        /// Move Up The Current Map Camera
        /// </summary>
        public void MoveUp()
        {
            float x = currentpos.X-1;
            if (x<0) x=0;
            currentpos = new Vector2(x, currentpos.Y);
        }
        /// <summary>
        /// Move Down The Current Map Camera
        /// </summary>
        public void MoveDown()
        {
            float x = currentpos.X + 1;
            if (x >= fields.GetLength(0)) x = fields.GetLength(0)-1;
            currentpos = new Vector2(x, currentpos.Y);
        }
        /// <summary>
        /// Move Left The Current Map Camera
        /// </summary>
        public void MoveLeft()
        {
            float y = currentpos.Y - 1;
            if (y < 0) y = 0;
            currentpos = new Vector2(currentpos.X, y);
        }
        /// <summary>
        /// Move Right The Current Map Camera
        /// </summary>
        public void MoveRight()
        {
            float y = currentpos.Y + 1;
            if (y >= fields.GetLength(1)) y = fields.GetLength(1)-1;
            currentpos = new Vector2(currentpos.X, y);
        }
        /// <summary>
        /// Check For Potential Collision
        /// </summary>
        /// <param name="pos">field position</param>
        /// <returns>return true if the any item collid with the current field in the position</returns>
        public bool IsCollid(Vector2 pos)
        {
            if (GetTitle(pos) != -1)
                return true;

            return false;
        }
        /// <summary>
        /// Get The Map Title in the map position
        /// </summary>
        /// <param name="pos">Map Position</param>
        /// <returns>return map field</returns>
        public int GetTitle(Vector2 pos)
        {
            Vector2 position = currentpos + (pos/titlesize);
            try
            {
                return fields[(int)position.X, (int)position.Y];
            }
            catch
            {
                return -1;
            }
        }
        /// <summary>
        /// Load Map
        /// </summary>
        /// <param name="Fields">Map Fields</param>
        public void Load(byte[,] Fields)
        {
            fields = Fields;
        }
        /*
        public void Load(string FileName)
        {

        }
        public void Save(string FileName)
        {

        }
         * */
        /// <summary>
        /// Draw The Map
        /// </summary>
        public void Draw()
        {
            int curx = (int)currentpos.X;
            int cury = (int)currentpos.Y;

            int recx = (int)(scr.Width / titlesize.X)+curx;
            int recy = (int)(scr.Height / titlesize.Y)+cury;
            if (recx > fields.GetLength(0))
                recx = fields.GetLength(0);

            if (recy > fields.GetLength(1))
                recy = fields.GetLength(1);

            for (int i = 0; i <= recx; i++)
            {
                for (int j = 0; j <= recy; j++)
                {
                    int x=(int)(i * titlesize.X)+scr.X;
                    int y=(int)(j * titlesize.Y)+scr.Y;
                    Rectangle rec = new Rectangle(y,x,(int)(titlesize.Y) ,(int)(titlesize.X));
                    if (x < scr.Width && y < scr.Height && (curx+i) < fields.GetLength(0) && (cury+j) < fields.GetLength(1))
                    sprite.Draw(texture[fields[i+curx, j+cury]], rec, color);
                }
            }
        }
    }
}