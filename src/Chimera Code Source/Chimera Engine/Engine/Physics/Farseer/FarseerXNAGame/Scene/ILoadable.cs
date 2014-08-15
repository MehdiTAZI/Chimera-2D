using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


namespace Chimera.Physics.Farseer.FarseerGames.FarseerXNAGame.Scene {
    interface ILoadable {
        void LoadGraphicsContent(GraphicsDevice graphicsDevice, ContentManager contentManager);        
    }
}
