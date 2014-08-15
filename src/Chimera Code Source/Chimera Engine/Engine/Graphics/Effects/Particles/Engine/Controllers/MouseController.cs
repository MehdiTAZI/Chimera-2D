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
    #if !XBOX
    public sealed class MouseController : Controller
    {
        #region [ Private Fields ]

        private MouseButtons _button;

        #endregion

        #region [ Constructors & Methods ]

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="trigger">The mouse button used to trigger the Emitter.</param>
        public MouseController(MouseButtons trigger):base()
        {
            _button = trigger;
        }

        /// <summary>
        /// Processes an Emitter.
        /// </summary>
        /// <param name="time">Game timing data.</param>
        /// <param name="controlled">Chimera.Graphics.Effects.Particles.Engine.Emitters.Emitter object to process.</param>
        public override void ProcessEmitter(GameTime time, Emitter controlled)
        {
            MouseState state = Microsoft.Xna.Framework.Input.Mouse.GetState();

            controlled.Position.X = (float)state.X;
            controlled.Position.Y = (float)state.Y;

            switch (_button)
            {
                case MouseButtons.Left:
                    {
                        if (state.LeftButton == ButtonState.Pressed)
                            controlled.Trigger();
                        break;
                    }
                case MouseButtons.Middle:
                    {
                        if (state.MiddleButton == ButtonState.Pressed)
                            controlled.Trigger();
                        break;
                    }
                case MouseButtons.Right:
                    {
                        if (state.RightButton == ButtonState.Pressed)
                            controlled.Trigger();
                        break;
                    }
            }
        }

        #endregion

        #region [ Enums ]

        /// <summary>
        /// Mouse buttons.
        /// </summary>
        public enum MouseButtons
        {
            Left,
            Middle,
            Right,
            None
        }

        #endregion
    }
#endif
}
