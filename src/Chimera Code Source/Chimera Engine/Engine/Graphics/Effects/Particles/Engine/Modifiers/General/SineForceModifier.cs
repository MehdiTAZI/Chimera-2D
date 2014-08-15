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
using Chimera.Graphics.Effects.Particles.Engine.Emitters;

namespace Chimera.Graphics.Effects.Particles.Engine.Modifiers
{
    public sealed class SineForceModifier : Modifier
    {
        #region [ Private Fields ]

        private float _freq;
        private float _amp;

        #endregion

        #region [ Public Interface ]

        /// <summary>
        /// Gets or sets the frequency of the sine wave.
        /// </summary>
        public float Frequency
        {
            get { return _freq; }
            set { _freq = Math.Max(value, 0f); }
        }

        /// <summary>
        /// Gets or sets the amplitude of the sine wave.
        /// </summary>
        public float Amplitude
        {
            get { return _amp; }
            set { _amp = Math.Max(value, 0f); }
        }

        #endregion

        #region [ Constructors & Methods ]

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="frequency">Frequency of the sine wave.</param>
        /// <param name="amplitude">Amplitude of the sine wave.</param>
        public SineForceModifier(float frequency, float amplitude)
        {
            Frequency = frequency;
            Amplitude = amplitude;
        }

        /// <summary>
        /// Modifies a single Particle.
        /// </summary>
        /// <param name="time">Game timing information.</param>
        /// <param name="particle">Particle to be modified.</param>
        public override void ProcessActiveParticle(GameTime time, Particle particle)
        {
            float sin = (float)Math.Sin((particle.Age * _freq) * MathHelper.TwoPi);
            particle.Position.X += sin * (_amp * (float)time.ElapsedGameTime.TotalSeconds);
        }

        #endregion
    }
}