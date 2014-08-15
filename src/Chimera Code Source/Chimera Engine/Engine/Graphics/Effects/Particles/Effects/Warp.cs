using Chimera.Graphics.Effects.Particles.Engine;
using Chimera.Graphics.Effects.Particles.Engine.Controllers;
using Chimera.Graphics.Effects.Particles.Engine.Emitters;
using Chimera.Graphics.Effects.Particles.Engine.Modifiers;
using Chimera.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Chimera.Graphics.Effects.Particles.Effects
{
    /// <summary>
    /// This Class Allow You generate The Warp Effect
    /// </summary>
    public class Warp : BaseEffect
    {

        #region Constructor
        #region Default
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="system">Mercury System Reference</param>
        /// <param name="frequency">Frequency</param>
        public Warp(ParticleSystem system, int frequency)
            : base(100)
        {
            this._system = system;
            param.spread = MathHelper.PiOver4;
            
            constructor(Chimera.Graphics.Enumeration.EParticleType.Spiral, frequency);
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">XNA Game Reference</param>
        /// <param name="frequency">Frequency</param>
        public Warp(Microsoft.Xna.Framework.Game game, int frequency)
            : base(100)
        {
            this._system = new ParticleSystem(game);
            param.spread = MathHelper.PiOver4;           
            constructor(Chimera.Graphics.Enumeration.EParticleType.Spiral, frequency);
        }
        #if !XBOX
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="system">Mercury System Reference</param>
        /// <param name="button">Mouse Button</param>
        public Warp(ParticleSystem system, MouseController.MouseButtons button)
            : base(100)
        {
            this._system = system;
            param.spread = MathHelper.PiOver4;
            constructor(Chimera.Graphics.Enumeration.EParticleType.Spiral, button);
        }
        

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">XNA Game Reference</param>
        /// <param name="button">Mouse Button</param>

        public Warp(Microsoft.Xna.Framework.Game game, MouseController.MouseButtons button):base(100)
        {
            this._system = new ParticleSystem(game);
            param.spread = MathHelper.PiOver4;
            
            constructor(Chimera.Graphics.Enumeration.EParticleType.Spiral, button);
        }
        #endif
        #endregion
        #region By Paramater

        /// <summary>
        /// /// Constructor
        /// </summary>
        /// <param name="system">Mercury System Reference</param>
        /// <param name="frequency">Frequency</param>
        /// <param name="type">Particle Effect Type</param>
        /// <param name="param">Parameter</param>
     
        public Warp(ParticleSystem system, int frequency, Enumeration.EParticleType type, parameters param)
        {
            this._system = system;
            this.param = param;
            constructor(type, frequency);
        }
        /// <summary>
        /// /// Constructor
        /// </summary>
        /// <param name="game">XNA Game Reference</param>
        /// <param name="frequency">Frequency</param>
        /// <param name="type">Particle Effect Type</param>
        /// <param name="param">Parameter</param>
      
        public Warp(Microsoft.Xna.Framework.Game game, int frequency, Enumeration.EParticleType type, parameters param)
        {
            this._system = new ParticleSystem(game);
            this.param = param;
            constructor(type, frequency);
        }
#if!XBOX
        /// <summary>
        /// /// Constructor
        /// </summary>
        /// <param name="system">Mercury System Reference</param>
        /// <param name="button">Mouse Button</param>
        /// <param name="type">Particle Effect Type</param>
        /// <param name="param">Parameter</param>
        public Warp(ParticleSystem system, MouseController.MouseButtons button, Enumeration.EParticleType type, parameters param)
        {
            this._system = system;
            this.param = param;
            constructor(type, button);

        }
        /// <summary>
        /// /// Constructor
        /// </summary>
        /// <param name="game">XNA Game Reference</param>
        /// <param name="button">Mouse Button</param>
        /// <param name="type">Particle Effect Type</param>
        /// <param name="param">Parameter</param>
     
        public Warp(Microsoft.Xna.Framework.Game game, MouseController.MouseButtons button, Enumeration.EParticleType type, parameters param)
        {
            this._system = new ParticleSystem(game);
            this.param = param;
            constructor(type, button);
        }
#endif
        #endregion
        #endregion
        #region init
        /// <summary>
        /// Initialize The Effect
        /// </summary>
        protected override void init()
        {
            this.scale = new RandomScaleModifier(0.1f, 0.6f);
            this.opacity = new OpacityModifier(1f, 0.5f, 0.2f, 0.1f);
            this.intialize();
            this._peff.Position = new Vector2(400f, 300f);
            this._peff.ParticleLifespan = 2700;
            this._peff.ParticleColor = Color.Chartreuse;
            this._peff.ParticleSpeedRange = 0f;
            this._peff.ParticleSpeed = 50f;
            this._peff.DischargeQuantity = 3;
           
           
           
        }
        #region Default
        /// <summary>
        /// Initialize The Effect
        /// </summary>
        /// <param name="system">Mercury System Reference</param>
        /// <param name="frequency">Frequency</param>
        public void Initialize(ParticleSystem system, int frequency)    
        {
            this._system = system;
            param.spread = MathHelper.PiOver4;
            base.setbudget(10);
            
            constructor(Chimera.Graphics.Enumeration.EParticleType.Spiral, frequency);

            
        }
        /// <summary>
        /// Initialize The Effect
        /// </summary>
        /// <param name="game">XNA Game Reference</param>
        /// <param name="frequency">Frequency</param>
        public void Initialize(Microsoft.Xna.Framework.Game game, int frequency)
        {
             
            this._system = new ParticleSystem(game);
            param.spread = MathHelper.PiOver4;
            base.setbudget(100);
            constructor(Chimera.Graphics.Enumeration.EParticleType.Spiral, frequency);
        }
#if !XBOX
        /// <summary>
        /// Initialize The Effect
        /// </summary>
        /// <param name="system">Mercury System Reference</param>
        /// <param name="button">Mouse Button</param>
        public void Initialize(ParticleSystem system, MouseController.MouseButtons button)           
        {
            this._system = system;
            param.spread = MathHelper.PiOver4;
            base.setbudget(100);
            constructor(Chimera.Graphics.Enumeration.EParticleType.Spiral, button);
        }
        /// <summary>
        /// Initialize The Effect
        /// </summary>
        /// <param name="game">XNA Game Reference</param>
        /// <param name="button">Mouse Button</param>
        public void Initialize(Microsoft.Xna.Framework.Game game, MouseController.MouseButtons button)
            
        {
            this._system = new ParticleSystem(game);
            param.spread = MathHelper.PiOver4;
            base.setbudget(100);
            constructor(Chimera.Graphics.Enumeration.EParticleType.Spiral, button);
        }
#endif
        #endregion
        #region By Paramater

        /// <summary>
        /// /// Initialize The Effect
        /// </summary>
        /// <param name="system">Mercury System Reference</param>
        /// <param name="frequency">Frequency</param>
        /// <param name="type">Particle Effect Type</param>
        /// <param name="param">Parameter</param>
     
        public void Initialize(ParticleSystem system, int frequency, Enumeration.EParticleType type, parameters param)
        {
            this._system = system;
            this.param = param;
            constructor(type, frequency);
        }
        /// <summary>
        /// /// Initialize The Effect
        /// </summary>
        /// <param name="game">XNA Game Reference</param>
        /// <param name="frequency">Frequency</param>
        /// <param name="type">Particle Effect Type</param>
        /// <param name="param">Parameter</param>

        public void Initialize(Microsoft.Xna.Framework.Game game, int frequency, Enumeration.EParticleType type, parameters param)
        {
            this._system = new ParticleSystem(game);
            this.param = param;
            constructor(type, frequency);
        }
#if !XBOX
        /// <summary>
        /// /// Initialize The Effect
        /// </summary>
        /// <param name="system">Mercury System Reference</param>
        /// <param name="button">Mouse Button</param>
        /// <param name="type">Particle Effect Type</param>
        /// <param name="param">Parameter</param>
        public void Initialize(ParticleSystem system, MouseController.MouseButtons button, Enumeration.EParticleType type, parameters param)
        {
            this._system = system;
            this.param = param;
            constructor(type, button);

        }
        /// <summary>
        /// /// Initialize The Effect
        /// </summary>
        /// <param name="game">XNA Game Reference</param>
        /// <param name="button">Mouse Button</param>
        /// <param name="type">Particle Effect Type</param>
        /// <param name="param">Parameters</param>

        public void Initialize(Microsoft.Xna.Framework.Game game, MouseController.MouseButtons button, Enumeration.EParticleType type, parameters param)
        {
            this._system = new ParticleSystem(game);
            this.param = param;
            constructor(type, button);
        }
#endif
        #endregion
        #endregion
        /// <summary>
        /// Set The Default Texture To The 
        /// </summary>
        /// <param name="game">XNA Game Reference</param>
        public void SetDefaultTexture(Microsoft.Xna.Framework.Game game)
        {
            base.SetDefaultTexture(game, 1);
        }
    }
}
