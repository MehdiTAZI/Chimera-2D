using System;
using System.Collections.Generic;
using System.Text;

namespace Chimera.Helpers
{
    /// <summary>
    /// This Class Give You Information About GraphicsDevice Capabilites
    /// </summary>
    public static class GraphicsDeviceCapabilities
    {
        private static Microsoft.Xna.Framework.Graphics.GraphicsDeviceCapabilities cap;
        /// <summary>
        /// Initialize Or Set The The public class
        /// </summary>
        /// <param name="graphicdevice">Xna Graphics Device Reference</param>
        public static void ApplyGraphicsDevice(Microsoft.Xna.Framework.Graphics.GraphicsDevice graphicdevice)
        {
            cap = graphicdevice.GraphicsDeviceCapabilities;
        }
        /// <summary>
        /// Get The Capabilites Of The GraphicsDevice
        /// </summary>
        /// <returns>return the capabalities</returns>
        public static Microsoft.Xna.Framework.Graphics.GraphicsDeviceCapabilities GetCapabilities()
        {   
            return cap;
        }
    }
}
