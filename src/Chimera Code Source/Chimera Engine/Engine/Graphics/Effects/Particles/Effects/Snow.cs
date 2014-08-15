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
    /// This Class Allow You Creating Snow Effect
    /// </summary>
    public class Snow : BaseEffect
    {
        #region Constructor
        #region Default
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="system">Mercury System Reference</param>
        /// <param name="frequency">Frequency</param>
        public Snow(ParticleSystem system, int frequency)
        {
            this._system = system;
            constructor(Chimera.Graphics.Enumeration.EParticleType.Line, frequency);
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">XNA Game Reference</param>
        /// <param name="frequency">Frequency</param>
        public Snow(Microsoft.Xna.Framework.Game game, int frequency)
        {
            this._system = new ParticleSystem(game);
            constructor(Chimera.Graphics.Enumeration.EParticleType.Line, frequency);
        }
#if !XBOX
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="system">Mercury System Reference</param>
        /// <param name="button">Mouse Button</param>
        public Snow(ParticleSystem system, MouseController.MouseButtons button)
        {
            this._system = system;
            constructor(Chimera.Graphics.Enumeration.EParticleType.Line, button);
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">XNA Game Reference</param>
        /// <param name="button">Mouse Button</param>

        public Snow(Microsoft.Xna.Framework.Game game, MouseController.MouseButtons button)
        {
            this._system = new ParticleSystem(game);
            constructor(Chimera.Graphics.Enumeration.EParticleType.Line, button);
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

        public Snow(ParticleSystem system, int frequency, Enumeration.EParticleType type, parameters param)
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

        public Snow(Microsoft.Xna.Framework.Game game, int frequency, Enumeration.EParticleType type, parameters param)
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
        public Snow(ParticleSystem system, MouseController.MouseButtons button, Enumeration.EParticleType type, parameters param)
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

        public Snow(Microsoft.Xna.Framework.Game game, MouseController.MouseButtons button, Enumeration.EParticleType type, parameters param)
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
            this.scale =  new RandomScaleModifier(.05f, .2f);
            this.force = new SineForceModifier (10f, 20f);
            this.opacity = new OpacityModifier(1f, 0f);

            this.intialize();
            
            this._peff.Position = new Vector2(400, 0);
            this._peff.ParticleLifespan = 15000;
            this._peff.ParticleColor = Color.Azure;
            this._peff.ParticleSpeedRange = 50f;
            this._peff.ParticleSpeed = 50f;   
        }
        #region Initialiaze
        #region Default
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="system">Mercury System Reference</param>
        /// <param name="frequency">Frequency</param>
        public void Initialize(ParticleSystem system, int frequency)
        {
            this._system = system;
            constructor(Chimera.Graphics.Enumeration.EParticleType.Line, frequency);
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">XNA Game Reference</param>
        /// <param name="frequency">Frequency</param>
        public void Initialize(Microsoft.Xna.Framework.Game game, int frequency)
        {
            this._system = new ParticleSystem(game);
            constructor(Chimera.Graphics.Enumeration.EParticleType.Line, frequency);
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
            constructor(Chimera.Graphics.Enumeration.EParticleType.Line, button);
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">XNA Game Reference</param>
        /// <param name="button">Mouse Button</param>

        public void Initialize(Microsoft.Xna.Framework.Game game, MouseController.MouseButtons button)
        {
            this._system = new ParticleSystem(game);
            constructor(Chimera.Graphics.Enumeration.EParticleType.Line, button);
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
        #endregion
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
