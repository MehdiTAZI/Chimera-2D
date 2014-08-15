#region Info/Author
//----------------------------------------------------------------------------
// Author:    Tazi Mehdi
// Source:    Chimera 2D GAMES ENGINE
// Info:      Complex Animation public class : Animation + Farseer
//-----------------------------------------------------------------------------
#endregion
#region Using Statement
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Chimera.Physics.Farseer.FarseerGames.FarseerXNAPhysics;
using Chimera.Physics.Farseer.FarseerGames.FarseerXNAPhysics.Dynamics;

#endregion

namespace Chimera.Graphics
{   
    /// <summary>
    /// This Class Allow You To Use The Animation public class With More Physics Options
    /// </summary>
    public class ComplexAnimation : Animation, Helpers.Interface.IDrawable, Helpers.Interface.IDrawables
    {
        #region Fields
        private RigidBody body;
        private bool visible;
        #endregion
        #region Properties

        /// <summary>
        /// Get Or Set Visible Option
        /// </summary>
        public bool Visible
        {
            get { return visible; }
            set { visible = value; }
        }

        /// <summary>
        /// Get Or Set RigidBody Options (Mass,Gravity....)
        /// </summary>
        public RigidBody Body
        {
            get { return body; }
            set { body = value; }
        }

        /// <summary>
        /// Get Or Set Image Position
        /// </summary>
        public new Vector2 Position
        {
            get { return base.Position; }
            set
            {
                base.Position = value;
                body.Position = value;
            }
        }

        /// <summary>
        /// Get Or Set Image Size
        /// </summary>
        public new Vector2 Size
        {
            get { return Size; }
            set
            {
                Size = value;
                Initialize(value, this.body.Mass);
            }
        }

        /// <summary>
        /// Get Or Set Rotation Degre
        /// </summary>
        public new float Rotation
        {
            get { return base.Rotation; }
            set
            {
                base.Rotation = value;
                body.Orientation = value;
            }
        }
        #endregion
        /// <summary>
        /// The Default Constructor
        /// </summary>
        public ComplexAnimation()
            : base()
        {
            visible = true;
        }

        /// <summary>
        /// Initialize The Animation
        /// </summary>
        /// <param name="size">Animation Size</param>
        public override void Initialize(Vector2 size)
        {
            base.Initialize(size);
            body = new RectangleRigidBody(size.X, size.Y, 1);
            body.CollisionEnabled = true;
            body.Position = this.Position;
            body.Orientation = this.Rotation;
            Chimera.Physics.FarseerEngine.Physics.Remove(body);
            Chimera.Physics.FarseerEngine.Physics.Add(body);
            
        }

        /// <summary>
        /// Initialize The Animation
        /// </summary>
        /// <param name="size">Animation Size</param>
        /// <param name="mass">Animaion Mass</param>
        public void Initialize(Vector2 size, float mass)
        {
            base.Initialize(size);
            body = new RectangleRigidBody(size.X, size.Y, mass);
            body.CollisionEnabled = true;
            body.Position = this.Position;
            body.Orientation = this.Rotation;
            Chimera.Physics.FarseerEngine.Physics.Remove(body);
            Chimera.Physics.FarseerEngine.Physics.Add(body);
        }

        /// <summary>
        /// Update The System Before The FarserEngine Update
        /// </summary>
        
        public void BeforeEngineUpdate()
        {
             body.Position=this.Position ;
             body.Orientation=this.Rotation  ;
        }
        
        /// <summary>
        /// Draw The Animation
        /// </summary>
        
        public override void Draw()
        {   
            if(this.visible==true)
                base.Draw();
        }
        /// <summary>
        /// Draw The Animation Without Stretch Mode
        /// </summary>
        
        public override void DefaultDraw()
        {
            if (this.visible == true)
                base.DefaultDraw();
        }
        /// <summary>
        /// Draw The Animation And Stretch It
        /// </summary>
        
        public override void StretchDraw()
        {
            if (this.visible == true)
                base.StretchDraw();
        }

        /// <summary>
        /// Update The System After The FarserEngine Updated
        /// </summary>
        
        public void AfterEngineUpdate()
        {
            this.Position = body.Position;
            this.Rotation = body.Orientation;
        }

    }
}
