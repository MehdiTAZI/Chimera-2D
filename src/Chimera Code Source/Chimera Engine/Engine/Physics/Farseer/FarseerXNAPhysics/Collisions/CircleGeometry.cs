using System;
using System.Collections.Generic;
using System.Text;

namespace Chimera.Physics.Farseer.FarseerGames.FarseerXNAPhysics.Collisions {
    public class CircleGeometry : Geometry {
        public CircleGeometry(float radius, int edgeCount)
            : base() {
            Initialize(radius, edgeCount);
        }

        private void Initialize(float radius, int edgeCount) {
            Vertices vertices = Vertices.CreateCircle(radius, edgeCount);
            SetVertices(vertices);
        }
    }
}
