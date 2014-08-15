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

namespace Chimera.Graphics.Effects.Particles.Engine.Controllers
{
    public sealed class ExplosionController : Controller
    {
        #region [ Private Fields ]

        private Emitter _emitter;

        #endregion

        #region [ Constructors & Methods ]

        /// <summary>
        /// Default Constructor.
        /// </summary>
        /// <param name="emitter">Emitter whos Particles will explode on expiry.</param>
        public ExplosionController(Emitter emitter)
        {
            _emitter = emitter;
            emitter.Disposing += new EventHandler(Disposed);
        }

        #endregion

        #region [ Events & Event Handlers ]

        private void Disposed(object sender, EventArgs e)
        {
            Subscriptions.ForEach(delegate(Emitter emitter)
            {
                emitter.Trigger(_emitter.FirstActiveParticle.Value.Position);
            });
        }

        #endregion
    }
}
