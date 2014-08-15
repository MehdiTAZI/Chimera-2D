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
    public class PolygonEntityBase<T> where T : RigidBody {
        protected T _rigidBody;

        public T RigidBody {
            get { return _rigidBody; }
        }

        public Vector2 Position {
            get { return _rigidBody.Position; }
            set { _rigidBody.Position = value; }
        }

        public float Orientation {
            get { return _rigidBody.Orientation; }
            set { _rigidBody.Orientation = value; }
        }

        public float Mass {
            get { return _rigidBody.Mass; }
            set { _rigidBody.Mass = Mass; }
        }

        public float RotationalDragCoefficient {
            get { return _rigidBody.RotationalDragCoefficient; }
            set { _rigidBody.RotationalDragCoefficient = value; }
        }

        public float LinearDragCoefficient {
            get { return _rigidBody.LinearDragCoefficient; }
            set { _rigidBody.LinearDragCoefficient = value; }
        }

        public float FrictionCoefficient {
            get { return _rigidBody.FrictionCoefficient; }
            set { _rigidBody.FrictionCoefficient = value; }
        }

        public float RestitutionCoefficient {
            get { return _rigidBody.RestitutionCoefficient; }
            set { _rigidBody.RestitutionCoefficient = value; }
        }

        public void LoadToPhysicsSimulator(PhysicsSimulator physicsSimulator) {
            physicsSimulator.Add(_rigidBody);
        }

        public void Update() { }

        protected bool isDisposed = false;
        public bool IsDisposed {
            get { return isDisposed; }
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {
            //subpublic classes can override incase they need to dispose of resources
            //otherwise do nothing.
            if (!isDisposed) {
                if (disposing) {
                    _rigidBody.Dispose();
                }
                isDisposed = true;
            }
        }
    }
}
