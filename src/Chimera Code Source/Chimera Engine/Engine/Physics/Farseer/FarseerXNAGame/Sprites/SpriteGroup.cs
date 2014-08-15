using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Chimera.Physics.Farseer.FarseerGames.FarseerXNAGame.Scene;

namespace Chimera.Physics.Farseer.FarseerGames.FarseerXNAGame.Sprites {
    public class SpriteGroup : SceneObject, ILoadable {
        public void LoadGraphicsContent(GraphicsDevice graphicsDevice, Microsoft.Xna.Framework.Content.ContentManager contentManager) {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
