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
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Chimera.Graphics.Effects.Particles.Engine.Emitters;
using Chimera.Graphics.Effects.Particles.Engine.Modifiers;

namespace Chimera.Graphics.Effects.Particles.Engine
{
    public class ParticleSystem : DrawableGameComponent
    {
        #region [ Private Fields ]

        private List<Emitter> _emitters;
        private List<Modifier> _modifiers;
        private ContentManager _content;
        private SpriteBatch _batch;
        private ImageStrip _defaultImageStrip;
        private bool _paused;

        #endregion

        #region [ Public Interface ]

        /// <summary>
        /// Gets or sets wether or not the ParticleSystem is paused.
        /// </summary>
        public bool Paused
        {
            get { return _paused; }
            set { _paused = value; }
        }

        /// <summary>
        /// Returns the total number of Particles currently active in each Emitter contained
        /// within the ParticleSystem.
        /// </summary>
        public int ActiveParticles
        {
            get
            {
                int num = 0;
                _emitters.ForEach(delegate(Emitter emitter)
                {
                    num += emitter.ActiveParticles;
                });

                return num;
            }
        }

        /// <summary>
        /// Returns the sum of the budget of each Emitter contained within the ParticleSystem.
        /// </summary>
        public int Budget
        {
            get
            {
                int budget = 0;
                _emitters.ForEach(delegate(Emitter emitter)
                {
                    budget += emitter.Budget;
                });

                return budget;
            }
        }

        /// <summary>
        /// Gets the default ImageStrip.
        /// </summary>
        public ImageStrip DefaultImageStrip
        {
            get { return _defaultImageStrip; }
        }

        /// <summary>
        /// Returns a string describing the status of the ParticleSystem. The string is
        /// formatted "[#active particles]/[total budget] in [# emitters] emitters."
        /// </summary>
        public string Status
        {
            get
            {
                int active = 0;
                int budget = 0;
                _emitters.ForEach(delegate(Emitter emitter)
                {
                    active += emitter.ActiveParticles;
                    budget += emitter.Budget;
                });

                return active.ToString() + "\\" + budget.ToString() + " in " + _emitters.Count.ToString() + " emitters.";
            }
        }

        #endregion

        #region [ Constructors & Methods ]

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="game">Instance of your Microsoft.Xna.Framework.Game object.</param>
        public ParticleSystem(Game game)
            : base(game)
        {
            _emitters = new List<Emitter>();
            _modifiers = new List<Modifier>();
            _content = new ContentManager(game.Services);

            //Create the default ImageStrip...
            _defaultImageStrip = new ImageStrip(game, @"Resources\DefaultParticle", 32, 32, 2);

            //Register self with game components...
            game.Components.Add(this);
        }

        /// <summary>
        /// Applies a Modifier to each Emitter within the ParticleSystem.
        /// </summary>
        /// <param name="mod">Chimera.Graphics.Effects.Particles.Engine.Modifiers.Modifier object to add.</param>
        /// <returns>True if the Modifier was successfully added, else false.</returns>
        /// <remarks>The system is smart enough to prevent the same Modifier being applied
        /// multiple times.</remarks>
        public bool ApplyModifier(Modifier mod)
        {
            if (_modifiers.Contains(mod)) { return false; }

            _modifiers.Add(mod);

            _emitters.ForEach(delegate(Emitter emitter)
            {
                emitter.ApplyModifier(mod);
            });

            return true;
        }

        /// <summary>
        /// Removes a Modifier from each Emitter within the ParticleSystem.
        /// </summary>
        /// <param name="mod">Chimera.Graphics.Effects.Particles.Engine.Modifiers.Modifier object to remove.</param>
        /// <returns>True if the Modifier was successfully removed, else false.</returns>
        public bool RemoveModifier(Modifier mod)
        {
            if (!_modifiers.Contains(mod)) { return false; }

            _modifiers.Remove(mod);

            _emitters.ForEach(delegate(Emitter emitter)
            {
                emitter.RemoveModifier(mod);
            });

            return true;
        }

        /// <summary>
        /// Adds an Emitter to the ParticleSystem.
        /// </summary>
        /// <param name="emitter">Chimera.Graphics.Effects.Particles.Engine.Emitters.Emitter object to add.</param>
        /// <returns>True if the Emitter was successfully added, else false.</returns>
        /// <remarks>The system is smart enough to prevent the same Emitter being added
        /// multiple times.</remarks>
        public bool AddEmitter(Emitter emitter)
        {
            if (_emitters.Contains(emitter)) { return false; }

            _emitters.Add(emitter);

            _modifiers.ForEach(delegate(Modifier modifier)
            {
                emitter.ApplyModifier(modifier);
            });

            return true;
        }

        /// <summary>
        /// Removes an Emitter from the ParticleSystem.
        /// </summary>
        /// <param name="emitter">Chimera.Graphics.Effects.Particles.Engine.Emitters.Emitter object to remove.</param>
        /// <returns>True if the Emitter was successfully removed, else false.</returns>
        public bool RemoveEmitter(Emitter emitter)
        {
            if (!_emitters.Contains(emitter)) { return false; }

            _modifiers.ForEach(delegate(Modifier modifier)
            {
                emitter.RemoveModifier(modifier);
            });

            return true;
        }

        /// <summary>
        /// Toggles the pause state of the ParticleSystem.
        /// </summary>
        public void TogglePause()
        {
            _paused = !_paused;
        }

        protected override void LoadContent()
        {
            
                _batch = new SpriteBatch(GraphicsDevice);
            

//            base.LoadGraphicsContent(loadAllContent);
            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            
            
                _content.Unload();
                _batch.Dispose();
            
            base.UnloadContent();
            //base.UnloadGraphicsContent(unloadAllContent);
        }

        /// <summary>
        /// Updates the ParticleSystem.
        /// </summary>
        /// <param name="time">Game timing data.</param>
        public override void Update(GameTime time)
        {
            base.Update(time);

            if (!_paused)
            {
                _emitters.ForEach(delegate(Emitter emitter)
                {
                    emitter.Update(time);
                });
            }
        }

        /// <summary>
        /// Renders the ParticleSystem.
        /// </summary>
        /// <param name="time">Game timing data.</param>
        public override void Draw(GameTime time)
        {
            _emitters.ForEach(delegate(Emitter emitter)
            {
                emitter.Draw(_batch);
            });
        }

        #endregion
    }
}
