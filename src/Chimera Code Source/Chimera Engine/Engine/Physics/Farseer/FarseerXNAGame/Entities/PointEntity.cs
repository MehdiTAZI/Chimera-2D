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
    public class PointEntity : PolygonEntityBase<PointRigidBody>, IEntity {
        public PointEntity() {
            PointEntityConstructor(1);
        }
        
        public PointEntity(float mass) {
            PointEntityConstructor(mass);
        }       

        private void PointEntityConstructor(float mass){
            _rigidBody = new PointRigidBody(mass);
        }
    }
}
