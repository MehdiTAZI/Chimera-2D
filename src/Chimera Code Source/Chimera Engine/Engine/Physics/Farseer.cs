#region Info/Author
//----------------------------------------------------------------------------
// Author:    Tazi Mehdi
// Source:    Chimera 2D GAMES ENGINE
// Info:      Farser Engine
//-----------------------------------------------------------------------------
#endregion
#region Using Statement
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Chimera.Physics.Farseer.FarseerGames.FarseerXNAPhysics;
using Chimera.Physics.Farseer.FarseerGames.FarseerXNAPhysics.Dynamics;
#endregion
namespace Chimera.Physics
{
    /// <summary>
    /// This Class Help You To Use The Farseer Physics Engine
    /// </summary>
    public static class FarseerEngine
    {
        private static PhysicsSimulator physics = new PhysicsSimulator(new Vector2(0f, 0f));
       /// <summary>
       /// Get Or Set PhysicsSimulator public class
       /// </summary>
        public static PhysicsSimulator Physics
        {
            get { return physics; }
            set { physics = value; }
        }
        /// <summary>
        /// Setup The Farseer Engine
        /// </summary>
        /// <param name="gravity"></param>
        public static void Setup(Vector2 gravity)
        {
            physics.Gravity = gravity;
        }
        /// <summary>
        /// Update Farseer Engine
        /// </summary>
        
        public static void Update(GameTime gameTime)
        {
            physics.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
          
        }
        /// <summary>
        /// Update Farseer Engine
        /// </summary>
        /// <param name="dt"></param>
        public static void Update(float dt)
        {
            physics.Update(dt);
        }
    }
}
