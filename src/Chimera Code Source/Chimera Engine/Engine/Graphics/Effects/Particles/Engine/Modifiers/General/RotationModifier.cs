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
    public sealed class RotationModifier : Modifier
    {
        #region [ Private Fields ]

        private float _rps;

        #endregion

        #region [ Public Interface ]

        /// <summary>
        /// Gets or sets revolutions per second.
        /// </summary>
        public float Revolutions
        {
            get { return _rps; }
            set { _rps = value; }
        }

        #endregion

        #region [ Constructrors & Methods ]

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="revolutions">Particle revolutions per second.</param>
        public RotationModifier(float revolutions)
        {
            _rps = revolutions;
        }

        /// <summary>
        /// Modifies a single Particle.
        /// </summary>
        /// <param name="time">Game timing information.</param>
        /// <param name="particle">Particle to modify.</param>
        public override void ProcessActiveParticle(GameTime time, Particle particle)
        {
            particle.Rotate(_rps * (float)time.ElapsedGameTime.TotalSeconds);
        }

        #endregion
    }
}
