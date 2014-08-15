//Mercury Particle Engine 2.0 Copyright (C) 2007 Matthew Davey

//This library is free software; you can redistribute it and/or modify it under the terms of
//the GNU Lesser General Public License as published by the Free Software Foundation; either
//version 2.1 of the License, or (at your option) any later version.

//This library is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
//without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
//See the GNU Lesser General Public License for more details.

//You should have received a copy of the GNU Lesser General Public License along with this
//library; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330,
//Boston, MA 02111-1307 USA 

using System;
using Microsoft.Xna.Framework;

namespace Chimera.Graphics.Effects.Particles.Engine.Emitters
{
    public sealed class CircleEmitter : Emitter
    {
        #region [ Private Fields ]

        private float _radius;
        private bool _ring;

        #endregion

        #region [ Public Interface ]

        /// <summary>
        /// Gets or sets the radius of the CircleEmitter.
        /// </summary>
        public float Radius
        {
            get { return _radius; }
            set { _radius = Math.Max(value, 0f); }
        }

        /// <summary>
        /// Gets or sets wether or not the CircleEmitter is a ring.
        /// </summary>
        public bool Ring
        {
            get { return _ring; }
            set { _ring = value; }
        }

        #endregion

        #region [ Constructors & Methods ]

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="system">ParticleSystem that this Emitter will add itself to.</param>
        /// <param name="budget">Number of Particles available to this Emitter.</param>
        /// <param name="radius">Radius of the CircleEmitter.</param>
        /// <param name="ring">True if the CircleEmitter is a ring, else false.</param>
        public CircleEmitter(ParticleSystem system, int budget, float radius, bool ring)
            : base(system, budget)
        {
            _radius = radius;
            _ring = ring;
        }

        /// <summary>
        /// Processes a Particle to give it its initial position and orientation.
        /// </summary>
        /// <param name="snap">Snapshot taken when the Particle was discharged.</param>
        /// <param name="position">The Particles offset from the position of the Emitter.</param>
        /// <param name="orientation">The particles orientation (unit vector).</param>
        protected override void GetParticlePositionAndOrientation(Snapshot snap, ref Vector2 position, ref Vector2 orientation)
        {
            CircleSnapshot circleSnap = (CircleSnapshot)snap;

            float angle = (float)Rnd.NextDouble() * MathHelper.TwoPi;

            orientation.X = (float)Math.Sin(angle);
            orientation.Y = (float)Math.Cos(angle);

            float distance = circleSnap.Radius;
            if (!circleSnap.Ring) { distance *= (float)Rnd.NextDouble(); }

            position.X = (float)Math.Sin(angle) * distance;
            position.Y = (float)Math.Cos(angle) * distance;
        }

        #endregion

        #region [ Snapshot public class & Methods ]

        /// <summary>
        /// Snapshot public class for CircleEmitter.
        /// </summary>
        private class CircleSnapshot : Snapshot
        {
            private float _radius;
            private bool _ring;

            public float Radius
            {
                get { return _radius; }
                set { _radius = value; }
            }
            public bool Ring
            {
                get { return _ring; }
                set { _ring = value; }
            }
        }

        /// <summary>
        /// Generates a CircleSnapshot object.
        /// </summary>
        /// <returns></returns>
        protected override Emitter.Snapshot GenerateSnapshot()
        {
            return new CircleSnapshot();
        }

        /// <summary>
        /// Takes a snapshot of the Emitters state.
        /// </summary>
        /// <param name="snap"></param>
        protected override void TakeSnapshot(ref Snapshot snap)
        {
            base.TakeSnapshot(ref snap);

            CircleSnapshot circleSnap = (CircleSnapshot)snap;

            circleSnap.Radius = _radius;
            circleSnap.Ring = _ring;
        }

        #endregion
    }
}
