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
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Chimera.Graphics.Effects.Particles.Engine
{
    public class Particle
    {
        #region [ Private Fields ]

        private bool _expired;
        private int _lifespan;
        private int _creation;
        private float _age;
        private float _scale;
        private float _rotation;
        private UserDataCollection _userData;

        #endregion

        #region [ Public Fields ]

        /// <summary>
        /// The current position of the Particle.
        /// </summary>
        public Vector2 Position;

        /// <summary>
        /// The current momentum of the Particle.
        /// </summary>
        public Vector2 Momentum;

        /// <summary>
        /// The current color of the Particle as a Microsoft.Xna.Framework.Vector4.
        /// </summary>
        public Vector4 Color;

        #endregion

        #region [ Public Interface ]

        /// <summary>
        /// Returns true if the Particle has expired.
        /// </summary>
        public bool Expired
        {
            get { return _expired; }
        }

        /// <summary>
        /// Returns the age of the Particle in the range 0 - 1.
        /// </summary>
        public float Age
        {
            get { return _age; }
        }

        /// <summary>
        /// Gets or sets the opacity of the Particle.
        /// </summary>
        public float Opacity
        {
            get { return Color.W; }
            set { Color.W = MathHelper.Clamp(value, 0f, 1f); }
        }

        /// <summary>
        /// Gets or sets the scale of the Particle.
        /// </summary>
        public float Scale
        {
            get { return _scale; }
            set { _scale = Math.Max(value, 0f); }
        }

        /// <summary>
        /// Gets or sets the rotation of the Particle in radians.
        /// </summary>
        public float Rotation
        {
            get { return _rotation; }
            set { _rotation = value; }
        }

        /// <summary>
        /// Gets a Chimera.Graphics.Effects.Particles.Engine.UserDataCollection object containing all current
        /// user defined objects attached to the Particle.
        /// </summary>
        public UserDataCollection UserData
        {
            get { return _userData; }
        }

        #endregion

        #region [ Constructors & Methods ]

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Particle()
        {
            _userData = new UserDataCollection();
            Position = Vector2.Zero;
            Color = Vector4.One;
        }

        /// <summary>
        /// Rotates the Particle the specified angle, in addition to its current rotation.
        /// </summary>
        /// <param name="angle">Angle to rotate in radians.</param>
        public void Rotate(float angle)
        {
            _rotation += angle;
        }

        /// <summary>
        /// Grows (or shrinks) the particle the specified amount, in addition to its current
        /// scale.
        /// </summary>
        /// <param name="multiplier"></param>
        public void Grow(float multiplier)
        {
            _scale += multiplier;
        }

        internal void Activate(GameTime time, int lifespan)
        {
            _lifespan = lifespan;
            _creation = Environment.TickCount;
            _expired = false;
            _age = 0f;
            _userData.Clear();
            Position = Vector2.Zero;
            Momentum = Vector2.Zero;
        }

        internal void Update(GameTime time)
        {
            _age = ((float)Environment.TickCount - (float)_creation) / (float)_lifespan;

            if (_age >= 1f)
            {
                _expired = true;
                return;
            }

            //Position += (Momentum * (float)time.ElapsedGameTime.TotalSeconds);

            //Calculate momentum based on delta time...
            Vector2 dtMomentum;
            Vector2.Multiply(ref Momentum, (float)time.ElapsedGameTime.TotalSeconds, out dtMomentum);

            //Add it to the particles position...
            Vector2.Add(ref Position, ref dtMomentum, out Position);
        }

        internal void Draw(SpriteBatch batch, Texture2D texture, Rectangle source, Vector2 origin, float layerDepth)
        {
            batch.Draw(texture, Position, source, new Color(Color), _rotation,
                origin, _scale, SpriteEffects.None, layerDepth);
        }


        #endregion
    }
}
