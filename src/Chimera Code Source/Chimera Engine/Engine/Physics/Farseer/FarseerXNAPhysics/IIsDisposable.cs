using System;
using System.Collections.Generic;
using System.Text;

namespace Chimera.Physics.Farseer.FarseerGames.FarseerXNAPhysics {
    /// <summary>
    /// Extends IDisposable to include the boolean check 'IsDisposed'.
    /// <para><c>if(object.IsDisposed){return;}</c></para>
    /// </summary>
    public interface IIsDisposable : IDisposable {
       bool IsDisposed {get;}
    }
}
