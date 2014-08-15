using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using Chimera.Physics.Farseer.FarseerGames.FarseerXNAGame.Scene;
using Chimera.Physics.Farseer.FarseerGames.FarseerXNAGame.Entities;

namespace Chimera.Physics.Farseer.FarseerGames.FarseerXNAGame.Sprites {
    public class SpriteEntityView : Sprite, IEntityView {
        private IEntity _entity;

        public SpriteEntityView(IEntity entity, string textureAssetName)
            : base(textureAssetName, 0) {
            _entity = entity;
        }

        public SpriteEntityView(IEntity entity, string textureAssetName, float layer) : base(textureAssetName,layer) {
            _entity = entity;           
        }

        public void Update() {
            this.Position =  _entity.Position;
            this.Orientation = _entity.Orientation;
        }
    }


}
