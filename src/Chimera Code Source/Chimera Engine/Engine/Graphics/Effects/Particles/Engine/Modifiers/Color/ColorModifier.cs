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
using Microsoft.Xna.Framework.Graphics;

namespace Chimera.Graphics.Effects.Particles.Engine.Modifiers
{
    public sealed class ColorModifier : Modifier
    {
        #region [ Private Fields ]

        private PreCurve _red;
        private PreCurve _green;
        private PreCurve _blue;

        #endregion

        #region [ Constructors & Methods ]

        /// <summary>
        /// Creates a ColorModifier with 2 color values.
        /// </summary>
        /// <param name="initial">Initial color of Particles.</param>
        /// <param name="ultimate">Ultimate color of Particles.</param>
        public ColorModifier(Color initial, Color ultimate)
        {
            CurveKeyCollection keysRed = new CurveKeyCollection();
            keysRed.Add(new CurveKey(0f, (float)initial.R / 255f));
            keysRed.Add(new CurveKey(1f, (float)ultimate.R / 255f));

            CurveKeyCollection keysGreen = new CurveKeyCollection();
            keysGreen.Add(new CurveKey(0f, (float)initial.G / 255f));
            keysGreen.Add(new CurveKey(1f, (float)ultimate.G / 255f));

            CurveKeyCollection keysBlue = new CurveKeyCollection();
            keysBlue.Add(new CurveKey(0f, (float)initial.B / 255f));
            keysBlue.Add(new CurveKey(1f, (float)ultimate.B / 255f));

            Initialize(keysRed, keysGreen, keysBlue);
        }

        /// <summary>
        /// Creates a ColorModifier with 3 color values.
        /// </summary>
        /// <param name="initial">Initial color of Particles.</param>
        /// <param name="mid">Middle color of Particles.</param>
        /// <param name="sweep">Position of the middle color.</param>
        /// <param name="ultimate">Ultimate color of Particles.</param>
        public ColorModifier(Color initial, Color mid, float sweep, Color ultimate)
        {
            CurveKeyCollection keysRed = new CurveKeyCollection();
            keysRed.Add(new CurveKey(0f, (float)initial.R / 255f));
            keysRed.Add(new CurveKey(sweep, (float)mid.R / 255f));
            keysRed.Add(new CurveKey(1f, (float)ultimate.R / 255f));

            CurveKeyCollection keysGreen = new CurveKeyCollection();
            keysGreen.Add(new CurveKey(0f, (float)initial.G / 255f));
            keysGreen.Add(new CurveKey(sweep, (float)mid.G / 255f));
            keysGreen.Add(new CurveKey(1f, (float)ultimate.G / 255f));

            CurveKeyCollection keysBlue = new CurveKeyCollection();
            keysBlue.Add(new CurveKey(0f, (float)initial.B / 255f));
            keysBlue.Add(new CurveKey(sweep, (float)mid.B / 255f));
            keysBlue.Add(new CurveKey(1f, (float)ultimate.B / 255f));

            Initialize(keysRed, keysGreen, keysBlue);
        }
        
        private void Initialize(CurveKeyCollection keysRed, CurveKeyCollection keysGreen,
            CurveKeyCollection keysBlue)
        {
            _red = new PreCurve(255);
            _green = new PreCurve(255);
            _blue = new PreCurve(255);

            foreach(CurveKey key in keysRed)
            {
                _red.Keys.Add(key);
            }

            foreach(CurveKey key in keysGreen)
            {
                _green.Keys.Add(key);
            }

            foreach(CurveKey key in keysBlue)
            {
                _blue.Keys.Add(key);
            }

            _red.PreCalculate();
            _green.PreCalculate();
            _blue.PreCalculate();
        }

        /// <summary>
        /// Modifies a single Particle.
        /// </summary>
        /// <param name="time">Game timing information.</param>
        /// <param name="particle">Particle to modify.</param>
        public override void ProcessActiveParticle(GameTime time, Particle particle)
        {
            particle.Color.X = _red.Evaluate(particle.Age);
            particle.Color.Y = _green.Evaluate(particle.Age);
            particle.Color.Z = _blue.Evaluate(particle.Age);
        }

        #endregion
    }
}
