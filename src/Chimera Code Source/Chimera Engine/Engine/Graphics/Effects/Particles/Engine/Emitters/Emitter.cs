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
using Chimera.Graphics.Effects.Particles.Engine.Controllers;
using Chimera.Graphics.Effects.Particles.Engine.Modifiers;

namespace Chimera.Graphics.Effects.Particles.Engine.Emitters
{
    public class Emitter
    {
        #region [ Static Members ]

        private static Random _rnd = new Random();

        /// <summary>
        /// Gets the random number generator.
        /// </summary>
        protected static Random Rnd
        {
            get { return _rnd; }
        }

        #endregion

        #region [ Private Fields ]

        private LinkedList<Particle> _active;
        private Queue<Particle> _idle;
        private List<Modifier> _modifiers;
        private List<Controller> _controllers;
        private Queue<Snapshot> _snapCache;
        private Queue<Snapshot> _snapshots;
        private bool _sleeping;
        private int _budget;
        private int _dischargeQuantity;
        private int _lifespan;
        private Color _color;
        private float _opacity;
        private float _scale;
        private ImageStrip _strip;
        private byte _frame;
        private SpriteBlendMode _blend;
        private float _layerDepth;
        private float _speed;
        private float _speedRange;

        #endregion

        #region [ Public Fields ]

        /// <summary>
        /// The current position of the Emitter.
        /// </summary>
        public Vector2 Position;

        #endregion

        #region [ Public Interface ]

        /// <summary>
        /// The number of currently active Particles.
        /// </summary>
        public int ActiveParticles
        {
            get { return _active.Count; }
        }

        public LinkedListNode<Particle> FirstActiveParticle
        {
            get { return _active.First; }
        }

        /// <summary>
        /// The number of Particles available to the Emitter.
        /// </summary>
        public int Budget
        {
            get { return _budget; }
        }

        /// <summary>
        /// The number of Particles to discharge each time the Emitter is triggered.
        /// </summary>
        public int DischargeQuantity
        {
            get { return _dischargeQuantity; }
            set { _dischargeQuantity = Math.Max(value, 1); }
        }

        /// <summary>
        /// The lifespan of discharged Particles.
        /// </summary>
        public int ParticleLifespan
        {
            get { return _lifespan; }
            set { _lifespan = Math.Max(value, 25); }
        }

        /// <summary>
        /// The color of discharged Particles.
        /// </summary>
        public Color ParticleColor
        {
            get { return _color; }
            set { _color = value; }
        }

        /// <summary>
        /// The opacity of discharged Particles.
        /// </summary>
        public float ParticleOpacity
        {
            get { return _opacity; }
            set { _opacity = MathHelper.Clamp(value, 0f, 1f); }
        }

        /// <summary>
        /// The scale of discharged Particles.
        /// </summary>
        public float ParticleScale
        {
            get { return _scale; }
            set { _scale = Math.Max(value, 0f); }
        }

        /// <summary>
        /// The speed of discharged Particles.
        /// </summary>
        public float ParticleSpeed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        /// <summary>
        /// The range of the speed of discharged Particles.
        /// </summary>
        public float ParticleSpeedRange
        {
            get { return _speedRange; }
            set { _speedRange = value; }
        }

        /// <summary>
        /// The ImageStrip object used to render Particles.
        /// </summary>
        public ImageStrip ImageStrip
        {
            get { return _strip; }
            set { _strip = value; }
        }

        /// <summary>
        /// The frame on the ImageStrip to use when rendering Particles.
        /// </summary>
        public byte Frame
        {
            get { return _frame; }
            set { _frame = Math.Max(value, (byte)1); }
        }

        /// <summary>
        /// The blending mode to use when rendering Particles.
        /// </summary>
        public SpriteBlendMode BlendMode
        {
            get { return _blend; }
            set { _blend = value; }
        }

        public float LayerDepth
        {
            get { return _layerDepth; }
            set { _layerDepth = MathHelper.Clamp(value, 0f, 1f); }
        }

        #endregion

        #region [ Constructors & Methods ]

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="system">ParticleSystem that this Emitter will add itself to.</param>
        /// <param name="budget">Number of Particles available to this Emitter.</param>
        public Emitter(ParticleSystem system, int budget)
        {
            _budget = budget;

            _active = new LinkedList<Particle>();
            _idle = new Queue<Particle>(_budget);
            _modifiers = new List<Modifier>();
            _controllers = new List<Controller>();
            _snapCache = new Queue<Snapshot>(1024);
            _snapshots = new Queue<Snapshot>(1024);
            _sleeping = true;

            _dischargeQuantity = 1;
            _lifespan = 1000;
            _color = Color.White;
            _opacity = 1f;
            _scale = 1f;
            _speed = 0f;
            _speedRange = 0f;
            _strip = system.DefaultImageStrip;
            _frame = 1;
            _blend = SpriteBlendMode.Additive;

            Position = Vector2.Zero;

            //Fill the idle Particles queue with Particles...
            for (int i = 0; i < _budget; i++)
            {
                _idle.Enqueue(new Particle());
            }

            //Fill the Snapshot cache with empty Snapshots...
            for (int i = 0; i < 1024; i++)
            {
                _snapCache.Enqueue(GenerateSnapshot());
            }

            system.AddEmitter(this);
        }

