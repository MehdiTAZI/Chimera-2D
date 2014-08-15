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
using System.Threading;
using Microsoft.Xna.Framework;

namespace Chimera.Graphics.Effects.Particles.Engine.Emitters
{
    public sealed class SpiralEmitter : Emitter
    {
        #region [ Private Fields ]

        private float _radius;
        private float _curTime;
        private float _increment;
        private Timer _timer;
        private SpiralDirection _direction;

        #endregion

        #region [ Public Interface ]

        /// <summary>
        /// Gets or sets the radius of the spiral.
        /// </summary>
        public float Radius
        {
            get { return _radius; }
            set { _radius = value; }
        }

        /// <summary>
        /// Gets or sets the direction of the spiral.
        /// </summary>
        public SpiralDirection Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }

        #endregion

        #region [ Constructors & Methods ]

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="system">ParticleSystem that this emitter will add itself to.</param>
        /// <param name="budget">Number of Particles available to this Emitter.</param>
        /// <param name="radius">Radius of the spiral.</param>
        /// <param name="rate">The amount of time in seconds it will take for the spiral to turn 1 revolution.</param>
        /// <param name="direction">Direction of the spiral.</param>
        public SpiralEmitter(ParticleSystem system, int budget, float radius, int rate, SpiralDirection direction)
            : base(system, budget)
        {
            _radius = radius;
            _curTime = 0f;
            _increment = 1f / (float)rate;
            _direction = direction;

            AutoResetEvent autoReset = new AutoResetEvent(false);
            TimerCallback timerDelegate = new TimerCallback(Tick);

            _timer = new Timer(timerDelegate, autoReset, 0, rate);
        }

        protected override void GetParticlePositionAndOrientation(Snapshot snap, ref Vector2 position, ref Vector2 orientation)
        {
            SpiralSnapshot spiralSnap = (SpiralSnapshot)snap;

            float angle = MathHelper.Lerp(0f, MathHelper.TwoPi, _curTime);

            position.X = orientation.X = (float)Math.Sin(angle);
            position.Y = orientation.Y = (float)Math.Cos(angle);

            position.X *= spiralSnap.Radius;
            position.Y *= spiralSnap.Radius;
        }

        private void Tick(object stateInfo)
        {
            if (_direction == SpiralDirection.AntiClockwise)
            {
                _curTime += _increment;
                if (_curTime > 1f) { _curTime--; }
            }
            else
            {
                _curTime -= _increment;
                if (_curTime < -1f) { _curTime++; }
            }
        }

        #endregion

        #region [ Snapshot public class & Methods ]

        private class SpiralSnapshot : Snapshot
        {
            private float _radius;

            public float Radius
            {
                get { return _radius; }
                set { _radius = value; }
            }
        }

        protected override Snapshot GenerateSnapshot()
        {
            return new SpiralSnapshot();
        }

        protected override void TakeSnapshot(ref Snapshot snap)
        {
            SpiralSnapshot spiralSnap = (SpiralSnapshot)snap;
            spiralSnap.Radius = _radius;
        }

        #endregion

        #region [ Enums ]

        /// <summary>
        /// Spiral directions.
        /// </summary>
        public enum SpiralDirection
        {
            Clockwise,
            AntiClockwise
        }

        #endregion
    }
}
