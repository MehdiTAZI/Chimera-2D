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
using Chimera.Graphics.Effects.Particles.Engine.Emitters;

namespace Chimera.Graphics.Effects.Particles.Engine.Controllers
{
    public abstract class Controller
    {
        #region [ Private Fields ]

        private List<Emitter> _subscriptions;

        #endregion

        #region [ Public Interface ]

        /// <summary>
        /// A list of Emitter objects that the Controller has been applied to.
        /// </summary>
        protected List<Emitter> Subscriptions
        {
            get { return _subscriptions; }
        }

        #endregion

        #region [ Constructors & Methods ]

        public Controller()
        {
            _subscriptions = new List<Emitter>();
        }

        /// <summary>
        /// Process an Emitter.
        /// </summary>
        /// <param name="time">Game timing data.</param>
        /// <param name="controlled">Chimera.Graphics.Effects.Particles.Engine.Emitters.Emitter object to process.</param>
        public virtual void ProcessEmitter(GameTime time, Emitter controlled) { }

        internal void Subscribe(Emitter emitter)
        {
            if (!_subscriptions.Contains(emitter))
            {
                _subscriptions.Add(emitter);
            }
        }

        internal void UnSubscribe(Emitter emitter)
        {
            if (_subscriptions.Contains(emitter))
            {
                _subscriptions.Remove(emitter);
            }
        }

        #endregion
    }
}