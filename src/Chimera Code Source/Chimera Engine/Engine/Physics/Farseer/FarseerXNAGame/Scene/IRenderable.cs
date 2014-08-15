using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Chimera.Physics.Farseer.FarseerGames.FarseerXNAGame.Scene {
    interface IRenderable  {
        void Render(GraphicsDevice graphicsDevice);
    }
}
