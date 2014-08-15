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

namespace Chimera.Graphics.Effects.Particles.Engine.Modifiers
{
    public sealed class RandomScaleModifier : Modifier
    {
        #region [ Private Fields ]

        private Random _rnd;
        private float _min;
        private float _max;

        #endregion

        #region [ Public Interface ]

        /// <summary>
        /// Gets or sets the minimum scale to give new Particles.
        /// </summary>
        public float Min
        {
            get { return _min; }
            set { _min = (float)Math.Max(value, 0f); }
        }

        /// <summary>
        /// Gets or sets the maximum scale to give new Particles.
        /// </summary>
        public float Max
        {
            get { return _max; }
            set { _max = (float)Math.Max(value, 0f); }
        }

        #endregion

        #region [ Constructors & Methods ]

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="min">Minimum scale of new Particles.</param>
        /// <param name="max">Maximum scale of new Particles.</param>
        public RandomScaleModifier(float min, float max)
        {
            _rnd = new Random();
            _min = min;
            _max = max;
        }

        /// <summary>
        /// Modifies a single Particle.
        /// </summary>
        /// <param name="time">Game timing information.</param>
        /// <param name="particle">Particle to be modified.</param>
        public override void ProcessNewParticle(GameTime time, Particle particle)
        {
            particle.Scale = ((float)_rnd.NextDouble() * (_max - _min)) + _min;
        }

        #endregion
    }
}