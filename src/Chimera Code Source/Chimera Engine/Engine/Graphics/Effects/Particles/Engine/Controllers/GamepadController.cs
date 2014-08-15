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
using Microsoft.Xna.Framework.Input;
using Chimera.Graphics.Effects.Particles.Engine.Emitters;

namespace Chimera.Graphics.Effects.Particles.Engine.Controllers
{
    public sealed class GamepadController : Controller
    {
        #region [ Private Fields ]

        private ThumbSticks _stick;
        private Buttons _button;

        #endregion

        #region [ Constructors & Methods ]

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="stick">The thumbstick used to position the Emitter.</param>
        /// <param name="trigger">The button used to trigger the Emitter.</param>
        public GamepadController(ThumbSticks stick, Buttons trigger):base()
        {
            _stick = stick;
            _button = trigger;
        }

        /// <summary>
        /// Processes the Emitter.
        /// </summary>
        /// <param name="time">Game timing data.</param>
        /// <param name="controlled">Chimera.Graphics.Effects.Particles.Engine.Emitters.Emitter object to process.</param>
        public override void ProcessEmitter(GameTime time, Emitter controlled)
        {
            GamePadState state = GamePad.GetState(PlayerIndex.One, GamePadDeadZone.Circular);

            if (_stick == ThumbSticks.Left)
            {
                controlled.Position.X += (state.ThumbSticks.Left.X * 32f);
                controlled.Position.Y -= (state.ThumbSticks.Left.Y * 32f);
            }
            else
            {
                controlled.Position.X += (state.ThumbSticks.Right.X * 32f);
                controlled.Position.Y -= (state.ThumbSticks.Right.Y * 32f);
            }

            switch (_button)
            {
                case Buttons.A:
                    {
                        if (state.Buttons.A == ButtonState.Pressed) { controlled.Trigger(); }
                        break;
                    }
                case Buttons.B:
                    {
                        if (state.Buttons.B == ButtonState.Pressed) { controlled.Trigger(); }
                        break;
                    }
                case Buttons.X:
                    {
                        if (state.Buttons.X == ButtonState.Pressed) { controlled.Trigger(); }
                        break;
                    }
                case Buttons.Y:
                    {
                        if (state.Buttons.Y == ButtonState.Pressed) { controlled.Trigger(); }
                        break;
                    }
                case Buttons.LB:
                    {
                        if (state.Buttons.LeftShoulder == ButtonState.Pressed) { controlled.Trigger(); }
                        break;
                    }
                case Buttons.RB:
                    {
                        if (state.Buttons.RightShoulder == ButtonState.Pressed) { controlled.Trigger(); }
                        break;
                    }
            }
        }

        #endregion

        #region [ Enums ]

        /// <summary>
        /// Gamepad thumbsticks.
        /// </summary>
        public enum ThumbSticks
        {
            Left,
            Right
        }

        /// <summary>
        /// Gamepad buttons.
        /// </summary>
        public enum Buttons
        {
            A,B,
            X,Y,
            LB, RB,
            None
        }

        #endregion
    }
}
