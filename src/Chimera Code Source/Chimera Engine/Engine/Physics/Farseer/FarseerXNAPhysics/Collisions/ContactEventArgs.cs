using System;
using System.Collections.Generic;
using System.Text;

using Chimera.Physics.Farseer.FarseerGames.FarseerXNAPhysics.Dynamics;

namespace Chimera.Physics.Farseer.FarseerGames.FarseerXNAPhysics.Collisions {
    public class ContactEventArgs : EventArgs  {
        private RigidBody _contactedRigidBody=null;
        private float _seperation=0;

        public ContactEventArgs(Contact contact) {
            
        }

        
    }
}
