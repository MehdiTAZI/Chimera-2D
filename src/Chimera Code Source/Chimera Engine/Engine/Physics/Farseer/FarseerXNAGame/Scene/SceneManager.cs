using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using Chimera.Physics.Farseer.FarseerGames.FarseerXNAGame.Scene;

namespace Chimera.Physics.Farseer.FarseerGames.FarseerXNAGame.Scene {
    public class SceneManager : List<SceneObject> {

        public virtual void Draw(GraphicsDevice graphicsDevice) {
            FarseerGame.FarseerSpriteBatch.Begin(SpriteBlendMode.AlphaBlend,SpriteSortMode.BackToFront,SaveStateMode.None);
            foreach (SceneObject sceneObject in this) {
                sceneObject.Draw(graphicsDevice);
            }
            FarseerGame.FarseerSpriteBatch.End();
        }

        public virtual void LoadGraphicsContent(GraphicsDevice graphicsDevice, ContentManager contentManager) {
            foreach (SceneObject sceneObject in this) {
                if (sceneObject is ILoadable) {
                    ((ILoadable)sceneObject).LoadGraphicsContent(graphicsDevice, contentManager);
                }
            }
        }
    }
}
