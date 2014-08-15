using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;

using Chimera.Physics.Farseer.FarseerGames.FarseerXNAPhysics;

namespace Chimera.Physics.Farseer.FarseerGames.FarseerXNAGame.Entities {
    public interface IEntity : IIsDisposable  {   
        //empty for now
        Vector2 Position { get;set;}
        float Orientation { get;set;}
        
        void Update();

        void LoadToPhysicsSimulator(PhysicsSimulator physicsSimulator);

    }
}
