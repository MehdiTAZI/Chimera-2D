#region Info/Author
//----------------------------------------------------------------------------
// Author:    Tazi Mehdi
// Source:    Chimera 2D GAMES ENGINE
// Info:      Randomize
//-----------------------------------------------------------------------------
#endregion
#region Using Statement
using System;

#endregion

namespace Chimera.Helpers
{
   /// <summary>
   /// This Class allow you to generate and use randomize numbers
   /// </summary>
    public static class Randomize
    {
       private static Random rnd = new Random(DateTime.Now.Millisecond);
       private static int min,max;
        /// <summary>
        /// Get Or Set The Maximal Number That Will Be Generated
        /// </summary>
       public static int Max
        {
            get { return max; }
            set { max = value; }
        }
        
        ///<summary>
        ///Get Or Set The Minimal Number That Will Be Generated
        ///</summary>        
       public static int Min
        {
            get { return min; }
            set { min = value; }
        }
        /// <summary>
        /// Generate An Integer
        /// </summary>
        /// <returns>Return The Generated Number</returns>
       public static int GenerateInteger()
       {
           return rnd.Next(min,max);
       }
        /// <summary>
        /// Generate An Integer Number Betwen Min and Max
        /// </summary>
        /// <param name="_min">Minimal Value</param>
        /// <param name="_max">Maximal Value</param>
        /// <returns>Return The Generated Number</returns>
        public static int GenerateInteger(int _min,int _max)
        {
            return rnd.Next(_min, _max);
        }
        /// <summary>
        /// Generate A Double
        /// </summary>
        /// <returns>Return The Generated Number</returns>
        public static double GenerateDouble()
        {
            double x = rnd.NextDouble();
            return (x * (max - min)) + min;
        }
        /// <summary>
        /// Generate A Double Number Betwen Min And Max
        /// </summary>
        /// <param name="_min">Minimal Value</param>
        /// <param name="_max">Maximal Value</param>
        /// <returns>Return The Generated Number</returns>
        public static double GenerateDouble(int _min, int _max)
        {
              double x = rnd.NextDouble();
              return (x * (_max-_min))+_min;
        }
        /// <summary>
        /// Generate A Number
        /// </summary>
        /// <returns>Return The Generated Number</returns>
        public static int Generate()
        {
            return GenerateInteger();
        }
        /// <summary>
        /// Generate A Number Betwen Min And Max
        /// </summary>
        /// <returns>Return The Generated Number</returns>
        public static int Generate(int _min, int _max)
        {
            return GenerateInteger(_min,_max);
        }
    }
}
