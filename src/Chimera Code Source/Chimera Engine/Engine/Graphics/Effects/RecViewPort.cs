#region Using Statement
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace Chimera.Graphics.Effects
{
    /// <summary>
    /// This Offer You The Possibility To Animate And Redefine The ViewPort Size And Position
    /// </summary>
    public class RecViewPort : Chimera.Helpers.Interface.IUpdateable
    {
       /// <summary>
       /// The Default Constructor
       /// </summary>
        public RecViewPort()
        {
            irec = new Rectangle(400, 300, 400, 300);
            erec = new Rectangle(0, 0, 800, 600);
            value = new Vector2(1, 1);
        }
        #region Fields
        private Rectangle irec,erec;
        private Vector2 value;
        private bool enable;
        private GraphicsDeviceManager graphics;
        private Viewport viewport,saveviewport;
        #endregion
        #region Properties
        /// <summary>
        /// Get Or Set The Initial ViewPort Rectangle
        /// </summary>
        public Rectangle StartRectangle
        {
            get { return this.irec; }
            set { this.irec = value; }
        }
        /// <summary>
        /// Get Or Set The Final ViewPort Rectangle
        /// </summary>
        public Rectangle EndRectangle
        {
            get { return this.erec; }
            set { this.erec = value; }
        }
        /// <summary>
        /// Get Or Set The Value Of Resizing The ViewPort Rectangle
        /// </summary>
        public Vector2 Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
        /// <summary>
        /// Enable Or Disable The Effect
       /// </summary>
        public bool Enable
        {
            get { return this.enable; }
            set { this.enable = value; }
        }
        #endregion
      
        #region Main Function
        /// <summary>
        /// Initialize The Effect
        /// </summary>
        /// <param name="graphics">GraphicsDeviceManager Reference</param>
        public void Initialize(GraphicsDeviceManager graphics)
        {
            this.graphics = graphics;
            this.viewport = graphics.GraphicsDevice.Viewport;
            this.saveviewport = this.viewport;
            this.Reset();
        }
        /// <summary>
        /// Restore The Default ViewPort
        /// </summary>
        public void Restore()
        {
            graphics.GraphicsDevice.Viewport = this.saveviewport;
            graphics.ApplyChanges();
        }
        /// <summary>
        /// The The Effect Parameter
        /// </summary>
        /// <param name="first_rectangle">Initial Rectangle</param>
        /// <param name="last_rectangle">Final Rectangle</param>
        public void SetEffectParameter(Rectangle first_rectangle,Rectangle last_rectangle)
        {
            this.irec = first_rectangle;
            this.erec = last_rectangle;
            this.SetEffectParameter();
        }
        /// <summary>
        /// Set The Effect Parameter
        /// </summary>
        public void SetEffectParameter()
        {
            this.viewport.X = irec.X;
            this.viewport.Y = irec.Y;
            this.viewport.Width = irec.Width;
            this.viewport.Height = irec.Height;
        }
        /// <summary>
        /// Reset The Effect
        /// </summary>
        public void Reset()
        {
            this.enable = true;
            this.SetEffectParameter();
        }
        /// <summary>
        /// Update The Effect
        /// </summary>
        public void Update()
        {
            if (enable)
            {
             
            if(viewport.X!=erec.X)
            {
                    
                    if(irec.X<erec.X)
                        viewport.X +=(int)value.X ;
                    else 
                        viewport.X -= (int)value.X;
            }
            if (viewport.Y != erec.Y)
            {

                if (irec.Y < erec.Y) 
                    viewport.Y += (int)value.X;
                else 
                    viewport.Y -= (int)value.Y;
            }
            if (viewport.Width != erec.Width)
            {
                if (irec.Width < erec.Width) 
                    viewport.Width += (int)value.X;
                else 
                    viewport.Width -= (int)value.X;
            }
            if (viewport.Height != erec.Height)
            {
                if (irec.Height < erec.Height)
                    viewport.Height += (int)value.Y;
                else 
                    viewport.Height -= (int)value.Y;
            }      
                
                    graphics.GraphicsDevice.Viewport = viewport;
                    graphics.ApplyChanges();
                
                if ((viewport.X == erec.X) && (viewport.Y == erec.Y) && (viewport.Width == erec.Width) && (viewport.Height == erec.Height)) this.enable = false;
            }
        }
        #endregion

    }
}
