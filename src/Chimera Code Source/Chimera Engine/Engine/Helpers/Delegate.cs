#region Info/Author
//----------------------------------------------------------------------------
// Author:    Tazi Mehdi
// Source:    Chimera 2D GAMES ENGINE
// Info:      ___Delegate___
//-----------------------------------------------------------------------------
#endregion

namespace Chimera.Helpers.Delegate
{
    /// <summary>
    /// Generic Parametric Function Delegate
    /// </summary>
    /// <typeparam name="T">Function Type</typeparam>
    /// <param name="obj">An Object</param>
    /// <returns>Return A Value</returns>
    public delegate T GenericParam_Function<T>(T obj);
    /// <summary>
    /// Generic Parametric Method Delegate
    /// </summary>
    /// <typeparam name="T">Method Type</typeparam>
    /// <param name="obj">An Object</param>
    public delegate void GenericParam_Method<T>(T obj);
    /// <summary>
    /// Parametric Method Delegate
    /// </summary>
    /// <param name="obj">An Object</param>
    public delegate void Param_Method(object obj);
    /// <summary>
    /// Parametric Function Delegate
    /// </summary>
    /// <param name="obj">An Object</param>
    /// <returns></returns>
    public delegate object Param_Function(object obj);
    /// <summary>
    /// A Simple Function Delegate
    /// </summary>
    public delegate void Simple_Function();
}
