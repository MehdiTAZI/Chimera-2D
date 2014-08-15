using System;
using System.Collections.Generic;
using System.Text;

namespace Chimera.Physics.Farseer.FarseerGames.FarseerXNAPhysics.Dynamics {
    internal class RigidBodyList : List<RigidBody> {
        internal static bool IsDisposed(RigidBody a) {
            return a.IsDisposed;
        }
    }
}