        /// <summary>
        /// Applies a Modifier to the Emitter.
        /// </summary>
        /// <param name="mod">Modifier to apply.</param>
        /// <returns>True if the Modifier was successfully applied, else false.</returns>
        /// <remarks>The engine is smart enough to prevent the same Modifier being applied
        /// multiple times.</remarks>
        public bool ApplyModifier(Modifier mod)
        {
            if (_modifiers.Contains(mod)) { return false; }

            _modifiers.Add(mod);
            return true;
        }

        /// <summary>
        /// Removes a Modifier from the Emitter.
        /// </summary>
        /// <param name="mod">Modifier to remove.</param>
        /// <returns>True if the Modifier was successfully removed, else false.</returns>
        public bool RemoveModifier(Modifier mod)
        {
            return _modifiers.Remove(mod);
        }

        /// <summary>
        /// Applies a Controller to the Emitter.
        /// </summary>
        /// <param name="controller">Controller to apply.</param>
        /// <returns>True if the Controller was successfully applied, else false.</returns>
        /// <remarks>The engine is smart enough to prevent the same Controller being applied
        /// multiple times.</remarks>
        public bool ApplyController(Controller controller)
        {
            if (_controllers.Contains(controller)) { return false; }

            _controllers.Add(controller);
            controller.Subscribe(this);
            return true;
        }

        /// <summary>
        /// Removes a Controller from the Emitter.
        /// </summary>
        /// <param name="controller">Controller to be removed.</param>
        /// <returns>True if the Controller was successfully removed, else false.</returns>
        public bool RemoveController(Controller controller)
        {
            if (!_controllers.Contains(controller)) { return false; }

            _controllers.Remove(controller);
            controller.UnSubscribe(this);
            return true;
        }

        /// <summary>
        /// Triggers the Emitter at its current location.
        /// </summary>
        /// <returns>True if the Emitter was triggered.</returns>
        public bool Trigger()
        {
            return Trigger(Position);
        }

        /// <summary>
        /// Triggers the Emitter at the specified location.
        /// </summary>
        /// <param name="position">Location to trigger the Emitter.</param>
        /// <returns>True if the Emitter was triggered.</returns>
        public bool Trigger(Vector2 position)
        {
            if (_snapCache.Count == 0) { return false; }

            Snapshot snap = _snapCache.Dequeue();
            TakeSnapshot(ref snap);
            snap.Position = position;
            _snapshots.Enqueue(snap);

            return true;
        }

        private void Discharge(GameTime time)
        {
            //Flag set to true if a Particle is emitted...
            bool emitted = false;

            //Cycle through each Snapshot...
            while (_snapshots.Count > 0)
            {
                //Get the latest Emitter Snapshot...
                Snapshot snap = _snapshots.Dequeue();
                _snapCache.Enqueue(snap);

                //Discharge the correct number of Particles...
                for (int i = 0; i < _dischargeQuantity; i++)
                {
                    //If there are no available idle Particles...
                    if (_idle.Count == 0)
                    {
                        //Raise starving event...
                        RaiseEvent(Starving);
                        return;
                    }

                    //Get a fresh Particle from the idle Particles queue...
                    Particle spawn = _idle.Dequeue();
                    spawn.Activate(time, _lifespan);
                    _active.AddLast(spawn);

                    //Do stuff to the Particle here...
                    spawn.Color = new Vector4(_color.ToVector3(), _opacity);
                    spawn.Scale = _scale;

                    GetParticlePositionAndOrientation(snap, ref spawn.Position, ref spawn.Momentum);
                    //spawn.Position += snap.Position;
                    Vector2.Add(ref spawn.Position, ref snap.Position, out spawn.Position);

                    float speed = MathHelper.Lerp(_speed - (_speedRange / 2f), _speed + (_speedRange / 2f), (float)Rnd.NextDouble());
                    //spawn.Momentum *= speed;
                    Vector2.Multiply(ref spawn.Momentum, speed, out spawn.Momentum);

                    //Send the new Particle to the Modifiers...
                    _modifiers.ForEach(delegate(Modifier mod)
                    {
                        mod.ProcessNewParticle(time, spawn);
                    });

                    emitted = true;
                }
            }

            //If a particle has been emitted...
            if (emitted)
            {
                //Raise discharging event...
                RaiseEvent(Discharging);

                if (_sleeping)
                {
                    //Raise waking event...
                    RaiseEvent(Waking);

                    _sleeping = false;
                }
            }
        }

