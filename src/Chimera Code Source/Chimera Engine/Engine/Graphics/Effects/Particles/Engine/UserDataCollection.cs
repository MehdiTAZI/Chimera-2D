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

namespace Chimera.Graphics.Effects.Particles.Engine
{
    public sealed class UserDataCollection
    {
        #region [ Private Fields ]

        private Dictionary<Type, object> _dictionary;

        #endregion

        #region [ Constructors & Methods ]

        /// <summary>
        /// Default Constructor.
        /// </summary>
        public UserDataCollection()
        {
            _dictionary = new Dictionary<Type, object>();
        }

        /// <summary>
        /// Adds an object to the UserDataCollection.
        /// </summary>
        /// <typeparam name="T">The System.Type of the data to be added.</typeparam>
        /// <param name="data">The data to add.</param>
        public void Add<T>(T data)
        {
            _dictionary.Add(typeof(T), data);
        }

        /// <summary>
        /// Gets an object from the UserDataCollection.
        /// </summary>
        /// <typeparam name="T">The System.Type of the data to retrieve.</typeparam>
        /// <returns>The retrieved data (null on failure).</returns>
        public T Peek<T>()
        {
            return (T)_dictionary[typeof(T)];
        }

        /// <summary>
        /// Removes an object from the UserDataCollection.
        /// </summary>
        /// <typeparam name="T">The System.Type of the data to remove.</typeparam>
        public void Remove<T>()
        {
            _dictionary.Remove(typeof(T));
        }

        /// <summary>
        /// Clears all objects from the UserDataCollection.
        /// </summary>
        public void Clear()
        {
            _dictionary.Clear();
        }

        #endregion
    }
}