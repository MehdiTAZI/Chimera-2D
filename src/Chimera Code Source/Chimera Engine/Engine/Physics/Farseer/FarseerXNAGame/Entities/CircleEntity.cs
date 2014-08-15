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
    public class CircleEntity : PolygonEntityBase<CircleRigidBody> , IEntity {
        public CircleEntity(float radius) {
            CircleEntityConstructor(radius, 40, 1);
        }

        public CircleEntity(float radius, int edgeCount, float mass) {
            CircleEntityConstructor(radius,edgeCount,mass);      
        }

        private void CircleEntityConstructor(float radius, int edgeCount, float mass){
            _rigidBody = new CircleRigidBody(radius,edgeCount, mass);
        }      
    }
}
