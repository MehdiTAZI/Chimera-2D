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
    public abstract class Modifier
    {
        #region [ Constructors & Methods ]

        /// <summary>
        /// Modifies a Particle when it is first created.
        /// </summary>
        /// <param name="time">Game timing information.</param>
        /// <param name="particle">Particle to be modified.</param>
        public virtual void ProcessNewParticle(GameTime time, Particle particle) { }

        /// <summary>
        /// Modifies a Particle during its lifetime.
        /// </summary>
        /// <param name="time">Game timing information.</param>
        /// <param name="particle">Particle to be modified.</param>
        public virtual void ProcessActiveParticle(GameTime time, Particle particle) { }

        /// <summary>
        /// Modifies a Particle upon its expiry.
        /// </summary>
        /// <param name="time">Game timing information.</param>
        /// <param name="particle">Particle to be modified.</param>
        public virtual void ProcessExpiredParticle(GameTime time, Particle particle) { }

        #endregion

        #region [ Embedded public classes ]

        protected class PreCurve : Curve
        {
            private byte _samples;
            private float[] _data;

            public PreCurve(byte samples)
                : base()
            {
                _samples = samples;
                _data = new float[samples+1];

                Flush();
            }
            public new float Evaluate(float position)
            {
                position = MathHelper.Clamp(position, 0f, 1f);
                byte sample = (byte)(position * _samples);

                if (_data[sample] != -1)
                {
                    return _data[sample];
                }
                else
                {
                    float value = base.Evaluate(position);
                    _data[sample] = value;
                    return value;
                }
            }
            //public new CurveKeyCollection Keys
            //{
            //    get
            //    {
            //        Flush();
            //        return base.Keys;
            //    }
            //}
            public void PreCalculate()
            {
                float position;

                for (byte sample = 0; sample < _samples; sample++)
                {
                    position = (float)sample / (float)_samples;
                    _data[sample] = base.Evaluate(position);
                }
            }
            private void Flush()
            {
                for (ushort i = 1; i < _samples; i++)
                {
                    _data[i-1] = -1;
                }
            }
        }

        #endregion
    }
}