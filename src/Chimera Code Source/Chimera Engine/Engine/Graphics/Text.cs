#region Info/Author
//----------------------------------------------------------------------------
// Author:    Tazi Mehdi
// Source:    Chimera 2D GAMES ENGINE
// Info:      Text public class
//-----------------------------------------------------------------------------
#endregion
#region Using Statement
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace Chimera.Graphics
{
    /// <summary>
    /// This Class Offer You The Possibility To Write A Text
    /// </summary>
    public class TextWriter : Helpers.Interface.IDrawable 
    {
        #region Fields(spritebatch,spritefont,color,text,origin)
        private SpriteBatch spriteBatch;
        private SpriteFont spriteFont;
        private Color color;
        private string text;
        private Vector2 origin;
        private SpriteEffects effects;
        private Vector2 scale;
        private float rotation;
        private float depth;
        private Vector2 position;
        #endregion
        #region Properties(Position,origin,Rotation,Color,Scale)

       /// <summary>
       /// Get Or Set The Current Text
       /// </summary>
        public string Text
        {
            get {return text ;}
            set { text = value;}
        }

       /// <summary>
       /// Get Or Set The Text Position
       /// </summary>
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

       /// <summary>
       /// Get Or Set The Text Origin
       /// </summary>
        public Vector2 Origin
        {
            get { return origin; }
            set { origin = value; }
        }
       /// <summary>
       /// Get Or Set The Text Rotation
       /// </summary>
        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }
       /// <summary>
       /// Get Or Set The Text Depth
       /// </summary>
        public float Depth
        {
            get { return depth; }
            set { depth = value; }
        }
       /// <summary>
       /// Get Or Set The Text Color
       /// </summary>
        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

       /// <summary>
       /// Get Or Set The Text Scaling Valye
       /// </summary>
        public Vector2 Scale
        {
            get { return scale; }
            set { scale = value; }
        }
        #endregion
        #region Main Methods (LoadGraphicsContent,Initialize,Draw)
       /// <summary>
       /// Load Graphics Content
       /// </summary>
       /// <param name="spriteBatch">SpriteBatch</param>
        /// <param name="spriteFont">Font</param>
        public void LoadGraphicsContent(SpriteBatch spriteBatch, SpriteFont spriteFont)
        {
            this.spriteBatch = spriteBatch;
            this.spriteFont = spriteFont;
        }
       /// <summary>
       /// Initialize The Text
       /// </summary>
       /// <param name="position">Text Position</param>
        public void Initialize(Vector2 position)
        {
            this.color = Color.White;
            this.text = "";
            this.position = position;
            this.effects = SpriteEffects.None;
            this.scale = new Vector2(1, 1);
            this.rotation = 0f;
            this.depth = 0.5f;
        }
       /// <summary>
       /// Draw The Text
       /// </summary>   
        public void Draw()
        {
            spriteBatch.DrawString(this.spriteFont, this.Text, this.position, this.color, this.rotation, this.origin, this.scale, this.effects, this.depth);
        }
        #endregion
        #region Additional Functions()
       /// <summary>
       /// Calculat Text Origin
       /// </summary>
       /// <returns>Return The Text Origin</returns>
        public Vector2 CalculatOrigin()
        {
            Vector2 vect;
            vect = spriteFont.MeasureString(this.text) / 2;
            return vect;
        }
        /// <summary>
        /// Set The Text Origin
        /// </summary>
        public void SetOrigin()
        {
            this.origin = CalculatOrigin();
        }
        #endregion
    }
}
