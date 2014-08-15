using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
//Using Namespace
//Utilisation de l'espace de nom
using Chimera.Graphics.Effects.Particles.Effects;
namespace WindowsGame1
{
    
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        /// <summary>
        /// Create Same Effects
        /// Creer Quelques Effets
        /// </summary>
        Snow snow;
        Fire fire;
        Fire fireline;
        Plasma plasma;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
            //Snow Effect Will Be generated Every 20milisecond
            //L'effet neige sera generer chaque 20miliseconde
            snow = new Snow(this, 20);
            //Create Fire Effect In Disaable Mode
            //Creer Effet feu en mode desactivé
            fire = new Fire(this, 0);
            //Generate Plasma Effect Every right click mouse boutton
            //generer l'effet plasma a chaque clique sur le bouton droit de la sourie
            plasma = new Plasma(this, Chimera.Graphics.Effects.Particles.Engine.Controllers.MouseController.MouseButtons.Right);           
            //Fire Effect Will Be Generated After each die of plasma particle
            //L'effect feu sera generé apres chaque mort d'une particule plasma
            fire.ApplyEffect(plasma);
            
            //Select Default texture for fire effect
            //Selectione la texture par defaut pour l'effet feu
            fire.SetDefaultTexture(this);
            /*For The Default texture you have to inculde resource floder see the second tutorial*/
            /*Pour les texture par defaut vous avez a inclure le dossier resource lisez le seconde cours*/
            
            ///Create parameters effect with 300 particle
            ///Creer les parametre de l'effet avec 300 particule
            parameters param = new parameters(300);

            param.direction = 2;

            ///Create Fire Line Effect Every 100 Milisecond With The Param parameter
            ///Creer leffet feu sous forme de ligne chaque 100 milisecond avec comme parametre param
            fireline = new Fire(this, 100, Chimera.Graphics.Enumeration.EParticleType.Line, param);
            fireline.Position = new Vector2(400,500 );
            fireline.SetDefaultTexture(this);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            Chimera.Engine.Initialize(this, graphics);
            
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            Chimera.GUI.MainWindow.Properties.Title = "Click Right Mouse Button";
            //Switch Betwen FullScreen And Window Screen Mode
            //Aletrne entre le mode plien ecran et le mode fenetre
            if (Chimera.Input.KeyBoard.IsKeyPressed(Keys.F11))
                Chimera.Helpers.GameOptions.Properties.ToggleFullScreen();
            //Take a ScreenShoot
            //Prend une capture d'ecran
            if (Chimera.Input.KeyBoard.IsKeyPressed(Keys.Enter))
                Chimera.Helpers.ScreenShoot.TakeScreen(@"c:\ChimeraShoot.png", ImageFileFormat.Png);
            // TODO: Add your update logic here
            Chimera.Input.KeyBoard.Update();
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
