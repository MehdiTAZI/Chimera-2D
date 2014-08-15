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
    public class PolygonEntity : PolygonEntityBase<PolygonRigidBody>, IEntity {
        public PolygonEntity(Vertices vertices) {
            PolygonEntityContstrutor(vertices, 1);
        }

        public PolygonEntity(Vertices vertices,float mass) {
            PolygonEntityContstrutor(vertices, mass);
        }

        private void PolygonEntityContstrutor(Vertices vertices, float mass) {
            _rigidBody = new PolygonRigidBody(mass, vertices);
        }
    }
}
