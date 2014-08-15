using System;
using System.Collections.Generic;
using System.Text;

namespace Chimera.Physics.Farseer.FarseerGames.FarseerXNAPhysics.Dynamics {
    internal class BodyList : List<Body> {
        internal static bool IsDisposed(Body a) {
            return a.IsDisposed;
        }
    }
}
