#region Info/Author
//----------------------------------------------------------------------------
// Author:    Tazi Mehdi
// Source:    Chimera 2D GAMES ENGINE
// Info:      Bounding Sphere public class
//-----------------------------------------------------------------------------
#endregion
#region Using Statement
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace Chimera.Physics
{
    /// <summary>
    /// This Class Allow You To Use The BoundingSphere Collision
    /// </summary>
    public class BoundingSphere : Chimera.Helpers.Interface.IUpdateable
    {
        #region Fields
        private Microsoft.Xna.Framework.BoundingSphere _boundingsphere;
        private Graphics.Image img;
        #endregion
        #region Properties
        /// <summary>
        /// Get The XNA BoundingSphere Reference
        /// </summary>
        public Microsoft.Xna.Framework.BoundingSphere Boundingsphere
        {
            get {return this._boundingsphere ;}
        }
        #endregion
        #region Main Methods

        /// <summary>
        /// Initialize The System
        /// </summary>
        /// <param name="img">Image</param>
        public void Initialize(Graphics.Image img)
        {
            this.img = img;
            this._boundingsphere = new Microsoft.Xna.Framework.BoundingSphere(new Vector3(this.img.Position.X + (this.img.Size.X / 2), this.img.Position.Y + (this.img.Size.Y / 2), 0), this.img.Size.X / 2);
        }
        /// <summary>
        /// Update The System
        /// </summary>
        public void Update()
        {
            this._boundingsphere = new Microsoft.Xna.Framework.BoundingSphere(new Vector3(this.img.Position.X + (this.img.Size.X / 2), this.img.Position.Y + (this.img.Size.Y / 2), 0), this.img.Size.X / 2);
        }

        #endregion
        #region Collision Functions
        /// <summary>
        /// Check For A Potential Collision
        /// </summary>
        /// <param name="box">XNA BoudingBox Reference</param>
        /// <returns>Return True If The Two Image Are In Collision)</returns>
        public bool IsCollid(Physics.BoundingBox box)
       { 
            return (box.Boundingbox.Intersects(this._boundingsphere));   
       }
       /// <summary>
       /// Check For A Potential Collision
       /// </summary>
       /// <param name="sphere">XNA BoudingSphere Reference</param>
       /// <returns>Return True If The Two Image Are In Collision)</returns>
        public bool IsCollid(Physics.BoundingSphere sphere)
        {
            return (sphere.Boundingsphere.Intersects(this._boundingsphere));
        }
        #endregion
    }
}
