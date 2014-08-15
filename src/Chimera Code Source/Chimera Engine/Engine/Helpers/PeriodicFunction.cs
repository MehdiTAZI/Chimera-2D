#region Info/Author
//----------------------------------------------------------------------------
// Author:    Tazi Mehdi
// Source:    Chimera 2D GAMES ENGINE
// Info:      __________Execute Function For A Period Of Time______________
//-----------------------------------------------------------------------------
#endregion
#region Using Statement
using System.Collections.Generic;
using Chimera;
using Microsoft.Xna.Framework;
#endregion
namespace Chimera.Helpers
{
    /// <summary>
    /// This Class Allow A Function To Be called during a period of time
    /// </summary>
    public class PeriodicFunction
    {
        private System.TimeSpan period = System.TimeSpan.Zero ;
        private System.TimeSpan lasttime = System.TimeSpan.Zero;
        private bool enable = false;
        private Delegate.Simple_Function function;
        /// <summary>
        /// Get Or Set The Periodic Function
        /// </summary>
        public Delegate.Simple_Function Function
        {
            get {return this.function ;}
            set { this.function = value ;}
        }
        /// <summary>
        /// Get Or Set The Period Of Time
        /// </summary>
        public System.TimeSpan Period 
        {
            get{return this.period;}
            set{
                if (value >System.TimeSpan.Zero)
                    this.period=value;
                else
                    this.period = System.TimeSpan.Zero;
            }
        }
        /// <summary>
        /// Enable Or Disable The Function Caller
        /// </summary>
        public bool Enable 
        {
            get{return enable;}
            set{enable=value;}
        }

        /// <summary>
        /// The Default Constructor
        /// </summary>
        public PeriodicFunction()
        {
            period = System.TimeSpan.Zero;
            lasttime = System.TimeSpan.Zero;
            function = null;
            enable = false;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="func">The Function To Be Called</param>
        public PeriodicFunction(Delegate.Simple_Function func)
        {
            period = System.TimeSpan.Zero;
            lasttime = System.TimeSpan.Zero;
            function = func;
            enable = false;
        }

        /// <summary>
        /// This Function Initialize The public class
        /// </summary>
        /// <param name="_period">Set The Period Of Time</param>
        public void Initialize(System.TimeSpan _period)
        {
            period = _period;    
        }
        /// <summary>
        /// This Function Initialize The public class
        /// </summary>
        /// <param name="_period">Set The Period Of Time</param>
        /// <param name="func">Set The function which will be called during a period of time</param>
        public void Initialize(System.TimeSpan _period, Delegate.Simple_Function func)
        {
            period = _period;
            function = func;
        }
        /// <summary>
        /// Reset The System 
        /// </summary>
        public void Reset()
        {
            lasttime = System.TimeSpan.Zero;
            enable = true;
           
        }

        /// <summary>
        /// Update The System
        /// </summary>
        
        public void Update(GameTime gameTime)
        {
            if (enable == true)
            {
                if (lasttime==System.TimeSpan.Zero)
                    lasttime = gameTime.TotalRealTime;

                if (gameTime.TotalRealTime <= lasttime + period)
                {
                    function();
                }
                else
                {
                    enable = false;
                    lasttime = System.TimeSpan.Zero;
                }


            }
        }
    }
}
