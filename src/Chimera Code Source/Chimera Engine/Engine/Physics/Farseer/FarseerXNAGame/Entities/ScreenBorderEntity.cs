using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;

using Chimera.Physics.Farseer.FarseerGames.FarseerXNAPhysics;
using Chimera.Physics.Farseer.FarseerGames.FarseerXNAPhysics.Dynamics;

namespace Chimera.Physics.Farseer.FarseerGames.FarseerXNAGame.Entities {
    public class ScreenBorderEntity : IEntity {
        public RectangleRigidBody LeftBorder;
        public RectangleRigidBody RightBorder;
        public RectangleRigidBody TopBorder;
        public RectangleRigidBody BottomBorder;
        public float borderThickness = 1;

        public ScreenBorderEntity(float screenWidth, float screenHeight) {
            screenWidth = ConvertUnits.ToMeters(screenWidth);
            screenHeight = ConvertUnits.ToMeters(screenHeight);

            LeftBorder = new RectangleRigidBody(borderThickness, screenHeight, 1);
            LeftBorder.Position = new Vector2(-borderThickness/2,screenHeight/2);
            LeftBorder.FrictionCoefficient = .5f;
            LeftBorder.IsStatic = true;
            LeftBorder.RestitutionCoefficient = 0;

            RightBorder = new RectangleRigidBody(borderThickness, screenHeight, 1);
            RightBorder.Position = new Vector2(screenWidth + borderThickness / 2, screenHeight / 2);
            RightBorder.FrictionCoefficient = .5f;
            RightBorder.IsStatic = true;
            RightBorder.RestitutionCoefficient = 0;

            TopBorder = new RectangleRigidBody(screenWidth, borderThickness, 1);
            TopBorder.Position = new Vector2(screenWidth / 2, -borderThickness / 2);
            TopBorder.FrictionCoefficient = .5f;
            TopBorder.IsStatic = true;
            TopBorder.RestitutionCoefficient = 0;

            BottomBorder = new RectangleRigidBody(screenWidth, borderThickness, 1);
            BottomBorder.Position = new Vector2(screenWidth / 2, ConvertUnits.ToMeters(-2) + screenHeight + borderThickness / 2);
            BottomBorder.FrictionCoefficient = .5f;
            BottomBorder.IsStatic = true;
            BottomBorder.RestitutionCoefficient = 0;
        }

        public Vector2 Position {
            get { return new Vector2(0,0); }
            set { }
        }

        public float Orientation {
            get { return 0; }
            set { }
        }

        public void Update() { }

        public void LoadToPhysicsSimulator(PhysicsSimulator physicsSimulator) {
            physicsSimulator.Add(LeftBorder);
            physicsSimulator.Add(RightBorder);
            physicsSimulator.Add(TopBorder);
            physicsSimulator.Add(BottomBorder);
        }

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
                    LeftBorder.Dispose();
                    RightBorder.Dispose();
                    TopBorder.Dispose();
                    BottomBorder.Dispose();
                }
                isDisposed = true;
            }
        }
    }
}
