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
    /// This Class Allow You Creating Plasma Effect
    /// </summary>
    public class Plasma:BaseEffect
    {
        #region Constructor
        #region Default
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="system">Mercury System Reference</param>
        /// <param name="frequency">Frequency</param>
        public Plasma(ParticleSystem system, int frequency)
            : base(1500)
        {
            this._system = system;
            
            constructor(Chimera.Graphics.Enumeration.EParticleType.None, frequency);
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">XNA Game Reference</param>
        /// <param name="frequency">Frequency</param>
        public Plasma(Microsoft.Xna.Framework.Game game, int frequency)
            : base(1500)
        {
            this._system = new ParticleSystem(game);
            
            constructor(Chimera.Graphics.Enumeration.EParticleType.None, frequency);
        }
#if !XBOX
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="system">Mercury System Reference</param>
        /// <param name="button">Mouse Button</param>
        public Plasma(ParticleSystem system, MouseController.MouseButtons button)
            : base(1500)
        {
            this._system = system;
            
            constructor(Chimera.Graphics.Enumeration.EParticleType.None, button);
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">XNA Game Reference</param>
        /// <param name="button">Mouse Button</param>

        public Plasma(Microsoft.Xna.Framework.Game game, MouseController.MouseButtons button)
            : base(1500)
        {
            this._system = new ParticleSystem(game);
            
            constructor(Chimera.Graphics.Enumeration.EParticleType.None, button);
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
        public Plasma(ParticleSystem system, int frequency, Enumeration.EParticleType type, parameters param)
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

        public Plasma(Microsoft.Xna.Framework.Game game, int frequency, Enumeration.EParticleType type, parameters param)
        {
            this._system = new ParticleSystem(game);
            this.param = param;
            constructor(type, frequency);
        }
#if !XBOX
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="system">Mercury System Reference</param>
        /// <param name="button">Mouse Button</param>
        /// <param name="type">Particle Effect Type</param>
        /// <param name="param">Parameter</param>
        public Plasma(ParticleSystem system, MouseController.MouseButtons button, Enumeration.EParticleType type, parameters param)
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

        public Plasma(Microsoft.Xna.Framework.Game game, MouseController.MouseButtons button, Enumeration.EParticleType type, parameters param)
        {
            this._system = new ParticleSystem(game);
            this.param = param;
            constructor(type, button);
        }
#endif
        #endregion
        #endregion 
        #region init
           #region Default
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="system">Mercury System Reference</param>
        /// <param name="frequency">Frequency</param>
        public void Initialize(ParticleSystem system, int frequency)
        {
            this._system = system;
            base.setbudget(1500);
            constructor(Chimera.Graphics.Enumeration.EParticleType.None, frequency);
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">XNA Game Reference</param>
        /// <param name="frequency">Frequency</param>
        public void Initialize(Microsoft.Xna.Framework.Game game, int frequency)
        {
            this._system = new ParticleSystem(game);
            base.setbudget(1500);
            constructor(Chimera.Graphics.Enumeration.EParticleType.None, frequency);
        }
#if !XBOX
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="system">Mercury System Reference</param>
        /// <param name="button">Mouse Button</param>
        public void Initialize(ParticleSystem system, MouseController.MouseButtons button)
        {
            this._system = system;
            base.setbudget(1500);
            constructor(Chimera.Graphics.Enumeration.EParticleType.None, button);
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">XNA Game Reference</param>
        /// <param name="button">Mouse Button</param>

        public void Initialize(Microsoft.Xna.Framework.Game game, MouseController.MouseButtons button)
        {
            this._system = new ParticleSystem(game);
            base.setbudget(1500);
            constructor(Chimera.Graphics.Enumeration.EParticleType.None, button);
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
        public void Initialize(ParticleSystem system, int frequency, Enumeration.EParticleType type, parameters param)
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

        public void Initialize(Microsoft.Xna.Framework.Game game, int frequency, Enumeration.EParticleType type, parameters param)
        {
            this._system = new ParticleSystem(game);
            this.param = param;
            constructor(type, frequency);
        }
#if !XBOX
        /// <summary>
        /// /// Constructor
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
        /// /// Constructor
        /// </summary>
        /// <param name="game">XNA Game Reference</param>
        /// <param name="button">Mouse Button</param>
        /// <param name="type">Particle Effect Type</param>
        /// <param name="param">Parameter</param>

        public void Initialize(Microsoft.Xna.Framework.Game game, MouseController.MouseButtons button, Enumeration.EParticleType type, parameters param)
        {
            this._system = new ParticleSystem(game);
            this.param = param;
            constructor(type, button);
        }
#endif
        #endregion
        /// <summary>
        /// Initialize The Effect
        /// </summary>
        protected override void init()
        {
            this.scale = new RandomScaleModifier(.1f, 2f);
            this.opacity = new  OpacityModifier(1f, 0f);
            
            
            this.intialize();

            this._peff.ApplyModifier(new RandomColorModifier());
            this._peff.Position = new Vector2(400, 0);
            this._peff.ParticleLifespan = 8000;
            this._peff.ParticleSpeedRange = 50f;
            this._peff.ParticleSpeed = 50f;
            _peff.DischargeQuantity = 3;   
        }
        #endregion
        /// <summary>
        /// Set The Default Texture To The 
        /// </summary>
        /// <param name="game">XNA Game Reference</param>
        public void SetDefaultTexture(Microsoft.Xna.Framework.Game game)
        {
            base.SetDefaultTexture(game, 0);
        }
    }
}
