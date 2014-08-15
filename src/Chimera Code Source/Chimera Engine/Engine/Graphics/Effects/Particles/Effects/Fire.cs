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
    /// This Class Allow You Creating Fire Effect
    /// </summary>
    public class Fire : BaseEffect
    {
        #region Contructor
        #region Default
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="system">Mercury System Reference</param>
        /// <param name="frequency">Frequency</param>
        public Fire(ParticleSystem system, int frequency):base(150)
        {
            this._system = system;
            constructor(Chimera.Graphics.Enumeration.EParticleType.Circle, frequency);
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">XNA Game Reference</param>
        /// <param name="frequency">Frequency</param>
        public Fire(Microsoft.Xna.Framework.Game game, int frequency):base(150)
        {
            this._system = new ParticleSystem(game);
            constructor(Chimera.Graphics.Enumeration.EParticleType.Circle, frequency);
        }
#if !XBOX
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="system">Mercury System Reference</param>
        /// <param name="button">Mouse Button</param>
        public Fire(ParticleSystem system, MouseController.MouseButtons button): base(150)
        {
            this._system = system;
            constructor(Chimera.Graphics.Enumeration.EParticleType.Circle, button);
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">XNA Game Reference</param>
        /// <param name="button">Mouse Button</param>
        public Fire(Microsoft.Xna.Framework.Game game, MouseController.MouseButtons button)
            : base(150)
        {
            this._system = new ParticleSystem(game);
            constructor(Chimera.Graphics.Enumeration.EParticleType.Circle, button);
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
        public Fire(ParticleSystem system, int frequency, Enumeration.EParticleType type, parameters param)
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
        public Fire(Microsoft.Xna.Framework.Game game, int frequency, Enumeration.EParticleType type, parameters param)
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
        public Fire(ParticleSystem system, MouseController.MouseButtons button, Enumeration.EParticleType type, parameters param)
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
        public Fire(Microsoft.Xna.Framework.Game game, MouseController.MouseButtons button, Enumeration.EParticleType type, parameters param)
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
            base.setbudget(150);
            constructor(Chimera.Graphics.Enumeration.EParticleType.Circle, frequency);
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">XNA Game Reference</param>
        /// <param name="frequency">Frequency</param>
        public void Initialize(Microsoft.Xna.Framework.Game game, int frequency)
        {
            this._system = new ParticleSystem(game);
            base.setbudget(150);
            constructor(Chimera.Graphics.Enumeration.EParticleType.Circle, frequency);
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
            base.setbudget(150);
            constructor(Chimera.Graphics.Enumeration.EParticleType.Circle, button);
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">XNA Game Reference</param>
        /// <param name="button">Mouse Button</param>
        public void Initialize(Microsoft.Xna.Framework.Game game, MouseController.MouseButtons button)
        {
            this._system = new ParticleSystem(game);
            base.setbudget(150);
            constructor(Chimera.Graphics.Enumeration.EParticleType.Circle, button);
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
        /// Constructor
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
        /// Constructor
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
            this.opacity = new OpacityModifier(0f, .5f, 0.25f, 0);
            this.scale = new RandomScaleModifier(1f, 3f);
            
            this.intialize();

            this._peff.Position = new Vector2(320, 240);
            this._peff.ParticleLifespan = 6000;
            this._peff.ParticleSpeedRange = 40f;
            this._peff.ParticleSpeed = 40f;
            this._peff.ApplyModifier(new RotationModifier(.05f));
            this._peff.ApplyModifier(new ColorModifier(Color.Firebrick, Color.Goldenrod, 0.75f, Color.WhiteSmoke));
            this._peff.DischargeQuantity = 3;
            
        }
        #endregion   
        /// <summary>
        /// Set The Default Texture To The 
        /// </summary>
        /// <param name="game">XNA Game Reference</param>
        public void SetDefaultTexture(Microsoft.Xna.Framework.Game game)
        {
            base.SetDefaultTexture(game, 3);
        }
    }
}