        protected virtual void GetParticlePositionAndOrientation(Snapshot snap, ref Vector2 position, ref Vector2 orientation)
        {
            position.X = position.Y = 0;

            float angle = (float)Rnd.NextDouble() * MathHelper.TwoPi;

            orientation.X = (float)Math.Sin(angle);
            orientation.Y = (float)Math.Cos(angle);
        }

        /// <summary>
        /// Updates the Emitter.
        /// </summary>
        /// <param name="time">Game timing data.</param>
        public void Update(GameTime time)
        {
            //Send the emitter to the controllers...
            _controllers.ForEach(delegate(Controller controller)
            {
                controller.ProcessEmitter(time, this);
            });

            //Discharge Particles if necessary...
            if (_snapshots.Count > 0)
            {
                Discharge(time);
            }

            //If there are no active Particles - bail!
            if (_active.Count == 0) { return; }

            //Update all the active Particles...
            LinkedListNode<Particle> node = _active.First;
            while (node != null)
            {
                node.Value.Update(time);
                node = node.Next;
            }

            //Remove expired Particles...
            //ADD BY TAZI MEHDI FOR NOT BUG   
            while ((_active.First!=null) && _active.First.Value.Expired)
            {
                _modifiers.ForEach(delegate(Modifier mod)
                {
                    mod.ProcessExpiredParticle(time, _active.First.Value);
                });

                RaiseEvent(Disposing);

                _idle.Enqueue(_active.First.Value);
                _active.RemoveFirst();

                //If we just deleted the last Particle...
                if (_active.Count == 0)
                {
                    if (!_sleeping)
                    {
                        //Raise sleeping event...
                        RaiseEvent(Sleeping);
                        _sleeping = true;

                        return;
                    }
                }
            }
        
            //Send the remaining active Particles to the Modifiers...
            node = _active.First;
            while (node != null)
            {
                _modifiers.ForEach(delegate(Modifier mod)
                {
                    mod.ProcessActiveParticle(time, node.Value);
                });

                node = node.Next;
            }
        }

        /// <summary>
        /// Renders the Emitter.
        /// </summary>
        /// <param name="batch">Microsoft.Xna.Framework.Graphics.SpriteBatch object to use.</param>
        public void Draw(SpriteBatch batch)
        {

            if (_active.Count == 0) { return; }

            batch.Begin(_blend);

            LinkedListNode<Particle> node = _active.First;
            while (node != null)
            {
                node.Value.Draw(batch, _strip.Texture, _strip.Source[_frame-1], _strip.Origin, _layerDepth);
                node = node.Next;
            }

            batch.End();
        }

        /// <summary>
        /// Returns a string describing the status of the Emitter. The string is formatted as
        /// [#active particles]/[budget].
        /// </summary>
        /// <returns></returns>
        public string Status()
        {
            return ActiveParticles.ToString() + "\\" + _budget.ToString();
        }

        #endregion

        #region [ Snapshot public class & Methods ]

        /// <summary>
        /// Emitter Snapshot public class.
        /// </summary>
        protected class Snapshot
        {
            public Vector2 Position;

            public Snapshot()
            {
                Position = Vector2.Zero;
            }
        }

        /// <summary>
        /// Generate a Snapshot object.
        /// </summary>
        /// <returns>Emitter.Snapshot object.</returns>
        protected virtual Snapshot GenerateSnapshot()
        {
            return new Snapshot();
        }

        /// <summary>
        /// Takes a Snapshot of the Emitters state.
        /// </summary>
        /// <param name="snap">Emitter Snapshot.</param>
        protected virtual void TakeSnapshot(ref Snapshot snap)
        {
            snap.Position = Position;
        }

        #endregion

        #region [ Events & Event Handlers ]

        /// <summary>
        /// Raises the specified event.
        /// </summary>
        /// <param name="e">Event to raise.</param>
        private void RaiseEvent(EventHandler e)
        {
            if (e != null) { e(this, EventArgs.Empty); }
        }

        /// <summary>
        /// The Emitter is waking after a period of inactivity.
        /// </summary>
        public event EventHandler Waking;

        /// <summary>
        /// The Emitter has no further active Particles.
        /// </summary>
        public event EventHandler Sleeping;

        /// <summary>
        /// The Emitter has not enough Particles to trigger (consider increasing budget).
        /// </summary>
        public event EventHandler Starving;

        /// <summary>
        /// The Emitter is discharging Particles.
        /// </summary>
        public event EventHandler Discharging;

        /// <summary>
        /// The Emitter is disposing of a Particle.
        /// </summary>
        public event EventHandler Disposing;

        #endregion
    }
}