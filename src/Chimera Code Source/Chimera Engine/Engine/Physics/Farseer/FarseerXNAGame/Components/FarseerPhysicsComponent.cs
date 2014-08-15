
#region Using Statements
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;

using Chimera.Physics.Farseer.FarseerGames.FarseerXNAPhysics;
#endregion

namespace Chimera.Physics.Farseer.FarseerGames.FarseerXNAGame.Components {
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public partial class FarseerPhysicsComponent : Microsoft.Xna.Framework.GameComponent {
        public PhysicsSimulator _physicsSimulator;

        public FarseerPhysicsComponent(Game game)
            : base(game) {
            _physicsSimulator = new PhysicsSimulator(new Vector2(0,0));
            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize() {
            Game.Services.AddService(typeof(PhysicsSimulator), _physicsSimulator);
            base.Initialize();
        }

        public PhysicsSimulator PhysicsSimulator {
            get { return _physicsSimulator; }
        }

        public Vector2 Gravity {
            get { return _physicsSimulator.Gravity; }
            set { _physicsSimulator.Gravity = value; }
        }

        public int IterationsPerCollision {
            get { return _physicsSimulator.Iterations; }
            set { _physicsSimulator.Iterations = value; }
        }

        public float AllowedCollisionPenetration {
            get { return _physicsSimulator.AllowedPenetration; }
            set { _physicsSimulator.AllowedPenetration= value; }
        }

        public float ImpulseBiasFactor {
            get { return _physicsSimulator.BiasFactor; }
            set { _physicsSimulator.BiasFactor = value; }
        }

        /// <summary>
        /// Allows the game component to Update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your Update code here
            _physicsSimulator.Update(gameTime.ElapsedGameTime.Milliseconds * .001f);
            base.Update(gameTime);
        }
    }
}


