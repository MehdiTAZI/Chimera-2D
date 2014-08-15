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
    public sealed class OpacityModifier : Modifier
    {
        #region [ Private Fields ]

        private PreCurve _curve;

        #endregion

        #region [ Constructors & Methods ]

        /// <summary>
        /// Constructor with 3 opactity values.
        /// </summary>
        /// <param name="initial">Initial opacity.</param>
        /// <param name="mid">Middle opacity.</param>
        /// <param name="sweep">Position of middle opacity.</param>
        /// <param name="ultimate">Ultimate opacity.</param>
        public OpacityModifier(float initial, float mid, float sweep, float ultimate)
        {
            initial = MathHelper.Clamp(initial, 0f, 1f);
            mid = MathHelper.Clamp(mid, 0f, 1f);
            sweep = MathHelper.Clamp(sweep, 0f, 1f);
            ultimate = MathHelper.Clamp(ultimate, 0f, 1f);

            CurveKeyCollection keys = new CurveKeyCollection();
            keys.Add(new CurveKey(0f, initial));
            keys.Add(new CurveKey(sweep, mid));
            keys.Add(new CurveKey(1f, ultimate));

            Initialize(keys);
        }

        /// <summary>
        /// Constructor with 2 opacity values.
        /// </summary>
        /// <param name="initial">Initial opacity.</param>
        /// <param name="ultimate">Ultimate opacity.</param>
        public OpacityModifier(float initial, float ultimate)
        {
            initial = MathHelper.Clamp(initial, 0f, 1f);
            ultimate = MathHelper.Clamp(ultimate, 0f, 1f);

            CurveKeyCollection keys = new CurveKeyCollection();
            keys.Add(new CurveKey(0f, initial));
            keys.Add(new CurveKey(1f, ultimate));

            Initialize(keys);
        }

        /// <summary>
        /// Constructor with multiple opacity values.
        /// </summary>
        /// <param name="keys">A cCurveKeyCollection containing multiple opacity/position keys.</param>
        public OpacityModifier(CurveKeyCollection keys)
        {
            Initialize(keys);
        }
        
        private void Initialize(CurveKeyCollection keys)
        {
            _curve = new PreCurve(255);

            foreach (CurveKey key in keys)
            {
                _curve.Keys.Add(key);
            }

            _curve.PreCalculate();
        }

        /// <summary>
        /// Modifies a single Particle.
        /// </summary>
        /// <param name="time">Game timing information.</param>
        /// <param name="particle">Particle to be modified.</param>
        public override void ProcessActiveParticle(GameTime time, Particle particle)
        {
            particle.Opacity = _curve.Evaluate(particle.Age);
        }

        #endregion
    }
}
