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
    public sealed class LineEmitter : Emitter
    {
        #region [ Private Fields ]

        private float _length;
        private float _angle;
        private Vector2 _normal;

        #endregion

        #region [ Public Interface ]

        /// <summary>
        /// Gets or sets the length of the line in pixels.
        /// </summary>
        public float Length
        {
            get { return _length; }
            set { _length = (float)Math.Max(value, 1f); }
        }

        /// <summary>
        /// Gets or sets the length of the line in radians.
        /// </summary>
        public float Angle
        {
            get { return _angle; }
            set { _angle = value; }
        }

        #endregion

        #region [ Public Fields ]

        /// <summary>
        /// The normal of the line.
        /// </summary>
        public Vector2 Normal
        {
            get { return _normal; }
            set { _normal = value; }
        }

        #endregion

        #region [ Constructors & Methods ]

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="system">ParticleSystem that this Emitter will add itself to.</param>
        /// <param name="budget">Number of Particles available to this Emitter.</param>
        /// <param name="length">The length of the line in pixels.</param>
        /// <param name="angle">The rotation of the line in radians.</param>
        public LineEmitter(ParticleSystem system, int budget, float length, float angle)
            : base(system, budget)
        {
            _length = length;
            _angle = angle;
            _normal = Vector2.UnitY;
        }

        /// <summary>
        /// Rotates the line by the specified angle in addition to its current rotation.
        /// </summary>
        /// <param name="angle">Angle to rotate,</param>
        public void Rotate(float angle)
        {
            _angle += angle;
        }

        protected override void GetParticlePositionAndOrientation(Snapshot snap, ref Vector2 position, ref Vector2 orientation)
        {
            LineSnapshot lineSnap = (LineSnapshot)snap;

            position.X = ((float)Rnd.NextDouble() * lineSnap.Length) - (lineSnap.Length / 2f);

            Matrix trans = Matrix.CreateRotationZ(lineSnap.Angle);
            //position = Vector2.Transform(position, trans);
            Vector2.Transform(ref position, ref trans, out position);
            //orientation = Vector2.Transform(_normal, trans);
            Vector2.Transform(ref lineSnap.Normal, ref trans, out orientation);
        }

        #endregion

        #region [ Snapshot public class & Methods ]

        private class LineSnapshot : Snapshot
        {
            private float _length;
            private float _angle;

            public Vector2 Normal;

            public float Length
            {
                get { return _length; }
                set { _length = (float)Math.Max(value, 1f); }
            }
            public float Angle
            {
                get { return _angle; }
                set { _angle = MathHelper.Clamp(value, 0f, MathHelper.TwoPi); }
            }
        }

        protected override Snapshot GenerateSnapshot()
        {
            return new LineSnapshot();
        }

        protected override void TakeSnapshot(ref Snapshot snap)
        {
            base.TakeSnapshot(ref snap);

            LineSnapshot lineSnap = (LineSnapshot)snap;

            lineSnap.Length = _length;
            lineSnap.Angle = _angle;
            lineSnap.Normal = _normal;
        }

        #endregion
    }
}
