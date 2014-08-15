#region Info/Author
//----------------------------------------------------------------------------
// Author:    Tazi Mehdi
// Source:    Chimera 2D GAMES ENGINE
// Info:      ___Some Basics Functions___
//-----------------------------------------------------------------------------
#endregion
#region Using Statement
using System;
using System.Collections.Generic;
#endregion
namespace Chimera.Helpers
{
    #region Interval||Born
    /// <summary>
    /// A Simple Born public class
    /// </summary>
    /// <typeparam name="T">Elements Type</typeparam>
    public class Born<T>
    {
        protected T min;
        protected T max;
        /// <summary>
        /// Get Or Set The Minimal Value
        /// </summary>
        public T Min
        {
            get { return this.min; }
            set { this.min=value; }
        }
        /// <summary>
        /// Get Or Set The Maximal Value
        /// </summary>
        public T Max
        {
            get { return this.max;}
            set { this.max = value; }
        }
        /// <summary>
        /// A Default Constructor
        /// </summary>
        public Born()
        {

        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="min">Set Minimal Value</param>
        /// <param name="max">Set The Maximal Value</param>
        public Born(T min, T max) { this.min = min; this.max = max; }
        
    }
    /// <summary>
    /// A Simple Interval public class
    /// </summary>
    /// <typeparam name="T">Elements Type</typeparam>
    public class Interval<T> : Born<T>
    {
        private T value;
        /// <summary>
        /// The Value
        /// </summary>
        public T Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
        public Interval()
        {
            
        }
        public Interval(T min , T max , T value)
        {
            this.min = min;
            this.max = max;
            this.value = value;
        }
    }
    #endregion
    /// <summary>
    /// This Class Offer You Some Basic Functions
    /// </summary>
    public static class BasicFunctions
    {
        /// <summary>
        /// Permute The Frist Element With The Second
        /// </summary>
        /// <typeparam name="T">Element Type</typeparam>
        /// <param name="elem1">First Element</param>
        /// <param name="elem2">Second Element</param>
        public static void Permute<T>(ref T elem1,ref T elem2)
        {
            T tmp;
            tmp = elem2;
            elem2 = elem1;
            elem1 = tmp;
        }     
        #region Percent Functions
        /// <summary>
        /// Calculate The Percent Of MaxValue
        /// </summary>
        /// <param name="Value">The Value</param>
        /// <param name="MaxValue">The Maximal Value</param>
        /// <returns>Return The Percent Of Value In MaxValue</returns>
        public static float PerCent(float Value, float MaxValue) { return ((Value * 100) / MaxValue); }
        /// <summary>
        /// Calculate The Percent Of MaxValue
        /// </summary>
        /// <param name="Value">The Value</param>
        /// <param name="MaxValue">The Maximal Value</param>
        /// <returns>Return The Percent Of Value In MaxValue</returns>
        public static int PerCent(int Value, int MaxValue) { return ((Value * 100) / MaxValue); }
        /// <summary>
        /// Calculate The Percent Of MaxValue
        /// </summary>
        /// <param name="Value">The Value</param>
        /// <param name="MaxValue">The Maximal Value</param>
        /// <returns>Return The Percent Of Value In MaxValue</returns>
        public static double PerCent(double Value, double MaxValue) { return ((Value * 100) / MaxValue); }
        /// <summary>
        /// Calculate The Percent Of MaxValue
        /// </summary>
        /// <param name="Value">The Value</param>
        /// <param name="MaxValue">The Maximal Value</param>
        /// <returns>Return The Percent Of Value In MaxValue</returns>
        public static byte PerCent(byte Value, byte MaxValue) { return (byte)((Value * 100) / MaxValue); }
        #endregion
    }
}
