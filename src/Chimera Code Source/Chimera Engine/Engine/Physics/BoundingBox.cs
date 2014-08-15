#region Info/Author
//----------------------------------------------------------------------------
// Author:    Tazi Mehdi
// Source:    Chimera 2D GAMES ENGINE
// Info:      BoundingBox_
//-----------------------------------------------------------------------------
#endregion
#region Using Statement
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace Chimera.Physics
{
    /// <summary>
    /// This Class Allow You To Use The BoundingBox Collision
    /// </summary>
    public class BoundingBox : Chimera.Helpers.Interface.IUpdateable
    {
        #region Fields
        private Microsoft.Xna.Framework.BoundingBox _boundingbox;
        private Graphics.Image img;
        #endregion
        #region Properties
       /// <summary>
       /// Return The BoundingBox Reference
       /// </summary>
        public Microsoft.Xna.Framework.BoundingBox Boundingbox
        {
            get { return this._boundingbox; }
        }
        #endregion
        #region Main Methods
       /// <summary>
       /// Initialize The System
       /// </summary>
       /// <param name="img"></param>
        public void Initialize(Graphics.Image img)
        {
            this.img = img;
            this._boundingbox = new Microsoft.Xna.Framework.BoundingBox(new Vector3(img.Position.X,img.Position.Y,0),new Vector3(img.Position.X+img.Size.X,img.Position.Y+ img.Size.Y,0)  );
        }
       /// <summary>
       /// Update The Sytem
       /// </summary>
        public void Update()
        {
            this._boundingbox = new Microsoft.Xna.Framework.BoundingBox(new Vector3(img.Position.X, img.Position.Y, 0), new Vector3(img.Position.X + img.Size.X, img.Position.Y + img.Size.Y, 0));
        }
        #endregion
        #region Collision Functions
       /// <summary>
       /// Collision Test
       /// </summary>
       /// <param name="box"></param>
       /// <returns></returns>
        public bool IsCollid(Physics.BoundingBox box)
        {
            return (box.Boundingbox.Intersects(this._boundingbox));
        }
       /// <summary>
       /// Collision Test
       /// </summary>
       /// <param name="sphere"></param>
       /// <returns></returns>
        public bool IsCollid(Physics.BoundingSphere sphere)
        {
            return (sphere.Boundingsphere.Intersects(this._boundingbox)) ;
        }
        #endregion
    }

}
