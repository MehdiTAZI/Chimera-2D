using Chimera.Graphics.Effects.Particles.Engine;
using Chimera.Graphics.Effects.Particles.Engine.Controllers;
using Chimera.Graphics.Effects.Particles.Engine.Emitters;
using Chimera.Graphics.Effects.Particles.Engine.Modifiers;
using Chimera.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Chimera.Graphics.Effects.Particles.Effects
{
    #region parameter public class
    /// <summary>
    /// Define Or Get PArtciles Effects Parameters
    /// </summary>
    public class parameters
    {
        /// <summary>
        /// Number Or Particles In The Animation
        /// </summary>
        public int budget;
        /// <summary>
        /// Length
        /// </summary>
        public float length;
        /// <summary>
        /// Radius Of Particle Animation
        /// </summary>
        public float radius;
        /// <summary>
        /// Rate Of particle Animation
        /// </summary>
        public int rate;
        /// <summary>
        /// Direction Particle Animation
        /// </summary>
        public float direction;
        /// <summary>
        /// Sprital Direction Of Particle Animation
        /// </summary>
        public SpiralEmitter.SpiralDirection spiraldirection;
        /// <summary>
        /// Spread
        /// </summary>
        public float spread;
        /// <summary>
        /// Constructor
        /// </summary>
        public parameters()
        {
            Initialize();
        }
        /// <summary>
        /// Constuctor
        /// </summary>
        /// <param name="budget">Number Of Particle In The Animation</param>
        public parameters(int budget)
        {
            Initialize();
            this.budget = budget;
        }
        /// <summary>
        /// Initialize The Paramters With Default Values
        /// </summary>
        public void Initialize()
        {
            budget = 1500;
            length = 800;
            direction = MathHelper.Pi;
            radius = 50;
            rate = 30;
            spiraldirection = SpiralEmitter.SpiralDirection.Clockwise;
            spread = -50;
        }
    }
    #endregion
    /// <summary>
    /// Base Particle Effect Class
    /// </summary>
    abstract public class BaseEffect
    {
        #region Fields

        protected Emitter _peff;
        protected ParticleSystem _system;
        protected Emitter ec;
        protected ColorModifier colors;
        protected ExplosionController ex;
        protected Enumeration.EParticleType type;
        protected SineForceModifier force;
        protected RandomScaleModifier scale;
        protected OpacityModifier opacity;
        protected GravityModifier gravity;
        protected bool enable;
        protected Controller ctr;
        protected parameters param;
        protected int frequency;
        #endregion 
        #region Properties
        /// <summary>
        /// Set Or Get Particles System Frequency
        /// </summary>
        public int Frequency
        {
            get
            {
                if (this.ctr is TimedTriggerController) return this.frequency;
                else return 0;
            }
            set 
            {
                if ( this.ctr is TimedTriggerController)
                {
                    this.frequency = value;                   
                    this._peff.RemoveController(ctr);
                    ctr = new TimedTriggerController(this.frequency);
                    this._peff.ApplyController(ctr);
                }   
            }
        }
        /// <summary>
        /// Enable Or Disable The Effect
        /// </summary>
        public bool Enable
        {
            get { return this.enable;}
            set {
                this.enable = value;
                if (this.enable == false)
                    _peff.RemoveController(ctr);
                else _peff.ApplyController(ctr);
                ;}
        }
        /// <summary>
        /// Get Or Set The Discharge Quantity
        /// </summary>
        public int Discharge
        {
            get { return _peff.DischargeQuantity; }
            set { this._peff.DischargeQuantity = value; }
        }

        /// <summary>
        /// Get The  Particle Type
        /// </summary>
        public Enumeration.EParticleType Type
        {
            get { return type;}
        }
        /// <summary>
        /// Return Or Define The Image to applie to this particle effect
        /// </summary>
        public ImageStrip Image
        {
            get { return _peff.ImageStrip ;}
            set { _peff.ImageStrip = value;}
        }

        /// <summary>
        /// Get Or Set The Particle Color TO Use
        /// </summary>
        public Color Color
        {
            get { return _peff.ParticleColor; }
            set {
                _peff.ParticleColor =value;
                _peff.RemoveModifier(colors);
                }
        }
        /// <summary>
        /// Get Or Set The Speed Range Of paticle
        /// </summary>
        public float SpeedRange
        {
            get { return _peff.ParticleSpeedRange; }
            set { _peff.ParticleSpeedRange = value;}
        }
        /// <summary>
        /// Get Or Set Particles Speed
        /// </summary>
        public float Speed
        {
            get { return _peff.ParticleSpeed; }
            set { _peff.ParticleSpeed = value;}
        }
        /// <summary>
        /// Get Or Set Particles Life
        /// </summary>
        public int Life
        {
            get { return _peff.ParticleLifespan; }
            set {_peff.ParticleLifespan = value; }
        }
        /// <summary>
        /// Get Or Set particles position
        /// </summary>
        public Vector2 Position
        {
            get { return _peff.Position; }
            set { _peff.Position = value;}
        }
        /// <summary>
        /// Get Or Set Particle Scale Value
        /// </summary>
        public RandomScaleModifier Scale
        {
            get { return this.scale; }
            set
            {
                this._peff.RemoveModifier(scale);
                this.scale = value;               
                this._peff.ApplyModifier(scale);
            }
        }
        /// <summary>
        /// Get Or Set Particle Gravity
        /// </summary>
        public GravityModifier Gravity
        {
            get { return this.gravity; }
            set
            {
                this._peff.RemoveModifier(gravity);
                this.gravity = value;
                this._peff.ApplyModifier(gravity);
            }
        }
        /// <summary>
        /// Get Or Set Paticiles Colors
        /// </summary>
        public ColorModifier Colors
        {
            get { return this.colors; }
            set
            {
                this._peff.RemoveModifier(colors);
                this.colors = value;
                this._peff.ApplyModifier(colors);
            }
        }
        /// <summary>
        /// Get Or Set Paticles Force
        /// </summary>
        public SineForceModifier Force
        {
            get { return this.force; }
            set
            {
                _peff.RemoveModifier(force);
                this.force = value;
                _peff.ApplyModifier(force);
            }
        }
        /// <summary>
        /// Get Or Set Paticles Opacity
        /// </summary>
        public OpacityModifier Opacity
        {
            get { return this.opacity; }
            set
            {
                _peff.RemoveModifier(opacity);
                 this.opacity = value;              
                _peff.ApplyModifier(opacity);
            }
        }
        
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="x">particle Type</param>
        /// <param name="frq">frequency</param>
        protected void constructor(Enumeration.EParticleType x, int frq)
        {
            this.type = x;
            this.init();
            ctr = new TimedTriggerController(frq);
            this.frequency = frq;
            this._peff.ApplyController(ctr);
        }
        #if !XBOX
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="x">particle Type</param>
        /// <param name="btn">Mouse Button</param>
        protected void constructor(Enumeration.EParticleType x, MouseController.MouseButtons btn)
        {
            this.type = x;
            this.init();
            this.frequency = -999;
            ctr = new MouseController(btn);
            this._peff.ApplyController(ctr);
        }
        #endif
        protected BaseEffect()
        {
            param = new parameters();
        }
        protected BaseEffect(int budget)
        {
            setbudget(budget);
        }
        #endregion
        #region init
        protected void setbudget(int budget)
        {
            param = new parameters(budget);
        }
        protected void intialize()
        {
            if (type == Enumeration.EParticleType.Line) this._peff = new LineEmitter(_system, param.budget, param.length, 0f);
            else if (type == Enumeration.EParticleType.None) this._peff = new Emitter(_system, param.budget);
            else if (type == Enumeration.EParticleType.Circle) this._peff = new CircleEmitter(_system, param.budget,param.radius, false);
            else if (type == Enumeration.EParticleType.Spiral) this._peff = new SpiralEmitter(_system, param.budget, param.radius, param.rate, param.spiraldirection);
            else if (type == Enumeration.EParticleType.Spray) this._peff = new SprayEmitter(_system, param.budget,param.direction, param.spread);
            

            if (this.scale != null) this._peff.ApplyModifier(scale);
            if (this.force != null) this._peff.ApplyModifier(force);
            if (this.opacity != null) this._peff.ApplyModifier(opacity);
            if (this.gravity != null) this._peff.ApplyModifier(gravity);
            if (this.colors != null) this._peff.ApplyModifier(colors);
            enable = true;      
        }
        
        protected abstract void init();
      
        #endregion
        #region Functions
        #if !XBOX
        /// <summary>
        /// Generate Effect When We Click Mouse Button
        /// </summary>
        /// <param name="button">Which Button TO Use</param>
        public void GenerateBy(MouseController.MouseButtons button)
        {
            this._peff.RemoveController(this.ctr);
            this.ctr = new MouseController(button);
            this._peff.ApplyController(ctr);
        }
        #endif
        /// <summary>
        /// Generate Effect Every Interval Of Time
        /// </summary>
        /// <param name="frequency">The Frequency</param>
        public void GenerateBy(int frequency)
        {
            this.frequency = frequency;
            this._peff.RemoveController(this.ctr);
            this.ctr = new TimedTriggerController(frequency);
            this._peff.ApplyController(ctr);
        }


        /// <summary>
        /// Applie This Effect To Effect In Parameter
        /// </summary>
        /// <param name="effect">Effect To Applie</param>
        public void ApplyEffect(BaseEffect effect)
        {
            ApplyEffect(effect._peff);
        }
        /// <summary>
        /// Applie This Effect To Effect In Parameter
        /// </summary>
        /// <param name="effect">Emitter Mecrucy Reference</param>
        public void ApplyEffect(Emitter effect)
        {
           this.ec = effect;
           this.ex = new ExplosionController(this.ec);
           this._peff.ApplyController(this.ex);
        }
        /// <summary>
        /// Remove The Effect Controler
        /// </summary>
        public void RemoveEffect()
        {
            this._peff.RemoveController(this.ex); 
        }
        /// <summary>
        /// Set Default Texture
        /// </summary>
        /// <param name="game">XNA Game Reference</param>
        /// <param name="index">Default Texture to use (1-4)</param>
        public void SetDefaultTexture(Microsoft.Xna.Framework.Game game, int index)
        {
            
            if (index == 1)
            {
                this.Image = new ImageStrip(game, @"resources\ExtraParticles", 128, 128, 4);
                this._peff.Frame = 1;
            }
            else if (index == 2)
            {
                this.Image = new ImageStrip(game, @"resources\ExtraParticles", 128, 128, 4);
                this._peff.Frame = 2;
            }
            else if ( index == 3)
            { 
                this.Image = new ImageStrip(game, @"resources\ExtraParticles", 128, 128, 4);
                this._peff.Frame = 3;
            }

            else
            {
                this.Image = new ImageStrip(game, @"resources\DefaultParticle", 32, 32, 2);
                this._peff.Frame = 1;
            }

        }
        #endregion

    }
}
