using System;
using System.Collections.Generic;
using System.Text;

using Chimera.Physics.Farseer.FarseerGames.FarseerXNAPhysics;

namespace Chimera.Physics.Farseer.FarseerGames.FarseerXNAGame.Entities {
    public class EntityManager : List<IEntity> {
        public void Update() {
        
        }

        public void LoadToPhysicsSimulator(PhysicsSimulator physicsSimulator) {
            foreach (IEntity entity in this) {
                entity.LoadToPhysicsSimulator(physicsSimulator);
            }
        }
    }
}
