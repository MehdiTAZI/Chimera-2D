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
    /// This Class Allow you to generate an explosion particles effect
    /// </summary>
    public class Explosion : BaseEffect
    {

        #region Constructor
        #region Default
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="system">Mercury System Reference</param>
        /// <param name="frequency">Frequency</param>
        public Explosion(ParticleSystem system, int frequency)
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
        public Explosion(Microsoft.Xna.Framework.Game game, int frequency)
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
        public Explosion(ParticleSystem system, MouseController.MouseButtons button)
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
        public Explosion(Microsoft.Xna.Framework.Game game, MouseController.MouseButtons button)
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
        public Explosion(ParticleSystem system, int frequency, Enumeration.EParticleType type, parameters param)
        {
            this._system = system;
            this.param = param;
            constructor(type, frequency);
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">XNA Game Reference</param>
        /// <param name="frequency">Frequency</param>
        /// <param name="type">Particle Effect Type</param>
        /// <param name="param">Parameter</param>
        public Explosion(Microsoft.Xna.Framework.Game game, int frequency, Enumeration.EParticleType type, parameters param)
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
        public Explosion(ParticleSystem system, MouseController.MouseButtons button, Enumeration.EParticleType type, parameters param)
        {
            this._system = system;
            this.param = param;
            constructor(type, button);

        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">XNA Game Reference</param>
        /// <param name="button">Mouse Button</param>
        /// <param name="type">Particle Effect Type</param>
        /// <param name="param">Parameter</param>
        public Explosion(Microsoft.Xna.Framework.Game game, MouseController.MouseButtons button, Enumeration.EParticleType type, parameters param)
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
            this.opacity = new OpacityModifier(1f,0f);
            this.colors = new ColorModifier(Color.PowderBlue, Color.Red);
            this.intialize();
            this._peff.Position = new Vector2(400f, 300f);
            this._peff.ParticleLifespan = 1700;
            this._peff.ParticleSpeedRange = 300f;
            this._peff.ParticleScale = 0.5f;
            this._peff.ParticleSpeed = 155f;
            this._peff.DischargeQuantity = 250;
            this._peff.ApplyModifier(new AtmosphereModifier(0.2f));
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
