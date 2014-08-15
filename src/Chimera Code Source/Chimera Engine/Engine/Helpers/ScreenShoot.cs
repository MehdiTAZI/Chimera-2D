using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Chimera.Helpers
{
    #if !XBOX
    /// <summary>
    /// This Class Allow You To Take ScreenShoots
    /// </summary>
    public static class ScreenShoot
    {
        private static GraphicsDevice gd;
        /// <summary>
        /// Set Or Get The XNA GraphicsDevice Reference
        /// </summary>
        public static GraphicsDevice GraphicsDevice
        {
            get { return gd; }
            set { gd = value;}
        }
        /// <summary>
        /// Take A ScreenShoot Of Current Screen
        /// </summary>
        /// <param name="path">Picture Path</param>
        /// <param name="format">Picture Format</param>
        /// <param name="graphicsDevice">XNA GraphicsDevice</param>

        public static void TakeScreen(string path, ImageFileFormat format,GraphicsDevice graphicsDevice)
        {
            try
            {
                ResolveTexture2D text;
                text = new ResolveTexture2D(graphicsDevice, graphicsDevice.PresentationParameters.BackBufferWidth, graphicsDevice.PresentationParameters.BackBufferHeight, 1, graphicsDevice.PresentationParameters.BackBufferFormat);
                graphicsDevice.ResolveBackBuffer(text);
                text.Save(path, format);
            }
            catch (System.Exception e)
            {
                throw e;
            }

        }

        /// <summary>
        /// Take A ScreenShoot Of Current Screen
        /// </summary>
        /// <param name="path">Picture Path</param>
        /// <param name="format">Picture Format</param>
        
        public static void TakeScreen(string path, ImageFileFormat format)
        {
            TakeScreen(path, format, gd);
        }
        
        
    }
#endif
}
