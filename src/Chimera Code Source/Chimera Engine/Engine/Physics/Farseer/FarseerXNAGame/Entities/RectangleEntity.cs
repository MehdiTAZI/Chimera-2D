using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Chimera.Physics.Farseer.FarseerGames.FarseerXNAGame.Entities;
using Chimera.Physics.Farseer.FarseerGames.FarseerXNAGame.Sprites;

using Chimera.Physics.Farseer.FarseerGames.FarseerXNAPhysics;
using Chimera.Physics.Farseer.FarseerGames.FarseerXNAPhysics.Collisions;
using Chimera.Physics.Farseer.FarseerGames.FarseerXNAPhysics.Dynamics;

namespace Chimera.Physics.Farseer.FarseerGames.FarseerXNAGame.Entities {
    public class RectangleEntity : PolygonEntityBase<RectangleRigidBody> , IEntity {
        public RectangleEntity(float width, float height) {
            RectangleEntityConstructor(width, height, 1);
        }

        public RectangleEntity(float width, float height, float mass) {
            RectangleEntityConstructor(width, height, mass);      
        }

        private void RectangleEntityConstructor(float width, float height, float mass){
            _rigidBody = new RectangleRigidBody(width, height, mass);
        }      
    }
}
