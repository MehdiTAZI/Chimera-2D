using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Chimera.Graphics.Effects
{
    /// <summary>
    /// This Class Allow You Using The Motion Blur Effect
    /// </summary>
    public static class MotionBlur
    {
        #region Fields
        private static byte lvl;
        private static ResolveTexture2D texture;
        private static SpriteBatch _sprite;
        private static GraphicsDeviceManager _graphics;
        private static Vector2 scale;
        #endregion
        #region Properties
        /// <summary>
        /// Set Or Get Motion Blur Level 
        /// </summary>
        public static byte Level
        {
            get {return (byte)(lvl - 80);}
            set {
                
                if (value+80 > 250)
                    lvl = 250;
                else
                    lvl = (byte)(value+80);
            }
        }
        /// <summary>
        /// Get Of Set Scaling Value
        /// </summary>
        public static Vector2 Scale
        {
            get {return scale ;}
            set {scale = value ;}
        }
        #endregion
        #region Functions

        
        private static void _graphics_event_changing(object obj, PreparingDeviceSettingsEventArgs e)
        {
            e.GraphicsDeviceInformation.PresentationParameters.RenderTargetUsage = RenderTargetUsage.PreserveContents;
        }
        /// <summary>
        /// Initialize The Effect
        /// </summary>
        /// <param name="graphics">XNA Graphics Device Reference</param>
        /// <param name="sprite">XNA Sprite Batch Reference</param>
        public static void Initialize(GraphicsDeviceManager graphics,SpriteBatch sprite)
        {
            _sprite = sprite;
            scale = new Vector2(1);
            _graphics = graphics;

            lvl = 240;

                if (_graphics != null)
                {                    
                    _graphics.PreparingDeviceSettings += new System.EventHandler<PreparingDeviceSettingsEventArgs>(_graphics_event_changing);
                    _graphics.PreferMultiSampling = true;
                    _graphics.ApplyChanges();
                    texture = new ResolveTexture2D(graphics.GraphicsDevice,
                        _graphics.GraphicsDevice.PresentationParameters.BackBufferWidth,
                        _graphics.GraphicsDevice.PresentationParameters.BackBufferHeight,1,
                        _graphics.GraphicsDevice.PresentationParameters.BackBufferFormat);
                     _graphics.GraphicsDevice.Clear(Color.Black);
                      
                      
                    _graphics.GraphicsDevice.ResolveBackBuffer(texture);
                    
                }
            
        }
    
        /// <summary>
        /// Draw The Effect
        /// </summary>
        public static void Draw()
        {  
              _sprite.Draw(texture, new Rectangle(0, 0, (int)(texture.Width * scale.X), (int)(texture.Height * scale.Y)), new Color(255,255,255,lvl));
        }

        /// <summary>
        /// UpDate The Effect
        /// </summary>
        /// <param name="color">Clearing Color</param>
        public static void Update(Color color)
        {
                if (_graphics != null)
                {
                    _graphics.GraphicsDevice.ResolveBackBuffer(texture);
                    _graphics.GraphicsDevice.Clear(color);
                    
                }
        }
        /// <summary>
        /// Update The Effect
        /// </summary>
        public static void Update()
        {
                if (_graphics != null)
                    _graphics.GraphicsDevice.ResolveBackBuffer(texture);//XNA 2 Recupere la texture et efface lecran !

        }
        #endregion

      }
}
