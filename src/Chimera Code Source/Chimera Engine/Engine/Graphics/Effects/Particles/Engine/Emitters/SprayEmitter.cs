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
    public sealed class SprayEmitter : Emitter
    {
        #region [ Private Fields ]

        private float _direction;
        private float _spread;

        #endregion

        #region [ Public Interface ]

        /// <summary>
        /// Gets or sets the spray direction in radians.
        /// </summary>
        public float Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }

        /// <summary>
        /// Gets or sets the spray spread in radians.
        /// </summary>
        public float Spread
        {
            get { return _spread; }
            set { _spread = MathHelper.Clamp(value, 0f, MathHelper.TwoPi); }
        }

        #endregion

        #region [ Constructors & Methods ]

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="system">ParticleSystem that this emitter will add itself to.</param>
        /// <param name="budget">Number of particles available to the Emitter.</param>
        /// <param name="direction">Spray direction in radians.</param>
        /// <param name="spread">Spray spread in radians.</param>
        public SprayEmitter(ParticleSystem system, int budget, float direction, float spread)
            : base(system, budget)
        {
            _direction = direction;
            _spread = spread;
        }
        protected override void GetParticlePositionAndOrientation(Snapshot snap, ref Vector2 position, ref Vector2 orientation)
        {
            SpraySnapshot spraySnap = (SpraySnapshot)snap;

            float angle = spraySnap.Direction + ((float)Rnd.NextDouble() * spraySnap.Spread) - (spraySnap.Spread / 2f);

            orientation.X = (float)Math.Sin(angle);
            orientation.Y = (float)Math.Cos(angle);
        }

        #endregion

        #region [ Snapshot public class & Methods ]

        private class SpraySnapshot : Snapshot
        {
            private float _direction;
            private float _spread;

            public float Direction
            {
                get { return _direction; }
                set { _direction = value; }
            }
            public float Spread
            {
                get { return _spread; }
                set { _spread = MathHelper.Clamp(value, 0f, MathHelper.TwoPi); }
            }
        }

        protected override Snapshot GenerateSnapshot()
        {
            return new SpraySnapshot();
        }

        protected override void TakeSnapshot(ref Snapshot snap)
        {
            SpraySnapshot spraySnap = (SpraySnapshot)snap;

            spraySnap.Direction = _direction;
            spraySnap.Spread = _spread;
        }

        #endregion
    }
}
